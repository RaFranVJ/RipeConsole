using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RipeConsole.Program.Loaders;
using RipeConsole.Program.Serializables.Config.KeyDefs.MenuKeys;

namespace RipeConsole.Program.Graphics.Menus
{
/// <summary> Allows the User to Select a Task from a List of Tasks. </summary>

internal partial class TaskSelector : ListViewer<TaskInfo>
{
/// <summary> Creates a new Instance of the <c>TaskSelector</c>. </summary>

public TaskSelector()
{
headerText = "HEADER_TASK_SELECTOR";
adviceText = "ADVICE_CHOOSE_TASK";

baseList = new();
}

/** <summary> Displays the <c>LanguageMenu</c>. </summary>
<returns> The Language selected by the User. </returns> */

public override TaskInfo DynamicSelection()
{
ActionWrapper<TaskInfo, int> displayAction = new( PrintAction );
ActionWrapper<TaskInfo, int> onSelectAction = new( SelectiveAction );

TaskInfo selectedTask = default;
ShowOptions(baseList, ref selectedTask, displayAction.Init, onSelectAction.Init);

return selectedTask;
}

/** <summary> Prints info related to the LocStrings given as a Parameter (NativeName, DisplayName and Culture). </summary>
<param name = "sourceItem"> The LocStrings where the Info will be Obtained from. </param> */

public override void PrintAction(TaskInfo sourceItem, int index)
{

if(sourceItem.TaskContext.Status == TaskStatus.RanToCompletion || sourceItem.TaskContext.Status == TaskStatus.Canceled ||
sourceItem.TaskContext.Status == TaskStatus.Faulted)
{
sourceItem.StopTimer();

Text.Print(true, "{0} - {1} (Status: {2}) [ {3} elapsed ]", index + 1, sourceItem.TaskName, 
sourceItem.TaskContext.Status, sourceItem.GetElapsedTime() );
}

else 
Text.Print(true, "{0} - {1} (Status: {2})", index + 1, sourceItem.TaskName, sourceItem.TaskContext.Status);

}

/** <summary> Displays the LocStrings selected by the User. </summary>

<param name = "selectedItem"> The Strings selected by the User. </param>
<param name = "elementIndex"> The Index of the selected Strings. </param> */

public override void SelectiveAction(TaskInfo selectedItem, int elementIndex)
{
Text.PrintAdvice(false, Text.ProgramStrings.LocateByID(adviceText) );

Text.Print(ConsoleColor.DarkYellow, true, selectedItem.TaskName);
}

// Displays Info related to the Current Element

protected override void DisplayElementInfo(TaskInfo element, int index)
{
Text.PrintSubHeader("Task Info");

Text.PrintLine(false, "Index: {0}", index);
Text.PrintLine(false, "Name: {0}", element.TaskName);
Text.PrintLine(false, "ID: {0}", element.TaskContext.Id); 
Text.PrintLine(false, "Status: {0}", element.TaskContext.Status); // Add Loc Strs (Short Variation)
Text.PrintLine(false, "Is Completed: {0}", element.TaskContext.IsCompleted);
Text.PrintLine(false, "Is Completed Successfully: {0}", element.TaskContext.IsCompletedSuccessfully);
Text.PrintLine(false, "Is Faulted: {0}", element.TaskContext.IsFaulted);
Text.PrintLine(false, "Is Cancelled: {0}", element.TaskContext.IsCanceled);

if(element.TaskContext.Status == TaskStatus.RanToCompletion || element.TaskContext.Status == TaskStatus.Canceled ||
element.TaskContext.Status == TaskStatus.Faulted)
Text.PrintLine(false, "Elapsed Time: {0}", element.GetElapsedTime() );

Text.PrintLine();
}

// Displays the Selection Dialog

protected override void SelectionDialog()
{
KeyDefsForTaskSelector defs = new();

string selectOptionMsg = Text.ProgramStrings.LocateByID("DIALOG_SELECT_ELEMENT_FROM_LIST3");

Text.PrintDialog(true, string.Format(selectOptionMsg, defs.NavigationKey_Up, defs.NavigationKey_Down, defs.SelectionKey,
defs.OmissionKey, defs.AddElement, defs.RemoveElement, defs.StartTask, defs.CancelTask, defs.ExecuteAll, defs.CancelAll) );
}

// Evaluates the Key Pressed by User

protected override void EvaluateKey(List<TaskInfo> options, ConsoleKey key, ref int index,
ref TaskInfo value, ref string numberPressed, ref bool confirm)
{
KeyDefsForTaskSelector definitions = new();

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
{
TaskInfo task = TaskLoader.NormalLoad();

AddTask(options, task);
}

else if(key == definitions.RemoveElement)
options.RemoveAt(index);

else if(key == definitions.StartTask)
options[index].StartTask();

else if(key == definitions.CancelTask)
options[index].CancelTask();

else if(key == definitions.ExecuteAll)
{
var filteredTasks = FilterTasksByStatus(TaskStatus.Created);

if(filteredTasks == null || filteredTasks.Count <= 0)
{
Text.PrintErrorMsg("There are no Tasks to Execute.\n" +
$"Tasks must be {TaskStatus.Created} but not been Scheluded yet before Running.");

_ = continueDialog.Popup();
return;
}

ExecuteAllTasks(filteredTasks);
}

else if(key == definitions.CancelAll)
{
var filteredTasks = FilterTasksByStatus(TaskStatus.Running | TaskStatus.WaitingForActivation);

if(filteredTasks == null || filteredTasks.Count <= 0)
{
Text.PrintErrorMsg("There are no Tasks to Cancel.\n"
+ $"Tasks must be {TaskStatus.Running} or {TaskStatus.WaitingForActivation} before being Cancelled.");

_ = continueDialog.Popup();
return;
}

CancelAllTasks(filteredTasks);
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

private static void TaskLimitDialog(int maxCount)
{
Text.PrintErrorMsg($"Number of Task exceded (Limit is {maxCount}).\n" +
"Make sure to Remove the Tasks you don't need before Adding new ones\n\n" +

"You can also set the Task Limit to '-1' (or Lower) in order to avoid this Limitations\n\n" +

"(Note that an excesive Number of Tasks running simultaniously might affect your PC performance)");

_ = continueDialog.Popup();
}

// Add new Task to the List

public void AddTask(TaskInfo task)
{
int maxCount = Program.GetAppConfig.TaskScheduleConfig.MaxTasksCount;

if(baseList.Count >= maxCount && maxCount > 0)
{
TaskLimitDialog(maxCount);

return;
}

baseList.Add(task);
}

// Add new Task to the List

public static void AddTask(List<TaskInfo> tasksList, TaskInfo task)
{
int maxCount = Program.GetAppConfig.TaskScheduleConfig.MaxTasksCount;

if(tasksList.Count >= maxCount && maxCount > 0)
{
TaskLimitDialog(maxCount);

return;
}

tasksList.Add(task);
}

// Executes all Tasks in the List

public static void ExecuteAllTasks(List<TaskInfo> tasks) => tasks.ForEach(t => t.StartTask() );

// Cancels all Tasks in the List

public static void CancelAllTasks(List<TaskInfo> tasks) => tasks.ForEach(t => t.CancelTask() );

// Filter Tasks by matching Status

public List<TaskInfo> FilterTasksByStatus(TaskStatus status) => baseList.Where(i => i.TaskContext.Status == status).ToList();

// Exclude Tasks that match the Specified Status

public List<TaskInfo> ExcludeTasksByStatus(TaskStatus status) => baseList.Where(i => i.TaskContext.Status != status).ToList();

public TaskInfo GetTaskByIndex(int index) => baseList[index];
}

}