using System;
using System.Collections.Generic;
using System.Linq;
using ZCore.Serializables.SortUtils;

namespace RipeConsole.Program.Graphics.Menus
{
/// <summary> Allows the User to Sele. </summary>

internal partial class FunctionsMenu : Menu<Function, Function>
{
/** <summary> Gets or Sets the main Group related to the Default GroupCategory. </summary>

<returns> The main Group of the GroupCategory. </returns> */

private static FuncGroup mainGroup = new();

/// <summary> Creates a new Instance of the <c>FunctionsMenu</c>. </summary>

public FunctionsMenu()
{
headerText = mainGroup.GroupName;
adviceText = "ADVICE_SELECT_FUNCTION";
}

/** <summary> Displays the <c>FunctionsMenu</c>. </summary>

<returns> The Function selected by the User. </returns> */

public override Function DynamicSelection()
{
ActionWrapper<Function, int> displayAction = new( PrintAction );
ActionWrapper<Function, int> onSelectAction = new( SelectiveAction );

if(mainGroup.GetSubGroups() != null && mainGroup.GetSubGroups().Count > 0)
{
GroupsMenu groupsSelector = new();
groupsSelector.UpdateParentGroup(mainGroup);

mainGroup = groupsSelector.DynamicSelection();
}

Function selectedFunc = default;
ShowOptions(mainGroup.GetFunctions(), ref selectedFunc, displayAction.Init, onSelectAction.Init);

return selectedFunc;
}

/** <summary> Prints info related to the Function given as a Parameter (Name and ID). </summary>

<param name = "sourceItem"> The Function where the Info will be Obtained from. </param> */

public override void PrintAction(Function sourceItem, int index)
{
string displayName = Text.ProgramStrings.LocateByID(sourceItem.FuncName);

Text.Print(true, "{0}. {1}", sourceItem.FuncID, displayName);
}

/** <summary> Displays the Function selected by the User. </summary>

<param name = "selectedItem"> The Function selected by the User. </param>
<param name = "elementIndex"> The Index of the Function selected. </param> */

public override void SelectiveAction(Function selectedItem, int elementIndex)
{
Text.PrintAdvice(false, Text.ProgramStrings.LocateByID(adviceText) );

Text.Print(ConsoleColor.DarkYellow, true, selectedItem.FuncID.ToString() );
}

// Displays Info related to the Current Element

protected override void DisplayElementInfo(Function element, int index)
{
Text.PrintSubHeader("Func Info");

Text.PrintLine(false, "Index: {0}", index);
Text.PrintLine(false, "Name: {0} ({1})", Text.ProgramStrings.LocateByID(element.FuncName), element.FuncName);
Text.PrintLine(false, "ID: {0}", element.FuncID);

if(element.ArchivesFilter != null)
{
string n = Text.GetObjectValue(element.ArchivesFilter.FileNamesToSearch, false, false);
Text.PrintLine(false, "Allowed FileNames: {0}", n);

string ext = Text.GetObjectValue(element.ArchivesFilter.FileExtensionsToSearch, false, false);
Text.PrintLine(true, "Allowed FileExts: {0}", ext);
}

}

// Get Int from String

protected override int? TransformNumericInput(string input, List<Function> options)
{
int minValue = options.Min(a => a.FuncID);
int maxValue = options.Max(b => b.FuncID);

if(int.TryParse(input, out int numericValue) && numericValue >= minValue && numericValue <= maxValue)
{
int index = options.FindIndex(c => c.FuncID == numericValue);

return index == -1 ? null : index;
}

return null;
}

/** <summary> Updates the main Group used as a Reference on this Menu. </summary>

<param name = "sourceGroup"> The Group to be Set. </param> */

public void UpdateParentGroup(FuncGroup sourceGroup)
{
mainGroup = sourceGroup;
headerText = mainGroup.GroupName;
}

}

}