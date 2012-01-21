#if !LOCALTEST

using System;

namespace System.Runtime.InteropServices {

	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class OutAttribute : Attribute {
		public OutAttribute() { }
	}

}

#endif
