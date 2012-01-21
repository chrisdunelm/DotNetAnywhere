#if !LOCALTEST
using System;

namespace System.Collections {
	public struct DictionaryEntry {

		private object key;
		private object val;

		public DictionaryEntry(object key, object value) {
			if (key == null) {
				throw new ArgumentNullException("key");
			}

			this.key = key;
			val = value;
		}

		public object Key {
			get {
				return key;
			}
			set {
				if (value == null) {
					throw new ArgumentNullException("value");
				}
				key = value;
			}
		}

		public object Value {
			get {
				return val;
			}
			set {
				val = value;
			}
		}

	}
}

#endif
