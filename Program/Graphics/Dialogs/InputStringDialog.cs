using System;

namespace RipeConsole.Program.Graphics.Dialogs
{
/// <summary> Asks the User to Enter a String. </summary>

internal partial class InputStringDialog : Dialog<string>
{
/// <summary> Creates a new Instance of the <c>InputStringDialog</c>. </summary>

public InputStringDialog()
{
headerText = "PARAM_INPUT_STRING";
adviceText = "ADVICE_ENTER_STRING";
}

// Get Input Str

protected string GetInputString(int minLength, int maxLength, string body = null, string adv = null)
{

if(!string.IsNullOrEmpty(body) )
Text.PrintLine(false, body);

var mRange = $"{maxLength} {Text.ProgramStrings.LocateByID("RANGE_ELEMENT_CHAR", false)}";
var rangeInfo = string.Format(Text.ProgramStrings.LocateByID("DIALOG_INPUT_RANGE"), minLength, mRange);

Text.PrintDialog(true, rangeInfo);
Text.PrintLine();

adv = !string.IsNullOrEmpty(adv) ? adv : Text.ProgramStrings.LocateByID(adviceText);
Text.PrintAdvice(false, adv);

string inputStr = Console.ReadLine();

while(string.IsNullOrEmpty(inputStr) || inputStr.Length < minLength || inputStr.Length > maxLength)
{
Text.PrintAdvice(false, Text.ProgramStrings.LocateByID("ADVICE_REDIRECT_INPUT") );

inputStr = Console.ReadLine();
}

return inputStr;
}

/** <summary> Displays the <c>InputStringDialog</c>. </summary>

<returns> The String entered by the User. </returns> */

public override string Popup()
{
Text.PrintHeader(Text.ProgramStrings.LocateByID(headerText) );

return GetInputString(1, Array.MaxLength);
}

/** <summary> Displays the <c>InputStringDialog</c>. </summary>

<param name = "inputRange"> The Range which user Input must follow. </param>

<returns> The String entered by the User. </returns> */

public override string Popup(Limit<int> inputRange)
{
Text.PrintHeader(Text.ProgramStrings.LocateByID(headerText) );

return GetInputString(inputRange.MinValue, inputRange.MaxValue);
}

/** <summary> Displays the <c>InputStringDialog</c> with the Specific Advice. </summary>

<param name = "sourceAdvice"> The Advice to Display on Screen. </param>
<param name = "sourceBody"> The Body to Display on Screen (Optional). </param>

<returns> The String entered by the User. </returns> */

public override string Popup(string sourceAdvice, string sourceBody = null)
{
Text.PrintHeader(Text.ProgramStrings.LocateByID(headerText) );

return GetInputString(1, Array.MaxLength, sourceBody, sourceAdvice);
}

/** <summary> Displays the <c>InputStringDialog</c> with the Specific Advice. </summary>

<param name = "sourceAdvice"> The Advice to Display on Screen. </param>
<param name = "sourceBody"> The Body to Display on Screen (Optional). </param>
<param name = "inputRange"> The Range which user Input must follow. </param>

<returns> The String entered by the User. </returns> */

public override string Popup(string sourceAdvice, Limit<int> inputRange, string sourceBody = null)
{
Text.PrintHeader(Text.ProgramStrings.LocateByID(headerText) );

return GetInputString(inputRange.MinValue, inputRange.MaxValue, sourceBody, sourceAdvice);
}

}

}