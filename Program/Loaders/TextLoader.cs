using System.Collections.Generic;
using System.Linq;
using RipeConsole.Program.Serializables;
using ZCore.Serializables;

namespace RipeConsole.Program.Loaders
{
/// <summary> Loads a List of Entries used for Localizing Strings by Language. </summary>

public static class TextLoader
{
/** <summary> Gets a List of <c>LocalizedStrings</c>  filtered by Language. </summary>
<returns> The Strings Sorted by Language. </returns> */

internal static List<LocalizedStrings> StringsByLanguage;

/** <summary> Gets an Instance of the <c>LocalizedStrings</c> that Match the UserLanguage. </summary>
<returns> The UserBased Strings. </returns> */

public static LocalizedStrings GetUserBasedStrings()
{
Settings appConfig = Program.GetAppConfig;

StringsByLanguage = new LocalizedStrings().ReadObjects();
var strFound = StringsByLanguage.Find(strInfo => strInfo.CultureName == appConfig.UserLanguage);

if(strFound == null)
{
Text.PrintWarning($"No Strings found for {appConfig.UserLanguage}, using default ones Instead");

return StringsByLanguage.Find(defStr => defStr.CultureName == "en-US");
}

return strFound;
}

}

}