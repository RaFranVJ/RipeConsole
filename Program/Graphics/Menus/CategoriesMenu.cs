using System;
using System.Collections.Generic;
using System.Linq;
using ZCore.Serializables.SortUtils;

namespace RipeConsole.Program.Graphics.Menus
{
/// <summary> The Categories Menu of this Program. </summary>

internal partial class CategoriesMenu : Menu<GroupCategory, GroupCategory>
{
/// <summary> The Categories to Display on this Menu. </summary>

protected List<GroupCategory> categoriesList;

/// <summary> Creates a new Instance of the <c>CategoriesMenu</c>. </summary>

public CategoriesMenu()
{
headerText = "HEADER_FRAMEWORK_SELECTION";
adviceText = "ADVICE_SELECT_FRAMEWORK";
}

/** <summary> Displays the <c>CategoriesMenu</c>. </summary>

<returns> The GroupCategory selected by the User. </returns> */

public override GroupCategory DynamicSelection()
{
ActionWrapper<GroupCategory, int> displayAction = new( PrintAction );
ActionWrapper<GroupCategory, int> onSelectAction = new( SelectiveAction );

GroupCategory selectedCategory = default;
ShowOptions(categoriesList, ref selectedCategory, displayAction.Init, onSelectAction.Init);

return selectedCategory;
}

/** <summary> Prints info related to the GroupCategory given as a Parameter (Name and ID). </summary>

<param name = "sourceItem"> The GroupCategory where the Info will be Obtained from. </param> */

public override void PrintAction(GroupCategory sourceItem, int index)
{
string displayName = Text.ProgramStrings.LocateByID(sourceItem.CategoryName);

Text.Print(true, "{0}) {1}", sourceItem.CategoryID, displayName);
}

/** <summary> Displays the GroupCategory selected by the User. </summary>

<param name = "selectedItem"> The GroupCategory selected by the User. </param>
<param name = "elementIndex"> The Index of the GroupCategory selected. </param> */

public override void SelectiveAction(GroupCategory selectedItem, int elementIndex)
{
Text.PrintAdvice(false, Text.ProgramStrings.LocateByID(adviceText) );

Text.Print(ConsoleColor.DarkYellow, false, selectedItem.CategoryID.ToString() );
}

// Displays Info related to the Current Element

protected override void DisplayElementInfo(GroupCategory element, int index)
{
Text.PrintSubHeader("GroupCategory Info");

Text.PrintLine(false, "Index: {0}", index);
Text.PrintLine(false, "Name: {0} ({1})", Text.ProgramStrings.LocateByID(element.CategoryName), element.CategoryName);
Text.PrintLine(false, "ID: {0}", element.CategoryID);
Text.PrintLine(false, "Allow Files Input: {0}", element.AllowInputFromFiles);
Text.PrintLine(false, "Allow Dirs Input: {0}", element.AllowInputFromDirs);
Text.PrintLine(false, "Groups Dir: {0}", element.PathToGroupsDir);
Text.PrintLine(true, "Groups Count: {0}", element.GetGroups().Count);
}

// Get Int from String

protected override int? TransformNumericInput(string input, List<GroupCategory> options)
{
int minValue = options.Min(a => a.CategoryID);
int maxValue = options.Max(b => b.CategoryID);

if(int.TryParse(input, out int numericValue) && numericValue >= minValue && numericValue <= maxValue)
{
int index = options.FindIndex(c => c.CategoryID == numericValue);

return index == -1 ? null : index;
}

return null;
}

/** <summary> Updates the List of Categories from this Menu. </summary>
<param name = "sourceList"> The Categories to be Set. </param> */

public void UpdateCategories(List<GroupCategory> sourceList) => categoriesList = sourceList;
}

}