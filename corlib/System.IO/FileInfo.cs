#if !LOCALTEST

namespace System.IO {
	public sealed class FileInfo : FileSystemInfo {

		private bool exists;

		public FileInfo(string path) {
			CheckPath(path);

			base.originalPath = path;
			base.fullPath = Path.GetFullPath(path);
		}

		public override bool Exists {
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public override string Name {
			get {
				return Path.GetFileName(base.fullPath);
			}
		}

	}
}

#endif
