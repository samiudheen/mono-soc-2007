using System;
using System.Collections;

using Mono.Debugger;
using Mono.Debugger.Languages;
using Mono.Debugger.Frontend.TreeModel;

namespace Mono.Debugger.Frontend
{
	public class BreakpointsStore: RemoteTreeStore
	{
		DebuggerService debuggerService;
		Interpreter interpreter;
		
		Hashtable breakpointToTreeNode = new Hashtable();
		
		public const int ColumnReference   = 0;
		public const int ColumnLocation    = 1;
		public const int ColumnImage       = 2;
		public const int ColumnID          = 3;
		public const int ColumnEnabled     = 4;
		public const int ColumnActivated   = 5;
		public const int ColumnThreadGroup = 6;
		public const int ColumnName        = 7;
		
		public static Type[] ColumnTypes = new Type[] {
			typeof(RemoteTreeNodeRef),
			typeof(SourceCodeLocation),
			typeof(Gdk.Pixbuf),
			typeof(int),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string)
		};
		
		public BreakpointsStore(DebuggerService debuggerService, Interpreter interpreter)
		{
			this.debuggerService = debuggerService;
			this.interpreter = interpreter;
		}
		
		void UpdateBreakpoint(SourceBreakpoint breakpoint)
		{
			RemoteTreeNode node = (RemoteTreeNode)breakpointToTreeNode[breakpoint];

			if (node == null) {
				node = RootNode.AppendNode();
				breakpointToTreeNode.Add(breakpoint, node);
			}
			
			node.SetValue(ColumnLocation, new SourceCodeLocation(breakpoint.Location));
			node.SetValue(ColumnImage, breakpoint.IsEnabled && breakpoint.IsActivated ? Pixmaps.Breakpoint : Pixmaps.BreakpointDisabled);
			node.SetValue(ColumnID, breakpoint.Index);
			node.SetValue(ColumnEnabled, breakpoint.IsEnabled ? "Yes" : "No");
			node.SetValue(ColumnActivated, breakpoint.IsActivated ? "Yes" : "No");
			node.SetValue(ColumnThreadGroup, breakpoint.ThreadGroup != null ? breakpoint.ThreadGroup.Name : "global");
			node.SetValue(ColumnName, breakpoint.Name);
		}
		
		void RemoveBreakpoint(SourceBreakpoint breakpoint)
		{
			RemoteTreeNode node = (RemoteTreeNode)breakpointToTreeNode[breakpoint];
			
			if (node != null) {
				node.Remove();
			}
			
			breakpointToTreeNode.Remove(breakpoint);
		}
		
		public void UpdateTree()
		{
			bool abort = false;
			UpdateTree(ref abort);
		}
		
		public void UpdateTree(ref bool abort)
		{
			Hashtable breakpointsToRemove = (Hashtable)breakpointToTreeNode.Clone();
			
			foreach (Event handle in interpreter.Session.Events) {
				if (abort) return;
				if (handle is SourceBreakpoint) {
					UpdateBreakpoint((SourceBreakpoint)handle);
					breakpointsToRemove.Remove(handle); // Ok if not in the table
				}
			}
			
			foreach (SourceBreakpoint breakpoint in breakpointsToRemove.Keys) {
				RemoveBreakpoint(breakpoint);
			}
		}
	}
}
