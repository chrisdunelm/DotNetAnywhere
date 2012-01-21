#if !LOCALTEST

using System;

namespace System.Collections.Generic {

	public interface IList<T> : ICollection<T> {

		// DO NOT change the order of these method definitions.
		// The auto-generated IList<T> interface on Array relies on the order.

		int IndexOf(T item);

		void Insert(int index, T item);

		void RemoveAt(int index);

		T this[int index] {
			get;
			set;
		}

	}

}
#endif
