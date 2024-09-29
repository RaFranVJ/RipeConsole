using System;

namespace RipeConsole.Program.Serializables.Config.KeyDefs.MenuKeys
{
/// <summary> Represents the Key Definitions for the List Editor. </summary>

public class KeyDefsForListEditor : KeyDefsForListViewer
{
/// <summary> Clones the Current Element when Pressed. </summary>

public ConsoleKey CloneElement{ get; set; }

/// <summary> Edits the Value of the Current Element when Pressed. </summary>

public ConsoleKey EditElement{ get; set; }

/// <summary> Creates a new Instance of <c>KeyDefsForListEditor</c>. </summary>

public KeyDefsForListEditor()
{
CloneElement = ConsoleKey.C;
EditElement = ConsoleKey.E;
}

}

}