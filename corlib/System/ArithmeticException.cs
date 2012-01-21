#if !LOCALTEST

namespace System {
	public class ArithmeticException : SystemException {

		public ArithmeticException() : base("Overflow or underflow in the arithmetic operation.") { }
		public ArithmeticException(string msg) : base(msg) { }

	}
}

#endif
