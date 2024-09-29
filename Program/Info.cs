using System;
using System.IO;
using System.Reflection;

namespace RipeConsole.Program
{
/// <summary> Initializes Analisis Functions about the Assembly Info of this Program when its Being Compiled. </summary>

internal class Info
{
/** <summary> Gets Info about the Title of this Program. </summary>
<returns> The Title of the Program. </returns> */

public static string ProgramTitle
{

get
{
Attribute[] customAttributes = Attribute.GetCustomAttributes(EnvInfo.CurrentAssembly, typeof(AssemblyTitleAttribute), false);

var assemblyAttribute = (AssemblyTitleAttribute)customAttributes[0];

return assemblyAttribute.Title;
}

}

/** <summary> Gets Info about the Description of this Program. </summary>
<returns> The Description of the Program. </returns> */

public static string ProgramDescription
{

get
{
Attribute[] customAttributes = Attribute.GetCustomAttributes(EnvInfo.CurrentAssembly, typeof(AssemblyDescriptionAttribute), false);

var assemblyAttribute = (AssemblyDescriptionAttribute)customAttributes[0];

return assemblyAttribute.Description;
}

}

/** <summary> Gets Info about the Producer of this Program. </summary>
<returns> The Producer of the Program. </returns> */

public static string ProgramProducer
{

get
{
Attribute[] customAttributes = Attribute.GetCustomAttributes(EnvInfo.CurrentAssembly, typeof(AssemblyCompanyAttribute), false);

var assemblyAttribute = (AssemblyCompanyAttribute)customAttributes[0];

return assemblyAttribute.Company;
}

}

/** <summary> Gets Info about the Product of this Program. </summary>
<returns> The Product of the Program. </returns> */

public static string ProgramProduct
{

get
{
Attribute[] customAttributes = Attribute.GetCustomAttributes(EnvInfo.CurrentAssembly, typeof(AssemblyProductAttribute), false);

var assemblyAttribute = (AssemblyProductAttribute)customAttributes[0];

return assemblyAttribute.Product;
}

}

/** <summary> Gets Info about the Build Config of this Program. </summary>
<returns> The Build Config of the Program. </returns> */

public static string BuildConfig
{

get
{
Attribute[] customAttributes = Attribute.GetCustomAttributes(EnvInfo.CurrentAssembly, typeof(AssemblyConfigurationAttribute), false);

var assemblyAttribute = (AssemblyConfigurationAttribute)customAttributes[0];

return assemblyAttribute.Configuration;
}

}

/** <summary> Gets Info about the Compile Version of this Program. </summary>
<returns> The Compile Version of the Program. </returns> */

public static Version CompileVersion{ get => EnvInfo.AssemblyVersion; }

/** <summary> Gets Info about the Version of this Program. </summary>
<returns> The Version of Program. </returns> */

public static string ProgramVersion
{

get
{
Attribute[] customAttributes = Attribute.GetCustomAttributes(EnvInfo.CurrentAssembly, typeof(AssemblyFileVersionAttribute), false);

var assemblyAttribute = (AssemblyFileVersionAttribute)customAttributes[0];

return assemblyAttribute.Version;
}

}

/** <summary> Gets Info about the Last Compilation Time of this Program. </summary>
<returns> The Last Compilation Time of the Program. </returns> */

public static DateTime CompilationTime => File.GetLastWriteTime(EnvInfo.AssemblyLocation);
}

}