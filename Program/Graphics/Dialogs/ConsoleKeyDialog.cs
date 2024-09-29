using System;

namespace RipeConsole.Program.Graphics.Dialogs
{
/// <summary> Asks the User to Enter a Key through the Console. </summary>

internal partial class ConsoleKeyDialog : Dialog<ConsoleKeyInfo>
{
/// <summary> Creates a new Instance of the <c>CloseProgramDialog</c>. </summary>

public ConsoleKeyDialog()
{
headerText = "HEADER_CONSOLE_KEY";
adviceText = "ADVICE_PRESS_KEY";
}

// Get UserKey

protected ConsoleKeyInfo GetUserKey(string body = null, string adv = null)
{

if(!string.IsNullOrEmpty(headerText) )
Text.PrintHeader(Text.ProgramStrings.LocateByID(headerText) );

if(!string.IsNullOrEmpty(body) )
Text.PrintDialog(false, body);

else if(!string.IsNullOrEmpty(bodyText) )
Text.PrintDialog(false, Text.ProgramStrings.LocateByID(bodyText) );

if(!string.IsNullOrEmpty(adv) )
{

if(!string.IsNullOrEmpty(body) || !string.IsNullOrEmpty(bodyText) )
Text.PrintLine();

Text.PrintAdvice(false, adv);
}

else if(!string.IsNullOrEmpty(adviceText) )
{

if(!string.IsNullOrEmpty(body) || !string.IsNullOrEmpty(bodyText) )
Text.PrintLine();

Text.PrintAdvice(false, Text.ProgramStrings.LocateByID(adviceText) );
}


ConsoleKeyInfo inputKeyInfo = Console.ReadKey(true);

Text.PrintLine();
Interface.ShowKeyPressed(inputKeyInfo);

return inputKeyInfo;
}

/** <summary> Displays the <c>ConsoleKeyDialog</c>. </summary>
<returns> The Key Pressed by User. </returns> */

public override ConsoleKeyInfo Popup() => GetUserKey();

/** <summary> Displays the <c>ConsoleKeyDialog</c>. </summary>
<returns> The Key Pressed by User. </returns> */

public override ConsoleKeyInfo Popup(string sourceAdvice, string sourceBody = null)
{
return GetUserKey(sourceBody ?? Text.ProgramStrings.LocateByID(bodyText), sourceAdvice);
}

}

}