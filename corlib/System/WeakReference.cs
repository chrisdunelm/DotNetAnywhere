#if !LOCALTEST

using System.Runtime.CompilerServices;
namespace System {
	public class WeakReference {

		// These MUST agree with the definition in System.WeakReference.c
#pragma warning disable 0169, 0649, 0414
        private object target = null;
		private bool trackResurrection;
		private IntPtr nextWeakRef;
#pragma warning restore 0169, 0649, 0414

        public WeakReference(object target) : this(target, false) { }

		public WeakReference(object target, bool trackResurrection) {
			this.trackResurrection = trackResurrection;
			this.Target = target;
		}

		~WeakReference() {
			this.Target = null;
		}

		public bool TrackResurrection {
			get {
				return this.trackResurrection;
			}
		}

		extern public object Target {
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public bool IsAlive {
			get {
				return (this.Target != null);
			}
		}
	}
}

#endif
