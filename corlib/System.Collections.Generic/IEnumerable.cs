#if !LOCALTEST

using System;
using System.Collections;

namespace System.Collections.Generic {

	public interface IEnumerable<T> : IEnumerable {

		new IEnumerator<T> GetEnumerator();

	}

}
#endif
