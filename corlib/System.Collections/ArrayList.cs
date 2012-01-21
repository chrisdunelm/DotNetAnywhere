using System;
using System.Collections.Generic;

#if LOCALTEST
using System.Collections;
namespace System_.Collections {
#else
namespace System.Collections {
#endif
	public class ArrayList : IList, ICollection, IEnumerable, ICloneable {

		private List<object> list;

		public ArrayList() {
			this.list = new List<object>();
		}

		public ArrayList(ICollection c) {
			if (c == null) {
				throw new ArgumentNullException();
			}
			this.list = new List<object>(c.Count);
			foreach (object o in c) {
				this.list.Add(o);
			}
		}

		public ArrayList(int capacity) {
			this.list = new List<object>(capacity);
		}

		public virtual int Add(object value) {
			return this.list.Add(value);
		}

		public virtual void AddRange(ICollection c) {
			if (c == null) {
				throw new ArgumentNullException();
			}
			foreach (object o in c) {
				this.list.Add(o);
			}
		}

		public virtual void Clear() {
			this.list.Clear();
		}

		public virtual object Clone() {
			throw new NotImplementedException();
		}

		public virtual bool Contains(object item) {
			return this.list.Contains(item);
		}

		public virtual void CopyTo(Array array) {
			this.list.CopyTo(array, 0);
		}

		public virtual void CopyTo(Array array, int arrayIndex) {
			throw new NotImplementedException();
		}

		public virtual void CopyTo(int index, Array array, int arrayIndex, int count){
			throw new NotImplementedException();
		}

		public virtual IEnumerator GetEnumerator() {
			return this.list.GetEnumerator();
		}

		public virtual IEnumerable GetEnumerator(int index, int count) {
			throw new NotImplementedException();
		}

		public virtual ArrayList GetRange(int index, int count) {
			throw new NotImplementedException();
		}

		public virtual int IndexOf(object value) {
			return this.list.IndexOf(value);
		}

		public virtual int IndexOf(object value, int startIndex) {
			return this.list.IndexOf(value, startIndex);
		}

		public virtual int IndexOf(object value, int startIndex, int count) {
			return this.list.IndexOf(value, startIndex, count);
		}

		public virtual void Insert(int index, object value) {
			this.list.Insert(index, value);
		}

		public virtual void InsertRange(int index, ICollection c) {
			List<object> insert = new List<object>(c.Count);
			foreach (object o in c) {
				insert.Add(o);
			}
			this.list.InsertRange(index, insert);
		}

		public virtual int LastIndexOf(object value) {
			throw new NotImplementedException();
		}

		public virtual int LastIndexOf(object value, int startIndex) {
			throw new NotImplementedException();
		}

		public virtual int LastIndexOf(object value, int startIndex, int count) {
			throw new NotImplementedException();
		}

		public virtual void Remove(object obj) {
			this.list.Remove(obj);
		}

		public virtual void RemoveAt(int index) {
			this.list.RemoveAt(index);
		}

		public virtual void RemoveRange(int index, int count) {
			throw new NotImplementedException();
		}

		public virtual void Reverse() {
			throw new NotImplementedException();
		}

		public virtual void Reverse(int index, int count) {
			throw new NotImplementedException();
		}

		public virtual void SetRange(int index, ICollection c) {
			throw new NotImplementedException();
		}

		public virtual void Sort() {
			throw new NotImplementedException();
		}

		public virtual void Sort(IComparer comparer) {
			throw new NotImplementedException();
		}

		public virtual void Sort(int index, int count, IComparer comparer) {
			throw new NotImplementedException();
		}

		public virtual object[] ToArray() {
			throw new NotImplementedException();
		}

		public virtual Array ToArray(Type type) {
			throw new NotImplementedException();
		}

		public virtual void TromToSize() {
			throw new NotImplementedException();
		}

		public virtual int Capacity {
			get {
				return this.list.Capacity;
			}
			set {
				this.list.Capacity = value;
			}
		}

		public virtual int Count {
			get {
				return this.list.Count;
			}
		}

		public virtual bool IsFixedSize {
			get {
				return false;
			}
		}

		public virtual bool IsReadOnly {
			get {
				return false;
			}
		}

		public virtual bool IsSynchronized {
			get {
				return false;
			}
		}

		public virtual object this[int index] {
			get {
				return this.list[index];
			}
			set {
				this.list[index] = value;
			}
		}

		public virtual object SyncRoot {
			get {
				return this;
			}
		}

	}
}
