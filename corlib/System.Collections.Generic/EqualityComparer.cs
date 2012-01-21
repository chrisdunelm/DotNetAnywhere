#if !LOCALTEST

using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic {

	public abstract class EqualityComparer<T> : IEqualityComparer, IEqualityComparer<T> {

		private class DefaultComparer : EqualityComparer<T> {

			public override bool Equals(T x, T y) {
				if (x == null) {
					return y == null;
				}
				return x.Equals(y);
			}

			public override int GetHashCode(T obj) {
				if (obj == null) {
					return 0;
				}
				return obj.GetHashCode();
			}

		}

		static EqualityComparer() {
			// Need to use the GenericEqualityComparer, but can't because
			// cannot instantiate the type yet, because T needs to implement IEquatable<T>
			Default = new DefaultComparer();
		}

		public static EqualityComparer<T> Default { get; private set; }

		public abstract bool Equals(T x, T y);

		public abstract int GetHashCode(T obj);

		bool IEqualityComparer.Equals(object x, object y) {
			return this.Equals((T)x, (T)y);
		}

		int IEqualityComparer.GetHashCode(object obj) {
			return this.GetHashCode((T)obj);
		}

	}

}

#endif
