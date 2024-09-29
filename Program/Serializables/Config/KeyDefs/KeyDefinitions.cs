namespace RipeConsole.Program.Serializables.Config.KeyDefs
{
/// <summary> Represents a Class where each Key defines an Action. </summary>

public class KeyDefinitions
{
/** <summary> Gets or Sets a Boolean that Determines if the Combination of Control Keys should be Allowed. </summary>
<returns> <b>true</b> if Enabled; otherwise, <b>false</b>. </returns> */

public bool AllowControlKeys{ get; set; }

/// <summary> Creates a new Instance of the <c>KeyDefinitions</c>. </summary>

public KeyDefinitions()
{
}

}

}