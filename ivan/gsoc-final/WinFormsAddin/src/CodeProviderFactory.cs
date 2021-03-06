
//
// CreateDesignerLoader.cs
//
// Authors:
//   Ivan N. Zlatev <contact@i-nz.net>
//
// Copyright (C) 2007 Ivan N. Zlatev
//
//
// This source code is licenced under The MIT License:
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.ComponentModel.Design.Serialization;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace WinFormsAddin
{
	internal static class CodeProviderFactory
	{

		private static CodeDomProvider _csProvider = null;
		private static CodeDomProvider _vbProvider = null;

		public static CodeDomProvider CreateCodeProvider (string file)
		{
			if (file.EndsWith (".cs")) {
				if (_csProvider == null)
					_csProvider = new Microsoft.CSharp.CSharpCodeProvider ();
				return _csProvider;
			} else if (file.EndsWith (".vb")) {
				if (_vbProvider == null)
					_vbProvider = new Microsoft.VisualBasic.VBCodeProvider ();
				return _vbProvider;
			} else {
				throw new InvalidOperationException ("No supported DesignerLoader for " + file);
			}
		}
	}
}
