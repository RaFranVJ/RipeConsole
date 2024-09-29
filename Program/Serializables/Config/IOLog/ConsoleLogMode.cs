using System;

namespace RipeConsole.Program.Serializables.Config.IOLog
{
/// <summary> Determines how to Create Logs. </summary>

[Flags]

public enum ConsoleLogMode
{
/// <summary> No Logs will be Saved at the End of the Program Rutine. </summary>
DisableLogs = 0,

/// <summary> Only User Input will be Saved to Logs. </summary>
Input = 1,

/// <summary> Only Console Output will be Saved to Logs. </summary>
Output = 2,

/// <summary> Only Exception Traces will be Saved to Logs. </summary>
Errors = 4,

/// <summary> User Input and Console Output will be Saved to Logs. </summary>
InputAndOutput = Input | Output,

/// <summary> User Input and Exception Traces will be Saved to Logs. </summary>
InputAndErrors = Input | Errors,

/// <summary> Everything will be Saved to Logs. </summary>
All = Input | Output | Errors
}

}