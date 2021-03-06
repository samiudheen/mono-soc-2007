//
// Shape.cs
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
using System.ComponentModel;
using System.Windows.Media;
#if Implementation
using System;
using System.Windows;
namespace Mono.System.Windows.Shapes
{
#else
namespace System.Windows.Shapes {
#endif
	[Localizability (LocalizationCategory.None, Readability = Readability.Unreadable)]
	public abstract class Shape : FrameworkElement
	{
		#region Public Fields
		#region Dependency Properties
		public static readonly DependencyProperty FillProperty = DependencyProperty.Register ("Fill", typeof (Brush), typeof (Shape), new FrameworkPropertyMetadata (null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));
		public static readonly DependencyProperty StretchProperty = DependencyProperty.Register ("Stretch", typeof (Stretch), typeof (Shape), new FrameworkPropertyMetadata (Stretch.None, FrameworkPropertyMetadataOptions.AffectsArrange));
		public static readonly DependencyProperty StrokeDashArrayProperty = DependencyProperty.Register ("StrokeDashArray", typeof (DoubleCollection), typeof (Shape), new FrameworkPropertyMetadata (new DoubleCollection (), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty StrokeDashCapProperty = DependencyProperty.Register ("StrokeDashCap", typeof (PenLineCap), typeof (Shape), new FrameworkPropertyMetadata (PenLineCap.Flat, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty StrokeDashOffsetProperty = DependencyProperty.Register ("StrokeDashOffset", typeof (double), typeof (Shape), new FrameworkPropertyMetadata (0D, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty StrokeEndLineCapProperty = DependencyProperty.Register ("StrokeEndLineCap", typeof (PenLineCap), typeof (Shape), new FrameworkPropertyMetadata (PenLineCap.Flat, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty StrokeLineJoinProperty = DependencyProperty.Register ("StrokeLineJoin", typeof (PenLineJoin), typeof (Shape), new FrameworkPropertyMetadata (PenLineJoin.Miter, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty StrokeMiterLimitProperty = DependencyProperty.Register ("StrokeMiterLimit", typeof (double), typeof (Shape), new FrameworkPropertyMetadata (10D, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register ("Stroke", typeof (Brush), typeof (Shape), new FrameworkPropertyMetadata (null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));
		public static readonly DependencyProperty StrokeStartLineCapProperty = DependencyProperty.Register ("StrokeStartLineCap", typeof (PenLineCap), typeof (Shape), new FrameworkPropertyMetadata (PenLineCap.Flat, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
		public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register ("StrokeThickness", typeof (double), typeof (Shape), new FrameworkPropertyMetadata (1D, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
		#endregion
		#endregion

		#region Protected Constructors
		public Shape ()
		{
		}
		#endregion

		#region Public Properties
		#region Dependency Properties
		public Brush Fill {
			get { return (Brush)GetValue (FillProperty); }
			set { SetValue (FillProperty, value); }
		}

		public Stretch Stretch {
			get { return (Stretch)GetValue (StretchProperty); }
			set { SetValue (StretchProperty, value); }
		}

		public Brush Stroke {
			get { return (Brush)GetValue (StrokeProperty); }
			set { SetValue (StrokeProperty, value); }
		}

		public DoubleCollection StrokeDashArray {
			get { return (DoubleCollection)GetValue (StrokeDashArrayProperty); }
			set { SetValue (StrokeDashArrayProperty, value); }
		}

		public PenLineCap StrokeDashCap {
			get { return (PenLineCap)GetValue (StrokeDashCapProperty); }
			set { SetValue (StrokeDashCapProperty, value); }
		}

		public double StrokeDashOffset {
			get { return (double)GetValue (StrokeDashOffsetProperty); }
			set { SetValue (StrokeDashOffsetProperty, value); }
		}

		public PenLineCap StrokeEndLineCap {
			get { return (PenLineCap)GetValue (StrokeEndLineCapProperty); }
			set { SetValue (StrokeEndLineCapProperty, value); }
		}

		public PenLineJoin StrokeLineJoin {
			get { return (PenLineJoin)GetValue (StrokeLineJoinProperty); }
			set { SetValue (StrokeLineJoinProperty, value); }
		}

		public double StrokeMiterLimit {
			get { return (double)GetValue (StrokeMiterLimitProperty); }
			set { SetValue (StrokeMiterLimitProperty, value); }
		}

		public PenLineCap StrokeStartLineCap {
			get { return (PenLineCap)GetValue (StrokeStartLineCapProperty); }
			set { SetValue (StrokeStartLineCapProperty, value); }
		}

		[TypeConverter (typeof (LengthConverter))]
		public double StrokeThickness {
			get { return (double)GetValue (StrokeThicknessProperty); }
			set { SetValue (StrokeThicknessProperty, value); }
		}
		#endregion

		public virtual Transform GeometryTransform {
			get {
				return Transform.Identity;
			}
		}

		public virtual Geometry RenderedGeometry {
			get {
				return new StreamGeometry ();
			}
		}
		#endregion

		#region Protected Properties
		protected abstract Geometry DefiningGeometry { 
			get; 
		}
		#endregion

		#region Protected Methods
		protected override Size ArrangeOverride (Size finalSize)
		{
			if (Stretch == Stretch.None)
				return finalSize;
			if (Stretch == Stretch.Fill) {
				//FIXME: How is it used?
				object dummy = DefiningGeometry;
				if (Stroke != null) {
					double stroke_thickness = StrokeThickness;
					return new Size (stroke_thickness, stroke_thickness);
				}
			}
			return new Size (0, 0);
		}

		protected override Size MeasureOverride (Size availableSize)
		{
			Geometry defining_geometry = DefiningGeometry;
			if (defining_geometry == null)
				throw new NullReferenceException ();
			if (Stretch == Stretch.Fill && Stroke != null) {
				double stroke_thickness = StrokeThickness;
				return new Size (stroke_thickness, stroke_thickness);
			}
			return new Size (0, 0);
		}

		protected override void OnRender (DrawingContext drawingContext)
		{
			base.OnRender (drawingContext);
			Pen pen = CreatePen ();
			Geometry defining_geometry = DefiningGeometry;
			if (Stretch == Stretch.Fill) {
				Rect defining_geometry_bounds = defining_geometry.GetRenderBounds (pen);
				if (defining_geometry_bounds.Left < 0 || defining_geometry_bounds.Top < 0)
					defining_geometry.Transform = new MatrixTransform (1, 0, 0, 1, defining_geometry_bounds.Left < 0 ? -defining_geometry_bounds.Left : 0, defining_geometry_bounds.Top < 0 ? -defining_geometry_bounds.Top : 0);
			}
			drawingContext.DrawGeometry (Fill, pen, defining_geometry);
		}
		#endregion

		#region Private Methods
		Pen CreatePen ()
		{
			Pen result = new Pen (Stroke, StrokeThickness);
			result.DashStyle = new DashStyle (StrokeDashArray, StrokeDashOffset);
			result.DashCap = StrokeDashCap;
			result.StartLineCap = StrokeStartLineCap;
			result.EndLineCap = StrokeEndLineCap;
			result.LineJoin = StrokeLineJoin;
			result.MiterLimit = StrokeMiterLimit;
			return result;
		}
		#endregion
	}
}