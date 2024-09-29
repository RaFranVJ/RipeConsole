using System;

namespace RipeConsole.Program.Graphics.Dialogs
{
/// <summary> Suspends every Task that is being Executed by this Program until the User presses Enter or Any Key. </summary>

internal partial class ContinueDialog : ConsoleKeyDialog
{
/// <summary> Creates a new Instance of the <c>ContinueDialog</c>. </summary>

public ContinueDialog()
{
headerText = string.Empty;
bodyText = "DIALOG_SYSTEM_PAUSE";

adviceText = string.Empty;
}

/** <summary> Displays the <c>ContinueDialog</c>. </summary>
<returns> The Key Pressed by User. </returns> */

public override ConsoleKeyInfo Popup() => Popup(null, null);

/** <summary> Displays the <c>ContinueDialog</c>. </summary>
<returns> The Key Pressed by User. </returns> */

public override ConsoleKeyInfo Popup(string sourceAdvice, string sourceBody = null)
{
sourceBody = string.IsNullOrEmpty(sourceBody) ? 
string.Format(Text.ProgramStrings.LocateByID(bodyText), ConsoleKey.Enter) : sourceBody;

return GetUserKey(sourceBody, sourceAdvice);
}

}

}