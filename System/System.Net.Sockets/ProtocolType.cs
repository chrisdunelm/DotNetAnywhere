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

namespace System.Net.Sockets {
	public enum ProtocolType {
		IP = 0,
		Icmp = 1,
		Igmp = 2,
		Ggp = 3,
		Tcp = 6,
		Pup = 12,
		Udp = 17,
		Idp = 22,
		IPv6 = 41,
		ND = 77,
		Raw = 255,
		Unspecified = 0,
		Ipx = 1000,
		Spx = 1256,
		SpxII = 1257,
		Unknown = -1,
		IPv4 = 4,
		IPv6RoutingHeader = 43,
		IPv6FragmentHeader = 44,
		IPSecEncapsulatingSecurityPayload = 50,
		IPSecAuthenticationHeader = 51,
		IcmpV6 = 58,
		IPv6NoNextHeader = 59,
		IPv6DestinationOptions = 60,
		IPv6HopByHopOptions = 0,
	}
}
