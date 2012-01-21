#if !LOCALTEST

namespace System {
	public class NullReferenceException : SystemException {

		public NullReferenceException(string msg) : base(msg) { }

		public NullReferenceException()
			: base("A null value was found where an object instance was required.") {
		}
	}
}

#endif
