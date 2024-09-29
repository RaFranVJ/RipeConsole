using System;
using System.Collections.Generic;
using System.Reflection;
using RipeConsole.Program.Graphics.Dialogs;
using RipeConsole.Program.Graphics.UserSelections;
using RipeConsole.Program.Serializables.Config.KeyDefs.MenuKeys;

namespace RipeConsole.Program.Graphics.Menus
{
/// <summary> Allows the User to View Elements inside a List. </summary>

internal partial class ListViewer<T> : Menu<T, T>
{
/// <summary> The Base List where the Selection will be made </summary>

protected List<T> baseList;

/// <summary> Creates a new Instance of the <c>ListViewer</c>. </summary>

public ListViewer()
{
headerText = "HEADER_LIST_VIEWER";
adviceText = "ADVICE_CHOOSE_ELEMENT";

baseList = new();
}

/// <summary> Creates a new Instance of the <c>ListViewer</c>. </summary>

public ListViewer(List<T> list)
{
headerText = "HEADER_LIST_VIEWER";
adviceText = "ADVICE_CHOOSE_ELEMENT";

baseList = list;
}

/** <summary> Displays the <c>ListViewer</c>. </summary>
<returns> The Elements selected by the User. </returns> */

public override T DynamicSelection()
{
ActionWrapper<T, int> displayAction = new( PrintAction );
ActionWrapper<T, int> onSelectAction = new( SelectiveAction );

T selectedItem = default;
ShowOptions(baseList, ref selectedItem, displayAction.Init, onSelectAction.Init);

return selectedItem;
}

// Displays the Selection Dialog

protected override void SelectionDialog()
{
KeyDefsForListViewer defs = new();

string selectOptionMsg = Text.ProgramStrings.LocateByID("DIALOG_SELECT_ELEMENT_FROM_LIST");

Text.PrintDialog(true, string.Format(selectOptionMsg, defs.NavigationKey_Up, defs.NavigationKey_Down, defs.SelectionKey,
defs.OmissionKey, defs.AddElement, defs.RemoveElement) );
}

// Recursive Selection for List inside another

private static List<T> InnerSelection(List<T> list)
{
ListEditor<T> innerSelector = new();

innerSelector.UpdateList(list ?? new() );
innerSelector.DynamicSelection();

return innerSelector.GetBaseList();
}

// ReadElement for List

protected static T ReadElement(T element = default)
{
Type elementType = typeof(T);
object inputCast;

if(elementType == typeof(bool) )
inputCast = new LogicSelection().GetSelectionParam();

else if(elementType == typeof(string) )
inputCast = new InputStringDialog().Popup();

else if(elementType == typeof(byte[]) )
inputCast = new BufferDialog().Popup();

else if(elementType == typeof(DateTime) )
inputCast = new DateTimeSelection().GetSelectionParam();

else if(elementType.IsEnum)
{
Type selectorType = typeof(EnumSelector<>).MakeGenericType(elementType);

dynamic selector = Activator.CreateInstance(selectorType);

inputCast = selector.DynamicSelection();
}

else if(typeof(System.Collections.IEnumerable).IsAssignableFrom(elementType) )
inputCast = InnerSelection(element as List<T>);

else if(elementType.IsArray)
inputCast = InnerSelection(element as List<T>).ToArray();

else if(elementType.IsClass && elementType != typeof(string) )
{
ParamsMenu paramSelector = new();
paramSelector.UpdateParamInstance(element);

var selectedProperty = paramSelector.DynamicSelection() as PropertyInfo;
inputCast = selectedProperty.GetValue(default);
}

else
{
Type selectorType = typeof(GenericSelection<>).MakeGenericType(elementType);

dynamic selector = Activator.CreateInstance(selectorType);

inputCast = selector.GetGenericParam(); 
}

return (T)inputCast;
}

// Evaluates the Key Pressed by User

protected override void EvaluateKey(List<T> options, ConsoleKey key, ref int index, 
ref T value, ref string numberPressed, ref bool confirm)
{
KeyDefsForListViewer definitions = new();

int optionsCount = options.Count;

if(key == definitions.NavigationKey_Up)
index = (index - 1 + optionsCount) % optionsCount;

else if(key == definitions.NavigationKey_Down)
index = (index + 1) % optionsCount;

else if(key == definitions.InfoKey)
{
Text.PrintLine(true);

DisplayElementInfo(options[index], index);

_ = continueDialog.Popup();
}

else if(key == definitions.SelectionKey)
{
value = options[index];

confirm = true;
}

else if(key == definitions.OmissionKey)
{
Console.Clear();

confirm = true;
}

else if(key == definitions.AddElement)
options.Add(ReadElement() );

else if(key == definitions.RemoveElement)
{
options.RemoveAt(index);

if(index > 0)
index--;

confirm = index >= options.Count;
}

else if(key >= ConsoleKey.D0 && key <= ConsoleKey.D9)
{
numberPressed += (key - ConsoleKey.D0).ToString();

int? entry = TransformNumericInput(numberPressed, options);

if(entry.HasValue)
index = entry.Value;

else
numberPressed = string.Empty;

}

}

// Get Base List

public List<T> GetBaseList() => baseList;

/** <summary> Updates the List used as a Reference on this Menu. </summary>
<param name = "sourceList"> The List to be Set. </param> */

public void UpdateList(List<T> sourceList) => baseList = sourceList;
}

}