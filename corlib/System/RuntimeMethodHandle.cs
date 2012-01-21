#if !LOCALTEST

namespace System {
	public struct RuntimeMethodHandle {

		IntPtr value;

		internal RuntimeMethodHandle(IntPtr v) {
			value = v;
		}

		public IntPtr Value {
			get {
				return value;
			}
		}

	}
}

#endif
