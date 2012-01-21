#if !LOCALTEST

namespace System {
	public struct Decimal {

		// internal representation of decimal
		private uint flags;
		private uint hi;
		private uint lo;
		private uint mid;

		public static int[] GetBits(Decimal d) {
			return new int[] { 0, 0, 0, 0 };
		}

	}
}

#endif