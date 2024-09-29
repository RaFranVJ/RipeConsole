using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RipeConsole.Program.Graphics.Dialogs;
using RipeConsole.Program.Serializables.Config.KeyDefs.MenuKeys;

namespace RipeConsole.Program.Graphics
{
/// <summary> Represents a Menu of this Program. </summary>

public class Menu<T, Result> : Graphics
{
// CONTINUE

internal static ContinueDialog continueDialog = new();

/** <summary> Displays the <c>Menu</c>. </summary>

<returns> The Option Selected by User. </returns> */

public virtual Result DynamicSelection() => default;

/** <summary> An Action used for Printing the Options available. </summary>

<param name = "sourceItem"> The Item to be Diplayed. </param> */

public virtual void PrintAction(T sourceItem, int elementIndex)
{
Text.Print(true, "{0} - {1}", elementIndex + 1, sourceItem);
}

/** <summary> An Action used for Printing the Option selected by User. </summary>

<param name = "selectedItem"> The Item selected by User. </param>
<param name = "elementIndex"> The Index of the Element selected. </param> */

public virtual void SelectiveAction(T selectedItem, int elementIndex)
{
Text.PrintAdvice(false, Text.ProgramStrings.LocateByID(adviceText) );

Text.Print(ConsoleColor.DarkYellow, false, elementIndex.ToString() );
}

// Displays the Selection Dialog

protected virtual void SelectionDialog()
{
KeyDefsForMenus defs = new();

string selectOptionMsg = Text.ProgramStrings.LocateByID("DIALOG_SELECT_OPTION"); // Only aplies for this Menu

Text.PrintDialog(true, string.Format(selectOptionMsg, defs.NavigationKey_Up, defs.NavigationKey_Down, defs.SelectionKey,
defs.OmissionKey) );
}

// Displays Info related to the Current Element

protected virtual void DisplayElementInfo(T element, int index)
{
Text.PrintSubHeader("Element Info");

Text.PrintLine(false, "Index: {0}", index);
Text.PrintLine(false, "Value: {0}", element);
Text.PrintLine(true, "Type: {0}", typeof(T).Name);
}

// Get Int from String

protected virtual int? TransformNumericInput(string input, List<T> options)
{

if(int.TryParse(input, out int numericValue) && numericValue >= 0 && numericValue < options.Count)
return numericValue;

return null;
}

// Evaluates the Key Pressed by User

protected virtual void EvaluateKey(List<T> options, ConsoleKey key, ref int index, 
ref T value, ref string numberPressed, ref bool confirm)
{
KeyDefsForMenus definitions = new();

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
numberPressed = string.Empty;

confirm = true;
}

else if(key == definitions.OmissionKey)
{
Console.Clear();

numberPressed = string.Empty;
confirm = true;
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

/** <summary> Shows the Options of this Menu. </summary>

<param name = "userOptions"> The List of Options to be Diplayed. </param>
<param name = "expectedValue"> A value expected to be Returned once the User mades the Selection. </param>
<param name = "displayAction"> An Action that Defines how Options should be Displayed on Screen. </param>
<param name = "onSelectAction"> An Action that Defines how to Display the Option selected by the User. </param> */

protected virtual void ShowOptions(List<T> userOptions, ref T expectedValue, Action<T, int> displayAction = default, 
Action<T, int> onSelectAction = default)
{
displayAction ??= PrintAction;
onSelectAction ??= SelectiveAction;

if(userOptions == null || userOptions.Count <= 0)
{
Text.PrintWarning("No options available, this may cause Exceptions due to null Selection");
return;
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

Task<ConsoleKeyInfo> keyInfo = Task.Run( () => Console.ReadKey(true) ); 

EvaluateKey(userOptions, keyInfo.Result.Key, ref selectedOptionIndex, ref expectedValue, ref numberPressed,
ref confirmSelection);

Text.PrintLine();
Console.ResetColor();
}

Console.CursorVisible = true;
}

/** <summary> Shows the Options of this Menu. </summary>

<param name = "userOptions"> The Array of Options to be Diplayed. </param>
<param name = "expectedValue"> A value expected to be Returned once the User mades the Selection. </param>
<param name = "displayAction"> An Action that Defines how Options should be Displayed on Screen. </param>
<param name = "onSelectAction"> An Action that Defines how to Display the Option selected by the User. </param> */

protected void ShowOptions(T[] userOptions, ref T expectedValue, Action<T, int> displayAction = default, 
Action<T, int> onSelectAction = default) 
{
ShowOptions(new List<T>(userOptions), ref expectedValue, displayAction, onSelectAction);
}

}

}