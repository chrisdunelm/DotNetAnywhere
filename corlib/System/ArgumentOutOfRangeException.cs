#if !LOCALTEST

namespace System {
	public class ArgumentOutOfRangeException : ArgumentException {

		public ArgumentOutOfRangeException() : base("Argument is out of range.") { }

		public ArgumentOutOfRangeException(string paramName) : base("Argument is out of range.", paramName) { }

		public ArgumentOutOfRangeException(string paramName, string msg) : base(msg, paramName) { }
	}
}

#endif
