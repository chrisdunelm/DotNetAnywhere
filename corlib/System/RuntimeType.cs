#if !LOCALTEST

using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.Generic;

namespace System {

	class RuntimeType : Type {

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private RuntimeType GetNestingParentType();

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private RuntimeType Internal_GetGenericTypeDefinition();

		extern public override Type BaseType {
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern override bool IsEnum {
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		extern public override string Namespace {
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		extern public override string Name {
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public override string FullName {
			get {
				StringBuilder ret = new StringBuilder(32);
				ret.Append(this.Namespace);
				ret.Append('.');
				RuntimeType nestingParentType = this.GetNestingParentType();
				if (nestingParentType != null) {
					List<Type> nestingParents = new List<Type>();
					nestingParents.Add(nestingParentType);
					nestingParentType = nestingParentType.GetNestingParentType();
					while (nestingParentType != null) {
						nestingParents.Add(nestingParentType);
						nestingParentType = nestingParentType.GetNestingParentType();
					}
					for (int ofs = nestingParents.Count - 1; ofs >= 0; ofs--) {
						ret.Append(nestingParents[ofs].Name);
						ret.Append('+');
					}
				}
				ret.Append(this.Name);
				return ret.ToString();
			}
		}

		extern public override bool IsGenericType {
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public override Type GetGenericTypeDefinition() {
			if (!this.IsGenericType) {
				throw new InvalidOperationException("This is not a generic type");
			}
			return this.Internal_GetGenericTypeDefinition();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public override Type[] GetGenericArguments();

	}

}

#endif
