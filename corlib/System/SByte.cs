#if !LOCALTEST

using System.Globalization;
namespace System {
	public struct SByte : IFormattable, IComparable, IComparable<sbyte>, IEquatable<sbyte> {
		public const sbyte MinValue = -128;
		public const sbyte MaxValue = 127;

		internal sbyte m_value;

		public override bool Equals(object obj) {
			return (obj is sbyte) && ((sbyte)obj).m_value == this.m_value;
		}

		public override int GetHashCode() {
			return (int)this.m_value;
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
			if (!(obj is int)) {
				throw new ArgumentException();
			}
			return this.CompareTo((sbyte)obj);
		}

		#endregion

		#region IComparable<sbyte> Members

		public int CompareTo(sbyte x) {
			return (this.m_value > x) ? 1 : ((this.m_value < x) ? -1 : 0);
		}

		#endregion

		#region IEquatable<sbyte> Members

		public bool Equals(sbyte x) {
			return this.m_value == x;
		}

		#endregion

	}
}

#endif