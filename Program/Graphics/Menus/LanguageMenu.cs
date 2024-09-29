using System;
using System.Globalization;
using RipeConsole.Program.Loaders;
using ZCore.Serializables;

namespace RipeConsole.Program.Graphics.Menus
{
/// <summary> Allows the User to Select a <b>Language</b>. </summary>

internal partial class LanguageMenu : Menu<LocalizedStrings, string>
{
/// <summary> Creates a new Instance of the <c>LanguageMenu</c>. </summary>

public LanguageMenu()
{
headerText = "CONFIG_USER_LANGUAGE";
adviceText = "ADVICE_CHOOSE_LANGUAGE";
}

/** <summary> Displays the <c>LanguageMenu</c>. </summary>
<returns> The Language selected by the User. </returns> */

public override string DynamicSelection()
{
ActionWrapper<LocalizedStrings, int> displayAction = new( PrintAction );
ActionWrapper<LocalizedStrings, int> onSelectAction = new( SelectiveAction );

LocalizedStrings selectedStr = default;
ShowOptions(TextLoader.StringsByLanguage, ref selectedStr, displayAction.Init, onSelectAction.Init);

return selectedStr.CultureName;
}

/** <summary> Prints info related to the LocStrings given as a Parameter (NativeName, DisplayName and Culture). </summary>
<param name = "sourceItem"> The LocStrings where the Info will be Obtained from. </param> */

public override void PrintAction(LocalizedStrings sourceItem, int index)
{
sourceItem.SetLanguageInfo();

CultureInfo languageInfo = sourceItem.GetLanguageInfo();

Text.Print(true, "{0} => {1}  [ {2} ]", languageInfo.NativeName, languageInfo.DisplayName, sourceItem.CultureName);
}

/** <summary> Displays the LocStrings selected by the User. </summary>

<param name = "selectedItem"> The Strings selected by the User. </param>
<param name = "elementIndex"> The Index of the selected Strings. </param> */

public override void SelectiveAction(LocalizedStrings selectedItem, int elementIndex)
{
Text.PrintAdvice(false, Text.ProgramStrings.LocateByID(adviceText) );

Text.Print(ConsoleColor.DarkYellow, true, selectedItem.CultureName);
}

// Displays Info related to the Current Element

protected override void DisplayElementInfo(LocalizedStrings element, int index)
{
Text.PrintSubHeader("LocStrings Info");

CultureInfo languageInfo = element.GetLanguageInfo();

Text.PrintLine(false, "Index: {0}", index);
Text.PrintLine(false, "Culture Name: {0}", element.CultureName);
Text.PrintLine(false, "Native Name: {0}", languageInfo.NativeName);
Text.PrintLine(false, "Display Name: {0}", languageInfo.DisplayName);
Text.PrintLine(true, "Strings Count: {0}", element.StringsMap.Count);
}

}

}