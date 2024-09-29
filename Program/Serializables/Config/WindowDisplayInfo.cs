using System;
using ZCore.Serializables.Config;

namespace RipeConsole.Program.Serializables.Config
{
/// <summary> Groups Info related to the Size and Position of the Console Window. </summary>

public class WindowDisplayInfo : ConfigField
{
/** <summary> Gets or Sets the Width of the Window. </summary>
<returns> The Window Width. </returns> */

public int Width{ get; set; }

/** <summary> Gets or Sets the Leftmost Postion of the Window. </summary>

<remarks> Window area is Relative to the Screen Buffer. </remarks>

<returns> The Window Leftmost Position. </returns> */

public int Left{ get; set; }

/** <summary> Gets or Sets the Top Postion of the Window. </summary>

<remarks> Window area is Relative to the Screen Buffer. </remarks>

<returns> The Top Window Position. </returns> */

public int Top{ get; set; }

/** <summary> Gets or Sets the Height of the Window. </summary>
<returns> The Window Height. </returns> */

public int Height{ get; set; }

/// <summary> Creates a new Instance of the <c>WindowDisplayInfo</c>. </summary>

public WindowDisplayInfo()
{
Width = Console.WindowWidth;
Left = Console.WindowLeft;

Top = Console.WindowTop;
Height = Console.WindowHeight;
}

public override void CheckForNullFields()
{
Width = Width < Console.BufferWidth ? Console.BufferWidth : Width;
}

}

}