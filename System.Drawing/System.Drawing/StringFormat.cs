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

namespace System.Drawing {
	public sealed class StringFormat : MarshalByRefObject, IDisposable, ICloneable {

		internal IntPtr native = IntPtr.Zero;

		private static StringFormat genericDefault = null;
		private static StringFormat genericTypographic = null;

		private StringTrimming trimming;
		private StringAlignment alignment;
		private StringAlignment lineAlignment;
		private StringFormatFlags stringFormatFlags;

		public StringFormat() : this(0) { }

		public StringFormat(StringFormatFlags options) {
			this.native = LibIGraph.CreateStringFormat
				(StringAlignment.Near, options, StringAlignment.Near, StringTrimming.None);
			this.trimming = StringTrimming.None;
			this.alignment = StringAlignment.Near;
			this.lineAlignment = StringAlignment.Near;
			this.stringFormatFlags = options;
		}

		~StringFormat() {
			this.Dispose();
		}

		public StringTrimming Trimming {
			get {
				return this.trimming;
			}
			set {
				LibIGraph.StringFormat_SetTrimming(this.native, value);
				this.trimming = value;
			}
		}

		public StringAlignment Alignment {
			get {
				return this.alignment;
			}
			set {
				LibIGraph.StringFormat_SetAlignment(this.native, value);
				this.alignment = value;
			}
		}

		public StringAlignment LineAlignment {
			get {
				return this.lineAlignment;
			}
			set {
				LibIGraph.StringFormat_SetLineAlignment(this.native, value);
				this.lineAlignment = value;
			}
		}

		public StringFormatFlags FormatFlags {
			get {
				return this.stringFormatFlags;
			}
			set {
				LibIGraph.StringFormat_SetFormatFlags(this.native, value);
				this.stringFormatFlags = value;
			}
		}

		public static StringFormat GenericDefault {
			get {
				if (genericDefault == null) {
					genericDefault = new StringFormat();
					genericDefault.Trimming = StringTrimming.Character;
				}
				return genericDefault;
			}
		}

		public static StringFormat GenericTypographic {
			get {
				if (genericTypographic == null) {
					genericTypographic = new StringFormat
						(StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox | StringFormatFlags.LineLimit);
				}
				return genericTypographic;
			}
		}

		public object Clone() {
			throw new Exception("The method or operation is not implemented.");
		}

		public void Dispose() {
			if (this.native != IntPtr.Zero) {
				LibIGraph.DisposeStringFormat(this.native);
				this.native = IntPtr.Zero;
				GC.SuppressFinalize(this);
			}
		}
	}
}
