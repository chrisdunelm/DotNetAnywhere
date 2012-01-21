#if !LOCALTEST

using System.Runtime.CompilerServices;
namespace System {
	public static class GC {

		public static int MaxGeneration {
			get {
				return 2;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private static int Internal_CollectionCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public static void Collect();

		public static void Collect(int generation) {
			if (generation < 0) {
				throw new ArgumentOutOfRangeException("generation");
			}
			Collect();
		}

		public static int CollectionCount(int generation) {
			if (generation < 0) {
				throw new ArgumentOutOfRangeException("generation");
			}
			if (generation != 0) {
				return 0;
			}
			return Internal_CollectionCount();
		}

		public static int GetGeneration(object obj) {
			if (obj == null) {
				throw new NullReferenceException();
			}
			return 0;
		}

		public static int GetGeneration(WeakReference wr) {
			if (wr == null || !wr.IsAlive) {
				throw new NullReferenceException();
			}
			return 0;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public static long GetTotalMemory(bool forceFullCollection);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public static void SuppressFinalize(object obj);

	}
}

#endif
