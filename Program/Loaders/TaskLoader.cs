using RipeConsole.Program.Loaders.JavaScript;
using RipeConsole.Program.Loaders.SortUtils;
using ZCore.Serializables.SortUtils;

namespace RipeConsole.Program.Loaders
{
/// <summary> Loads a Task from the ScriptsLoader. </summary>

public static class TaskLoader
{
/** <summary> Gets a Task corresponding to the Function selected by the User. </summary>

<param name = "sourceFunc"> The Function selected by User. </param>

<returns> The Task obtained from user Function. </returns> */

private static TaskInfo GetFuncTask(Function sourceFunc, string quickPath = default)
{
TaskInfo funcTask;

if(sourceFunc == null)
funcTask = new("<NoFunc>", () => Text.PrintWarning(Text.ProgramStrings.LocateByID("WARNING_NO_FUNCTION_SELECTED") ) );

else
{
void scriptAction() => ScriptsLoader.RunScript(sourceFunc, Program.GetAppConfig.AlertInfo, quickPath);

funcTask = new(Text.ProgramStrings.LocateByID(sourceFunc.FuncName, false), scriptAction);
}

return funcTask;
}

/** <summary> Gets a Task corresponding to the Group selected by the User. </summary>

<param name = "sourceGroup"> A Group where the Selection will be Made from. </param>

<returns> The Task obtained from the Category. </returns> */

private static TaskInfo GetGroupTask(FuncGroup sourceGroup, string quickPath = default)
{
TaskInfo groupTask;

if(sourceGroup == null)
groupTask = new("<NoGroup>", () => Text.PrintWarning(Text.ProgramStrings.LocateByID("WARNING_NO_GROUP_SELECTED") ) );

else
{
Function selectedFunc = FuncsLoader.GetFuncFromGroup(sourceGroup);

string groupName = Text.ProgramStrings.LocateByID(sourceGroup.GroupName, false);
var funcInfo = GetFuncTask(selectedFunc, quickPath);

groupTask = funcInfo;
groupTask.TaskName = string.Format("{0} => {1}", groupName, funcInfo.TaskName);
}

return groupTask;
}

/** <summary> Gets a Task corresponding to the Category selected by the User. </summary>

<param name = "sourceCategory"> The Category selected by User. </param>

<returns> The Task obtained from the Category. </returns> */

private static TaskInfo GetCategoryTask(GroupCategory sourceCategory, string quickPath = default)
{
TaskInfo categoryTask;

if(sourceCategory == null)
categoryTask = new("<NoCategory>", () => Text.PrintWarning(Text.ProgramStrings.LocateByID("WARNING_NO_FRAMEWORK_SELECTED") ) );


else
{
FuncGroup parentGroup = GroupsLoader.GetGroupFromCategory(sourceCategory);
categoryTask = GetGroupTask(parentGroup, quickPath);
}

return categoryTask;
}

/** <summary> Gets a Task corresponding to User selection. </summary>
<returns> The Task obtained from Use selection. </returns> */

private static TaskInfo GetUserTask()
{
GroupCategory targetCategory = CategoriesLoader.GetSelectedCategory();

var categoryTask = GetCategoryTask(targetCategory);

return categoryTask;
}

/** <summary> Loads the Functions of this Program when its being Launched on its Normal Mode. </summary>
<returns> A Task that Represents the Process attached to the Function that was Selected. </returns> */

public static TaskInfo NormalLoad() => GetUserTask();

/** <summary> Loads the Functions of this Program when its being Launched on its Quick Mode. </summary>

<param name = "sourcePath"> The Path of the File/Folder dragged to the Program. </param>

<returns> A Task that Represents the Process attached to the Function that was Selected. </returns> */

public static TaskInfo QuickLoad(string sourcePath)
{
var quickCategorys = CategoriesLoader.FilterCategories(sourcePath);

GroupCategory targetCategory = CategoriesLoader.GetSelectedCategory(quickCategorys);

return GetCategoryTask(targetCategory, sourcePath);
}

}

}