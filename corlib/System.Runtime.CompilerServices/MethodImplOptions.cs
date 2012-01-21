#if !LOCALTEST

using System;

namespace System.Runtime.CompilerServices {
	[Flags]
	public enum MethodImplOptions {
		Unmanaged =		0x0004,
		ForwardRef =	0x0010,
		InternalCall =	0x1000,
		Synchronized =	0x0020,
		NoInlining =	0x0008,
		PreserveSig =	0x0080,
	}
}

#endif
