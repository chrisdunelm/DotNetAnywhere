#if !LOCALTEST

namespace System {
	public struct Boolean : IComparable, IComparable<bool>, IEquatable<bool> {

		public static readonly string TrueString = "True";
		public static readonly string FalseString = "False";

		internal bool m_value;

		public override string ToString() {
			return this.m_value ? TrueString : FalseString;
		}

		public override bool Equals(object obj) {
			return (obj is bool) && ((bool)obj).m_value == this.m_value;
		}

		public override int GetHashCode() {
			return (this.m_value) ? 1 : 0;
		}

		public static bool Parse(string value) {
			if (value == null) {
				throw new ArgumentNullException("value");
			}
			value = value.Trim();
			if (value == TrueString) {
				return true;
			}
			if (value == FalseString) {
				return false;
			}
			throw new FormatException("Value is not a valid boolean");
		}

		#region IComparable Members

		public int CompareTo(object obj) {
			if (obj == null) {
				return 1;
			}
			if (!(obj is int)) {
				throw new ArgumentException();
			}
			return this.CompareTo((bool)obj);
		}

		#endregion

		#region IComparable<bool> Members

		public int CompareTo(bool x) {
			return (this.m_value == x) ? 0 : ((this.m_value) ? 1 : -1);
		}

		#endregion

		#region IEquatable<bool> Members

		public bool Equals(bool x) {
			return this.m_value == x;
		}

		#endregion
	
	}
}

#endif