using System;
using ZCore;

namespace RipeConsole.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a <b>DateTime</b>. </summary>

internal partial class DateTimeSelection : UserSelection<DateTime>
{
/// <summary> Creates a new Instance of the <c>DateTimeSelection</c>. </summary>

public DateTimeSelection()
{
headerText = Text.ProgramStrings.LocateByID("PARAM_DATE_TIME");

bodyText = string.Format(Text.ProgramStrings.LocateByID("USER_SELECTION_DATE_TIME"), '\\', ':', "AM", "PM");
adviceText = Text.ProgramStrings.LocateByID("ADVICE_SELECT_DATE_TIME");
}

// Get DateTime set by User

private DateTime GetUserDate(string adv = null)
{
Text.PrintHeader(headerText);

Text.PrintLine(true, bodyText);

Limit<DateTime> inputRange = Limit<DateTime>.GetLimitRange();
var rInfo = string.Format(Text.ProgramStrings.LocateByID("DIALOG_INPUT_RANGE"), inputRange.MinValue, inputRange.MaxValue);

Text.PrintDialog(true, rInfo);
Text.PrintLine();

adv = !string.IsNullOrEmpty(adv) ? adv : adviceText;
Text.PrintAdvice(false, adv);

DateTime selectedValue = InputHelper.FilterDateTime(Console.ReadLine() );
Text.PrintLine();

return selectedValue;
}

/** <summary> Displays the <c>DateTimeSelection<c>. </summary>

<returns> The <b>DateTime</b> selected by User. </returns> */

public override DateTime GetSelectionParam() => GetUserDate();

/** <summary> Displays the <c>DateTimeSelection<c>. </summary>

<returns> The <b>DateTime</b> selected by User. </returns> */

public override DateTime GetSelectionParam(string adv) => GetUserDate(adv);
}

}