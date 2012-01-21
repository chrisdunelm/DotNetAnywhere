#if !LOCALTEST

namespace System.Text {
	public abstract class Decoder {
		public virtual int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex) {
			return GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
		}

		public virtual int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush) {
			if (bytes == null || chars == null) {
				throw new ArgumentNullException();
			}
			if (byteIndex < 0 || byteCount < 0 || charIndex < 0 ||
				byteIndex + byteCount > bytes.Length || charIndex >= chars.Length) {
				throw new ArgumentOutOfRangeException();
			}
			return GetCharsSafe(bytes, byteIndex, byteCount, chars, charIndex, flush);
		}

		protected abstract int GetCharsSafe(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush);
	}
}

#endif
