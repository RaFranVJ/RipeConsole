using System;
using System.Collections.Generic;
using RipeConsole.Program.Serializables.Config.KeyDefs.MenuKeys;

namespace RipeConsole.Program.Graphics.Menus
{
/// <summary> Allows the User to Select a File from a List. </summary>

internal partial class FileMenu : ListViewer<string>
{
/// <summary> Creates a new Instance of the <c>FileMenu</c>. </summary>

public FileMenu()
{
headerText = "HEADER_SELECT_FILE";
adviceText = "ADVICE_CHOOSE_FILE";

baseList = new();
}

/// <summary> Creates a new Instance of the <c>FileMenu</c>. </summary>

public FileMenu(List<string> list)
{
headerText = "HEADER_SELECT_FILE";
adviceText = "ADVICE_CHOOSE_FILE";

baseList = list as List<string> ?? new();
}

/** <summary> Displays the <c>ListViewer</c>. </summary>
<returns> The Elements selected by the User. </returns> */

public override string DynamicSelection()
{
ActionWrapper<string, int> displayAction = new( PrintAction );
ActionWrapper<string, int> onSelectAction = new( SelectiveAction );

string selectedItem = default;
ShowOptions(baseList, ref selectedItem, displayAction.Init, onSelectAction.Init); //

return selectedItem;
}

// Displays the Selection Dialog

protected override void SelectionDialog()
{
KeyDefsForListViewer defs = new();

string selectOptionMsg = Text.ProgramStrings.LocateByID("DIALOG_SELECT_FILE");

Text.PrintDialog(true, string.Format(selectOptionMsg, defs.NavigationKey_Up, defs.NavigationKey_Down, defs.SelectionKey,
defs.OmissionKey, defs.AddElement, defs.RemoveElement) );
}

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
value = options[index];

confirm = true;
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