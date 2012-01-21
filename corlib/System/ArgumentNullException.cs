#if !LOCALTEST

namespace System {
	public class ArgumentNullException : ArgumentException {

		public ArgumentNullException() : base("Argument cannot be null.") { }

		public ArgumentNullException(string paramName) : base("Argument cannot be null.", paramName) { }

		public ArgumentNullException(string paramName, string message) : base(message, paramName) { }

		public ArgumentNullException(string message, Exception innerException)
			: base(message, innerException) {
		}
	
	}
}

#endif