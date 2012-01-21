#if !LOCALTEST

using System;

namespace System.Collections.Generic {
	public class KeyNotFoundException : SystemException {

		public KeyNotFoundException() : base("The given key was not present in the dictionary.") { }

		public KeyNotFoundException(string message) : base(message) { }

		public KeyNotFoundException(string message, Exception inner) : base(message, inner) { }

	}
}
#endif
