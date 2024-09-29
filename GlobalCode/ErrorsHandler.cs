using System;
using System.Collections;
using System.Collections.Generic;
using RipeConsole.Program;

/// <summary> Initializes Handling functions for the Exceptions that Occur when Running this Program. </summary>

public static class ErrorsHandler
{
/// <summary> Displays all the Info related to the Exception that was Caught. </summary>

public static void DisplayErrorInfo(Exception errorCaught)
{
Text.PrintHeader(Text.ProgramStrings.LocateByID("HEADER_ERROR_INFO") );

Text.PrintDictionary(GetErrorInfo(errorCaught), true);
}

/** <summary> Gets all the Info related to the Exception that was Caught. </summary>

<returns> The Info Obtained from the Exception that was Caught. </returns> */

private static Dictionary<string, object> GetErrorInfo(Exception errorCaught)
{
#region ====== Store Default ErrorInfo in Dictionary ======

Dictionary<string, object> errorInfo = new()
{

{ Text.ProgramStrings.LocateByID("ERROR_INFO_TYPE"), errorCaught.GetType() },
{ Text.ProgramStrings.LocateByID("ERROR_INFO_MESSAGE"), errorCaught.Message },
{ Text.ProgramStrings.LocateByID("ERROR_INFO_HELPFUL_LINK"), errorCaught.HelpLink }

};

#endregion

errorInfo.Add(Text.ProgramStrings.LocateByID("ERROR_INFO_HANDLE_RESULT"), errorCaught.HResult);
errorInfo.Add(Text.ProgramStrings.LocateByID("ERROR_INFO_TRACE_CODE"), errorCaught.StackTrace);

errorInfo.Add(Text.ProgramStrings.LocateByID("ERROR_INFO_TARGET_SITE"), errorCaught.TargetSite);
errorInfo.Add(Text.ProgramStrings.LocateByID("ERROR_INFO_SOURCE"), errorCaught.Source);

errorInfo.Add(Text.ProgramStrings.LocateByID("ERROR_INFO_BASE_EXCEPTION"), errorCaught.GetBaseException() );
errorInfo.Add(Text.ProgramStrings.LocateByID("ERROR_INFO_INNER_EXCEPTION"), errorCaught.InnerException);

IDictionary errorData = errorCaught.Data;
string errorInfo_Data = Text.ProgramStrings.LocateByID("ERROR_INFO_DATA");

if(errorData.Count > 0)
errorInfo.Add(errorInfo_Data, errorData);

else
errorInfo.Add(errorInfo_Data, Text.ProgramStrings.LocateByID("DIALOG_NO_DATA_AVAILABLE") );

return errorInfo;
}

}