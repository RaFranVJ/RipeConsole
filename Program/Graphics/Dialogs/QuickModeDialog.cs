namespace RipeConsole.Program.Graphics.Dialogs
{
/// <summary> Alerts the User that the Program started in the Quick Mode. </summary>

internal partial class QuickModeDialog : Dialog<object>
{
/// <summary> Creates a new Instance of the <c>QuickModeDialog</c>. </summary>

public QuickModeDialog()
{
headerText = "HEADER_QUICK_MODE";
adviceText = "DIALOG_QUICK_MODE";
}

/** <summary> Displays the <c>QuickModeDialog</c>. </summary>

<returns> The Path dragged by the User. </returns> */

public override object Popup()
{
Text.PrintHeader(Text.ProgramStrings.LocateByID(headerText) );

Text.PrintAdvice(true, Text.ProgramStrings.LocateByID(adviceText) );
Text.PrintLine();

return new ContinueDialog().Popup();
}

}

}