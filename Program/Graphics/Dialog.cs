namespace RipeConsole.Program.Graphics
{
/// <summary> Represents a Dialog of this Program. </summary>

public class Dialog<T> : Graphics
{
/// <summary> Creates a new Instance of the <c>Dialog</c>. </summary>

public Dialog()
{
}

/** <summary> Shows the <c>Dialog</c> on Screen. </summary>
<returns> The Type expected to be Entered by the user. </returns> */

public virtual T Popup() => default;

/** <summary> Shows the <c>Dialog</c> on Screen. </summary>

<param name = "inputRange"> The Range which user Input must follow. </param>

<returns> The Type expected to be Entered by the user. </returns> */

public virtual T Popup(Limit<int> inputRange) => default;

/** <summary> Shows the <c>Dialog</c> with the specified Advice and Body on Screen. </summary>

<param name = "sourceAdvice"> The Advice to Display on Screen. </param>
<param name = "sourceBody"> The Body to Display on Screen (Optional). </param>

<returns> The Type expected to be Entered by the user. </returns> */

public virtual T Popup(string sourceAdvice, string sourceBody = null) => default;

/** <summary> Shows the <c>Dialog</c> with the specified Advice and Body on Screen. </summary>

<param name = "sourceAdvice"> The Advice to Display on Screen. </param>
<param name = "inputRange"> The Range which user Input must follow. </param>
<param name = "sourceBody"> The Body to Display on Screen (Optional). </param>

<returns> The Type expected to be Entered by the user. </returns> */

public virtual T Popup(string sourceAdvice, Limit<int> inputRange, string sourceBody = null) => default;
}

}