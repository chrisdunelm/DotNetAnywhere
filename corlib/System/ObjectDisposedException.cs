#if !LOCALTEST

namespace System {
	public class ObjectDisposedException : InvalidOperationException {

		private string objectName;

		public ObjectDisposedException(string objectName)
			: base("The object was used after being disposed") {
			this.objectName = objectName;
		}

		public ObjectDisposedException(string objectName, string msg)
			: base(msg) {
			this.objectName = objectName;
		}

		public string ObjectName {
			get {
				return this.objectName;
			}
		}

	}
}

#endif
