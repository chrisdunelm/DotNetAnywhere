#if !LOCALTEST

namespace System {
	public class ArrayTypeMismatchException : SystemException {

		public ArrayTypeMismatchException() : base("Source array type cannot be assigned to destination array type.") { }
		public ArrayTypeMismatchException(string msg) : base(msg) { }

	}
}

#endif
