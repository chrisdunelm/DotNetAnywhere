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
using System.Runtime.CompilerServices;

namespace System.Net.Sockets {
	public class Socket : IDisposable {

		#region Internal Methods

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private static IntPtr Internal_CreateSocket(int family, int type, int proto, out int error);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private static void Internal_Close(IntPtr native);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private static void Internal_Bind(IntPtr native, uint address, int port, out int error);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private static void Internal_Listen(IntPtr native, int backLog, out int error);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private static IntPtr Internal_Accept(IntPtr native, out int error);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private static void Internal_Connect(IntPtr native, uint address, int port, out int error);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private static int Internal_Receive(IntPtr native, byte[] buffer, int offset, int size, int flags, out int error);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private static int Internal_Send(IntPtr native, byte[] buffer, int offset, int size, int flags, out int error);

		#endregion

		private IntPtr native;
		private AddressFamily family;
		private SocketType type;
		private ProtocolType proto;

		public Socket(AddressFamily family, SocketType type, ProtocolType proto) {
			this.family = family;
			this.type = type;
			this.proto = proto;

			int error;
			this.native = Internal_CreateSocket((int)family, (int)type, (int)proto, out error);
			this.CheckError(error);
		}

		private Socket(AddressFamily family, SocketType type, ProtocolType proto, IntPtr native) {
			this.family = family;
			this.type = type;
			this.proto = proto;
			this.native = native;
		}

		~Socket() {
			this.Dispose(false);
		}

		private void Dispose(bool disposing) {
			if (this.native != IntPtr.Zero) {
				Internal_Close(this.native);
				this.native = IntPtr.Zero;
				if (disposing) {
					GC.SuppressFinalize(this);
				}
			}
		}

		void IDisposable.Dispose() {
			this.Dispose(true);
		}

		public void Close() {
			this.Dispose(true);
		}

		private void CheckDisposed() {
			if (this.native == IntPtr.Zero) {
				throw new ObjectDisposedException(this.GetType().ToString());
			}
		}

		private void CheckError(int error) {
			if (error != 0) {
				Console.WriteLine("SOCKET_ERROR {0}", error);
				throw new SocketException(error);
			}
		}

		private void GetIPInfo(EndPoint ep, out uint addr, out int port) {
			if (ep.AddressFamily != AddressFamily.InterNetwork) {
				throw new ArgumentException("EndPoint", "Can only handle IPv4 addresses");
			}
			SocketAddress sockAddr = ep.Serialize();
			port = (((int)sockAddr[2]) << 8) | sockAddr[3];
			addr = ((((uint)sockAddr[7]) << 24) | (((uint)sockAddr[6]) << 16) |
				(((uint)sockAddr[5]) << 8) | (uint)sockAddr[4]);
		}

		public void Bind(EndPoint epLocal) {
			this.CheckDisposed();
			if (epLocal == null) {
				throw new ArgumentNullException("epLocal");
			}
			int port;
			uint addr;
			this.GetIPInfo(epLocal, out addr, out port);
			int error;
			Internal_Bind(this.native, addr, port, out error);
			this.CheckError(error);
		}

		public void Listen(int backLog) {
			this.CheckDisposed();
			int error;
			Internal_Listen(this.native, backLog, out error);
			this.CheckError(error);
		}

		public Socket Accept() {
			this.CheckDisposed();
			int error;
			IntPtr socket = Internal_Accept(this.native, out error);
			this.CheckError(error);
			return new Socket(this.family, this.type, this.proto, socket);
		}

		public void Connect(EndPoint epRemote) {
			this.CheckDisposed();
			if (epRemote == null) {
				throw new ArgumentNullException("epRemote");
			}
			int port;
			uint addr;
			this.GetIPInfo(epRemote, out addr, out port);
			int error;
			Internal_Connect(this.native, addr, port, out error);
			this.CheckError(error);
		}

		public int Send(byte[] buffer) {
			return this.Send(buffer, 0, buffer.Length, SocketFlags.None);
		}

		public int Send(byte[] buffer, SocketFlags flags) {
			return this.Send(buffer, 0, buffer.Length, flags);
		}

		public int Send(byte[] buffer, int size, SocketFlags flags) {
			return this.Send(buffer, 0, size, flags);
		}

		public int Send(byte[] buffer, int offset, int size, SocketFlags flags) {
			this.CheckDisposed();
			if (buffer == null) {
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || size < 0 || offset + size > buffer.Length) {
				throw new ArgumentOutOfRangeException();
			}
			int error;
			int ret = Internal_Send(this.native, buffer, offset, size, (int)flags, out error);
			this.CheckError(error);
			return ret;
		}

		public int Receive(byte[] buffer) {
			return this.Receive(buffer, 0, buffer.Length, SocketFlags.None);
		}

		public int Receive(byte[] buffer, SocketFlags flags) {
			return this.Receive(buffer, 0, buffer.Length, flags);
		}

		public int Receive(byte[] buffer, int size, SocketFlags flags) {
			return this.Receive(buffer, 0, size, flags);
		}

		public int Receive(byte[] buffer, int offset, int size, SocketFlags flags) {
			this.CheckDisposed();
			if (buffer == null) {
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || size < 0 || offset + size > buffer.Length) {
				throw new ArgumentOutOfRangeException();
			}
			int error;
			int ret = Internal_Receive(this.native, buffer, offset, size, (int)flags, out error);
			this.CheckError(error);
			return ret;
		}
	}
}
