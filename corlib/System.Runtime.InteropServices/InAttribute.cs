#if !LOCALTEST

namespace System.Runtime.InteropServices {

	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class InAttribute : Attribute {
		public InAttribute() { }
	}

}

#endif
