using System;
using System.Collections.Generic;
using System.Linq;
using RipeConsole.Program.Loaders.SortUtils;
using ZCore.Serializables.SortUtils;

namespace RipeConsole.Program.Graphics.Menus
{
/// <summary> Allows the User to Select a Group of Functions inside a Fr. </summary>

internal partial class GroupsMenu : Menu<FuncGroup, FuncGroup>
{
/** <summary> Gets or Sets the main GroupCategory where the Program will be working on. </summary>
<returns> The main GroupCategory of the Program. </returns> */

private static GroupCategory mainCategory = CategoriesLoader.AppCategories[0];

/** <summary> Gets or Sets the main GroupCategory where the Program will be working on. </summary>
<returns> The main GroupCategory of the Program. </returns> */

private static List<FuncGroup> subGroupsList;

/// <summary> Creates a new Instance of the <c>GroupsMenu</c>. </summary>

public GroupsMenu()
{
headerText = mainCategory.CategoryName;
adviceText = "ADVICE_SELECT_GROUP";
}

/** <summary> Displays the <c>GroupsMenu</c>. </summary>
<returns> The FuncGroup selected by the User. </returns> */

public override FuncGroup DynamicSelection()
{
FuncGroup selectedGroup = default;
List<FuncGroup> groupsToDisplay = null;

subGroupsList = null;

while(true)
{
ActionWrapper<FuncGroup, int> displayAction = new( PrintAction );
ActionWrapper<FuncGroup, int> onSelectAction = new( SelectiveAction );

groupsToDisplay = subGroupsList != null && subGroupsList.Count > 0 ? 
subGroupsList : mainCategory.GetGroups();
        
ShowOptions(groupsToDisplay, ref selectedGroup, displayAction.Init, onSelectAction.Init);

if(selectedGroup != null)
{
var subGroups = selectedGroup.GetSubGroups();

if(subGroups != null && subGroups.Count > 0)
UpdateParentGroup(selectedGroup);

else
break;

}

else
break;

}

return selectedGroup;
}

/** <summary> Prints info related to the Function given as a Parameter (Name and ID). </summary>
<param name = "sourceItem"> The Function where the Info will be Obtained from. </param> */

public override void PrintAction(FuncGroup sourceItem, int index) 
{
string displayName = Text.ProgramStrings.LocateByID(sourceItem.GroupName);

Text.Print(true, "{0}. {1}", sourceItem.GroupID, displayName);
}

/** <summary> Displays the Group selected by the User. </summary>

<param name = "selectedItem"> The Group selected by the User. </param>
<param name = "elementIndex"> The Index of the selected Group. </param> */

public override void SelectiveAction(FuncGroup selectedItem, int elementIndex)
{
Text.PrintAdvice(false, Text.ProgramStrings.LocateByID(adviceText) );

Text.Print(ConsoleColor.DarkYellow, true, selectedItem.GroupID.ToString() );
}

// Displays Info related to the Current Element

protected override void DisplayElementInfo(FuncGroup element, int index)
{
Text.PrintSubHeader("Group Info");

Text.PrintLine(false, "Index: {0}", index);
Text.PrintLine(false, "Name: {0} ({1})", Text.ProgramStrings.LocateByID(element.GroupName), element.GroupName);
Text.PrintLine(false, "ID: {0}", element.GroupID);

if(!string.IsNullOrEmpty(element.PathToSubGroupsDir) )
{
Text.PrintLine(false, "Sub-Groups Dir: {0}", element.PathToSubGroupsDir);
Text.PrintLine(true, "Sub-Groups Count: {0}", element.GetSubGroups().Count);
}

else
{
Text.PrintLine(false, "Funcs Dir: {0}", element.PathToFuncsDir);
Text.PrintLine(true, "Funcs Count: {0}", element.GetFunctions().Count);
}

}

// Get Int from String

protected override int? TransformNumericInput(string input, List<FuncGroup> options)
{
int minValue = options.Min(a => a.GroupID);
int maxValue = options.Max(b => b.GroupID);

if(int.TryParse(input, out int numericValue) && numericValue >= minValue && numericValue <= maxValue)
{
int index = options.FindIndex(c => c.GroupID == numericValue);

return index == -1 ? null : index;
}

return null;
}

/** <summary> Updates the main GroupCategory used as a Reference on this Menu. </summary>
<param name = "sourceCategory"> The GroupCategory to be Set. </param> */

public void UpdateMainCategory(GroupCategory sourceCategory)
{
mainCategory = sourceCategory;
headerText = mainCategory.CategoryName;
}

/** <summary> Updates the parent Group used as a Reference on this Menu. </summary>
<param name = "sourceGroup"> The Group to be Set. </param> */

public void UpdateParentGroup(FuncGroup sourceGroup)
{
subGroupsList = sourceGroup.GetSubGroups();
headerText = sourceGroup.GroupName;
}

}

}