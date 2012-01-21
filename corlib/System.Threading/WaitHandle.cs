#if !LOCALTEST

using System;

namespace System.Threading {
	public abstract class WaitHandle : MarshalByRefObject, IDisposable {

		public void Dispose() {
		}

	}
}

#endif
