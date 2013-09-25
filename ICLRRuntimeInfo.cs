using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vss3WayMerge
{
	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("BD39D1D2-BA2F-486A-89B0-B4B0CB466891")]
	interface ICLRRuntimeInfo
	{
		void xGetVersionString();
		void xGetRuntimeDirectory();
		void xIsLoaded();
		void xIsLoadable();
		void xLoadErrorString();
		void xLoadLibrary();
		void xGetProcAddress();
		void xGetInterface();
		void xSetDefaultStartupFlags();
		void xGetDefaultStartupFlags();

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void BindAsLegacyV2Runtime();
	}
}
