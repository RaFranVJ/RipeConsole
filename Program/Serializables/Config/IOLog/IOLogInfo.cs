using System;
using System.IO;
using ZCore;
using ZCore.Serializables.Config;

namespace RipeConsole.Program.Serializables.Config.IOLog
{
/// <summary> Groups Info related to how the Data should be Received or Written. </summary>

public class IOLogInfo : ConfigField
{
/** <summary> Gets or Sets the Encoding of the Console Reader. </summary>
<returns> The Input Encoding. </returns> */

public string ReaderEncoding{ get; set; }

/** <summary> Gets or Sets the Encoding of the Console Writer. </summary>
<returns> The Output Encoding. </returns> */

public string WriterEncoding{ get; set; }

/** <summary> Gets or Sets a Option that determines how to Create the Logs. </summary>
<returns> The Log Mode. </returns> */

public ConsoleLogMode LogMode{ get; set; }

/** <summary> Gets or Sets a Path where the Logs will be Saved. </summary>
<returns> The Logs Path. </returns> */

public string PathForLogs{ get; set; }

/// <summary> Creates a new Instance of the <c>BufferDisplayInfo</c>. </summary>

public IOLogInfo()
{
ReaderEncoding = Console.InputEncoding.ToString();
WriterEncoding = Console.OutputEncoding.ToString();

PathForLogs = GetDefaultLogPath();
}

/// <summary> Checks each nullable Field of this Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
IOLogInfo defaultInfo = new();

ReaderEncoding ??= defaultInfo.ReaderEncoding;
WriterEncoding ??= defaultInfo.WriterEncoding;
PathForLogs ??= defaultInfo.PathForLogs;

PathHelper.CheckExistingPath(PathForLogs, LogMode != ConsoleLogMode.DisableLogs);
}

/** <summary> Gets the default Input Path basing on the CurrentAppDirectory. </summary>
<returns> The default Input Path. </returns> */

/** <summary> Gets the default Error Path basing on the CurrentAppDirectory. </summary>
<returns> The default Error Path. </returns> */

private static string GetDefaultLogPath() => EnvInfo.CurrentAppDirectory + Path.DirectorySeparatorChar + typeof(IOLogInfo).Name + ".log";
}

}