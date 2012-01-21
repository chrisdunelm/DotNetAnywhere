#if !LOCALTEST

using System;
using System.Collections.Generic;
using System.Text;

namespace System.Reflection {
	public abstract class MemberInfo {

		protected MemberInfo() {
		}

		public abstract string Name { get;}

	}
}

#endif
