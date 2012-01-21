#if !LOCALTEST

using System.Runtime.CompilerServices;
namespace System.Diagnostics {
	public sealed class Debugger {

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public static void Break();

	}
}

#endif
