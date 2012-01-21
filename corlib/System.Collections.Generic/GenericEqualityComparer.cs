using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic {
	class GenericEqualityComparer<T> : EqualityComparer<T> where T : IEquatable<T> {

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
}
