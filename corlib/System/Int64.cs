#if !LOCALTEST

using System.Runtime.CompilerServices;
using System.Globalization;
namespace System {
	public struct Int64 : IFormattable, IComparable, IComparable<long>, IEquatable<long> {
		public const long MaxValue = 0x7fffffffffffffff;
		public const long MinValue = unchecked((long)0x8000000000000000);

		private long m_value;

		public override bool Equals(object o) {
			return (o is long) && ((long)o).m_value == this.m_value;
		}

		public override int GetHashCode() {
			return (int)(m_value & 0xffffffff) ^ (int)(m_value >> 32);
		}

		public override string ToString() {
			return NumberFormatter.FormatGeneral(new NumberFormatter.NumberStore(this.m_value));
		}

		public string ToString(IFormatProvider formatProvider) {
			return NumberFormatter.FormatGeneral(new NumberFormatter.NumberStore(this.m_value), formatProvider);
		}

		public string ToString(string format) {
			return ToString(format, null);
		}

		public string ToString(string format, IFormatProvider formatProvider) {
			NumberFormatInfo nfi = NumberFormatInfo.GetInstance(formatProvider);
			return NumberFormatter.NumberToString(format, m_value, nfi);
		}

		#region IComparable Members

		public int CompareTo(object obj) {
			if (obj == null) {
				return 1;
			}
			if (!(obj is long)) {
				throw new ArgumentException();
			}
			return this.CompareTo((long)obj);
		}

		#endregion

		#region IComparable<long> Members

		public int CompareTo(long x) {
			return (this.m_value > x) ? 1 : ((this.m_value < x) ? -1 : 0);
		}

		#endregion

		#region IEquatable<long> Members

		public bool Equals(long x) {
			return this.m_value == x;
		}

		#endregion

	}
}

#endif
