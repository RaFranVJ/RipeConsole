using System;

namespace RipeConsole.Program.Graphics.UserSelections
{
/// <summary> Allows the User to Select a generic Value. </summary>

internal partial class GenericSelection<T> : UserSelection<T> where T : struct, IComparable<T>
{

protected Limit<T> inputRange;

/// <summary> Creates a new Instance of the <c>GenericSelection</c>. </summary>

public GenericSelection()
{
adviceText = Text.ProgramStrings.LocateByID("ADVICE_CHOOSE_VALUE");
inputRange = Limit<T>.GetLimitRange();
}

// Get Generic Value

private T GetGenericValue(string header, string adv = null)
{
header ??= typeof(T).Name;
Text.PrintHeader(header);

var rInfo = string.Format(Text.ProgramStrings.LocateByID("DIALOG_INPUT_RANGE"), inputRange.MinValue, inputRange.MaxValue);
Text.PrintDialog(true, rInfo);

Text.PrintLine();

adv = !string.IsNullOrEmpty(adv) ? adv : adviceText;
Text.PrintAdvice(false, adv);

T selectedValue = InputHelper.FilterNumber<T>(Console.ReadLine() ?? string.Empty);
Text.PrintLine();

return selectedValue;
}

/** <summary> Displays the <c>GenericSelection</c>. </summary>

<param name = "sourceHeader"> The Header to be Displayed on Screen (Optional). </param>

<returns> The generic Value selected by User. </returns> */

public override T GetSelectionParam() => GetGenericValue(null);

/** <summary> Displays the <c>GenericSelection</c> with the Specific Advice. </summary>

<param name = "sourceAdvice"> The Advice to Display on Screen. </param>

<returns> The generic Value selected by the User. </returns> */

public override T GetSelectionParam(string sourceAdvice) => GetGenericValue(null, sourceAdvice);

/** <summary> Displays the <c>GenericSelection</c> with the Specific Advice. </summary>

<param name = "sourceAdvice"> The Advice to Display on Screen. </param>
<param name = "sourceHeader"> The Header to be Displayed on Screen (Optional). </param>

<returns> The generic Value selected by the User. </returns> */

public override T GetSelectionParam(string sourceAdvice, string sourceHeader = null)
{
return GetGenericValue(sourceHeader, sourceAdvice);
}

// Set range

public void SetInputRange(Limit<T> newRange) => inputRange = newRange;
}

}