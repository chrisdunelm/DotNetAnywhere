#if !LOCALTEST

using System.Globalization;
namespace System {
	public struct UInt16:IFormattable,IComparable,IComparable<ushort>,IEquatable<ushort> {
		public const ushort MaxValue = 0xffff;
		public const ushort MinValue = 0;

		internal ushort m_value;

		public override bool Equals(object obj) {
			return (obj is ushort) && ((ushort)obj).m_value == this.m_value;
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
			if (!(obj is ushort)) {
				throw new ArgumentException();
			}
			return this.CompareTo((ushort)obj);
		}

		#endregion

		#region IComparable<ushort> Members

		public int CompareTo(ushort x) {
			return (this.m_value > x) ? 1 : ((this.m_value < x) ? -1 : 0);
		}

		#endregion

		#region IEquatable<ushort> Members

		public bool Equals(ushort x) {
			return this.m_value == x;
		}

		#endregion
	
	}
}

#endif
