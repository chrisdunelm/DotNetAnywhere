#if !LOCALTEST

namespace System {
	public class NotSupportedException : SystemException {

		public NotSupportedException() : base("Operation is not supported.") { }

		public NotSupportedException(string message) : base(message) { }

		public NotSupportedException(string message, Exception innerException) : base(message, innerException) { }

	}
}

#endif
