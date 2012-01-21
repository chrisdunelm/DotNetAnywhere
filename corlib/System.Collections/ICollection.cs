#if !LOCALTEST

using System;

namespace System.Collections {
	public interface ICollection : IEnumerable {

		int Count { get; }

		bool IsSynchronized { get; }

		object SyncRoot { get; }

		void CopyTo(Array array, int index);

	}
}
#endif
