using System;

namespace RipeConsole.Program.Graphics.Dialogs
{
/// <summary> Ask the User to Enter a <b>BytesArray</b>. </summary>

internal partial class BufferDialog : Dialog<byte[]>
{
/// <summary> Creates a new Instance of the <c>BufferDialog</c>. </summary>

public BufferDialog()
{
headerText = "PARAM_BYTES_ARRAY";
adviceText = "ADVICE_ENTER_BYTES";
}

// Get Buffer

protected byte[] GetBuffer(int minLength, int maxLength, string body = null, string adv = null)
{

if(!string.IsNullOrEmpty(body) )
Text.PrintLine(false, body);

var mRange = $"{maxLength} {Text.ProgramStrings.LocateByID("RANGE_ELEMENT_BYTE", false)}";
var rangeInfo = string.Format(Text.ProgramStrings.LocateByID("DIALOG_INPUT_RANGE"), minLength, mRange);

Text.PrintDialog(true, rangeInfo);
Text.PrintLine();

adv = !string.IsNullOrEmpty(adv) ? adv : Text.ProgramStrings.LocateByID(adviceText);
Text.PrintAdvice(false, adv);

string inputStr = Console.ReadLine();
byte[] buffer = Console.InputEncoding.GetBytes(inputStr ?? string.Empty);

while(buffer == null || buffer.Length < minLength || buffer.Length > maxLength)
{
Text.PrintAdvice(false, Text.ProgramStrings.LocateByID("ADVICE_REDIRECT_INPUT") );

inputStr = Console.ReadLine();
buffer = Console.InputEncoding.GetBytes(inputStr ?? string.Empty);
}

return buffer;
}


/** <summary> Displays the <c>BufferDialog/c>. </summary>
<returns> The <b>Bytes</b> entered by User. </returns> */

public override byte[] Popup()
{
Text.PrintHeader(Text.ProgramStrings.LocateByID(headerText) );

return GetBuffer(1, Array.MaxLength);
}

/** <summary> Displays the <c>BufferDialog</c>. </summary>

<param name = "inputRange"> The Range which user Input must follow. </param>

<returns> The <b>Bytes</b> entered by User. </returns> */

public override byte[] Popup(Limit<int> inputRange)
{
Text.PrintHeader(Text.ProgramStrings.LocateByID(headerText) );

return GetBuffer(inputRange.MinValue, inputRange.MaxValue);
}

}

}