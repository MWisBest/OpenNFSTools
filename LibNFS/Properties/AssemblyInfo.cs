using System.Reflection;
using System.Runtime.InteropServices;
using static NFSTools.LibNFS.Common.BuildInfo;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle( "LibNFS" )]
[assembly: AssemblyDescription( "" )]
[assembly: AssemblyConfiguration( "" )]
[assembly: AssemblyCompany( "" )]
[assembly: AssemblyProduct( "NFSTools.LibNFS.Properties" )]
[assembly: AssemblyCopyright( "Copyright © 2016" )]
[assembly: AssemblyTrademark( "" )]
[assembly: AssemblyCulture( "" )]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible( false )]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid( "ccaf842a-8ba0-4551-95d2-8cabcfea9777" )]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// NOTE: PATCH_VER IS INTENTIONALLY LEFT OUT FOR LIBNFS ASSEMBLYVERSION
//       Patch versions should not break APIs, so if we had the patch version here
//       a recompile against it would be necessary all the time which is stupid.
[assembly: AssemblyVersion( MAJOR_VER + "." + MINOR_VER )]
[assembly: AssemblyInformationalVersion( MAJOR_VER + "." + MINOR_VER + "." + PATCH_VER )]
