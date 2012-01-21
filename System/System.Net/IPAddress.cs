// Copyright (c) 2009 DotNetAnywhere
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace System.Net {
	public class IPAddress {

		public static readonly IPAddress Any = new IPAddress(0);
		public static readonly IPAddress Broadcast = IPAddress.Parse("255.255.255.255");
		public static readonly IPAddress Loopback = IPAddress.Parse("127.0.0.1");
		public static readonly IPAddress None = IPAddress.Parse("255.255.255.255");

		public static IPAddress Parse(string ip) {
			IPAddress addr;
			if (TryParse(ip, out addr)) {
				return addr;
			}
			throw new FormatException("An invalid IP address was specified");
		}

		public static bool TryParse(string ip, out IPAddress address) {
			// Only handle IPv4
			if (ip == null) {
				throw new ArgumentNullException("ip");
			}
			if (ip.Length == 0 || ip == " ") {
				address = new IPAddress(0);
				return true;
			}
			string[] parts = ip.Split('.');
			if (parts.Length != 4) {
				address = null;
				return false;
			}
			uint a = 0;
			for (int i = 0; i < 4; i++) {
				int val;
				if (!int.TryParse(parts[i], out val)) {
					address = null;
					return false;
				}
				a |= ((uint)val) << (i << 3);
			}
			address = new IPAddress((long)a);
			return true;
		}

		private uint ip4Address;
		private AddressFamily family = AddressFamily.InterNetwork;

		public IPAddress(long addr) {
			this.ip4Address = (uint)addr;
		}

		public AddressFamily AddressFamily {
			get {
				return this.family;
			}
		}

		internal uint InternalIPv4Address {
			get { return this.ip4Address; }
		}

		public override int GetHashCode() {
			return (int)this.ip4Address;
		}

		public override bool Equals(object obj) {
			IPAddress a = obj as IPAddress;
			return a != null && a.ip4Address == this.ip4Address;
		}

		public override string ToString() {
			return string.Format("{0}.{1}.{2}.{3}",
				this.ip4Address & 0xff, (this.ip4Address >> 8) & 0xff,
				(this.ip4Address >> 16) & 0xff, this.ip4Address >> 24);
		}
	}
}
