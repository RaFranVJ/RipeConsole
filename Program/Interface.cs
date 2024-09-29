using RipeConsole.Program.Graphics.Dialogs;
using System;
using ZCore;

namespace RipeConsole.Program
{
/// <summary> The Interface of this Program. </summary>

internal static class Interface
{
/// <summary> Displays all the Visual Elements of this Program. </summary>

public static void DisplayElements()
{
string consoleTitle;

if(Info.BuildConfig == "Debug")
consoleTitle = $"{Info.ProgramTitle} v{Info.ProgramVersion} ({Info.BuildConfig})";

else
consoleTitle = Info.ProgramTitle;

Console.Title = consoleTitle;
string welcomeMsg = Text.ProgramStrings.LocateByID("PROGRAM_WELCOME_MESSAGE");

Text.PrintHeader(string.Format(welcomeMsg, EnvInfo.UserName, Info.ProgramTitle) );

Text.PrintLine(true, Info.ProgramDescription);
_ = new ContinueDialog().Popup();
}

/** <summary> Shows the Key Pressed by the User. </summary>
<param name = "keyInfo"> The Info about the Key Pressed. </param> */

public static void ShowKeyPressed(ConsoleKeyInfo keyInfo)
{
Text.PrintLine();
Text.PrintAdvice(false, Text.ProgramStrings.LocateByID("ADVICE_KEYS_PRESSED") );

ConsoleModifiers keyModifier = keyInfo.Modifiers;
string modifierType;

if( (keyModifier & ConsoleModifiers.Alt) != 0)
{
modifierType = "Alt + ";
Text.Print(false, modifierType);
}

if( (keyModifier & ConsoleModifiers.Shift) != 0)
{
modifierType = "Shift + ";
Text.Print(false, modifierType);
}

if( (keyModifier & ConsoleModifiers.Control) != 0)
{
modifierType = "Ctrl + ";
Text.Print(false, modifierType);
}

Text.Print(false, keyInfo.Key.ToString() );
Text.PrintLine(false, "\n");
}

}

}