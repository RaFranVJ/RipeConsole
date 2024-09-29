using System;

namespace RipeConsole.Program.Serializables.Config.KeyDefs.MenuKeys
{
/// <summary> Represents the Key Definitions for Menus. </summary>

public class KeyDefsForMenus : KeyDefinitions
{
/// <summary> Advances one Position in the Menu when Pressed. </summary>

public ConsoleKey NavigationKey_Up{ get; set; }

/// <summary> Moves back one Position in the Menu when Pressed. </summary>

public ConsoleKey NavigationKey_Down{ get; set; }

/// <summary> Display Info related to the current Element of the Menu when Pressed. </summary>

public ConsoleKey InfoKey{ get; set; }

/// <summary> Selects the current Element of the Menu when Pressed. </summary>
	
public ConsoleKey SelectionKey{ get; set; }

/// <summary> Exits the Menu when Pressed. </summary>

public ConsoleKey OmissionKey{ get; set; }

/// <summary> Creates a new Instance of <c>KeyDefsForMenus</c>. </summary>

public KeyDefsForMenus()
{
NavigationKey_Up = ConsoleKey.UpArrow;
NavigationKey_Down = ConsoleKey.DownArrow;

InfoKey = ConsoleKey.I;
SelectionKey = ConsoleKey.Enter;

OmissionKey = ConsoleKey.Escape;
}

}

}