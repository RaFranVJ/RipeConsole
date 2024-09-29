using RipeConsole.Program.Graphics.Dialogs;
using RipeConsole.Program.Loaders.JavaScript;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace RipeConsole.Program
{
/// <summary> The Termination of this Program. </summary>

internal class Termination
{
/// <summary> Closes the Program, releasing all the memory Consumed by its Process. </summary>

public static async Task CloseProgram()
{
MemoryManager.ReleaseMemory(Process.GetCurrentProcess() );

var inputKeyInfo = new CloseProgramDialog().Popup();
var keyDefs = Program.GetAppConfig.MappedKeys.TerminationKeys;

if(inputKeyInfo.Key == keyDefs.ReturnKey)
await Program.Main( Array.Empty<string>() );

else if(inputKeyInfo.Key == keyDefs.ExitKey)
return;

else
{
string warningMsg = Text.ProgramStrings.LocateByID("WARNING_PRESS_EXIT_OR_RETURN_KEY");

int regresiveSeconds = 3;
Text.PrintWarning(string.Format(warningMsg, keyDefs.ReturnKey, keyDefs.ExitKey, regresiveSeconds) );

int exitDelay = Constants.MILLISECONDS * regresiveSeconds;
CancellationTokenSource cancelTokenSrc = new();

_ = StartShutdownTimer(exitDelay, cancelTokenSrc.Token);
bool returnToMainMenu = await WaitForReturnKey(exitDelay);

if(returnToMainMenu)
{
cancelTokenSrc.Cancel();

await Program.Main( Array.Empty<string>() );
}

ScriptsLoader.CloseEngine();
}

}

/** <summary> Gets the Elapsed Time of a Function that is Completed. </summary>

<param name = "sourceMs"> The Elapsed Time of a Function expressed in Milliseconds. </param>

<returns> The Elapsed Time of the Function. </returns> */

public static string GetElapsedTime(long sourceMs)
{
#region ====== Min Clock Criteria ======

long minSecondsValue = Constants.MILLISECONDS;
long minMinutesValue = minSecondsValue * Constants.SECONDS;
long minHoursValue = minMinutesValue * Constants.MINUTES;

#endregion


#region ====== Calculate Elapsed Time ======

long elapsedSeconds = sourceMs / Constants.MILLISECONDS;
long elapsedMinutes = elapsedSeconds / Constants.SECONDS;
long elapsedHours = elapsedMinutes / Constants.MINUTES;

#endregion


#region ====== Format Elapsed Time ======

string completionInMilliseconds = sourceMs.ToString("n0", EnvInfo.CurrentCulture);
string completionInSeconds = elapsedSeconds.ToString("n0", EnvInfo.CurrentCulture);
string completionInMinutes = elapsedMinutes.ToString("n0", EnvInfo.CurrentCulture);
string completionInHours = elapsedHours.ToString("n0", EnvInfo.CurrentCulture);

#endregion

string elapsedTime;

if(sourceMs >= minHoursValue)
elapsedTime = $"{completionInHours} h {completionInMinutes} min {completionInSeconds} s {completionInMilliseconds} ms";

else if(sourceMs >= minMinutesValue)
elapsedTime = $"{completionInMinutes} min {completionInSeconds} s {completionInMilliseconds} ms";

else if(sourceMs >= minSecondsValue)
elapsedTime = $"{completionInSeconds} s {completionInMilliseconds} ms";

else
elapsedTime = $"{completionInMilliseconds} ms";

return elapsedTime;
}

/** <summary> Starts a Countdown with the specified <c>CancellationToken</c> and Duration for Closing this Program. </summary>

<param name = "countdownDuration"> The duration of the Countdown, expressed in Milliseconds. </param>
<param name = "cancelToken"> Indicates if the shutdown Countdown should be Cancelled or not. </param>

<returns> A Task that Represents the Countdown made. </returns> */

private static async Task StartShutdownTimer(int countdownDuration, CancellationToken cancelToken)
{
int countdownSeconds = countdownDuration / Constants.MILLISECONDS;

for(int i = countdownSeconds; i >= 0; i--)
{

if(cancelToken.IsCancellationRequested)
return;

string countdownMsg = string.Format(Text.ProgramStrings.LocateByID("ACTION_CLOSE_PROGRAM"), Info.ProgramTitle, i);
Text.Print(false, "\r" + countdownMsg);

await Task.Delay(Constants.MILLISECONDS, cancelToken);
}

}

/** <summary> Waits the User for Entering the return Key in the specified Delay. </summary>

<param name = "exitDelay"> The delay before Closing the Program, expressed in Milliseconds. </param>

<returns> A boolean that Indicates if the User pressed the return Key or not. </returns> */

private static async Task<bool> WaitForReturnKey(int exitDelay)
{
DateTime startTime = DateTime.Now;

while(true)
{

if(Console.KeyAvailable)
{
ConsoleKey keyPressed = Console.ReadKey(true).Key;

if(keyPressed == Program.GetAppConfig.MappedKeys.TerminationKeys.ReturnKey)
return true;

if (keyPressed == Program.GetAppConfig.MappedKeys.TerminationKeys.ExitKey)
return false;

// Keep waiting otherwise
}

TimeSpan timeDifference = DateTime.Now - startTime;

if(timeDifference.TotalMilliseconds >= exitDelay)
return false;

await Task.Delay(100);
}

}

}

}