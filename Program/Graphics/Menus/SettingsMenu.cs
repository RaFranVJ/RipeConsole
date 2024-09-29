namespace RipeConsole.Program.Graphics.Menus
{
/// <summary> The Settings Menu of this Program. </summary>

public partial class SettingsMenu : ParamsMenu
{
/// <summary> Creates a new Instance of the <c>SettingsMenu</c>. </summary>

public SettingsMenu()
{
paramInstance = Program.GetAppConfig;
}

}

}