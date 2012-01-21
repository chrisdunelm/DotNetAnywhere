#if !LOCALTEST

namespace System {
	public struct RuntimeFieldHandle {

		private IntPtr value;

		internal RuntimeFieldHandle(IntPtr v) {
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
