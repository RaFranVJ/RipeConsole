using System;
using System.Linq;
using System.Reflection;

namespace RipeConsole.Program.Graphics.Menus
{
/// <summary> The Params Menu of this Program. </summary>

public partial class ParamsMenu : Menu<PropertyInfo, object>
{
/// <summary> The Instance where Params should be Obtained from. </summary>

protected object paramInstance;

/// <summary> Creates a new Instance of the <c>ParamsMenu</c>. </summary>

public ParamsMenu()
{
headerText = "HEADER_PARAMS_EDITOR";
adviceText = "ADVICE_SELECT_PARAM";

paramInstance = this;
}

/** <summary> Displays the Value of a Property according to its Type. </summary>
<param name = "targetPropInfo"> Info related to the Property to be Displayed on Screen. </param> */

private void DisplayPropertyValue(PropertyInfo targetPropInfo, int index)
{
object propertyValue = targetPropInfo.GetValue(paramInstance);

Text.Print(true, "[{0}] => {1}: {2}", index, targetPropInfo.Name, Text.GetObjectValue(propertyValue, false, false) );
}

/** <summary> Updates the value of the selected Property. </summary>

<param name = "property"> The Property to be Updated. </param> */

protected void UpdatePropertyValue(PropertyInfo property)
{

if(property == null)
return;

object newValue = null;

if(property.PropertyType == typeof(bool) )
newValue = PropertyReader.GetBoolean(property);

else if(property.PropertyType == typeof(string) )
newValue = PropertyReader.GetString(property);

else if (property.PropertyType == typeof(byte[]) )
newValue = PropertyReader.GetBytes(property);

else if(property.PropertyType == typeof(DateTime) )
newValue = PropertyReader.GetDateTime(property);

else if(property.PropertyType == typeof(ConsoleKey) )
newValue = PropertyReader.GetUserKey(property);

else if (property.PropertyType.IsEnum && property.PropertyType != typeof(ConsoleKey) )
newValue = PropertyReader.GetEnum(property);

else if(typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType) &&
property.PropertyType.IsGenericType)
{
var elementType = property.PropertyType.GetGenericArguments()[0];
var method = typeof(PropertyReader).GetMethod("GetList").MakeGenericMethod(elementType);

newValue = method.Invoke(null, [property, paramInstance] );
}

else if(property.PropertyType.IsArray)
{
var elementType = property.PropertyType.GetElementType();
var method = typeof(PropertyReader).GetMethod("GetList").MakeGenericMethod(elementType);

var list = method.Invoke(null, [property, paramInstance] );
var toArrayMethod = typeof(Enumerable).GetMethod("ToArray").MakeGenericMethod(elementType);

newValue = toArrayMethod.Invoke(null, [list] );
}

else if(property.PropertyType.IsClass && property.PropertyType != typeof(string) )
{
var subClassInstance = property.GetValue(paramInstance);

if(subClassInstance == null)
{
subClassInstance = Activator.CreateInstance(property.PropertyType);

property.SetValue(paramInstance, subClassInstance);
}

ParamsMenu innerMenu = new();
innerMenu.UpdateParamInstance(subClassInstance);

var selectedProperty = innerMenu.DynamicSelection() as PropertyInfo;

if(selectedProperty != null)
innerMenu.UpdatePropertyValue(selectedProperty);
        
}

else
newValue = PropertyReader.GetGenericValue(property);

if(newValue != null)
property.SetValue(paramInstance, newValue);

}

/** <summary> Displays the <c>ParamsMenu</c>. </summary>
<returns> The Parameter selected by the User. </returns> */

public override object DynamicSelection()
{
Type instanceType = paramInstance.GetType();
PropertyInfo[] paramFields = instanceType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

ActionWrapper<PropertyInfo, int> displayAction = new( PrintAction );
ActionWrapper<PropertyInfo, int> onSelectAction = new( SelectiveAction );

PropertyInfo selectedParam = default;
ShowOptions(paramFields, ref selectedParam, displayAction.Init, onSelectAction.Init);

return selectedParam;
}

/** <summary> Prints info related to the Param given (Name, and Value, if specified). </summary>
<param name = "sourceItem"> The Parameter where the Info will be Obtained from. </param> */

public override void PrintAction(PropertyInfo sourceItem, int index) => DisplayPropertyValue(sourceItem, index);

/** <summary> Displays the Parameter selected by User. </summary>

<param name = "selectedItem"> The Param selected by  User. </param>
<param name = "elementIndex"> The Index of the Param selected. </param> */

public override void SelectiveAction(PropertyInfo selectedItem, int elementIndex)
{
Text.PrintAdvice(false, Text.ProgramStrings.LocateByID(adviceText) );

Text.Print(ConsoleColor.DarkYellow, true, selectedItem.Name);
}

// Displays Info related to the Current Element

protected override void DisplayElementInfo(PropertyInfo element, int index)
{
Text.PrintSubHeader("Property Info");

object propertyValue = element.GetValue(paramInstance);

Text.PrintLine(false, "Index: {0}", index);
Text.PrintLine(false, "Name: {0}", element.Name);
Text.PrintLine(false, "Value: {0}", Text.GetObjectValue(propertyValue, false, true) );
Text.PrintLine(true, "Type: \"{0}\" from \"{1}\"", propertyValue.GetType().Name, propertyValue.GetType().Namespace);
}

/** <summary> Updates the Instance used as a Reference on this Menu. </summary>
<param name = "sourceInstance"> The Instance to be Set. </param> */

public void UpdateParamInstance(object sourceInstance)
{
paramInstance = sourceInstance;

headerText = paramInstance.GetType().Name;
}

}

}