using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.IO;
using ZCore.Serializables.Config.Fields;
using ZCore.Serializables.JavaScript;
using ZCore.Serializables.SortUtils;

namespace RipeConsole.Program.Loaders.JavaScript
{
/// <summary> Loads a JavaScript File from its Respective Entry. </summary>

public static class ScriptsLoader
{
// Base Entry 

private static readonly JSCriptEntry baseEntry = new();

// Main Engine

private static readonly V8ScriptEngine jsEngine = JSHelper.InitEngine(Program.GetAppConfig.JSEngineConfig);

// Map ScriptEntries with their Path

private static readonly Dictionary<string, JSCriptEntry> MappedScripts = new();

// Map CompiledScripts with their Path

private static readonly Dictionary<string, V8Script> CompiledScripts = new();

/** <summary> Loads the Functions grouped on a Category. </summary>

<param name = "targetGroup"> The Group from which to Select Functions. </param>

<returns> The Function that was Selected. </returns> */

private static JSCriptEntry GetEntryFromFunc(Function targetFunc)
{
targetFunc.PathToScriptEntry = PathUtils.AlignPathWithAppDir(targetFunc.PathToScriptEntry);

JSCriptEntry jsEntry;
	
if(!MappedScripts.TryGetValue(targetFunc.PathToScriptEntry, out JSCriptEntry value) )
{
jsEntry = baseEntry.ReadObject(targetFunc.PathToScriptEntry);

MappedScripts.Add(targetFunc.PathToScriptEntry, jsEntry);
}

else
jsEntry = value;

return jsEntry;
}

// Get Compiled Script from Dictionary or Create a new One

private static V8Script GetCompiledScript(JSCriptEntry sourceEntry)
{
sourceEntry.ScriptMetadata.PathToJScriptFile = PathUtils.AlignPathWithAppDir(sourceEntry.ScriptMetadata.PathToJScriptFile);

V8Script compiledScript;

if(!CompiledScripts.TryGetValue(sourceEntry.ScriptMetadata.PathToJScriptFile, out V8Script value) )
{
sourceEntry.PathToScriptCache = PathUtils.AlignPathWithAppDir(sourceEntry.PathToScriptCache);

compiledScript = JSHelper.CompileScript(sourceEntry, jsEngine, Program.GetAppConfig.JSEngineConfig.CompilationMode); 

CompiledScripts.Add(sourceEntry.ScriptMetadata.PathToJScriptFile, compiledScript);
}

else
compiledScript = value;

return compiledScript;
}

// Exclusive Action for Quick Mode

private static List<ScriptResult> QuickAction(Function func, V8Script script, UserAlertInfo alertInfo,
object[] args, string quickPath)
{
var pathType = PathHelper.CheckPathType(quickPath);

if(pathType == FileAttributes.Directory && !func.ForceSingleMode)
return JSUtils.BatchAction(quickPath, func, jsEngine, script, alertInfo);

return [JSUtils.SingleAction(func, jsEngine, script, args, alertInfo, quickPath) ];
}

// Get Script Result from FileSystem (File or Dir)

private static List<ScriptResult> ProcessFileSystem(Function func, V8Script script, UserAlertInfo alertInfo,
object[] args, string quickPath)
{

if(quickPath != null)
return QuickAction(func, script, alertInfo, args, quickPath);

string inputPath = ArgumentsLoader.GetPathFromArgs(func, args, true);

if(!string.IsNullOrEmpty(inputPath) && PathHelper.CheckPathType(inputPath) == FileAttributes.Directory)
{
string outputPath = ArgumentsLoader.GetPathFromArgs(func, args, false);

return JSUtils.BatchAction(inputPath, func, jsEngine, script, alertInfo, outputPath);
}

return [JSUtils.SingleAction(func, jsEngine, script, args, alertInfo) ];
}

// Parse and Run the Script

public static void RunScript(Function sourceFunc, UserAlertInfo alertInfo = null, string quickPath = default)
{
alertInfo ??= new();

JSCriptEntry jsEntry = GetEntryFromFunc(sourceFunc);
object[] args = Array.Empty<object>();

List<ScriptResult> scriptResult = new();

try
{
JSHelper.ExposeTypes(jsEngine, jsEntry.TypesToExpose, jsEntry.NamedItems);

V8Script compiledScript = GetCompiledScript(jsEntry);

if(string.IsNullOrEmpty(sourceFunc.ScriptMethodName) )
scriptResult.Add(JavaScriptExecutor.RunScript(jsEngine, compiledScript) );

else if(!string.IsNullOrEmpty(sourceFunc.ScriptMethodName) && sourceFunc.RunScriptForResultOnly)
{
args = JSUtils.GetScriptParams(jsEngine, sourceFunc);

scriptResult.Add(JavaScriptExecutor.RunScript(jsEngine, compiledScript, sourceFunc.ScriptMethodName, args) );
}

else
{
args = JSUtils.GetScriptParams(jsEngine, sourceFunc, quickPath);

scriptResult = ProcessFileSystem(sourceFunc, compiledScript, alertInfo, args, quickPath);
}

}

catch(Exception error)
{
ErrorsHandler.DisplayErrorInfo(error);
}

finally
{
JSUtils.DisplayScriptResult(sourceFunc, jsEntry, args, scriptResult);
}

}

// Dispose the ScriptEngine

public static void CloseEngine() => jsEngine.Dispose();
}

}