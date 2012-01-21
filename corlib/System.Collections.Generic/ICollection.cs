#if !LOCALTEST

using System;

namespace System.Collections.Generic {

	public interface ICollection<T> : IEnumerable<T> {

		// DO NOT change the order of these method definitions.
		// The auto-generated ICollection<T> interface on Array relies on the order.

		int Count {
			get;
		}

		bool IsReadOnly {
			get;
		}

		void Add(T item);

		void Clear();

		bool Contains(T item);

		void CopyTo(T[] array, int arrayIndex);

		bool Remove(T item);

	}

}
#endif
