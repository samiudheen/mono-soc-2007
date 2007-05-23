using Mono.WindowsPresentationFoundation;
using System;
using System.ComponentModel;
using System.Threading;
using System.Timers;
using System.Windows.Automation.Peers;
using System.Windows.Input;
using System.Windows.Threading;
#if Implementation
using System.Windows;
using System.Windows.Controls.Primitives;
namespace Mono.System.Windows.Controls.Primitives {
#else
namespace System.Windows.Controls.Primitives {
#endif
	public class RepeatButton : ButtonBase {
		#region Private Fields
		global::System.Timers.Timer delay_timer = new global::System.Timers.Timer();
		global::System.Timers.Timer interval_timer = new global::System.Timers.Timer();
		#endregion

		#region Dependency Property Fields
		public static readonly DependencyProperty DelayProperty = DependencyProperty.Register("Delay", typeof(int), typeof(RepeatButton), new PropertyMetadata(Utility.GetSystemDelay(), delegate(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			int value = (int)e.NewValue;
			//FIXME?: Support 0 value
			if (value == 0)
				value = 1;
			((RepeatButton)d).delay_timer.Interval = value;
		}), delegate(object value) {
			return (int)value > -1;
		});

		public static readonly DependencyProperty IntervalProperty = DependencyProperty.Register("Interval", typeof(int), typeof(RepeatButton), new PropertyMetadata(Utility.GetSystemInterval(), delegate(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			((RepeatButton)d).interval_timer.Interval = (int)e.NewValue;
		}), delegate(object value) {
			return (int)value > 0;
		});
		#endregion
		
		#region Public Constructors
		public RepeatButton() {
			ThemeStyle = Theme.GetStyle(typeof(RepeatButton));
			ClickMode = global::System.Windows.Controls.ClickMode.Press;
			
			delay_timer.Elapsed += delegate(object sender, ElapsedEventArgs e) {
				delay_timer.Enabled = false;
				interval_timer.Enabled = true;
			};
			
			interval_timer.Elapsed += delegate(object sender, ElapsedEventArgs e) {
				Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(delegate() {
					if (IsPressed)
						OnClick();
				}));
			};
		}
		#endregion
		
		#region Public Properties
		[Bindable(true)]
		public int Delay {
			get { return (int)GetValue(DelayProperty); }
			set { SetValue(DelayProperty, value); }
		}

		[Bindable(true)]
		public int Interval {
			get { return (int)GetValue(IntervalProperty); }
			set { SetValue(IntervalProperty, value); }
		}
		#endregion

        #region Protected Methods
        protected override void OnClick() {
			//WDTDH
			base.OnClick();
        }

        protected override AutomationPeer OnCreateAutomationPeer() {
#if Implementation
			return null;
#else
			return new RepeatButtonAutomationPeer(this);
#endif
		}

        protected override void OnKeyDown(KeyEventArgs e) {
			base.OnKeyDown(e);
			if (IsButtonKey(e))
				BeginRepeatStatus();
        }

        protected override void OnKeyUp(KeyEventArgs e) {
			base.OnKeyUp(e);
			if (IsButtonKey(e))
				EndRepeatStatus();
		}

        protected override void OnLostMouseCapture(MouseEventArgs e) {
			base.OnLostMouseCapture(e);
			EndRepeatStatus();
		}

		// I don't see any effect when overriding OnMouseEnter, OnMouseLeave in a derived class and not calling the base implementation.
		protected override void OnMouseEnter(MouseEventArgs e) {
			//WDTDH
			base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e) {
			//WDTDH
			base.OnMouseLeave(e);
		}

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
			base.OnMouseLeftButtonDown(e);
			BeginRepeatStatus();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e) {
			EndRepeatStatus();
			base.OnMouseLeftButtonUp(e);
        }
        #endregion

		#region Private Methods
		void BeginRepeatStatus() {
			delay_timer.Enabled = true;
		}

		void EndRepeatStatus() {
			delay_timer.Enabled = false;
			interval_timer.Enabled = false;
		}
		#endregion

		#region Internal Properties
		//FIXME: This should be used by lower-level classes when they are implemented.
		internal Style ThemeStyle {
			set {
				object x = this;
				Style = value;
			}
		}
		#endregion
	}
}