using System;

namespace RipeConsole.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a Boolean (true or false). </summary>

internal partial class LogicSelection : UserSelection<bool>
{
/// <summary> Creates a new Instance of the <c>LogicSelection</c>. </summary>

public LogicSelection()
{
adviceText = Text.ProgramStrings.LocateByID("ADVICE_CHOOSE_VALUE");
}

// Get User Selection

private bool GetUserSelection(string header, string adv = null)
{
header ??= typeof(bool).Name;

Text.PrintHeader(header);

char userSelection_Yes = char.ToUpper(Text.ProgramStrings.LocateByID("USER_SELECTION_BOOLEAN_TRUE")[0] );
char userSelection_No = char.ToUpper(Text.ProgramStrings.LocateByID("USER_SELECTION_BOOLEAN_FALSE")[0] );

string trueOption = $"true ({userSelection_Yes})";
string falseOption = $"false ({userSelection_No})";

var rInfo = string.Format(Text.ProgramStrings.LocateByID("DIALOG_INPUT_RANGE"), trueOption, falseOption);

Text.PrintDialog(true, rInfo);
Text.PrintLine();

adv = !string.IsNullOrEmpty(adv) ? adv : Text.ProgramStrings.LocateByID(adviceText);
char userKey;

// Read Key until is Y or N

do
{
Text.PrintAdvice(false, adv);

userKey = char.ToUpper(Console.ReadKey().KeyChar);

Text.PrintLine();
}

while(userKey != userSelection_Yes && userKey != userSelection_No);

return userKey == userSelection_Yes;
}

/** <summary> Displays the <c>LogicSelection</c> with the Specific Advice. </summary>

<param name = "sourceAdvice"> The Advice to Display on Screen. </param>
<param name = "sourceHeader"> The Header to be Displayed on Screen (Optional). </param>

<returns> The Boolean selected by User. </returns> */

public override bool GetSelectionParam(string sourceAdvice, string sourceHeader = null) 
{
return GetUserSelection(sourceHeader, sourceAdvice);
} 

}

}