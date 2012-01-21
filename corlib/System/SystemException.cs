#if !LOCALTEST

namespace System {
	public class SystemException : Exception {

		public SystemException() : base("A SystemException has occured.") { }

		public SystemException(string message) : base(message) { }

		public SystemException(string message, Exception innerException) : base(message, innerException) { }

	}
}

#endif
