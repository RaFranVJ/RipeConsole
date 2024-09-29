using RipeConsole.Program.Graphics.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using ZCore.Serializables.SortUtils;

namespace RipeConsole.Program.Loaders.SortUtils
{
/// <summary> Loades a Function from a Group of Functions. </summary>

public static class FuncsLoader
{
/** <summary> Creates a new Instance of the <c>FunctionsMenu</c>
<returns> The Functions Selector </returns> */

private static readonly FunctionsMenu funcSelector = new();

// Filter Function by Extension

private static bool ExtensionFilter(Function func, string ext)
{

if(func.ArchivesFilter.FileExtensionsToSearch != null)
{

if(func.ArchivesFilter.FileExtensionsToSearch.Contains(".*", StringComparer.OrdinalIgnoreCase) )
return true;

return func.ArchivesFilter.FileExtensionsToSearch.Contains(ext, StringComparer.OrdinalIgnoreCase);
}

return false;
}

/** <summary> Filters a List of Functions allowed for a File according to its Extension. </summary>

<param name = "sourceFuncs"> The List to be Filtered. </param>
<param name = "targetExt"> The File Extension. </param>

<returns> The Filtered Functions. </returns> */

private static List<Function> FilterFuncsByExt(List<Function> sourceFuncs, string targetExt)
{
List<Function> filteredFuncs = sourceFuncs;

return sourceFuncs.Where(func => ExtensionFilter(func, targetExt) ).ToList();
}

// Exclude funcs from Dirs

public static bool ExcludeFuncs(FuncGroup sourceGroup)
{
List<Function> funcs = sourceGroup.GetFunctions();

if(funcs == null)
return false;

return funcs.Count > 0;
}

// Exclude funcs from Files

public static bool ExcludeFuncs(FuncGroup sourceGroup, string targetExt)
{
List<Function> funcs = sourceGroup.GetFunctions();

if(funcs == null)
return false;

else
{
List<Function> filteredFuncs = FilterFuncsByExt(funcs, targetExt);

sourceGroup.SetFunctions(filteredFuncs);

return filteredFuncs.Count > 0;
}

}

/** <summary> Loads the Functions grouped on a GroupCategory. </summary>

<param name = "targetGroup"> The Group from which to Select Functions. </param>

<returns> The Function that was Selected. </returns> */

public static Function GetFuncFromGroup(FuncGroup targetGroup)
{
funcSelector.UpdateParentGroup(targetGroup);

return funcSelector.DynamicSelection();
}

}

}