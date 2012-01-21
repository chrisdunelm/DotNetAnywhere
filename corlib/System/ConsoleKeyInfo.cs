#if !LOCALTEST

namespace System {
	public struct ConsoleKeyInfo {
		internal static ConsoleKeyInfo Empty = new ConsoleKeyInfo('\0', 0, false, false, false);
		ConsoleKey key;
		char keychar;
		ConsoleModifiers modifiers;

		public ConsoleKeyInfo(char keyChar, ConsoleKey key, bool shift, bool alt, bool control) {
			this.key = key;
			this.keychar = keyChar;
			modifiers = 0;
			SetModifiers(shift, alt, control);
		}

		internal ConsoleKeyInfo(ConsoleKeyInfo other) {
			this.key = other.key;
			this.keychar = other.keychar;
			this.modifiers = other.modifiers;
		}

		internal void SetKey(ConsoleKey key) {
			this.key = key;
		}

		internal void SetKeyChar(char keyChar) {
			this.keychar = keyChar;
		}

		internal void SetModifiers(bool shift, bool alt, bool control) {
			this.modifiers = (shift) ? ConsoleModifiers.Shift : 0;
			this.modifiers |= (alt) ? ConsoleModifiers.Alt : 0;
			this.modifiers |= (control) ? ConsoleModifiers.Control : 0;
		}

		public ConsoleKey Key {
			get { return key; }
		}

		public char KeyChar {
			get { return keychar; }
		}

		public ConsoleModifiers Modifiers {
			get { return modifiers; }
		}

		public override bool Equals(object o) {
			if (!(o is ConsoleKeyInfo))
				return false;
			return Equals((ConsoleKeyInfo)o);
		}

		public bool Equals(ConsoleKeyInfo o) {
			return key == o.key && o.keychar == keychar && o.modifiers == modifiers;
		}

		public override int GetHashCode() {
			return key.GetHashCode() ^ keychar.GetHashCode() ^ modifiers.GetHashCode();
		}
	}
}

#endif
