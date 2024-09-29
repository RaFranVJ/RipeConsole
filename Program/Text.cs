using Newtonsoft.Json.Linq;
using RipeConsole.Program.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ZCore.Modules;
using ZCore.Serializables;

namespace RipeConsole.Program
{
/// <summary> The Text that this Program displays. </summary>

public static class Text
{
// Str

public static LocalizedStrings ProgramStrings => TextLoader.GetUserBasedStrings();

/** <summary> Gets a Format that Indicates how an Array of Objects should be Displayed on Screen. </summary>

<param name = "targetObjs"> The Objects to be Displayed. </param>

<returns> The Display Format. </returns> */

private static string GetDisplayFormat(object[] targetObjs)
{
int objsCount = (targetObjs == null) ? 0 : targetObjs.Length;
var lengthRange = Enumerable.Range(0, objsCount);

var displayStr = lengthRange.Select(i => $"{{{i}}}");

return string.Join(", ", displayStr);
}

// Get Object Value as a String for further Display

public static string GetObjectValue<T>(T targetObj, bool displayTypes, bool serializeObjs)
{
string objValue;

if(targetObj == null)
objValue = "<null>";

else
{
Type objType = targetObj.GetType();

if(objType.IsArray)
{
Type elementsType = objType.GetElementType();

if(elementsType == typeof(byte) )
{
string displayString = Console.InputEncoding.GetString(targetObj as byte[] );
objValue = string.Format("\"{0}\"", displayString);
}

else
{
var arr = (Array) (object)targetObj;

string separator = (elementsType.IsPrimitive || elementsType == typeof(string) ) ? ", " : Environment.NewLine;

objValue = string.Join(separator, arr.Cast<object>().Select(e => e.ToString() ).ToArray() );
}
 
}

else if(objType.IsEnum)
{
Enum enumValue = (Enum) (object)targetObj;
string enumName = Enum.GetName(objType, enumValue);

objValue = string.Format("{0} ({1})", enumName, Convert.ToInt32(enumValue) );
}

else if(objType.IsPrimitive)
objValue = string.Format("{0:n0}", targetObj);

else if(objType.IsClass)
{

if(objType ==  typeof(string) )
objValue = string.Format("\"{0}\"", targetObj);

else if(typeof(System.Collections.IEnumerable).IsAssignableFrom(objType) &&  objType.IsGenericType)
objValue = targetObj is not IEnumerable<T> lst ? "<EmptyList>" : string.Join(", ", lst.ToArray() );

else
{

if(serializeObjs)
objValue = string.Format("Object:\n{0}", JsonSerializer.SerializeObject(targetObj) );

else
{
var propertyMembers = objType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
objValue = string.Format("{0} x{1}", ProgramStrings.LocateByID("RANGE_ELEMENT_OTHER"), propertyMembers.Length);
}

}

}

else
objValue = targetObj.ToString();

if(displayTypes)
objValue = string.Format("{0}\n(Type is \"{1}\" from \"{2}\")", objValue, objType.Name, objType.Namespace);

}

return objValue;
}

/** <summary> Prints an Array of Objects on the Screen with the specified Format. </summary>

<param name = "addLineBreak"> A Boolean that Determines if a Linebreak should be Added after Printing. </param>
<param name = "sourceFormat"> The Format of the Object (Optional). </param>
<param name = "targetObjs"> The Array of Object to be Displayed. </param> */

public static void Print(bool addLineBreak, string sourceFormat = default, params object[] targetObjs)
{
sourceFormat ??= GetDisplayFormat(targetObjs);

if(targetObjs == null)
return;

Console.Write(sourceFormat, targetObjs);

if(addLineBreak)
PrintLine();

}

/** <summary> Prints an Array of Objects on the Screen with the specified Format and Color. </summary>

<param name = "textColor"> The Color of the Text displayed. </param>
<param name = "addLineBreak"> A Boolean that Determines if a Linebreak should be Added after Printing. </param>
<param name = "sourceFormat"> The Format of the Object (Optional). </param>
<param name = "targetObjs"> The Array of Object to be Displayed. </param> */

public static void Print(ConsoleColor textColor, bool addLineBreak, string sourceFormat = default, params object[] targetObjs)
{
Console.ForegroundColor = textColor;

Print(addLineBreak, sourceFormat, targetObjs);

Console.ResetColor();
}

/** <summary> Prints an Advice on the Screen. </summary>

<param name = "addLineBreak"> A Boolean that Determines if a Linebreak should be Added after Printing. </param>
<param name = "adviceText"> The Text of the Advice. </param> */

public static void PrintAdvice(bool addLineBreak, string adviceText)
{
var textColor = (Console.ForegroundColor == ConsoleColor.Magenta) ? ConsoleColor.Cyan : ConsoleColor.Magenta;

Print(textColor, addLineBreak, "i {0}", adviceText);
}

/** <summary> Prints a Array of Objects on Screen. </summary>

<param name = "sourceArr"> The Array to Display. </param> */

public static void PrintArray(bool showIndex, bool separateElements, params object[] sourceArr)
{

if(sourceArr == null || sourceArr.Length == 0)
PrintLine(true, "<EmptyArray>");

else
{
string displayFormat = showIndex ? "[{0}] = {1}" : "{0},";
displayFormat = separateElements ? displayFormat + "\n" : displayFormat;

for(int i = 0; i < sourceArr.Length; i++)
{
string objValue = GetObjectValue(sourceArr[i], false, true);

if(showIndex)
Print(true, displayFormat, i, objValue);

else
Print(true, displayFormat, objValue);

if(!separateElements && i == sourceArr.Length - 1)
PrintLine();

}

}

}

/** <summary> Prints a Dialog on the Screen. </summary>

<param name = "addLineBreak"> A Boolean that Determines if a Linebreak should be Added after Printing. </param>
<param name = "dialogText"> The Text of the Dialog. </param> */

public static void PrintDialog(bool addLineBreak, string dialogText)
{
var textColor = (Console.ForegroundColor == ConsoleColor.Cyan) ? ConsoleColor.Magenta : ConsoleColor.Cyan;

Print(textColor, addLineBreak, "* {0}", dialogText);
}

/** <summary> Prints the Contents of a Dictionary. </summary>
<param name = "sourceDictionary"> The Dicitonary to be Printed. </param> */

public static void PrintDictionary<TKey, TValue>(Dictionary<TKey, TValue> sourceDictionary, bool separateElements)
{
const string dictionaryStyle = "<{0}, {1}>";

if(sourceDictionary == null || sourceDictionary.Count <= 0)
PrintLine(true, "<EmptyDictionary>");

else
{
string displayFormat = separateElements ? dictionaryStyle + "\n" : dictionaryStyle;

for(int i = 0; i < sourceDictionary.Count; i++)
{
var pair = sourceDictionary.ElementAt(i);

Print(true, displayFormat, pair.Key, pair.Value);

if(!separateElements && i == sourceDictionary.Count - 1)
PrintLine();

}

}

}

/** <summary> Prints an Error Message on the Screen. </summary>
<param name = "sourceMsg"> The Message to Display, indicating a Task was Failed. </param> */

public static void PrintErrorMsg(string sourceMsg)
{
var textColor = (Console.ForegroundColor == ConsoleColor.DarkRed) ? ConsoleColor.Blue : ConsoleColor.DarkRed;

PrintLine(textColor, true, "X {0}", sourceMsg);
}

/** <summary> Prints a Header on Screen. </summary>
<param name = "headerText"> The Header Text. </param> */

public static void PrintHeader(string headerText) => PrintLine(true, "<----------------------- {0} ----------------------->", headerText);

/** <summary> Prints a Sub-Header on Screen. </summary>
<param name = "subHeaderText"> The Sub-Header Text. </param> */

public static void PrintSubHeader(string subHeaderText) => PrintLine(true, "============ {0} ============", subHeaderText);

/** <summary> Prints the Contents of a JSON Object on Screen. </summary>
<param name = "sourceObj"> The JSON Object. </param> */

public static void PrintJson(JObject sourceObj, bool separateElements)
{
const string jsonStyle = "{0}: {1}";

if(sourceObj == null || sourceObj.Count <= 0)
PrintLine(true, "<EmptyJson>");

else
{
string displayFormat = separateElements ? jsonStyle + "\n" : jsonStyle;
int i = 0;

foreach(var jsonProperty in sourceObj)
{
i++;

Print(true, displayFormat, jsonProperty.Key, jsonProperty.Value);

if(!separateElements && i == sourceObj.Count - 1)
PrintLine();

}

}

}

/// <summary> Prints a new Line on Screen. </summary>

public static void PrintLine() => Console.WriteLine();

/** <summary> Prints a formatted Line of Text represented by the specified Objects, followed by a LineBreak. </summary>

<param name = "addLineBreak"> A Boolean that Determines if an extra LineBreak should be Added after Printing. </param>
<param name = "sourceFormat"> The Format of the Object (Optional). </param>
<param name = "targetObjs"> The Array of Objects to be Displayed. </param> */

public static void PrintLine(bool addLineBreak, string sourceFormat = default, params object[] targetObjs)
{
sourceFormat ??= GetDisplayFormat(targetObjs);

if(targetObjs == null)
return;

Console.WriteLine(sourceFormat, targetObjs);

if(addLineBreak)
PrintLine();

}

/** <summary> Prints a formatted Line of a Text represented by the specified Objects, followed by a LineBreak. </summary>

<param name = "textColor"> The Color of the Text displayed. </param>
<param name = "addLineBreak"> A Boolean that Determines if an extra LineBreak should be Added after Printing. </param>
<param name = "sourceFormat"> The Format of the Object (Optional). </param>
<param name = "targetObjs"> The Array of Objects to be Displayed. </param> */

public static void PrintLine(ConsoleColor textColor, bool addLineBreak, string sourceFormat = default, params object[] targetObjs)
{
Console.ForegroundColor = textColor;

PrintLine(addLineBreak, sourceFormat, targetObjs);

Console.ResetColor();
}

/** <summary> Adds a new Line after the last Element is Printed. </summary>

<param name = "elementIndex"> The Index of the Element Displayed. </param>
<param name = "elementsCount"> The Number of Elements. </param> */

public static void PrintLineAfterLastElement(int elementIndex, int elementsCount)
{

if(elementIndex == elementsCount - 1)
PrintLine();

}

/** <summary> Prints a Success Message on the Screen. </summary>
<param name = "sourceMsg"> The Message to Display, indicating a Task was Successfully completed. </param> */

public static void PrintSuccessMsg(string sourceMsg)
{
var textColor = (Console.ForegroundColor == ConsoleColor.Green) ? ConsoleColor.Blue : ConsoleColor.Green;

PrintLine(textColor, true, "OK {0}", sourceMsg);
}

/** <summary> Prints a Warning Message on the Screen. </summary>
<param name = "warningMsg"> The Warning Message. </param> */

public static void PrintWarning(string warningMsg)
{
var textColor = (Console.ForegroundColor == ConsoleColor.DarkYellow) ? ConsoleColor.DarkBlue : ConsoleColor.DarkYellow;

PrintLine(textColor, true, "! {0}", warningMsg);
}

}

}