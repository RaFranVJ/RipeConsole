using RipeConsole.Program.Serializables.Config.KeyDefs;
using RipeConsole.Program.Serializables.Config.KeyDefs.MenuKeys;
using ZCore.Serializables.Config;

namespace RipeConsole.Program.Serializables.Config
{
/// <summary> Groups Info related to the Keys along with their Values. </summary>

public class MappedKeysInfo : ConfigField
{
/** <summary> Gets or Sets the Key Definitions used in the Program Termination. </summary>
<returns> The Termination Keys. </returns> */

public TerminationKeyDefs TerminationKeys{ get; set; }

/** <summary> Gets or Sets the Key Definitions used in Generic Menus. </summary>
<returns> The Generic Menu Keys. </returns> */

public KeyDefsForMenus GenericMenuKeys{ get; set; }

/** <summary> Gets or Sets the Key Definitions used in the List Viewer. </summary>
<returns> The List Viewer Keys. </returns> */

public KeyDefsForListViewer ListViewerKeys{ get; set; }

/** <summary> Gets or Sets the Key Definitions used in the List Viewer. </summary>
<returns> The List Viewer Keys. </returns> */

public KeyDefsForListEditor ListEditorKeys{ get; set; }

/** <summary> Gets or Sets the Key Definitions used in the Task Selector. </summary>
<returns> The List Viewer Keys. </returns> */

public KeyDefsForTaskSelector TaskSelectorKeys{ get; set; }

/// <summary> Creates a new Instance of the <c>MappedKeysInfo</c>. </summary>

public MappedKeysInfo()
{
TerminationKeys = new();
GenericMenuKeys = new();

ListViewerKeys = new();
ListEditorKeys = new();

TaskSelectorKeys = new();
}

}

}