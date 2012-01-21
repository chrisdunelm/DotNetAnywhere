#if !LOCALTEST

namespace System {
	public sealed class OperatingSystem : ICloneable {

		private PlatformID platformID;
		private Version version;

		public OperatingSystem(PlatformID platformID, Version version) {
			if (version == null) {
				throw new ArgumentNullException("version");
			}
			this.platformID = platformID;
			this.version = version;
		}

		public PlatformID Platform {
			get {
				return this.platformID;
			}
		}

		public Version Version {
			get {
				return this.version;
			}
		}

		public string ServicePack {
			get {
				return String.Empty;
			}
		}

		public string VersionString {
			get {
				return ToString();
			}
		}

		public override string ToString() {
			string str;

			switch (this.platformID) {
				case PlatformID.Win32NT:
					str = "Microsoft Windows NT";
					break;
				case PlatformID.Win32S:
					str = "Microsoft Win32S";
					break;
				case PlatformID.Win32Windows:
					str = "Microsoft Windows 98";
					break;
				case PlatformID.WinCE:
					str = "Microsoft Windows CE";
					break;
				case PlatformID.Unix:
					str = "Unix";
					break;
				default:
					str = "<unknown>";
					break;
			}

			return str + " " + this.version.ToString() + " (InterNet2)";
		}

		#region ICloneable Members

		public object Clone() {
			return (OperatingSystem)object.Clone(this);
		}

		#endregion
	}
}

#endif
