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

namespace System.Net {
	public static class Dns {

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern private static int[] Internal_GetHostEnt(string dnsName, out string hostName);

		public static IPHostEntry GetHostEntry(string hostNameOrAddress) {
			IPAddress ipAddr;
			bool isIPAddr = IPAddress.TryParse(hostNameOrAddress, out ipAddr);
			if (!isIPAddr) {
				string hostName;
				int[] ips = Internal_GetHostEnt(hostNameOrAddress, out hostName);
				IPAddress[] addresses = new IPAddress[ips.Length];
				for (int i = 0; i < ips.Length; i++) {
					addresses[i] = new IPAddress((uint)ips[i]);
				}
				IPHostEntry hostEnt = new IPHostEntry();
				hostEnt.AddressList = addresses;
				hostEnt.HostName = hostName;
				return hostEnt;
			} else {
				return GetHostEntry(ipAddr);
			}
		}

		public static IPHostEntry GetHostEntry(IPAddress addr) {
			if (addr == null) {
				throw new ArgumentNullException("address");
			}
			throw new NotImplementedException();
		}

	}
}
