#if !LOCALTEST

namespace System {
	public interface IFormattable {

		string ToString(string format, IFormatProvider formatProvider);

	}
}

#endif
