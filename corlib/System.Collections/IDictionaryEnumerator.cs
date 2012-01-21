#if !LOCALTEST

using System;

namespace System.Collections {
	public interface IDictionaryEnumerator : IEnumerator {

		DictionaryEntry Entry { get; }
		object Key { get; }
		object Value { get; }

	}
}
#endif
