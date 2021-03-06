//
// ProjectPackage.cs: A pkg-config package
//
// Authors:
//   Marcos David Marin Amador <MarcosMarin@gmail.com>
//
// Copyright (C) 2007 Marcos David Marin Amador
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
using System.IO;

using Mono.Addins;

using MonoDevelop.Projects.Serialization;

namespace CBinding
{
	public class ProjectPackage
	{
		[ItemProperty ("file")]
		private string file;
		
		[ItemProperty ("name")]
		private string name;
		
		[ItemProperty ("IsProject")]
		private bool is_project;
		
		public ProjectPackage (string file)
		{
			this.file = file;
			this.name = file;
			this.is_project = false;
		}
		
		public ProjectPackage (CProject project)
		{
			name = project.Name;
			file = Path.Combine (project.BaseDirectory, name + ".md.pc");
			is_project = true;
		}
		
		public ProjectPackage ()
		{
		}
		
		public string File {
			get { return file; }
			set { file = value; }
		}
		
		public string Name {
			get { return name; }
			set { name = value; }
		}
		
		public bool IsProject {
			get { return is_project; }
			set { is_project = value; }
		}
		
		public override bool Equals (object o)
		{
			ProjectPackage other = o as ProjectPackage;
			
			if (other == null) return false;
			
			return other.File.Equals (file);
		}
		
		public override int GetHashCode ()
		{
			return (file + name).GetHashCode ();
		}
	}
}
