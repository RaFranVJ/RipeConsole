using RipeConsole.Program.Graphics.Dialogs;
using RipeConsole.Program.Graphics.Menus;
using RipeConsole.Program.Graphics.UserSelections;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RipeConsole.Program
{
/// <summary> Reads Values for a Object Property. </summary>

public static class PropertyReader
{
// Default Msg

private const string defaultMsg = "Enter a {0} for \"{1}\": ";

// Get Boolean (true or false)

public static bool GetBoolean(PropertyInfo property)
{
string adv = string.Format(defaultMsg, property.PropertyType.Name, property.Name);

return new LogicSelection().GetSelectionParam(adv, null);
}

// Get Simple Type (int, float, etc)

public static object GetGenericValue(PropertyInfo property)
{
string adv = string.Format(defaultMsg, property.PropertyType.Name, property.Name);

Type selectorType = typeof(GenericSelection<>).MakeGenericType(property.PropertyType);
dynamic selector = Activator.CreateInstance(selectorType);

return selector.GetSelectionParam(adv);
}

// Get String (Simple String or Input/Output Path)

public static string GetString(PropertyInfo property)
{
string adv = string.Format(defaultMsg, property.PropertyType.Name, property.Name);

if(property.Name == "UserLanguage")
return new LanguageMenu().DynamicSelection() ?? "en-US";

else if(property.Name == "InputPath" || property.Name == "FilePath" || property.Name.Contains("PathTo") )
return new InputPathDialog().Popup(adv);

else if(property.Name.Contains("OutputPath") )
return new OutputPathDialog().Popup(adv);

else if(property.Name.Contains("Name") )
return new NewNameDialog().Popup(adv);

return new InputStringDialog().Popup(adv);
}

// Get Bytes (Buffer, CipherKey or AuthCode)

public static byte[] GetBytes(PropertyInfo property)
{
string adv = string.Format(defaultMsg, property.PropertyType.Name, property.Name);

if(property.Name.Contains("Key") )
return new CipherKeyDialog().Popup(adv);

else if(property.Name.Contains("Salt") )
return new SaltValueDialog().Popup(adv);

return new BufferDialog().Popup(adv);
}

// Get DateTime

public static DateTime GetDateTime(PropertyInfo property)
{
string adv = string.Format(defaultMsg, property.PropertyType.Name, property.Name);

return new DateTimeSelection().GetSelectionParam(adv);
}

// Get Enum

public static Enum GetEnum(PropertyInfo property)
{
Type selectorType = typeof(EnumSelector<>).MakeGenericType(property.PropertyType);

dynamic selector = Activator.CreateInstance(selectorType);

return selector.DynamicSelection();
}

// Get UserKey

public static ConsoleKey GetUserKey(PropertyInfo property)
{
string adv = string.Format("Set a Key for \"{0}\": ", property.Name);

return new ConsoleKeyDialog().Popup(adv).Key;
}

// Get List

public static List<T> GetList<T>(PropertyInfo property, object instance)
{
ListEditor<T> editor = new();

editor.UpdateList(property.GetValue(instance) as List<T> ?? new() );
editor.DynamicSelection();

return editor.GetBaseList();
}

}

}