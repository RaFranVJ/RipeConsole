using System;

namespace RipeConsole.Program.Serializables.Config.KeyDefs.MenuKeys
{
/// <summary> Represents the Key Definitions for the Task Selector. </summary>

public class KeyDefsForTaskSelector : KeyDefsForListViewer
{
/// <summary> Starts the Current Task when Pressed. </summary>

public ConsoleKey StartTask{ get; set; }

/// <summary> Cancels the current Task  when Pressed. </summary>

public ConsoleKey CancelTask{ get; set; }

/// <summary> Executes all the Tasks in the List when Pressed. </summary>

public ConsoleKey ExecuteAll{ get; set; }

/// <summary> Cancels all the Tasks in the List when Pressed. </summary>

public ConsoleKey CancelAll{ get; set; }

/// <summary> Creates a new Instance of <c>KeyDefsForTaskSelector/c>. </summary>

public KeyDefsForTaskSelector()
{
StartTask = ConsoleKey.S;
CancelTask = ConsoleKey.C;

ExecuteAll = ConsoleKey.W;
CancelAll = ConsoleKey.K;
}

}

}