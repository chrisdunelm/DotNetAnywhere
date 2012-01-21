#if !LOCALTEST

using System.Runtime.CompilerServices;
using System.Globalization;

namespace System.Threading {
	public sealed class Thread {

		// These member vars MUST be synced with C code.
		private int managedThreadID = 0;
		private MulticastDelegate threadStart = null;
		private object param = null;
		private ThreadState threadState = ThreadState.Unstarted;

		private CultureInfo currentCulture;
		
		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public static void Sleep(int millisecondsTimeout);

		extern public static Thread CurrentThread {
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public Thread(ThreadStart threadStart);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public Thread(ParameterizedThreadStart threadStart);

		public int ManagedThreadId {
			get {
				return this.managedThreadID;
			}
		}

		public ThreadState ThreadState {
			get {
				return this.threadState;
			}
		}

		public bool IsBackground {
			get {
				return ((this.threadState & ThreadState.Background) != 0);
			}
			set {
				if (value) {
					this.threadState |= ThreadState.Background;
				} else {
					this.threadState &= ~ThreadState.Background;
				}
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public void Start();

		public void Start(object param) {
			this.param = param;
			this.Start();
		}

		public CultureInfo CurrentCulture {
			get {
				if (this.currentCulture == null) {
					this.currentCulture = CultureInfo.InvariantCulture;
				}
				return this.currentCulture;
			}
			set {
				if (value == null) {
					throw new ArgumentNullException();
				}
				this.currentCulture = value;
			}
		}
	}
}

#endif
