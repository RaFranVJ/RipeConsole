using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RipeConsole.Program.Serializables.Config.KeyDefs.MenuKeys;

namespace RipeConsole.Program.Graphics.Menus
{
/// <summary> Allows the User to Edit Elements inside a List. </summary>

internal partial class ListEditor<T> : ListViewer<T>
{
// Key Defs

private readonly KeyDefsForListEditor definitions = new();

// Displays the Selection Dialog

protected override void SelectionDialog()
{
KeyDefsForListEditor defs = new();

string selectOptionMsg = Text.ProgramStrings.LocateByID("DIALOG_SELECT_ELEMENT_FROM_LIST2");

Text.PrintDialog(true, string.Format(selectOptionMsg, defs.NavigationKey_Up, defs.NavigationKey_Down, defs.SelectionKey,
defs.OmissionKey, defs.AddElement, defs.RemoveElement, defs.CloneElement, defs.EditElement) );
}

// Evaluates the Key Pressed by User

protected override void EvaluateKey(List<T> options, ConsoleKey key, ref int index,
ref T value, ref string numberPressed, ref bool confirm)
{
int optionsCount = options.Count;

if(key == definitions.NavigationKey_Up)
index = (index - 1 + optionsCount) % optionsCount;

else if(key == definitions.NavigationKey_Down)
index = (index + 1) % optionsCount;

else if(key == definitions.InfoKey)
{
Text.PrintLine();

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

else if(key == definitions.CloneElement)
options.Add(options[index] );

else if(key == definitions.EditElement)
options[index] = ReadElement(options[index] );

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

/** <summary> Shows the Options of this Menu. </summary>

<param name = "userOptions"> The List of Options to be Diplayed. </param>
<param name = "expectedValue"> A value expected to be Returned once the User mades the Selection. </param>
<param name = "displayAction"> An Action that Defines how Options should be Displayed on Screen. </param>
<param name = "onSelectAction"> An Action that Defines how to Display the Option selected by the User. </param> */

protected override void ShowOptions(List<T> userOptions, ref T expectedValue, Action<T, int> displayAction = default, 
Action<T, int> onSelectAction = default)
{
displayAction ??= PrintAction;
onSelectAction ??= SelectiveAction;

Task<ConsoleKeyInfo> keyInfo;

if(userOptions == null || userOptions.Count <= 0)
{
Text.PrintWarning("No options available, try adding a new Element to List or Skip this Selection");

keyInfo = Task.Run( () => Console.ReadKey(true) ); 

if(keyInfo.Result.Key != definitions.AddElement)
return;

userOptions.Add(ReadElement() );
}

int selectedOptionIndex = 0;
Console.CursorVisible = false;

string numberPressed = string.Empty;
bool confirmSelection = false;

while(!confirmSelection)
{
Console.Clear();

Text.PrintHeader(Text.ProgramStrings.LocateByID(headerText) );

for(int i = 0; i < userOptions.Count; i++)
{

if(i == selectedOptionIndex)
{
Console.BackgroundColor = (Console.ForegroundColor == ConsoleColor.Yellow) ? ConsoleColor.Blue : ConsoleColor.Yellow;
Console.ForegroundColor = ConsoleColor.DarkGray;
}

displayAction(userOptions[i], i);
Text.PrintLineAfterLastElement(i, userOptions.Count);

if(i == selectedOptionIndex)
Console.ResetColor();

}

SelectionDialog();

Console.SetCursorPosition(0, userOptions.Count + 8);
onSelectAction(userOptions[selectedOptionIndex], selectedOptionIndex);

keyInfo = Task.Run( () => Console.ReadKey(true) ); 

EvaluateKey(userOptions, keyInfo.Result.Key, ref selectedOptionIndex, ref expectedValue, ref numberPressed,
ref confirmSelection);

Text.PrintLine();
Console.ResetColor();
}

Console.CursorVisible = true;
}

}

}