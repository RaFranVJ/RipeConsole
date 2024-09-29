using System;

namespace RipeConsole.Program.Serializables.Config.KeyDefs
{
/// <summary> Represents the Keys for Program Termination. </summary>

public class TerminationKeyDefs : KeyDefinitions
{
/// <summary> Closes the Program when Pressed. </summary>

public ConsoleKey ExitKey{ get; set; }

/// <summary> Returns to the Main Menu when Pressed. </summary>

public ConsoleKey ReturnKey{ get; set; }

/// <summary> Creates a new Instance of the <c>KeyDefinitions</c>. </summary>

public TerminationKeyDefs()
{
ExitKey = ConsoleKey.X;
ReturnKey = ConsoleKey.R;
}

}

}