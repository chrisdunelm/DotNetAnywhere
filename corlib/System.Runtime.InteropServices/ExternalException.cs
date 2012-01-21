#if !LOCALTEST

namespace System.Runtime.InteropServices {
	public class ExternalException : SystemException {

		public ExternalException() : base("External exception") { }

		public ExternalException(string msg) : base(msg) { }

		public ExternalException(string msg, Exception inner) : base(msg, inner) { }

		public ExternalException(string msg, int err)
			: base(msg) {
			base.HResult = err;
		}

		public virtual int ErrorCode {
			get {
				return base.HResult;
			}
		}

	}
}

#endif
