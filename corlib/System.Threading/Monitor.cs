#if !LOCALTEST

using System.Runtime.CompilerServices;

namespace System.Threading {
	public static class Monitor {

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private static bool Internal_TryEnter(object obj, int msTimeout);

		public static void Enter(object obj) {
			if (obj == null) {
				throw new ArgumentNullException("obj");
			}
			Internal_TryEnter(obj, -1);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private static void Internal_Exit(object obj);

		public static void Exit(object obj) {
			if (obj == null) {
				throw new ArgumentNullException("obj");
			}
			Internal_Exit(obj);
		}

	}
}

#endif
