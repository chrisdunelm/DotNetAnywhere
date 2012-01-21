#if !LOCALTEST

namespace System.Text {

	public abstract class Encoding : ICloneable {

		private static object lockObj = new object();

		#region Static common Encoding properties

		private static Encoding unicodeEncoding = null;
		private static Encoding utf8Encoding = null;
		private static Encoding utf8UnmarkedEncoding = null;

		public static Encoding Unicode {
			get {
				if (unicodeEncoding == null) {
					lock (lockObj) {
						if (unicodeEncoding == null) {
							unicodeEncoding = new UnicodeEncoding(true, false);
						}
					}
				}
				return unicodeEncoding;
			}
		}

		public static Encoding UTF8 {
			get {
				if (utf8Encoding == null) {
					lock (lockObj) {
						if (utf8Encoding == null) {
							utf8Encoding = new UTF8Encoding(true);
						}
					}
				}
				return utf8Encoding;
			}
		}

		public static Encoding UTF8Unmarked {
			get {
				if (utf8UnmarkedEncoding == null) {
					lock (lockObj) {
						if (utf8UnmarkedEncoding == null) {
							utf8UnmarkedEncoding = new UTF8Encoding(false);
						}
					}
				}
				return utf8UnmarkedEncoding;
			}
		}

		#endregion

		public virtual byte[] GetPreamble() {
			return new byte[0];
		}

		public abstract int GetMaxCharCount(int byteCount);

		public abstract Decoder GetDecoder();


		#region ICloneable Members

		public object Clone() {
			return (Encoding)object.Clone(this);
		}

		#endregion
	}

}

#endif
