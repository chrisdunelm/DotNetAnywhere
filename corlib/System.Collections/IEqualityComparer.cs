#if !LOCALTEST

using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections {

	public interface IEqualityComparer {

		bool Equals(object x, object y);
		int GetHashCode(object obj);

	}

}

#endif
