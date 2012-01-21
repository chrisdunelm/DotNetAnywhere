using System;
using System.Collections.Generic;
using System.Text;

namespace System {
	public class MulticastNotSupportedException : SystemException {
		public MulticastNotSupportedException()
			: base("This operation cannot be performed with the specified delagates.") {
		}

		public MulticastNotSupportedException(string msg) : base(msg) { }
	}
}
