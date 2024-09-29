using System;

namespace RipeConsole.Program.Graphics.Dialogs
{
/// <summary> Asks the User to Enter a <b>InputPath</b>. </summary>

internal partial class InputPathDialog : InputStringDialog
{
/// <summary> Creates a new Instance of the <c>InputPathDialog</c>. </summary>

public InputPathDialog()
{
headerText = "PARAM_INPUT_PATH";
bodyText =  "DIALOG_DRAG_PATH_FOR_SELECTION";

adviceText = "ADVICE_ENTER_INPUT_PATH";
}

// Get User Path

protected string GetUserPath(int minLength, int maxLength)
{
Text.PrintHeader(Text.ProgramStrings.LocateByID(headerText) );

string bodyMsg = Text.ProgramStrings.LocateByID(bodyText);
string selectedPath = GetInputString(minLength, maxLength, bodyMsg);

return PathHelper.FilterPath(selectedPath);
}

/** <summary> Displays the <c>InputPathDialog</c>. </summary>
<returns> The <b>InputPath</b> entered by the User. </returns> */

public override string Popup() => GetUserPath(1, Array.MaxLength);

/** <summary> Displays the <c>InputPathDialog</c>. </summary>

<param name = "inputRange"> The Range which user Input must follow. </param>

<returns> The <b>InputPath</b> entered by the User. </returns> */

public override string Popup(Limit<int> inputRange) => GetUserPath(inputRange.MinValue, inputRange.MaxValue);
}

}