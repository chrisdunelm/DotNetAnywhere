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
	public enum SocketOptionName {
		Debug = 1,
		AcceptConnection = 2,
		ReuseAddress = 4,
		KeepAlive = 8,
		DontRoute = 16,
		Broadcast = 32,
		UseLoopback = 64,
		Linger = 128,
		OutOfBandInline = 256,
		DontLinger = -129,
		ExclusiveAddressUse = -5,
		SendBuffer = 4097,
		ReceiveBuffer = 4098,
		SendLowWater = 4099,
		ReceiveLowWater = 4100,
		SendTimeout = 4101,
		ReceiveTimeout = 4102,
		Error = 4103,
		Type = 4104,
		MaxConnections = 2147483647,
		IPOptions = 1,
		HeaderIncluded = 2,
		TypeOfService = 3,
		IpTimeToLive = 4,
		MulticastInterface = 9,
		MulticastTimeToLive = 10,
		MulticastLoopback = 11,
		AddMembership = 12,
		DropMembership = 13,
		DontFragment = 14,
		AddSourceMembership = 15,
		DropSourceMembership = 16,
		BlockSource = 17,
		UnblockSource = 18,
		PacketInformation = 19,
		NoDelay = 1,
		BsdUrgent = 2,
		Expedited = 2,
		NoChecksum = 1,
		ChecksumCoverage = 20,
		HopLimit = 21,
		UpdateAcceptContext = 28683,
		UpdateConnectContext = 28688,
	}
}
