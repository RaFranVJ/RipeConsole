using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using RipeConsole.Program;
using RipeConsole.Program.Graphics.Menus;
using RipeConsole.Program.Loaders.JavaScript;
using ZCore.Modules;
using ZCore.Serializables.Config.Fields;
using ZCore.Serializables.JavaScript;
using ZCore.Serializables.SortUtils;

/// <summary> Handles the Different Modes of Execution for JavaScript Code. </summary>

public static class JSUtils
{
// Display the Types expossed in the Script

private static void DisplayExpossedTypes(Dictionary<string, Type> types)
{
Text.PrintLine();

Text.PrintSubHeader(string.Format("Expossed Types x{0}", types.Count) );

Text.PrintDictionary(types, false);
}

// Display Named Items from Script

private static void DisplayNamedItems(Dictionary<string, object> items)
{
Text.PrintLine();

Text.PrintSubHeader(string.Format("Named Items x{0}", items.Count) );

Text.PrintDictionary(items, false);
}

// Display the Arguments passed to Script

private static void DisplayScriptArgs(object[] args)
{
Text.PrintLine();

Text.PrintSubHeader(string.Format("Script Arguments x{0}", args.Length) );

Text.PrintArray(true, true, args);
}

// Display Additional Script Details

private static void ShowScriptDetails(Function func, JSCriptEntry entry, object[] args)
{
Text.PrintHeader("Script Execution Info");

Text.PrintLine(false, "JS Entry Name: {0}", Path.GetFileName(func.PathToScriptEntry) );

string scriptName = Path.GetFileName(entry.ScriptMetadata.PathToJScriptFile);
Text.PrintLine(false, "Script Name: {0}", scriptName);

string scriptAlias = entry.ScriptMetadata.GetScriptName();

if(scriptAlias != scriptName)
Text.PrintLine(false, "Script Alias: {0}", scriptAlias);

if(entry.TypesToExpose != null && entry.TypesToExpose.Count > 0)
DisplayExpossedTypes(entry.TypesToExpose);

if(entry.NamedItems != null && entry.NamedItems.Count > 0)
DisplayNamedItems(entry.NamedItems);

if(!string.IsNullOrEmpty(func.ScriptMethodName) )
Text.PrintLine(false, "Method Call: {0}", func.ScriptMethodName);

Text.PrintLine(false, "Arguments Type: {0}", func.ArgsType);

if(args != null && args.Length > 0)
DisplayScriptArgs(args);

}

// Display the Script Result after Execution

public static void DisplayScriptResult(Function func, JSCriptEntry entry, object[] args, List<ScriptResult> results)
{
var errors = results.SelectMany(r => r.GetErrorsCaught() );

var totalElapsedTime = results.Select(r => r.GetElapsedTime() )
.Aggregate(TimeSpan.Zero, (total, elapsed) => total.Add(elapsed));

bool showDetails = Program.GetAppConfig.JSEngineConfig.ShowExecutionDetails;

if(showDetails)
ShowScriptDetails(func, entry, args);

if(results.Count == 1 && results[0].ValueReturned?.GetType() != typeof(Undefined) )
Text.PrintLine(showDetails && results[0].ValueReturned != null, "Script Result: {0}", 
Text.GetObjectValue(results[0].ValueReturned, showDetails, true) );

Text.PrintLine(errors.Count() > 1, "Execution Time: {0}", totalElapsedTime);

string errorLog = errors.Any() ? string.Join(" ", errors) : "<None>";
Text.PrintLine(errors.Count() > 1, "Exceptions Raised: {0}", errorLog);

string status = errors.Any() ? $"Faulted with {errors.Count()} error (s)" : "Success";
Text.PrintLine(false, "Execution Status: {0}", status);
}

// Get Input/Output Path and Args for Script Execution

public static object[] GetScriptParams(V8ScriptEngine jsEngine, Function sourceFunc, string quickPath = default)
{

List<object> args = sourceFunc.ArgsType switch
{
ScriptArgsType.Generic => ArgumentsLoader.GetGenericParams(sourceFunc, jsEngine, quickPath),
ScriptArgsType.Specific => ArgumentsLoader.GetSpecificParams(sourceFunc, jsEngine, quickPath),
_ => new()
};

return [.. args];
}

// Get Full Msg for Execution

private static string GetProcessMsg(Function func, string quickPath)
{
string msg = Text.ProgramStrings.LocateByID(func.ProcessMsgs.ExecutionOnProcess);

if(quickPath != null)
{
string quickName = Path.GetFileName(quickPath);

return string.Format(msg.Replace(@"{1}", string.Empty), quickName);
}

// Use Generic Paths

else if(func.ArgsType == ScriptArgsType.Generic && !string.IsNullOrEmpty(func.GenericPathForInput) )
{
string inputName = Path.GetFileName(func.GenericPathForInput);

if(!string.IsNullOrEmpty(func.GenericPathForOutput) )
{
string outputName = Path.GetFileName(func.GenericPathForOutput);

return string.Format(msg, inputName, outputName);
}

return string.Format(msg.Replace(@"{1}", string.Empty), inputName);
}

// Use Specifig Paths from UserParams

else if(func.ArgsType == ScriptArgsType.Specific && func.SpecificPathForInput != null)
{
var userParams = Program.GetUserParams;

string inputPath = userParams.GetFieldValue(func.SpecificPathForInput) as string;
string inputName = Path.GetFileName(inputPath);

if(func.SpecificPathForOutput != null)
{
string outputPath = userParams.GetFieldValue(func.SpecificPathForOutput) as string;
string outputName = Path.GetFileName(outputPath);

return string.Format(msg, inputName, outputName);
}

return string.Format(msg.Replace(@"{1}", string.Empty), inputName);
}

return msg;
}

// Process a Single File with the Script

public static ScriptResult SingleAction(Function func, V8ScriptEngine engine, V8Script script,
object[] args, UserAlertInfo alertInfo, string quickPath = null)
{
ScriptResult scriptResult = new();

try
{

if(alertInfo.DisplayStatusMsgs && func.ProcessMsgs != null)
Text.PrintLine(true, GetProcessMsg(func, quickPath));

scriptResult = JavaScriptExecutor.RunScript(engine, script, func.ScriptMethodName, args);
}

catch(Exception error)
{
error = error.GetType() == typeof(ScriptEngineException) ? error.GetBaseException() : error;

scriptResult.RegistException($"<{error.GetType().Name}>"); // Error only

if(alertInfo.DisplayStatusMsgs && func.ProcessMsgs != null)
Text.PrintErrorMsg(Text.ProgramStrings.LocateByID(func.ProcessMsgs.ExecutionFaulted));

if(alertInfo.NotifyErrors)
Console.Beep();

ErrorsHandler.DisplayErrorInfo(error);
}

finally
{

if(scriptResult.GetErrorsCaught().Count == 0 && alertInfo.DisplayStatusMsgs && func.ProcessMsgs != null)
{
Text.PrintSuccessMsg(Text.ProgramStrings.LocateByID(func.ProcessMsgs.ExecutionSuccessful) );

string outputPath = ArgumentsLoader.GetPathFromArgs(func, args, false);

if(!string.IsNullOrEmpty(outputPath) )
{
string saveDialog = Text.ProgramStrings.LocateByID("DIALOG_FILESYSTEM_SAVED_AT_PATH");

Text.PrintDialog(true, string.Format(saveDialog, outputPath) );

Text.PrintLine();
}

}

}

return scriptResult;
}

// Add Args before Calling the Method

public static object[] BuildScriptParams(string inputPath, string outputDir, Function func, V8ScriptEngine engine)
{
List<object> args = new();

if(!func.IgnoreInputPathOnMethodCalls)
args.Add(inputPath);

if(!func.IgnoreOutputPathOnMethodCalls)
{
string baseDir = Path.GetDirectoryName(inputPath);
string outputPath = inputPath.Replace(baseDir, outputDir);

args.Add(outputPath);
}
	
if(func.ArgsType == ScriptArgsType.Specific)
ArgumentsLoader.AddSpecificArgs(args, func, engine, Program.GetUserParams);

else
ArgumentsLoader.AddGenericArgs(args, func, engine);

return args.ToArray();
}


// Process a Directory

private static void ProcessFiles(string dir, Function func, V8ScriptEngine engine, V8Script script, 
UserAlertInfo alertInfo, List<ScriptResult> results, string outputDir)
{
string inputDirName = DirManager.GetFolderName(dir);
string outputDirName = DirManager.GetFolderName(outputDir);

if(inputDirName == outputDirName)
{
string inputParent = DirManager.GetFolderName(Path.GetDirectoryName(dir) );
inputDirName = inputParent + Path.DirectorySeparatorChar + inputDirName;

string outputParent = DirManager.GetFolderName(Path.GetDirectoryName(outputDir) );
outputDirName = outputParent + Path.DirectorySeparatorChar + outputDirName;
}

if(alertInfo.DisplayStatusMsgs)
{
Text.PrintAdvice(true, $"Processing Files from \"{inputDirName}\" to \"{outputDirName}\"...");

Text.PrintLine();
}

var files = DirManager.GetFileSystems(dir, true, Program.GetUserParams.FileManagerParams.FolderOptions.SearchOptionsForFiles);

if(files.Length == 0 && alertInfo.NotifyWarnings)
Text.PrintWarning($"\"{inputDirName}\" has no Files to Process");

else
{

if(Program.GetUserParams.FileManagerParams.FolderOptions.FilterFilesInsideDirs)
PathHelper.FilterFiles(ref files, func.ArchivesFilter.FileNamesToSearch, func.ArchivesFilter.FileExtensionsToSearch,
func.ArchivesFilter.FileNamesToExclude, func.ArchivesFilter.FileExtensionsToExclude);

if(!Program.GetUserParams.FileManagerParams.FolderOptions.ProcessAllFilesAtOnce)
{
JSFilePrompt prompt = new( new(files), outputDir, func, engine, script, alertInfo);

prompt.DynamicSelection();

results.AddRange(prompt.GetResults() );
}

else
{

for(int i = 0; i < files.Length; i++)
{

if(alertInfo.DisplayStatusMsgs)
Text.PrintLine(true, Text.ProgramStrings.LocateByID("ACTION_PERFORM_TASK_IN_BATCHES"), i+1, files.Length);

var fArgs = BuildScriptParams(files[i], outputDir, func, engine);
var fResult = SingleAction(func, engine, script, fArgs, alertInfo, files[i] );

results.Add(fResult);
}

}

}

}

// Process a Bunch of Files with the Script

public static List<ScriptResult> BatchAction(string inputDir, Function func, V8ScriptEngine engine,
V8Script script, UserAlertInfo alertInfo, string outputDir = null)
{
List<ScriptResult> scriptResults = new();

Text.PrintLine(true, "Batch Action Started: ");

if(!func.IgnoreOutputPathOnMethodCalls)
{
outputDir = string.IsNullOrEmpty(outputDir) ? inputDir + InputHelper.GenerateStringComplement() : outputDir;

DirManager.CheckMissingFolder(outputDir);
}

ProcessFiles(inputDir, func, engine, script, alertInfo, scriptResults, outputDir);

var options = Program.GetUserParams.FileManagerParams.FolderOptions.SearchOptionsForSubFolders;
var subFolders = DirManager.GetFileSystems(inputDir, false, options);

if(Program.GetUserParams.FileManagerParams.FolderOptions.FilterSubFoldersInsideDirs)
{
var filter = Program.GetUserParams.FileManagerParams.FolderOptions.FilterCriteriaForSubFolders;

PathHelper.FilterDirs(ref subFolders, filter.DirNamesToSearch, filter.MatchingContentLength); // ExcludeList
}

foreach(var dir in subFolders)
{
string outputSubDir = dir.Replace(inputDir, outputDir);

DirManager.CheckMissingFolder(outputSubDir);

ProcessFiles(dir, func, engine, script, alertInfo, scriptResults, outputSubDir);
}

Text.PrintLine(true, "Batch Action Finished");

return scriptResults;
}

}