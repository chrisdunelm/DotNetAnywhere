#if !LOCALTEST

using System.Globalization;
namespace System {
	public struct Int16 : IFormattable, IComparable, IComparable<short>, IEquatable<short> {
		public const short MaxValue = 0x7fff;
		public const short MinValue = -32768;

		internal short m_value;

		public override bool Equals(object obj) {
			return (obj is short) && ((short)obj).m_value == this.m_value;
		}

		public override int GetHashCode() {
			return (int)this.m_value;
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
			if (!(obj is short)) {
				throw new ArgumentException();
			}
			return this.CompareTo((short)obj);
		}

		#endregion

		#region IComparable<short> Members

		public int CompareTo(short x) {
			return (this.m_value > x) ? 1 : ((this.m_value < x) ? -1 : 0);
		}

		#endregion

		#region IEquatable<short> Members

		public bool Equals(short x) {
			return this.m_value == x;
		}

		#endregion

	}
}

#endif
