using System;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Threading;

/// <summary> Serves as a Reference to the Variables of the Environment where this Program runs. </summary> 

public static class EnvInfo
{
/// <summary> The Assembly that is being currently Executed. </summary>

public static Assembly CurrentAssembly => Assembly.GetExecutingAssembly();

/// <summary> The Name of the Current Assembly. </summary>

public static AssemblyName AssemblyName => CurrentAssembly.GetName();

/// <summary> The Version of the Current Assembly. </summary>

public static Version AssemblyVersion => AssemblyName.Version;

/// <summary> The Location of the Current Assembly. </summary>

public static string AssemblyLocation => CurrentAssembly.Location;

/// <summary> The Types defined in the Current Assembly. </summary>

public static Type[] AssemblyTypes => CurrentAssembly.GetTypes();

/// <summary> The Resources found in the Current Assembly. </summary>

public static string[] AssemblyResources => CurrentAssembly.GetManifestResourceNames();

/// <summary> The Operating System where the Program is being Executed. </summary>

public static OperatingSystem OS => Environment.OSVersion;

/// <summary> The CLR Version of the Environment. </summary>

public static Version CLR => Environment.Version;

/// <summary> The System Page Size. </summary>

public static int PageSize => Environment.SystemPageSize;

/// <summary> A Path to the System Directory. </summary>

public static string SystemDirectory => Environment.SystemDirectory;

/// <summary> The Name of User Machine. </summary>

public static string MachineName => Environment.MachineName;

/// <summary> A List of Drives the Device has. </summary>

public static string[] DrivesList => Directory.GetLogicalDrives();

/// <summary> The Number of Processors the Device has. </summary>

public static int ProcessorsCount => Environment.ProcessorCount;

/// <summary> The Directory which the Current App is using. </summary>

public static string CurrentAppDirectory => Path.GetDirectoryName(AssemblyLocation);

/// <summary> The Path of the Process which is being Executed. </summary>

public static string CurrentProcessPath => Environment.ProcessPath;

/** <summary> The WorkingSet which the Current App is using. </summary>
<remarks> <b>WorkingSet</b> refers to the amount of physical Memory used by the Application. </remarks> */

public static long AppWorkingSet => Environment.WorkingSet;

/// <summary> The Name of the User who is using the App. </summary>

public static string UserName => Environment.UserName;

/// <summary> The Domain of the User which is using the App. </summary>

public static string UserDomain => Environment.UserDomainName;

/// <summary> The Country where the User belongs to. </summary>

public static string UserCountry => CurrentRegion.DisplayName;

/** <summary> Gets the Architecture of the Device where the Program is Installed. </summary>
<returns> The Architecture of the Device </returns> */

public static string DeviceArchitecture => Environment.Is64BitOperatingSystem ? "64-Bits" : "32-Bits";

/** <summary> Gets the System Startup Time expressed in Milliseconds. </summary>
<returns> The System Startup Time. </returns> */

public static long SystemStartupTime => (DeviceArchitecture == "32-Bits") ? Environment.TickCount : Environment.TickCount64;

/** <summary> Gets Info related to the Region where the User belongs to. </summary>
<returns> The Region where the User belongs to. </returns> */

private static RegionInfo CurrentRegion => new(CurrentCulture.Name);

/** <summary> Gets the Language of the User in its Native Name. </summary>
<returns> The Language of the User. </returns> */

public static string CurrentLanguage => CurrentCulture.IsNeutralCulture ? CurrentCulture.NativeName : CurrentCulture.Parent.NativeName;

/** <summary> Gets or Sets the Culture which the Program should use. </summary>
<returns> The Current <c>CultureInfo</c>. </returns> */

public static CultureInfo CurrentCulture
{

get => Thread.CurrentThread.CurrentCulture;
set => Thread.CurrentThread.CurrentCulture = value;

}

/** <summary> Creates or Obtains a List of Variables used on the Environment of the Device. </summary>
<returns> The Environment Variables. </returns> */

public static IDictionary Variables
{

get => Environment.GetEnvironmentVariables();
set => Variables = value;

}

}