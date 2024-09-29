using System;
using ZCore.Serializables.Config;

namespace RipeConsole.Program.Serializables.Config
{
/// <summary> Groups Info related to the Display Colors for the Console. </summary>

public class ColorDisplayInfo : ConfigField
{
/** <summary> Gets or Sets the Background Color. </summary>
<returns> The Background Color. </returns> */

public ConsoleColor Background{ get; set; }

/** <summary> Gets or Sets the Foreground Color. </summary>
<returns> The Foreground Color. </returns> */

public ConsoleColor Foreground{ get; set; }

/// <summary> Creates a new Instance of the <c>ColorDisplayInfo</c>. </summary>

public ColorDisplayInfo()
{
Background = Console.BackgroundColor;
Foreground = Console.ForegroundColor;
}

}

}