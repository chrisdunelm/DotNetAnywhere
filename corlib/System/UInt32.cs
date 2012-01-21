#if !LOCALTEST

using System.Runtime.CompilerServices;
using System.Globalization;
namespace System {
	public struct UInt32 : IFormattable, IComparable, IComparable<uint>, IEquatable<uint> {
		public const uint MaxValue = 0xffffffff;
		public const uint MinValue = 0;

		internal uint m_value;

		public override bool Equals(object obj) {
			return (obj is uint) && ((uint)obj).m_value == this.m_value;
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
			return ToString(format, null);
		}

		public string ToString(string format, IFormatProvider formatProvider) {
			NumberFormatInfo nfi = NumberFormatInfo.GetInstance(formatProvider);
			return NumberFormatter.NumberToString(format, m_value, nfi);
		}

		#endregion

		#region IComparable Members

		public int CompareTo(object obj) {
			if (obj == null) {
				return 1;
			}
			if (!(obj is uint)) {
				throw new ArgumentException();
			}
			return this.CompareTo((uint)obj);
		}

		#endregion

		#region IComparable<uint> Members

		public int CompareTo(uint x) {
			return (this.m_value > x) ? 1 : ((this.m_value < x) ? -1 : 0);
		}

		#endregion

		#region IEquatable<uint> Members

		public bool Equals(uint x) {
			return this.m_value == x;
		}

		#endregion

	}
}

#endif
