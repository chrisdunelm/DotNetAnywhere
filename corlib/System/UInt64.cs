#if !LOCALTEST

using System.Globalization;
namespace System {
	public struct UInt64 : IFormattable, IComparable, IComparable<ulong>, IEquatable<ulong> {

		public const ulong MinValue = 0;
		public const ulong MaxValue = 0xffffffffffffffffL;

		internal ulong m_value;

		public override bool Equals(object obj) {
			return (obj is ulong) && ((ulong)obj).m_value == this.m_value;
		}

		public override int GetHashCode() {
			return (int)(this.m_value & 0xffffffff) ^ (int)(this.m_value >> 32);
		}

		#region ToString methods

		public override string ToString() {
			return NumberFormatter.FormatGeneral(new NumberFormatter.NumberStore(this.m_value));
		}

		public string ToString(IFormatProvider formatProvider) {
			return NumberFormatter.FormatGeneral(new NumberFormatter.NumberStore(this.m_value), formatProvider);
		}

		public string ToString(string format) {
			return this.ToString(format, null);
		}

		public string ToString(string format, IFormatProvider formatProvider) {
			NumberFormatInfo nfi = NumberFormatInfo.GetInstance(formatProvider);
			return NumberFormatter.NumberToString(format, this.m_value, nfi);
		}

		#endregion

		#region IComparable Members

		public int CompareTo(object obj) {
			if (obj == null) {
				return 1;
			}
			if (!(obj is ulong)) {
				throw new ArgumentException();
			}
			return this.CompareTo((ulong)obj);
		}

		#endregion

		#region IComparable<ulong> Members

		public int CompareTo(ulong x) {
			return (this.m_value > x) ? 1 : ((this.m_value < x) ? -1 : 0);
		}

		#endregion

		#region IEquatable<ulong> Members

		public bool Equals(ulong x) {
			return this.m_value == x;
		}

		#endregion

	}
}

#endif
