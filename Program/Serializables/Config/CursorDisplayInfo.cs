using System;
using ZCore.Serializables.Config;

namespace RipeConsole.Program.Serializables.Config
{
/// <summary> Groups Info related to the Size and Position of the Console Window. </summary>

public class CursorDisplayInfo : ConfigField
{
/** <summary> Gets or Sets the Leftmost Postion of the Cursor. </summary>
<returns> The Leftmost Cursor Position. </returns> */

public int Left{ get; set; }

/** <summary> Gets or Sets the Size of the Cursor. </summary>
<returns> The Cursor Size. </returns> */

public int Size{ get; set; }

/** <summary> Gets or Sets the Top Postion of the Cursor. </summary>
<returns> The Top Cursor Position. </returns> */

public int Top{ get; set; }

/** <summary> Gets or Sets a Option that determines if the Cursor is visible or not. </summary>
<returns> <b>true</b> if Cursor should be Displayed on Screen; otherwise, <b>false</b>. </returns> */

public bool IsVisible{ get; set; }

/// <summary> Creates a new Instance of the <c>CursorDisplayInfo</c>. </summary>

public CursorDisplayInfo()
{
Left = Console.CursorLeft;
Size = Console.CursorSize;

Top = Console.CursorTop;
IsVisible = Console.CursorVisible;
}

}

}