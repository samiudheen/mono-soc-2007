//
// Button.cs
//
// Author:
//   George Giolfan (georgegiolfan@yahoo.com)
//
// Copyright (C) 2007 George Giolfan
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
//FIXME: When in a tool bar, it does not display keyboard focus cues properly.
using System;
using System.Windows.Automation.Peers;
using System.Windows.Input;
#if Implementation
using System.Windows;
using System.Windows.Controls;
using Mono.System.Windows.Controls.Primitives;
namespace Mono.System.Windows.Controls
{
#else
using System.Windows.Controls.Primitives;
namespace System.Windows.Controls {
#endif
	public class Button : ButtonBase
	{
		#region Public Fields
		#region Dependency Properties
		static public readonly DependencyProperty IsCancelProperty = DependencyProperty.Register ("IsCancel", typeof (bool), typeof (Button), new FrameworkPropertyMetadata ());
		static public readonly DependencyProperty IsDefaultedProperty = DependencyProperty.RegisterReadOnly ("IsDefaulted", typeof (bool), typeof (Button), new FrameworkPropertyMetadata ()).DependencyProperty;
		static public readonly DependencyProperty IsDefaultProperty = DependencyProperty.Register ("IsDefault", typeof (bool), typeof (Button), new FrameworkPropertyMetadata ());
		#endregion
		#endregion

		#region Private Fields
		bool in_tool_bar;
		#endregion

		#region Static Constructor
		static Button ()
		{
#if Implementation
			Theme.Load ();
#endif
			DefaultStyleKeyProperty.OverrideMetadata (typeof (Button), new FrameworkPropertyMetadata (typeof (Button)));
		}
		#endregion

		#region Public Constructors
		public Button ()
		{
			//FIXME: Do this when the parent changes.
			LayoutUpdated += delegate (object sender, EventArgs e)
			{
				bool new_in_tool_bar = Parent is ToolBar;
				if (new_in_tool_bar != in_tool_bar) {
					in_tool_bar = new_in_tool_bar;
					//FIXME: I should not do this.
					DefaultStyleKey = in_tool_bar ? (object)"Mono ToolBar.ButtonStyleKey" : typeof (Button);
					//Style = in_tool_bar ? (Style)Application.Current.FindResource(ToolBar.ButtonStyleKey) : null;
				}
			};
		}
		#endregion

		#region Public Properties
		#region Dependency Properties
		public bool IsCancel {
			get { return (bool)GetValue (IsCancelProperty); }
			set { SetValue (IsCancelProperty, value); }
		}

		public bool IsDefault {
			get { return (bool)GetValue (IsDefaultProperty); }
			set { SetValue (IsDefaultProperty, value); }
		}

		public bool IsDefaulted {
			get { return (bool)GetValue (IsDefaultedProperty); }
		}
		#endregion
		#endregion

		#region Protected Methods
		protected override void OnClick ()
		{
			base.OnClick ();
			if (in_tool_bar)
				Keyboard.Focus (null);
		}

		protected override AutomationPeer OnCreateAutomationPeer ()
		{
#if Implementation
			return null;
#else
			return new ButtonAutomationPeer(this);
#endif
		}
		#endregion
	}
}