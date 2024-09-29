using RipeConsole.Program.Graphics.Menus;
using System.Collections.Generic;
using ZCore.Serializables.SortUtils;

namespace RipeConsole.Program.Loaders.SortUtils
{
/// <summary> Loades the Groups from a GroupCategory. </summary>

public static class GroupsLoader
{
/** <summary> Creates a new Instance of the <c>GroupsMenu</c>
<returns> The Groups Selector </returns> */

private static readonly GroupsMenu groupSelector = new();

// Exclude Groups from Dirs

public static void ExcludeGroups(FuncGroup sourceGroup)
{
FuncsLoader.ExcludeFuncs(sourceGroup);

List<FuncGroup> subGroups = sourceGroup.GetSubGroups();

if(subGroups != null && subGroups.Count > 0)
{
subGroups.ForEach( ExcludeGroups );

sourceGroup.SetSubGroups(subGroups);
}

}

// Exclude Groups from Files

public static void ExcludeGroups(FuncGroup sourceGroup, string targetExt)
{
FuncsLoader.ExcludeFuncs(sourceGroup, targetExt);

List<FuncGroup> subGroups = sourceGroup.GetSubGroups();

if(subGroups != null && subGroups.Count > 0)
{
subGroups.ForEach(subGroup => ExcludeGroups(subGroup, targetExt) );

sourceGroup.SetSubGroups(subGroups);
}

}

// Get FuncGroup from GroupCategory

public static FuncGroup GetGroupFromCategory(GroupCategory targetCategory)
{
groupSelector.UpdateMainCategory(targetCategory);

return groupSelector.DynamicSelection();
}

}

}