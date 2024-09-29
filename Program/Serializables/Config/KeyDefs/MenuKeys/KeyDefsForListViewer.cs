using System;

namespace RipeConsole.Program.Serializables.Config.KeyDefs.MenuKeys
{
/// <summary> Represents the Key Definitions for the List Viewer. </summary>

public class KeyDefsForListViewer : KeyDefsForMenus
{
/// <summary> Adds a new Element to the End of the List when Pressed. </summary>

public ConsoleKey AddElement{ get; set; }

/// <summary> Removes the Current Element when Pressed. </summary>

public ConsoleKey RemoveElement{ get; set; }

/// <summary> Creates a new Instance of <c>KeyDefsForListViewer/c>. </summary>

public KeyDefsForListViewer()
{
AddElement = ConsoleKey.A;
RemoveElement = ConsoleKey.R;
}

}

}