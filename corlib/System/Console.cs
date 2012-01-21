#if !LOCALTEST

using System.Runtime.CompilerServices;

namespace System {
	public static class Console {

		static byte[] ascii2ConsoleKey_Map = new byte[128] {
			0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 9   , 0x00, 0x00, 0x00, 13  , 0x00, 0x00, 
			0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 27  , 0x00, 0x00, 0x00, 0x00, 
			32  , 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
			48  , 49  , 50  , 51  , 52  , 53  , 54  , 55  , 56  , 57  , 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
			0x00, 65  , 66  , 67  , 68  , 69  , 70  , 71  , 72  , 73  , 74  , 75  , 76  , 77  , 78  , 79  , 
			80  , 81  , 82  , 83  , 84  , 85  , 86  , 87  , 88  , 89  , 90  , 0x00, 0x00, 0x00, 0x00, 0x00, 
			0x00, 65  , 66  , 67  , 68  , 69  , 70  , 71  , 72  , 73  , 74  , 75  , 76  , 77  , 78  , 79  , 
			80  , 81  , 82  , 83  , 84  , 85  , 86  , 87  , 88  , 89  , 90  , 0x00, 0x00, 0x00, 0x00, 0x00, 
		};

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public static void Write(string s);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private static int Internal_ReadKey();

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private static bool Internal_KeyAvailable();

		#region Write Methods

		public static void Write(object value) {
			if (value != null) {
				Write(value.ToString());
			}
		}

		public static void Write(char value) {
			Write(value.ToString());
		}

		public static void Write(string format, params object[] args) {
			Write(string.Format(format, args));
		}

		public static void Write(string format, object obj1) {
			Write(format, new object[] { obj1 });
		}

		public static void Write(string format, object obj1, object obj2) {
			Write(format, new object[] { obj1, obj2 });
		}

		public static void Write(string format, object obj1, object obj2, object obj3) {
			Write(format, new object[] { obj1, obj2, obj3 });
		}

		public static void Write(string format, object obj1, object obj2, object obj3, object obj4) {
			Write(format, new object[] { obj1, obj2, obj3, obj4 });
		}

		#endregion

		#region WriteLine Methods

		public static void WriteLine(string value) {
			Write(value);
			Write(Environment.NewLine);
		}

		public static void WriteLine() {
			WriteLine(string.Empty);
		}

		public static void WriteLine(bool value) {
			WriteLine(value.ToString());
		}

		public static void WriteLine(sbyte value) {
			WriteLine(value.ToString());
		}

		public static void WriteLine(byte value) {
			WriteLine(value.ToString());
		}

		public static void WriteLine(int value) {
			WriteLine(value.ToString());
		}

		public static void WriteLine(uint value) {
			WriteLine(value.ToString());
		}

		public static void WriteLine(long value) {
			WriteLine(value.ToString());
		}

		public static void WriteLine(ulong value) {
			WriteLine(value.ToString());
		}

		public static void WriteLine(char value) {
			WriteLine(value.ToString());
		}

		public static void WriteLine(float value) {
			WriteLine(value.ToString());
		}

		public static void WriteLine(double value) {
			WriteLine(value.ToString());
		}

		public static void WriteLine(object value) {
			if (value == null) {
				WriteLine();
			} else {
				WriteLine(value.ToString());
			}
		}

		public static void WriteLine(string format, params object[] args) {
			WriteLine(string.Format(format, args));
		}

		public static void WriteLine(string format, object obj1) {
			WriteLine(format, new object[] { obj1 });
		}

		public static void WriteLine(string format, object obj1, object obj2) {
			WriteLine(format, new object[] { obj1, obj2 });
		}

		public static void WriteLine(string format, object obj1, object obj2, object obj3) {
			WriteLine(format, new object[] { obj1, obj2, obj3 });
		}

		public static void WriteLine(string format, object obj1, object obj2, object obj3, object obj4) {
			WriteLine(format, new object[] { obj1, obj2, obj3, obj4 });
		}

		#endregion

		#region ReadKey Methods

		public static bool KeyAvailable {
			get {
				return Internal_KeyAvailable();
			}
		}

		public static ConsoleKeyInfo ReadKey() {
			return ReadKey(false);
		}

		public static ConsoleKeyInfo ReadKey(bool intercept) {
			int key = Internal_ReadKey();
			char c = (char)key;
			if (!intercept) {
				Write(c);
			}
			ConsoleKey k;
			if (key < 128) {
				k = (ConsoleKey)ascii2ConsoleKey_Map[key];
			} else {
				k = ConsoleKey.Unknown;
			}
			return new ConsoleKeyInfo(c, k, false, false, false);
		}

		#endregion

	}
}

#endif
