#if !LOCALTEST

using System;

namespace System.Collections.Generic {
	public struct KeyValuePair<TKey, TValue> {

		private TKey key;
		private TValue value;

		public KeyValuePair(TKey Key, TValue Value) {
			this.key = Key;
			this.value = Value;
		}

		public TKey Key {
			get {
				return key;
			}
		}

		public TValue Value {
			get {
				return value;
			}
		}

		public override string ToString() {
			return "[" + (Key != null ? Key.ToString() : string.Empty) +
				", " + (Value != null ? Value.ToString() : string.Empty) + "]";
		}

	}
}
#endif
