namespace RipeConsole.Program.Graphics.Dialogs
{
/// <summary> Asks the User to Enter a new Name. </summary>

internal partial class NewNameDialog : InputStringDialog
{
/// <summary> Creates a new Instance of the <c>NewNameDialog</c>. </summary>

public NewNameDialog()
{
headerText = "PARAM_NEW_NAME";
adviceText = "ADVICE_ENTER_NEW_NAME";
}

}

}