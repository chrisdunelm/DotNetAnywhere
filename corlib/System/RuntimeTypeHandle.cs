#if !LOCALTEST

namespace System {
	public struct RuntimeTypeHandle {

		private IntPtr value;

		internal RuntimeTypeHandle(IntPtr v) {
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
