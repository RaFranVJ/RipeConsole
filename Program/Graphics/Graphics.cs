namespace RipeConsole.Program.Graphics
{
/// <summary> The Graphics of this Program. </summary>

public class Graphics
{
/** <summary> Locates the Text of a Header. </summary>
<returns> The Text of the Header. </returns> */

protected string headerText;

/** <summary> Locates the Text of a Body. </summary>
<returns> The Text of the Body. </returns> */

protected string bodyText;

/** <summary> Locates the Text of an Advice. </summary>
<returns> The Text of the Advice. </returns> */

protected string adviceText;

/// <summary> Creates a new Instance of the <c>Graphics</c>. </summary>

public Graphics()
{
headerText = "<SET A HEADER TEXT>";
bodyText = "<SET A BODY TEXT>";

adviceText = "<SET A ADVICE TEXT>";
}

}

}