using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;

namespace RipeConsole.Program.Graphics.Menus
{
/// <summary> Represents a user Selection. </summary>

internal partial class EnumSelector<TEnum> : Menu<TEnum, TEnum> where TEnum : Enum
{
/// <summary> Creates a new Instance of the <c>EnumSelector</c>. </summary>

public EnumSelector()
{
headerText = typeof(TEnum).Name;
adviceText = "ADVICE_CHOOSE_VALUE";
}

/** <summary> Displays the <c>EnumsSelector</c>. </summary>
<returns> The GroupCategory selected by the User. </returns> */

public override TEnum DynamicSelection()
{
ActionWrapper<TEnum, int> displayAction = new( PrintAction );
ActionWrapper<TEnum, int> onSelectAction = new( SelectiveAction );

TEnum[] enumFlags = Enum.GetValues(typeof(TEnum) ) as TEnum[];
TEnum selectedEnum = default;

ShowOptions(enumFlags.ToList(), ref selectedEnum, displayAction.Init, onSelectAction.Init);
return selectedEnum;
}

/** <summary> Prints info related to the Enum given as a Parameter (Name and Value). </summary>
<param name = "sourceItem"> The Enum where the Info will be Obtained from. </param> */

public override void PrintAction(TEnum sourceItem, int index)
{
Text.Print(true, "{0} ---> {1}", sourceItem, Convert.ToInt32(sourceItem) );
}

/** <summary> Displays the Enum selected by the User. </summary>

<param name = "selectedItem"> The Enum selected by the User. </param>
<param name = "elementIndex"> The Index of the Enum selected. </param> */

public override void SelectiveAction(TEnum selectedItem, int elementIndex)
{
Text.PrintAdvice(false, Text.ProgramStrings.LocateByID(adviceText) );

Text.Print(ConsoleColor.DarkYellow, true, "{0} ({1})", selectedItem, Convert.ToInt32(selectedItem) );
}

// Displays Info related to the Current Element

protected override void DisplayElementInfo(TEnum element, int index)
{
Text.PrintSubHeader("Selected Enum Info");

Text.PrintLine(false, "Index: {0}", index);
Text.PrintLine(false, "Name: {0}", element);
Text.PrintLine(false, "Value: {0}", Convert.ToInt32(element) );
Text.PrintLine(true, "Type: {0}", typeof(TEnum).Name);

_ = continueDialog.Popup();
}

// Get Int from String

protected override int? TransformNumericInput(string input, List<TEnum> options)
{
int minValue = Convert.ToInt32(options.Min() );
int maxValue = Convert.ToInt32(options.Max() );

if(int.TryParse(input, out int numericValue) && numericValue >= minValue && numericValue <= maxValue)
{
int index = options.FindIndex(c => Convert.ToInt32(c) == numericValue);

return index == -1 ? null : index;
}

return null;
}

}

}