#if !LOCALTEST

using System;

namespace System.Runtime.CompilerServices {
	public static class RuntimeHelpers {

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern public static void InitializeArray(Array array, RuntimeFieldHandle fldHandle);

	}
}
#endif
