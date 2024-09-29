using RipeConsole.Program.Serializables.Config;
using RipeConsole.Program.Serializables.Config.IOLog;
using System;
using ZCore.Serializables;
using ZCore.Serializables.Config.Fields;
using ZCore.Serializables.Config.Fields.JavaScript;

namespace RipeConsole.Program.Serializables
{
/// <summary> Initializes Saving and Loading Functions for the Settings of this Program. </summary>

public class Settings : SerializableClass<Settings>
{
/** <summary> Gets or Sets a String that is Asociated with user's Language. </summary>
<returns> The User Language. </returns> */

public string UserLanguage{ get; set; }

/** <summary> Gets or Sets the Display Info of the Console Window. </summary>
<returns> The Window Info. </returns> */

public WindowDisplayInfo WindowInfo{ get; set; }

/** <summary> Gets or Sets the Display Info of the Buffer in the Console App. </summary>
<returns> The Buffer Info. </returns> */

public BufferDisplayInfo BufferInfo{ get; set; }

/** <summary> Gets or Sets the Color Display Info for the Console App. </summary>
<returns> The Color Info. </returns> */

public ColorDisplayInfo ColorInfo{ get; set; }

/** <summary> Gets or Sets the Display Info for the Cursor in the Console App. </summary>
<returns> The Cursor Info. </returns> */

public CursorDisplayInfo CursorInfo{ get; set; }

/** <summary> Gets or Sets some Info related to User Alerts (Sounds on Advices). </summary>
<returns> The Alert Info. </returns> */

public UserAlertInfo AlertInfo{ get; set; }

/** <summary> Gets or Sets Info about how Data should be Read/Written. </summary>
<returns> The Log Info. </returns> */

public IOLogInfo LogInfo{ get; set; } // To implement

/** <summary> Gets or Sets some Info related to Task Scheduling. </summary>
<returns> The Task Info. </returns> */

public TaskScheduleInfo TaskScheduleConfig{ get; set; }

/** <summary> Gets or Sets some Config related to the JavaScript Engine. </summary>
<returns> The JSEngine Config </returns> */

public JSEngineInfo JSEngineConfig{ get; set; }

/** <summary> Gets or Sets some Info related to the Keys pressed in Specific Actions. </summary>
<returns> The Mapped Keys </returns> */

public MappedKeysInfo MappedKeys{ get; set; }

/// <summary> Creates a new Instance of the </Settings Class. </summary>

public Settings()
{
UserLanguage = "en-US";

WindowInfo = new();
BufferInfo = new();

ColorInfo = new();
CursorInfo = new();

AlertInfo = new();
LogInfo = new();

MappedKeys = new();
TaskScheduleConfig = new();

JSEngineConfig = new();
}

/** <summary> Checks each nullable Field of the Settings instance given and Validates it, in case it's <c>null</c>. </summary>
<param name = "sourceConfig"> The config to be Analized. </param> */

protected override void CheckForNullFields()
{
Settings defaultConfig = new();

#region ======== Set default Values to Null Fields ========

UserLanguage ??= defaultConfig.UserLanguage;
WindowInfo ??= defaultConfig.WindowInfo;
BufferInfo ??= defaultConfig.BufferInfo;
ColorInfo ??= defaultConfig.ColorInfo;
CursorInfo ??= defaultConfig.CursorInfo;
LogInfo ??= defaultConfig.LogInfo;
AlertInfo ??= defaultConfig.AlertInfo;
MappedKeys ??= defaultConfig.MappedKeys;
TaskScheduleConfig ??= defaultConfig.TaskScheduleConfig;
JSEngineConfig ??= defaultConfig.JSEngineConfig;

#endregion

WindowInfo.CheckForNullFields();
LogInfo.CheckForNullFields();
}

/** <summary> Loads the Settings of this Program. </summary>
<param name = "sourceConfig"> The config to be Writen. </param>*/

public void Load()
{
Console.WindowWidth = WindowInfo.Width;
Console.WindowHeight = WindowInfo.Height;

Console.BackgroundColor = ColorInfo.Background;
Console.ForegroundColor = ColorInfo.Foreground;

Console.CursorLeft = CursorInfo.Left;
Console.CursorTop = CursorInfo.Top;
Console.CursorVisible = CursorInfo.IsVisible;

Console.InputEncoding = EncodeHelper.GetEncodingType(LogInfo.ReaderEncoding);
Console.OutputEncoding = EncodeHelper.GetEncodingType(LogInfo.WriterEncoding);

#if WINDOWS || WINDOWSCONSOLE

Console.WindowLeft = WindowInfo.Left;
Console.WindowTop = WindowInfo.Top;

Console.BufferWidth = BufferInfo.Width;
Console.BufferHeight = BufferInfo.Height;

Console.CursorSize = CursorInfo.Size;

#endif
}

}

}