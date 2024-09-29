using Newtonsoft.Json.Linq;
using RipeConsole.Program.Graphics.Menus;
using RipeConsole.Program.Serializables;
using System.IO;
using ZCore.Serializables;

namespace RipeConsole.Program
{
/// <summary> The Features of this Program. </summary>

public static class Features
{
/// <summary> Displays the Settings of this Program. </summary>

public static void DisplayAppSettings()
{
var newConfig = new AppSettings().DynamicSelection() as Settings;

Program.SetAppConfig(newConfig);

newConfig.WriteObject();
}

/// <summary> Displays info related to this Program. </summary>

public static void DisplayProgramInfo()
{
Text.PrintHeader(Text.ProgramStrings.LocateByID("HEADER_PROGRAM_INFO") );

Text.PrintJson(GetProgramInfo(), false);
}

/// <summary> Displays info about the User who is running the Program. </summary>

public static void DisplayUserInfo()
{
Text.PrintHeader(Text.ProgramStrings.LocateByID("HEADER_USER_INFO") );

Text.PrintJson(GetUserInfo(), false);
}

/// <summary> Allows the User to comit Changes to the ParamsGroup of this Program. </summary>

public static void EditParamsGroup()
{
var newParams = new ParamsEditor().DynamicSelection() as UserParams;

Program.SetUserParams(newParams);

newParams.WriteObject();
}

/** <summary> Gets Info about the curent Environment where the Program is being Executed. </summary>
<returns> Info related to the Environment where the Program is being Executed. </returns> */

private static JObject GetEnvironmentInfo()
{
#region ====== Store EnvironmentInfo in JObject ======

JObject environmentInfo = new()
{

{
Text.ProgramStrings.LocateByID("ENVIRONMENT_INFO_DEVICE_ARCHITECTURE"),
EnvInfo.DeviceArchitecture
},

{
Text.ProgramStrings.LocateByID("ENVIRONMENT_INFO_OS_VERSION"), 
JToken.FromObject(EnvInfo.OS)
},

{
Text.ProgramStrings.LocateByID("ENVIRONMENT_INFO_CLR_VERSION"), 
JToken.FromObject(EnvInfo.CLR)
},

{
Text.ProgramStrings.LocateByID("ENVIRONMENT_INFO_SYSTEM_STARTUP_TIME"), 
Termination.GetElapsedTime(EnvInfo.SystemStartupTime)
},

{
Text.ProgramStrings.LocateByID("ENVIRONMENT_INFO_SYSTEM_PAGE_SIZE"), 
InputHelper.GetDisplaySize(EnvInfo.PageSize)
},

{
Text.ProgramStrings.LocateByID("ENVIRONMENT_INFO_SYSTEM_DIRECTORY"), 
EnvInfo.SystemDirectory
},

{
Text.ProgramStrings.LocateByID("ENVIRONMENT_INFO_MACHINE_NAME"), 
EnvInfo.MachineName
},

{
Text.ProgramStrings.LocateByID("ENVIRONMENT_INFO_LOGICAL_DRIVES"), 
JToken.FromObject(EnvInfo.DrivesList)
},

{
Text.ProgramStrings.LocateByID("ENVIRONMENT_INFO_PROCESSORS_COUNT"), 
EnvInfo.ProcessorsCount
},

{
Text.ProgramStrings.LocateByID("ENVIRONMENT_INFO_CURRENT_APP_DIRECTORY"), 
EnvInfo.CurrentAppDirectory
},

{
Text.ProgramStrings.LocateByID("ENVIRONMENT_INFO_CURRENT_PROCESS_PATH"), 
EnvInfo.CurrentProcessPath
},

{
Text.ProgramStrings.LocateByID("ENVIRONMENT_INFO_APP_WORKING_SET"), 
EnvInfo.AppWorkingSet
},

{
Text.ProgramStrings.LocateByID("ENVIRONMENT_INFO_SYSTEM_VARIABLES"), 
JToken.FromObject(EnvInfo.Variables)
}

};

#endregion

JProperty dataContainer = new(Text.ProgramStrings.LocateByID("HEADER_ENVIRONMENT_INFO"), environmentInfo);

return new JObject(dataContainer);
}

/** <summary> Gets Info about this Program. </summary>
<returns> Info related to this Program. </returns> */

private static JObject GetProgramInfo()
{
#region ====== Store ProgramInfo in JObject ======

JObject programInfo = new()
{

{
Text.ProgramStrings.LocateByID("PROGRAM_INFO_PRODUCER"),
Info.ProgramProducer
},

{
Text.ProgramStrings.LocateByID("PROGRAM_INFO_NAME"),
Info.ProgramProduct
},

{
Text.ProgramStrings.LocateByID("PROGRAM_INFO_BUILD_CONFIG"),
Info.BuildConfig
},

{
Text.ProgramStrings.LocateByID("PROGRAM_INFO_VERSION"),
Info.ProgramVersion
},

{
Text.ProgramStrings.LocateByID("PROGRAM_INFO_COMPILE_VERSION"),
JToken.FromObject(Info.CompileVersion)
},

{
Text.ProgramStrings.LocateByID("PROGRAM_INFO_COMPILE_TIME"),
Info.CompilationTime
}

};

#endregion

return programInfo;
}

/** <summary> Gets Info about the current User who is Running this Program. </summary>
<returns> Info related to the User who is Running the Program. </returns> */

private static JObject GetUserInfo()
{
#region ====== Store UserInfo in JObject ======

JObject userInfo = new()
{

{
Text.ProgramStrings.LocateByID("USER_INFO_CURRENT_NAME"),
EnvInfo.UserName
},

{
Text.ProgramStrings.LocateByID("USER_INFO_CURRENT_DOMAIN"),
EnvInfo.UserDomain
},

{
Text.ProgramStrings.LocateByID("USER_INFO_CURRENT_COUNTRY"),
EnvInfo.UserCountry
},

{
Text.ProgramStrings.LocateByID("USER_INFO_CURRENT_LANGUAGE"),
EnvInfo.CurrentLanguage
}

};

#endregion

return userInfo;
}

/** <summary> Saves the Environment Info of this Program to a new File. </summary>
<param name = "outputPath"> The Path where the Environment Info will be Saved. </param> */

public static void SaveEnvironmentInfo(string outputPath)
{
string jsonText = GetEnvironmentInfo().ToString();

JsonSerializer.FormatJsonString(ref jsonText);

File.WriteAllText(outputPath, jsonText);
}

}

}