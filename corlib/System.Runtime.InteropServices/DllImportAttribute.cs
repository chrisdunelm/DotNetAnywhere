#if !LOCALTEST

namespace System.Runtime.InteropServices {
	public class DllImportAttribute : Attribute {

		// Sync with C code
		private string dllName;
		public bool BestFitMapping;
		public CallingConvention CallingConvention;
		public CharSet CharSet;
		public string EntryPoint;
		public bool ExactSpelling;
		public bool PreserveSig;
		public bool SetLastError;
		public bool ThrowOnUnmappableChar;

		public DllImportAttribute(string dllName) {
			this.dllName = dllName;
		}

		public string Value {
			get {
				return this.dllName;
			}
		}

	}
}

#endif
