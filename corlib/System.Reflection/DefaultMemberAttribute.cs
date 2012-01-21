#if !LOCALTEST

using System;

namespace System.Reflection {

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class DefaultMemberAttribute : Attribute {

		private string memberName;

		public DefaultMemberAttribute(string memberName) {
			this.memberName = memberName;
		}

		public string MemberName {
			get {
				return this.memberName;
			}
		}
	}
}

#endif
