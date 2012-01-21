#if !LOCALTEST

using System.Runtime.CompilerServices;

namespace System {
	public class Object {

		public Object() {
		}

		~Object() {
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public Type GetType();

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public virtual bool Equals(object obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public virtual int GetHashCode();

		public virtual string ToString() {
			return this.GetType().FullName;
		}

		public static bool Equals(object a, object b) {
			if (a == b) {
				return true;
			}
			if (a == null || b == null) {
				return false;
			}
			return a.Equals(b);
		}

		public static bool ReferenceEquals(object a, object b) {
			return (a == b);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern internal static object Clone(object obj);
	}
}
#endif
