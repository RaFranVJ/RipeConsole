using System;
using System.IO;

/// <summary> Handles the Structure of User-Defined Paths. </summary>

public static class PathUtils
{
/** <summary> Checks if the Path is a Relative Path or not. </summary>

<param name = "targetPath"> The Path Defined by User. </param>

<returns> The New Path. </returns> */

public static string AlignPathWithAppDir(string targetPath)
{

if(string.IsNullOrEmpty(targetPath) || Path.IsPathRooted(targetPath) )
return targetPath;

return Path.Combine(EnvInfo.CurrentAppDirectory, targetPath);
}

}