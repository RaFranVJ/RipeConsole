using System;
using System.Collections.Generic;
using Microsoft.ClearScript.V8;
using RipeConsole.Program.Serializables.Config.KeyDefs.MenuKeys;
using ZCore.Serializables.Config.Fields;
using ZCore.Serializables.JavaScript;
using ZCore.Serializables.SortUtils;

namespace RipeConsole.Program.Graphics.Menus
{
/// <summary> Allows the User to Select a File and Process it with a JS Code. </summary>

internal partial class JSFilePrompt : FileMenu
{
// Output Dir

private static string outputDir;

// Base Fun for JS

private static Function baseFunc;

// JS Engine

private static V8ScriptEngine jsEngine;

// JS Script

private static V8Script sourceScript;

// Alert Info

private UserAlertInfo alertInfo;

// Script Result

protected List<ScriptResult> results = new();

/// <summary> Creates a new Instance of the <c>JSFilePrompt</c>. </summary>

public JSFilePrompt(List<string> list, string outDir, Function func, V8ScriptEngine engine, V8Script script,
UserAlertInfo iAlert)
{
headerText = "HEADER_JS_PROMPT";
adviceText = "ADVICE_CHOOSE_FILE_TO_PROCESS";

baseList = list;
outputDir = outDir;

baseFunc = func;
jsEngine = engine;

sourceScript = script;
alertInfo = iAlert;
}

public List<ScriptResult> GetResults() => results;

// Evaluates the Key Pressed by User

protected override void EvaluateKey(List<string> options, ConsoleKey key, ref int index, 
ref string value, ref string numberPressed, ref bool confirm)
{
KeyDefsForListViewer definitions = new();

int optionsCount = options.Count;

if(key == definitions.NavigationKey_Up)
index = (index - 1 + optionsCount) % optionsCount;

else if(key == definitions.NavigationKey_Down)
index = (index + 1) % optionsCount;

else if(key == definitions.InfoKey)
{
Text.PrintLine(true);

DisplayElementInfo(options[index], index);

_ = continueDialog.Popup();
}

else if(key == definitions.SelectionKey)
{
var args = JSUtils.BuildScriptParams(options[index], outputDir, baseFunc, jsEngine);
results.Add(JSUtils.SingleAction(baseFunc, jsEngine, sourceScript, args, alertInfo, options[index] ) );

options.RemoveAt(index);

if(index > 0)
index--;

confirm = index >= options.Count;
}

else if(key == definitions.OmissionKey)
{
Console.Clear();

confirm = true;
}

else if(key == definitions.AddElement)
options.Add(ReadElement() );

else if(key == definitions.RemoveElement)
{
options.RemoveAt(index);

if(index > 0)
index--;

confirm = index >= options.Count;
}

else if(key >= ConsoleKey.D0 && key <= ConsoleKey.D9)
{
numberPressed += (key - ConsoleKey.D0).ToString();

int? entry = TransformNumericInput(numberPressed, options);

if(entry.HasValue)
index = entry.Value;

else
numberPressed = string.Empty;

}

}

}

}