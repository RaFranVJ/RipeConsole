namespace RipeConsole.Program.Graphics.Dialogs
{
/// <summary> Asks the User to Enter a <b>SaltValue</b>. </summary>

internal partial class SaltValueDialog : CipherKeyDialog
{
/// <summary> Creates a new Instance of the <c>SaltValueDialog</c>. </summary>

public SaltValueDialog()
{
headerText = "PARAM_SALT_VALUE";
adviceText = "ADVICE_ENTER_SALT_VALUE";
}

}

}