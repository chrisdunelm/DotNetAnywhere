#if !LOCALTEST

using System.Runtime.CompilerServices;
using System.IO;
namespace System {
	public static class Environment {

		public static string NewLine {
			get {
				return (Platform == PlatformID.Unix) ? "\n" : "\r\n";
			}
		}

		extern public static int TickCount {
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static string CultureDirectory {
			get {
				return string.Format(".{0}Cultures", Path.DirectorySeparatorStr);
			}
		}

		private static OperatingSystem os = null;

		internal static extern PlatformID Platform {
			[MethodImplAttribute(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		internal static extern string GetOSVersionString();

		public static OperatingSystem OSVersion {
			get {
				if (os == null) {
					Version v = Version.CreateFromString(GetOSVersionString());
					PlatformID p = Platform;
					os = new OperatingSystem(p, v);
				}
				return os;
			}
		}

		internal static bool IsRunningOnWindows {
			get {
				return Platform != PlatformID.Unix;
			}
		}

	}
}

#endif
