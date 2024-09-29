using RipeConsole.Program.Graphics.Dialogs;
using RipeConsole.Program.Graphics.Menus;
using RipeConsole.Program.Loaders;
using RipeConsole.Program.Serializables;
using System;
using System.Threading.Tasks;
using ZCore;
using ZCore.Serializables;

namespace RipeConsole.Program
{	
/// <summary> Initializes Launching Functions for this Program. </summary>

public static class Program
{
/// <summary> The Settings of this Program. </summary>

private static Settings config = new();

/// <summary> The Params set by User. </summary>

private static UserParams userParams = new();

/// <summary> The Path Passed by the User. </summary>

private static string quickPath;

/// <summary> Obtains a Reference of the Current App Config. </summary>

public static Settings GetAppConfig => config;

/// <summary> Obtains a Reference to the User Params. </summary>

public static UserParams GetUserParams => userParams;

/// <summary> Obtains a Reference to the Path Passed by User. </summary>

public static string GetQuickPath => quickPath;

/** <summary> Updates the App Config. </summary>
<param name = "newConfig"> The new Settings. </param> */

public static void SetAppConfig(Settings newConfig) => config = newConfig;

/** <summary> Updates the User Params. </summary>
<param name = "newParams"> The new Params. </param> */

public static void SetUserParams(UserParams newParams) => userParams = newParams;

/** <summary> Displays the Arguments passed to the Program. </summary>
<param name = "sourceArgs"> The Arguments to Display. </param> */

private static void DisplayArguments(string[] sourceArgs)
{
Text.PrintSubHeader("Program Arguments");

Text.PrintArray(false, true, sourceArgs);
}

/// <summary> Setups the Program's Environment (Settings and Params). </summary>

private static void SetupEnv()
{
// Read Params

config = config.ReadObject();
userParams = userParams.ReadObject();

// Load Config

config.Load();
ImageHelper.RegistPlatform();

Console.Clear();

Interface.DisplayElements();
}

/** <summary> Launches the Program on User's Enviroment. </summary>

<param name = "inputArgs"> The Arguments to be Passed to the Program in the Execution. </param>

<returns> A Task representing the Code Executed. </returns> */

public static async Task<int> Main(string[] inputArgs)
{
TaskSelector taskSelector = new();

TaskInfo currentAppTask = default;
int exitCode = 0;

try
{
Console.Clear();

SetupEnv();

if(Info.BuildConfig == "Debug" && inputArgs.Length > 0)
DisplayArguments(inputArgs);

if(inputArgs.Length == 1)
{
new QuickModeDialog().Popup();

quickPath = inputArgs[0];

taskSelector.AddTask( TaskLoader.QuickLoad(quickPath) );
}

else
taskSelector.AddTask( TaskLoader.NormalLoad() );

currentAppTask = config.TaskScheduleConfig.AllowMultiTasking ?
taskSelector.DynamicSelection() :
taskSelector.GetTaskByIndex(0);

if(currentAppTask.TaskContext.Status != TaskStatus.Created)
currentAppTask.StopTimer();

else
currentAppTask.StartTask();

}

catch(Exception error)
{
ErrorsHandler.DisplayErrorInfo(error);

exitCode = 1;
}

finally
{
Text.PrintLine();

if(currentAppTask != null)
{
await currentAppTask.TaskContext;
currentAppTask.StopTimer();

TaskUtils.EvaluateTaskStatus(currentAppTask.TaskContext);
Text.PrintLine(true, Text.ProgramStrings.LocateByID("PROCESS_ELAPSED_TIME"), currentAppTask.GetElapsedTime() );
}

}

await Termination.CloseProgram();

return exitCode;
}

}

}