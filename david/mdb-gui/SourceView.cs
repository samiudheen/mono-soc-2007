using System;

using Glade;
using Gtk;
using GtkSharp;

namespace Mono.Debugger.Frontend
{
	public class SourceView: TextView
	{
		MdbGui mdbGui;
		
		string currentlyLoadedSourceFile;
		
		public SourceView(MdbGui mdbGui)
		{
			this.mdbGui = mdbGui;
			
			// Create tags
			TextTag currentLineTag = new TextTag("currentLine");
			currentLineTag.Background = "yellow";
			this.Buffer.TagTable.Add(currentLineTag);
			
			TextTag breakpointTag = new TextTag("breakpoint");
			breakpointTag.Background = "red";
			this.Buffer.TagTable.Add(breakpointTag);
			
			TextTag disabledBreakpointTag = new TextTag("disabledBreakpoint");
			disabledBreakpointTag.Background = "gray";
			this.Buffer.TagTable.Add(disabledBreakpointTag);
			
			
			UpdateDisplay(new SourceCodeLocation[0]);
		}
		
		public void UpdateDisplay(SourceCodeLocation[] breakpoints)
		{
			string filename = mdbGui.DebuggerService.GetCurrentFilename();
			if (filename == null) {
				this.Buffer.Text = "No source code";
				currentlyLoadedSourceFile = null;
				return;
			}
			
			// Load the source file if neccessary
			if (currentlyLoadedSourceFile != filename) {
				string[] sourceCode = mdbGui.DebuggerService.ReadFile(filename);
				this.Buffer.Text = string.Join("\n", sourceCode);
				currentlyLoadedSourceFile = filename;
			}
			
			// Remove all current tags
			TextIter bufferBegin, bufferEnd;
			this.Buffer.GetBounds(out bufferBegin, out bufferEnd);
			this.Buffer.RemoveAllTags(bufferBegin, bufferEnd);
			
			// Add tag to show current line
			int currentLine = mdbGui.DebuggerService.GetCurrentLine();
			TextIter currentLineIter = AddSourceViewTag("currentLine", currentLine);
			
			// Add tags for breakpoints
			foreach (SourceCodeLocation breakpoint in breakpoints) {
				if (breakpoint.Filename == currentlyLoadedSourceFile) {
					// If it is current line, do not retag it
					if (breakpoint.Line != currentLine) {
						AddSourceViewTag("breakpoint", breakpoint.Line);
					}
				}
			}
			
			// Scroll to current line
			TextMark mark = this.Buffer.CreateMark(null, currentLineIter, false);
			this.ScrollToMark(mark, 0, false, 0, 0);
		}
		
		TextIter AddSourceViewTag(string tag, int line)
		{
			TextIter begin = this.Buffer.GetIterAtLine(line - 1);
			TextIter end   = this.Buffer.GetIterAtLine(line);
			this.Buffer.ApplyTag(tag, begin, end);
			return begin;
		}
		
		public void ToggleBreakpoint()
		{
			// Toggle breakpoint at current location
			if (currentlyLoadedSourceFile != null) {
				int line = this.Buffer.GetIterAtMark(this.Buffer.InsertMark).Line + 1;
				
				mdbGui.DebuggerService.ToggleBreakpoint(currentlyLoadedSourceFile, line);
			} else {
				Console.WriteLine("Error - no source file loaded");
			}
		}
	}
}
