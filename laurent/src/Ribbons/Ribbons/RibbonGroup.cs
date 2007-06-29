using Cairo;
using Gtk;
using System;

namespace Ribbons
{
	public class RibbonGroup : Bin
	{
		protected Theme theme = new Theme ();
		protected string lbl;
		protected Pango.Layout lbl_layout;
		
		protected double lineWidth = 1.0;
		protected double space = 2.0;
		
		public string Label
		{
			set
			{
				lbl = value;
				
				if(lbl == null)
					lbl_layout = null;
				else if(lbl_layout == null)
					lbl_layout = CreatePangoLayout (this.lbl);
				else
					lbl_layout.SetText (lbl);
			}
			get { return lbl; }
		}
		
		public RibbonGroup ()
		{
			// This is a No Window widget => it does not have its own Gdk Window => it can be transparent
			this.SetFlag (WidgetFlags.NoWindow);
			
			this.AddEvents ((int)(Gdk.EventMask.ButtonPressMask | Gdk.EventMask.ButtonReleaseMask | Gdk.EventMask.PointerMotionMask));
			
			Label = null;
			HeightRequest = 90;
			BorderWidth = 1;
		}
		
		protected override void OnSizeRequested (ref Requisition requisition)
		{
			base.OnSizeRequested (ref requisition);
			
			if(WidthRequest == -1)
			{
				if(Child != null && Child.Visible)
				{
					Requisition childRequisition = Child.SizeRequest ();
					requisition.Width = childRequisition.Width + (int)(2 * (2*lineWidth + BorderWidth));
				}
				else
				{
					int lw, lh;
					lbl_layout.GetPixelSize (out lw, out lh);
					requisition.Width = lw + (int)(2 * (2*(lineWidth+space)));
				}
			}
		}
		
		protected override void OnSizeAllocated (Gdk.Rectangle allocation)
		{
			base.OnSizeAllocated (allocation);
			
			if(Child != null && Child.Visible)
			{
				int lw, lh;
				lbl_layout.GetPixelSize (out lw, out lh);
				int frame_size = (int)(2*lineWidth + BorderWidth);
				int wi = allocation.Width - 2 * frame_size; 
				int he = allocation.Height - 2 * frame_size - (lh + (int)(2*space)); 
				Gdk.Rectangle r = new Gdk.Rectangle (allocation.X + frame_size, allocation.Y + frame_size, wi, he);
				Child.SizeAllocate (r);
			}
		}
		
		protected void Draw (Context cr)
		{
			Rectangle rect = new Rectangle (Allocation.X, Allocation.Y, Allocation.Width, Allocation.Height);
			theme.DrawGroup (cr, rect, 4.0, lineWidth, space, lbl_layout, this);
		}
		
		protected override bool OnExposeEvent (Gdk.EventExpose evnt)
		{
			Context cr = Gdk.CairoHelper.Create (this.GdkWindow);
			
			cr.Rectangle (evnt.Area.X, evnt.Area.Y, evnt.Area.Width, evnt.Area.Height);
			cr.Clip ();
			Draw (cr);
			
			return base.OnExposeEvent (evnt);
		}
	}
}