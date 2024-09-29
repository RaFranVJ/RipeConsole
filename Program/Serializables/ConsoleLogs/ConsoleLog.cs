using System;

namespace RipeConsole.Program.Serializables.ConsoleLogs
{
/// <summary> Logs the Data Received from the Console to Files. </summary>

public class ConsoleLog : SerializableClass<ConsoleLog>
{
/** <summary> Gets or Sets the Creation Date of the Console Log. </summary>
<returns> The Creation Date. </returns> */

public DateTime CreationDate{ get; private set; }

/** <summary> Gets or Sets the Type of Log expected. </summary>
<returns> The Log Type. </returns> */

public ConsoleLogType LogType{ get; protected set; }

/** <summary> Gets or Sets the Data Received from Console. </summary>
<returns> The Console Data. </returns> */

public string Data{ get; private set; }

/// <summary> Creates a new Instance of the <c>ConsoleLog</c>. </summary>

public ConsoleLog()
{
CreationDate = DateTime.Now;
LogType = ConsoleLogType.Input;

Data = Text.ProgramStrings.LocateByID("DIALOG_NO_DATA_AVAILABLE");
}

/// <summary> Creates a new Instance of the <c>ConsoleLog</c>. </summary>

public ConsoleLog(ConsoleLogType type)
{
CreationDate = DateTime.Now;
LogType = type;

CaptureData();
}

/// <summary> Captures the Data Stored int the Console Streams. </summary> 

public void CaptureData()
{
// Set Log Data

Data = LogType switch
{
ConsoleLogType.Output => Console.Out.ToString(),
ConsoleLogType.Errors => Console.Error.ToString(),
_ => Console.In.ReadToEnd(),
};

}

}

}