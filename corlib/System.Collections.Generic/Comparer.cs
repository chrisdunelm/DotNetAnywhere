#if !LOCALTEST

using System;
using System.Collections;

namespace System.Collections.Generic {
	public abstract class Comparer<T> : IComparer<T>,IComparer {

		private sealed class DefaultComparer : Comparer<T> {

			public override int Compare(T x, T y) {
				if (x == null) {
					return y == null ? 0 : -1;
				}
				if (y == null) {
					return 1;
				}
				IComparable<T> iComp = x as IComparable<T>;
				if (iComp != null) {
					return iComp.CompareTo(y);
				}
				IComparable iComp2 = x as IComparable;
				if (iComp2 != null) {
					return iComp2.CompareTo(y);
				}
				throw new ArgumentException("Does not implement IComparable");
			}

		}

		private sealed class DefaultComparerValueType : Comparer<T> {

			public override int Compare(T x, T y) {
				IComparable<T> iComp = x as IComparable<T>;
				if (iComp != null) {
					return iComp.CompareTo(y);
				}
				IComparable iComp2 = x as IComparable;
				if (iComp2 != null) {
					return iComp2.CompareTo(y);
				}
				throw new ArgumentException("Does not implement IComparable");
			}

		}

		static Comparer() {
			if (typeof(T).IsValueType) {
				Default = new DefaultComparerValueType();
			} else {
				Default = new DefaultComparer();
			}
		}

		public static Comparer<T> Default { get; private set; }

		public abstract int Compare(T x, T y);

		public int Compare(object x, object y) {
			if (x == null) {
				return y == null ? 0 : -1;
			}
			if (y == null) {
				return 1;
			}
			if (x is T && y is T) {
				return this.Compare((T)x, (T)y);
			}
			throw new ArgumentException();
		}

	}
}

#endif
