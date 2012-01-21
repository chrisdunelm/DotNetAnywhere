#if !LOCALTEST

using System;

namespace System.Collections.Generic {

	public interface IEnumerator<T> : IDisposable, IEnumerator {

		new T Current {
			get;
		}

	}

}
#endif
