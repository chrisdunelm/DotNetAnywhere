#if !LOCALTEST

namespace System {
	public class ArgumentException : SystemException {

		private string paramName;

		public ArgumentException() : base("An invalid argument was specified.") { }

		public ArgumentException(string message) : base(message) { }

		public ArgumentException(string message, Exception innerException) : base(message, innerException) { }

		public ArgumentException(string message, string paramName)
			: base(message) {

			this.paramName = paramName;
		}

		public virtual string ParamName {
			get {
				return paramName;
			}
		}

		public override string Message {
			get {
				string baseMsg = base.Message;
				if (baseMsg == null) {
					baseMsg = "An invalid argument was specified.";
				}
				if (paramName == null) {
					return baseMsg;
				} else {
					return baseMsg + Environment.NewLine + "Parameter name: " + paramName;
				}
			}
		}

	}
}

#endif
