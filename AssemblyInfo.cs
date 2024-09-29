using System.Reflection;
using System.Runtime.InteropServices;

[ assembly : AssemblyTitle("Ripe Console") ]
[ assembly : AssemblyDescription("With this Tool, you'll be Able to Run and Debug JavaScript Code") ]
[ assembly : AssemblyCompany("Fran") ]
[ assembly : AssemblyProduct("RipeJS") ]

# if DEBUG
[ assembly : AssemblyConfiguration("Debug") ]

# else
[ assembly : AssemblyConfiguration("Release") ]

# endif

[ assembly : ComVisible(false) ]
[ assembly : Guid("7a0ac07f-c08b-4e44-ad91-3f0ef1621a72") ]
[ assembly : AssemblyVersion("1.0.*") ]
[ assembly : AssemblyFileVersion("1.0.0.0") ]