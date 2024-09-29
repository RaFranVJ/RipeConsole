using Microsoft.ClearScript.V8;
using RipeConsole.Program.Graphics.Dialogs;
using System.Collections.Generic;
using System.IO;
using ZCore.Serializables;
using ZCore.Serializables.SortUtils;

namespace RipeConsole.Program.Loaders.JavaScript
{
/// <summary> Loads the Arguments of a Function and Adds it to a ScriptEngine. </summary>

public static class ArgumentsLoader
{
// Add and Build Quick Paths to List

private static void AddQuickPath(List<object> args, string quickPath, bool useOutputPath)
{
args.Add(quickPath);

if(useOutputPath)
{
string baseDir = Path.GetDirectoryName(quickPath);
string outputName = InputHelper.GenerateStringComplement() + Path.GetFileNameWithoutExtension(quickPath);

string outputPath = baseDir + Path.DirectorySeparatorChar + outputName + Path.GetExtension(quickPath);

args.Add(outputPath);
}

}

// Add Generic Paths to List

private static void AddGenericPaths(List<object> args, Function func, string quickPath = null)
{

if(quickPath != null)
AddQuickPath(args, quickPath, !func.IgnoreOutputPathOnMethodCalls);

else
{
	
if(!func.IgnoreInputPathOnMethodCalls)
args.Add(GetUserDefinedPath(func.GenericPathForInput, true) );

if(!func.IgnoreOutputPathOnMethodCalls)
args.Add(GetUserDefinedPath(func.GenericPathForOutput, false) );

}

}

// Process Generic Args

private static void ProcessGenericArgs(List<object> sourceArgs, List<object> targetArgs)
{

if(sourceArgs == null || targetArgs == null)
return;

sourceArgs.AddRange(targetArgs);
}

// Expose Generic Args

private static void ExposeGenericArgs(List<object> sourceArgs, Dictionary<string, object> targetArgs, V8ScriptEngine engine)
{

if(sourceArgs == null || targetArgs == null)
return;

foreach(var arg in targetArgs)
{
engine.AddHostObject(arg.Key, arg.Value);
sourceArgs.Add(arg.Value);
}

}

// Add Generic Args to List

public static void AddGenericArgs(List<object> args, Function func, V8ScriptEngine engine)
{

if(func.ExposeArgsFirst)
{
ExposeGenericArgs(args, func.GenericArgsToExpose, engine);
ProcessGenericArgs(args, func.GenericArgs);
}

else
{
ProcessGenericArgs(args, func.GenericArgs);
ExposeGenericArgs(args, func.GenericArgsToExpose, engine);
}

}

// Get generic Args

public static List<object> GetGenericParams(Function func, V8ScriptEngine engine, string quickPath = null)
{
List<object> args = new();

AddGenericPaths(args, func, quickPath);
AddGenericArgs(args, func, engine);

return args;
}

// Add Specific Paths to List

private static void AddSpecificPaths(List<object> args, Function func, UserParams userParams, string quickPath = null)
{

if(quickPath != null)
AddQuickPath(args, quickPath, !func.IgnoreOutputPathOnMethodCalls);

else
{
string path;
	
if(!func.IgnoreInputPathOnMethodCalls)
{
path = userParams.GetFieldValue(func.SpecificPathForInput) as string;

args.Add(GetUserDefinedPath(path, true) );
}

if(!func.IgnoreOutputPathOnMethodCalls)
{
path = userParams.GetFieldValue(func.SpecificPathForOutput) as string;

args.Add(GetUserDefinedPath(path, false) );
}

}

}

// Process Specific Args

private static void ProcessSpecificArgs(List<object> sourceArgs, List<string[]> targetArgs, UserParams userParams)
{

if(sourceArgs == null || targetArgs == null)
return;

foreach(var arg in targetArgs)
sourceArgs.Add(userParams.GetFieldValue(arg) );

}

// Expose Specific Args

private static void ExposeSpecificArgs(List<object> sourceArgs, Dictionary<string, string[]> targetArgs,
V8ScriptEngine engine, UserParams userParams)
{

if(sourceArgs == null || targetArgs == null)
return;

foreach(var arg in targetArgs)
{
object fieldValue = userParams.GetFieldValue(arg.Value);

engine.AddHostObject(arg.Key, fieldValue);
sourceArgs.Add(fieldValue);
}

}

// Add Specific Args to List

public static void AddSpecificArgs(List<object> args, Function func, V8ScriptEngine engine, UserParams userParams)
{

if(func.ExposeArgsFirst)
{
ExposeSpecificArgs(args, func.SpecificArgsToExpose, engine, userParams);
ProcessSpecificArgs(args, func.SpecificArgs, userParams);
}

else
{
ProcessSpecificArgs(args, func.SpecificArgs, userParams);
ExposeSpecificArgs(args, func.SpecificArgsToExpose, engine, userParams);
}

}

// Get specific Args

public static List<object> GetSpecificParams(Function func, V8ScriptEngine engine, string quickPath = null)
{
UserParams userParams = Program.GetUserParams;
List<object> args = new();

AddSpecificPaths(args, func, userParams, quickPath);
AddSpecificArgs(args, func, engine, userParams);

return args;
}

// Get Input/Output Path from a List of Arguments

public static string GetPathFromArgs(Function func, object[] args, bool forInput)
{

if(forInput && func.IgnoreInputPathOnMethodCalls || !forInput && func.IgnoreOutputPathOnMethodCalls)
return string.Empty;

int baseIndex = forInput ? 0 : 1;

if(func.ExposeArgsFirst && func.GenericArgs != null)
baseIndex =	forInput ? func.GenericArgs.Count : func.GenericArgs.Count + 1;

else if(func.ExposeArgsFirst && func.GenericArgsToExpose != null)
baseIndex =	forInput ? func.GenericArgsToExpose.Count : func.GenericArgsToExpose.Count + 1;

else if(func.ExposeArgsFirst && func.SpecificArgs != null)
baseIndex =	forInput ? func.SpecificArgs.Count : func.SpecificArgs.Count + 1;

else if(func.ExposeArgsFirst && func.SpecificArgsToExpose != null)
baseIndex =	forInput ? func.SpecificArgsToExpose.Count : func.SpecificArgsToExpose.Count + 1;

if(baseIndex < args.Length)
return args[baseIndex].ToString();

return string.Empty;
}

// Get Path defined by User from UserParams or in app Execution 

public static string GetUserDefinedPath(string targetPath, bool forInput)
{
var pathDialog = forInput ? new InputPathDialog() : new OutputPathDialog();

targetPath = PathUtils.AlignPathWithAppDir(targetPath);

return string.IsNullOrEmpty(targetPath) ? pathDialog.Popup() : targetPath;
}

}

}