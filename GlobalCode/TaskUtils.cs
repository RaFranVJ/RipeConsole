using System.Threading.Tasks;
using RipeConsole.Program;

/// <summary> Handles the Display of Tasks. </summary>

public static class TaskUtils
{
/** <summary> Gets the Completion Message of a Task, by Verifing if there are Errors or not. </summary>

<param name = "sourceTask"> The Task to be Evaluated. </param>

<returns> The Completion Message. </returns> */

private static string GetTaskCompletionMsg(Task sourceTask)
{
var errorsCount  = sourceTask.Exception?.InnerExceptions.Count;

if(errorsCount > 0)
{
string faultedCompletionMsg = Text.ProgramStrings.LocateByID("TASK_STATUS_COMPLETED_WITH_ERRORS");

return string.Format(faultedCompletionMsg, errorsCount);
}

return Text.ProgramStrings.LocateByID("TASK_STATUS_COMPLETED_SUCCESSFUL");
}

/** <summary> Evaluates and Displays the Status of a Task. </summary>

<param name = "sourceTask"> The Task to be Evaluated. </param>

<returns> The Task Status. </returns> */

public static TaskStatus EvaluateTaskStatus(Task sourceTask)
{
TaskStatus taskStatus = sourceTask.Status;

string statusMsg = taskStatus switch
{
TaskStatus.Created => Text.ProgramStrings.LocateByID("TASK_STATUS_CREATED"),
TaskStatus.WaitingForActivation => Text.ProgramStrings.LocateByID("TASK_STATUS_WAITING_FOR_ACTIVATION"),
TaskStatus.WaitingToRun => Text.ProgramStrings.LocateByID("TASK_STATUS_WAITING_TO_EXECUTION"),
TaskStatus.Running => Text.ProgramStrings.LocateByID("TASK_STATUS_RUNNING"),
TaskStatus.WaitingForChildrenToComplete => Text.ProgramStrings.LocateByID("TASK_STATUS_WAITING_CHILDREN_COMPLETION"),
TaskStatus.RanToCompletion => GetTaskCompletionMsg(sourceTask),
_ => Text.ProgramStrings.LocateByID("TASK_STATUS_FAILED"),
};

Text.PrintLine(false, "\n" + statusMsg);

return taskStatus;
}

}