#if !LOCALTEST

using System.Runtime.CompilerServices;

namespace System {
	public abstract class ValueType {

		protected ValueType() {
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private static object[] GetFields(object o1, object o2);

		public override bool Equals(object obj) {
			if (obj == null || this.GetType() != obj.GetType()) {
				return false;
			}
			object[] fields = GetFields(this, obj);
			int len = fields.Length;
			for (int i = 0; i < len; i += 2) {
				object meVal = fields[i];
				object youVal = fields[i + 1];
				if (!object.Equals(meVal, youVal)) {
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode() {
			object[] fields = GetFields(this, null);

			int hash = 0;
			int len = fields.Length;
			for (int i = 0; i < len; i++) {
				hash ^= fields[i].GetHashCode();
			}
			return hash;
		}
	}
}
#endif
