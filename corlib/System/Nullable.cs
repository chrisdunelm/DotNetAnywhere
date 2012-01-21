#if !LOCALTEST

namespace System {

	public static class Nullable {
		public static int Compare<T>(Nullable<T> left, Nullable<T> right) where T : struct {
			if (!left.HasValue && !right.HasValue) {
				return 0;
			}
			if (!left.HasValue) {
				return -1;
			}
			if (!right.HasValue) {
				return 1;
			}
			IComparable iComp = left.Value as IComparable;
			if (iComp == null) {
				throw new ArgumentException("At least one object must implement IComparable");
			}
			return iComp.CompareTo(right.Value);
		}

		public static bool Equals<T>(Nullable<T> value1, Nullable<T> value2) where T : struct {
			return value1.Equals(value2);
		}

		public static Type GetUnderlyingType(Type nullableType) {
			if (nullableType == null) {
				throw new ArgumentNullException("nullableType");
			}
			if (nullableType.IsGenericType && nullableType.GetGenericTypeDefinition() == typeof(Nullable<>)) {
				return nullableType.GetGenericArguments()[0];
			} else {
				return null;
			}
		}
	}

	public struct Nullable<T> where T : struct {

		// Note the order of these must not be changed, as the interpreter assumes this order
		private bool hasValue;
		private T value;

		public Nullable(T value) {
			this.hasValue = true;
			this.value = value;
		}

		public bool HasValue {
			get {
				return this.hasValue;
			}
		}

		public T Value {
			get {
				if (!this.hasValue) {
					throw new InvalidOperationException("Nullable object must have a value");
				}
				return this.value;
			}
		}

		public T GetValueOrDefault() {
			return this.GetValueOrDefault(default(T));
		}

		public T GetValueOrDefault(T def) {
			return this.hasValue ? this.value : def;
		}

		public override bool Equals(object obj) {
			if (obj == null) {
				return !this.hasValue;
			}
			Nullable<T> other = obj as Nullable<T>;
			if (other != null) {
				if (this.hasValue && other.hasValue) {
					// The values are value-types, so cannot be null
					return this.value.Equals(other.value);
				} else {
					return this.hasValue == other.hasValue;
				}
			}
			return false;
		}

		public override int GetHashCode() {
			if (this.hasValue) {
				return this.value.GetHashCode();
			} else {
				return 0;
			}
		}

		public override string ToString() {
			if (this.hasValue) {
				return this.value.ToString();
			} else {
				return string.Empty;
			}
		}
	}
}

#endif
