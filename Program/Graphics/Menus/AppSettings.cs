using System.Reflection;

namespace RipeConsole.Program.Graphics.Menus
{
/// <summary> Allows the User to comit Changes to the Settings of this Application. </summary>

public partial class AppSettings : SettingsMenu
{
/// <summary> Creates a new Instance of the <c>AppSettings</c>. </summary>

public AppSettings()
{
headerText = "HEADER_APP_SETTINGS";
adviceText = "ADVICE_SELECT_SETTING";
}

/** <summary> Displays the <c>AppSettings</c>. </summary>

<returns> The new Config. </returns> */

public override object DynamicSelection()
{
bool exitMenu = false;

while(!exitMenu)
{
var selectedConfig = base.DynamicSelection() as PropertyInfo;

if(selectedConfig == null)
break;

UpdatePropertyValue(selectedConfig);
}

return paramInstance;
}

}

}