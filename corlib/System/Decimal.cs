#if !LOCALTEST

namespace System {
	public struct Decimal {

		// internal representation of decimal
#pragma warning disable 0169, 0649
        private uint flags;
		private uint hi;
		private uint lo;
		private uint mid;
#pragma warning restore 0169, 0649

        public static int[] GetBits(Decimal d) {
			return new int[] { 0, 0, 0, 0 };
		}

	}
}

#endif