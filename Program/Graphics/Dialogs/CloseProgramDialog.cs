using System;

namespace RipeConsole.Program.Graphics.Dialogs
{
/// <summary> Closes the Program when the User presses Enter or any Key. </summary>

internal partial class CloseProgramDialog : ConsoleKeyDialog
{
/// <summary> Creates a new Instance of the <c>CloseProgramDialog</c>. </summary>

public CloseProgramDialog()
{
headerText = "HEADER_EXECUTION_COMPLETE";
bodyText = "DIALOG_PROGRAM_TERMINATION";

adviceText = string.Empty;
}

/** <summary> Displays the <c>ContinueDialog</c>. </summary>
<returns> The Key Pressed by User. </returns> */

public override ConsoleKeyInfo Popup() => Popup(null, null);

/** <summary> Displays the <c>ContinueDialog</c>. </summary>
<returns> The Key Pressed by User. </returns> */

public override ConsoleKeyInfo Popup(string sourceAdvice, string sourceBody = null)
{
var keyDefs = Program.GetAppConfig.MappedKeys.TerminationKeys;

sourceBody = string.IsNullOrEmpty(sourceBody) ? 
string.Format(Text.ProgramStrings.LocateByID(bodyText), keyDefs.ExitKey, keyDefs.ReturnKey) : sourceBody;

return GetUserKey(sourceBody, sourceAdvice);
}

}

}