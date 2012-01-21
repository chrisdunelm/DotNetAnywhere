#if !LOCALTEST

namespace System {
	public class NotImplementedException : SystemException {
		public NotImplementedException() : base("The requested feature is not implemented.") { }

		public NotImplementedException(string message) : base(message) { }

		public NotImplementedException(string message, Exception inner) : base(message, inner) { }
	}
}

#endif
