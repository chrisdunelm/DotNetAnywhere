#if !LOCALTEST

using System;

namespace System {
	public sealed class AttributeUsageAttribute : Attribute {

		AttributeTargets validOn;
		bool allowMultiple = false;
		bool inherited = true;

		public AttributeUsageAttribute(AttributeTargets validOn) {
			this.validOn = validOn;
		}

		public bool AllowMultiple {
			get {
				return allowMultiple;
			}
			set {
				allowMultiple = value;
			}
		}

		public bool Inherited {
			get {
				return inherited;
			}
			set {
				inherited = value;
			}
		}

		public AttributeTargets ValidOn {
			get {
				return validOn;
			}
		}

	}
}

#endif
