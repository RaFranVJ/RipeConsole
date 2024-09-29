namespace RipeConsole.Program.Graphics
{
/// <summary> Represents a user Selection. </summary>

public class UserSelection<T> : Graphics
{
/// <summary> Creates a new Instance of the <c>UserSelection</c>. </summary>

public UserSelection()
{
}

/** <summary> Displays the Parameters of a <c>UserSelection</c>. </summary>
<returns> The Selection made by the User. </returns> */

public virtual T GetSelectionParam() => default;

/** <summary> Displays the Parameters of a <c>UserSelection</c>. </summary>
<returns> The Selection made by the User. </returns> */

public virtual T GetSelectionParam(string adv) => default;

/** <summary> Displays the Parameters of a <c>UserSelection</c>. </summary>
<returns> The Selection made by the User. </returns> */

public virtual T GetSelectionParam(string adv, string header = null) => default;
}

}