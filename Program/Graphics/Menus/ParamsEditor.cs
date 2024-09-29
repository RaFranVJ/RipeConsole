using System.Reflection;

namespace RipeConsole.Program.Graphics.Menus
{
/// <summary> Allows the User to comit Changes to the ParamsGroup of this Application. </summary>

public partial class ParamsEditor : ParamsMenu
{
/// <summary> Creates a new Instance of the <c>ParamsEditor</c>. </summary>

public ParamsEditor()
{
paramInstance = Program.GetUserParams;
}

/** <summary> Displays the <c>ParamsEditor</c>. </summary>

<returns> Info related to Last Parameter selected by User. </returns> */

public override object DynamicSelection()
{
bool exitMenu = false;

while(!exitMenu)
{
var selectedParam = base.DynamicSelection() as PropertyInfo;

if(selectedParam == null)
break;

UpdatePropertyValue(selectedParam);
}

return paramInstance;
}

}

}