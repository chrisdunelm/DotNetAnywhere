#if !LOCALTEST

using System.Runtime.CompilerServices;
using System.Reflection;

namespace System {
	public abstract class Type : MemberInfo {

		public static readonly Type[] EmptyTypes = new Type[0];

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public static Type GetTypeFromHandle(RuntimeTypeHandle handle);

		public abstract Type BaseType {
			get;
		}

		public abstract bool IsEnum {
			get;
		}

		public abstract string Namespace {
			get;
		}

		public abstract string FullName {
			get;
		}

		public abstract bool IsGenericType {
			get;
		}

		public abstract Type GetGenericTypeDefinition();

		public abstract Type[] GetGenericArguments();

		extern public bool IsValueType {
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public override string ToString() {
			return this.FullName;
		}
	}
}

#endif
