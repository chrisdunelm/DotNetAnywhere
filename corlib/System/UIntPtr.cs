#if !LOCALTEST

namespace System {
	public unsafe struct UIntPtr {

		private void* value;

		public UIntPtr(uint value) {
			this.value = (void*)value;
		}

		public UIntPtr(ulong value) {
			this.value = (void*)value;
		}

		public UIntPtr(void* value) {
			this.value = value;
		}

		public static int Size {
			get {
				return sizeof(void*);
			}
		}

		public uint ToUInt32() {
			return (uint)this.value;
		}

		public ulong ToUInt64() {
			return (ulong)this.value;
		}

		public override bool Equals(object obj) {
			return (obj is UIntPtr && ((UIntPtr)obj).value == this.value);
		}

		public override int GetHashCode() {
			return (int)this.value;
		}

		public static bool operator ==(UIntPtr a, UIntPtr b) {
			return a.value == b.value;
		}

		public static bool operator !=(UIntPtr a, UIntPtr b) {
			return a.value != b.value;
		}

		public override string ToString() {
			if (Size == 4) {
				return string.Format("0x{0:x4}", (int)this.value);
			} else {
				return string.Format("0x{0:x8}", (long)this.value);
			}
		}

	}
}

#endif