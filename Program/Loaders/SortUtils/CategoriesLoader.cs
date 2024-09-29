using RipeConsole.Program.Graphics.Menus;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using ZCore.Serializables.SortUtils;
using ZCore.Modules;
using ZCore;

namespace RipeConsole.Program.Loaders.SortUtils
{
/// <summary> Loades the Categories of this Program. </summary>

public static class CategoriesLoader
{
/** <summary> Creates a new Instance of the <c>CategoriesMenu</c> </summary>
<returns> The Categories Selector </returns> */

private static readonly GroupCategory baseCategory = new();

/** <summary> Creates a new Instance of the <c>CategoriesMenu</c> </summary>
<returns> The Categories Selector </returns> */

private static readonly CategoriesMenu categoriesSelector = new();

// Sort Groups

private static void InitGroup(FuncGroup gp)
{

if(!string.IsNullOrEmpty(gp.PathToSubGroupsDir) )
{
gp.PathToSubGroupsDir = PathUtils.AlignPathWithAppDir(gp.PathToSubGroupsDir);
gp.LoadSubGroups();

var sortedSubGroups = gp.GetSubGroups().OrderBy(sub => sub.GroupID).ToList();

foreach(var sub in sortedSubGroups)
InitGroup(sub);

gp.SetSubGroups(sortedSubGroups);
}

else
{
gp.PathToFuncsDir = PathUtils.AlignPathWithAppDir(gp.PathToFuncsDir);
gp.LoadFunctions();

var sortedFuncs = gp.GetFunctions().OrderBy(f => f.FuncID).ToList();

gp.SetFunctions(sortedFuncs);
}

}

// Get Categories

private static List<GroupCategory> GetAppCategories()
{
List<GroupCategory> Categories = baseCategory.ReadObjects().OrderBy(fw => fw.CategoryID).ToList();

foreach(GroupCategory fw in Categories)
{
fw.PathToGroupsDir = PathUtils.AlignPathWithAppDir(fw.PathToGroupsDir);
fw.LoadGroups();

var sortedGroups = fw.GetGroups().OrderBy(gp => gp.GroupID).ToList();

foreach(FuncGroup gp in sortedGroups)
InitGroup(gp);

fw.SetGroups(sortedGroups);
}

return Categories;
}

/** <summary> Gets a List of Categories from this Application. </summary>
<returns> The App Categories. </returns> */

public static List<GroupCategory> AppCategories = GetAppCategories();

// Filter for Files

private static bool ArchivesFilter(GroupCategory category) => category.AllowInputFromFiles;

/** <summary> Gets a List of Categories related to File Operations. </summary>

<param name = "sourcePath"> The Path to the File that was Dragged to the Program. </param>

<returns> The File Categories. </returns> */

private static List<GroupCategory> GetCategoriesForFiles(string sourcePath)
{
var fileCategories = AppCategories.Where(ArchivesFilter).ToList();
string targetPath = FileManager.ChangePath(sourcePath, sourcePath);

foreach(GroupCategory fw in fileCategories)
{
List<FuncGroup> fGroups = fw.GetGroups();

fGroups.ForEach(gp => GroupsLoader.ExcludeGroups(gp, Path.GetExtension(sourcePath) ) );
fw.SetGroups(fGroups);

}

return fileCategories;
}

// Filter for Folders

private static bool DirsFilter(GroupCategory category) => category.AllowInputFromDirs;

/** <summary> Gets a List of Categories related to Folder Operations. </summary>

<param name = "sourcePath"> The Path to the Folder that was Dragged to the Program. </param>

<returns> The Dir Categories. </returns> */

private static List<GroupCategory> GetCategoriesForDirs(string sourcePath)
{
var dirCategories = AppCategories.Where(DirsFilter).ToList();
string targetPath = DirManager.ChangePath(sourcePath, sourcePath);

foreach(GroupCategory fw in dirCategories)
{
List<FuncGroup> fGroups = fw.GetGroups();
fGroups.ForEach( GroupsLoader.ExcludeGroups );

fw.SetGroups(fGroups);
}

return dirCategories;
}

/** <summary> Filters the List of Categories available for the given Path. </summary>

<param name = "sourcePath"> The Path to the File/Folder that was Dragged to the Program. </param>

<returns> The Filtered Categories. </returns> */

public static List<GroupCategory> FilterCategories(string sourcePath)
{
FileAttributes pathType = PathHelper.CheckPathType(sourcePath);
List<GroupCategory> filteredCategories;

if(pathType == FileAttributes.Archive)
filteredCategories = GetCategoriesForFiles(sourcePath);

else
filteredCategories = GetCategoriesForDirs(sourcePath);

return filteredCategories;
}

// Get Selected GroupCategory

public static GroupCategory GetSelectedCategory(List<GroupCategory> sourceList = null)
{
sourceList ??= AppCategories;

categoriesSelector.UpdateCategories(sourceList);

return categoriesSelector.DynamicSelection();
}

}

}