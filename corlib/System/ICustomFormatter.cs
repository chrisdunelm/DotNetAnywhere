#if !LOCALTEST

namespace System {
	public interface ICustomFormatter {

		string Format(string format, object arg, IFormatProvider formatProvider);

	}
}

#endif
