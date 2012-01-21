#if !LOCALTEST

using System.Runtime.CompilerServices;
namespace System.Threading {
	public static class Interlocked {

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public static int CompareExchange(ref int loc, int value, int comparand);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public static int Increment(ref int loc);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public static int Decrement(ref int loc);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public static int Add(ref int loc, int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public static int Exchange(ref int loc, int value);

	}
}

#endif
