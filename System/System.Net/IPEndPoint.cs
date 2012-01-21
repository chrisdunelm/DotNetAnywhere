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
	public class IPEndPoint : EndPoint {

		public const int MinPort = 0;
		public const int MaxPort = 0xffff;

		public IPAddress address;
		public int port;

		public IPEndPoint(IPAddress addr, int port) {
			if (addr == null) {
				throw new ArgumentNullException();
			}
			this.Address = addr;
			this.Port = port;
		}

		public IPEndPoint(long addr, int port) : this(new IPAddress(addr), port) { }

		public IPAddress Address {
			get {
				return this.address;
			}
			set {
				this.address = value;
			}
		}

		public int Port {
			get {
				return this.port;
			}
			set {
				if (value < MinPort || value > MaxPort) {
					throw new ArgumentOutOfRangeException("Port");
				}
				this.port = value;
			}
		}

		public override AddressFamily AddressFamily {
			get {
				return this.address.AddressFamily;
			}
		}

		public override EndPoint Create(SocketAddress sockaddr) {
			if (sockaddr.Size < 8) {
				return null;
			}
			if (sockaddr.Family != AddressFamily.InterNetwork) {
				// Only support IP4
				return null;
			}
			int port = (((int)sockaddr[2]) << 8) | (int)sockaddr[3];
			long address = (((long)sockaddr[7]) << 24) |
				(((long)sockaddr[6]) << 16) |
				(((long)sockaddr[5]) << 8) |
				(long)sockaddr[4];
			return new IPEndPoint(address, port);
		}

		public override SocketAddress Serialize() {
			SocketAddress sockaddr = null;
			switch (this.address.AddressFamily) {
				case AddressFamily.InterNetwork:
					// .net produces a 16 byte buffer, even though
					// only 8 bytes are used. I guess its just a
					// holdover from struct sockaddr padding.
					sockaddr = new SocketAddress(AddressFamily.InterNetwork, 16);

					// bytes 2 and 3 store the port, the rest
					// stores the address
					sockaddr[2] = (byte)((this.port >> 8) & 0xff);
					sockaddr[3] = (byte)(this.port & 0xff);
					uint addr = this.address.InternalIPv4Address;
					sockaddr[4] = (byte)(addr & 0xff);
					sockaddr[5] = (byte)((addr >> 8) & 0xff);
					sockaddr[6] = (byte)((addr >> 16) & 0xff);
					sockaddr[7] = (byte)((addr >> 24) & 0xff);
					break;
			}
			return sockaddr;
		}

		public override string ToString() {
			return this.address.ToString() + ":" + this.port.ToString();
		}

		public override int GetHashCode() {
			return this.address.GetHashCode() + this.port;
		}

		public override bool Equals(Object obj) {
			IPEndPoint p = obj as IPEndPoint;
			return p != null &&
				   p.port == this.port &&
				   p.address.Equals(this.address);
		}

	}
}
