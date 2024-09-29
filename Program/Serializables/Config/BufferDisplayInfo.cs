using System;
using ZCore.Serializables.Config;

namespace RipeConsole.Program.Serializables.Config
{
/// <summary> Groups Info related to the Size of the Console Buffer. </summary>

public class BufferDisplayInfo : ConfigField
{
/** <summary> Gets or Sets the Width of the Console Buffer. </summary>
<returns> The Buffer Width. </returns> */

public int Width{ get; set; }

/** <summary> Gets or Sets the Height of the Console Buffer. </summary>
<returns> The Buffer Height. </returns> */

public int Height{ get; set; }

/// <summary> Creates a new Instance of the <c>BufferDisplayInfo</c>. </summary>

public BufferDisplayInfo()
{
Width = Console.BufferWidth;
Height = Console.BufferHeight;
}

}

}