//
// Grid.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Windows.Markup;
using System.Windows.Media;
#if Implementation
using System;
using System.Windows;
using System.Windows.Controls;
namespace Mono.System.Windows.Controls
{
#else
namespace System.Windows.Controls {
#endif
	public class Grid : Panel
	{
		#region Public Fields
		#region Dependency Properties
		public static readonly DependencyProperty ShowGridLinesProperty = DependencyProperty.Register ("ShowGridLines", typeof (bool), typeof (Grid), new FrameworkPropertyMetadata (delegate (DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((Grid)d).InvalidateArrange ();
		}));
		#region Attached Properties
		public static readonly DependencyProperty ColumnProperty = DependencyProperty.RegisterAttached ("Column", typeof (int), typeof (Grid), new FrameworkPropertyMetadata (InvalidateGridMeasure), ValidateRowColumn);
		public static readonly DependencyProperty ColumnSpanProperty = DependencyProperty.RegisterAttached ("ColumnSpan", typeof (int), typeof (Grid), new FrameworkPropertyMetadata (1, InvalidateGridMeasure), ValidateSpan);
		public static readonly DependencyProperty IsSharedSizeScopeProperty = DependencyProperty.RegisterAttached ("IsSharedSizeScope", typeof (bool), typeof (Grid), new FrameworkPropertyMetadata ());
		public static readonly DependencyProperty RowProperty = DependencyProperty.RegisterAttached ("Row", typeof (int), typeof (Grid), new FrameworkPropertyMetadata (InvalidateGridMeasure), ValidateRowColumn);
		public static readonly DependencyProperty RowSpanProperty = DependencyProperty.RegisterAttached ("RowSpan", typeof (int), typeof (Grid), new FrameworkPropertyMetadata (1, InvalidateGridMeasure), ValidateSpan);
		#endregion
		#endregion
		#endregion

		#region Private Fields
		ColumnDefinitionCollection column_definitions;
		RowDefinitionCollection row_definitions;
		bool measure_called;
		GridLinesRenderer grid_lines_renderer;
		#endregion

		#region Public Constructors
		public Grid ()
		{
			column_definitions = new ColumnDefinitionCollection (this);
			row_definitions = new RowDefinitionCollection (this);
		}
		#endregion

		#region Public Properties
		public ColumnDefinitionCollection ColumnDefinitions {
			get { return column_definitions; }
		}

		public RowDefinitionCollection RowDefinitions {
			get { return row_definitions; }
		}

		#region Dependency Properties
		public bool ShowGridLines {
			get { return (bool)GetValue (ShowGridLinesProperty); }
			set { SetValue (ShowGridLinesProperty, value); }
		}
		#endregion
		#endregion

		#region Protected Properties
		protected override IEnumerator LogicalChildren {
			get {
				return base.LogicalChildren;
			}
		}

		protected override int VisualChildrenCount {
			get {
				return base.VisualChildrenCount + (grid_lines_renderer == null ? 0 : 1);
			}
		}
		#endregion

		#region Public Methods
		#region Attached Properties
		[AttachedPropertyBrowsableForChildren]
		public static int GetColumn (UIElement element)
		{
			return (int)element.GetValue (ColumnProperty);
		}

		[AttachedPropertyBrowsableForChildren]
		public static int GetColumnSpan (UIElement element)
		{
			return (int)element.GetValue (ColumnSpanProperty);
		}

		public static bool GetIsSharedSizeScope (UIElement element)
		{
			return (bool)element.GetValue (IsSharedSizeScopeProperty);
		}

		[AttachedPropertyBrowsableForChildren]
		public static int GetRow (UIElement element)
		{
			return (int)element.GetValue (RowProperty);
		}

		[AttachedPropertyBrowsableForChildren]
		public static int GetRowSpan (UIElement element)
		{
			return (int)element.GetValue (RowSpanProperty);
		}

		public static void SetColumn (UIElement element, int value)
		{
			element.SetValue (ColumnProperty, value);
		}

		public static void SetColumnSpan (UIElement element, int value)
		{
			element.SetValue (ColumnSpanProperty, value);
		}

		public static void SetIsSharedSizeScope (UIElement element, bool value)
		{
			element.SetValue (IsSharedSizeScopeProperty, value);
		}

		public static void SetRow (UIElement element, int value)
		{
			element.SetValue (RowProperty, value);
		}

		public static void SetRowSpan (UIElement element, int value)
		{
			element.SetValue (RowSpanProperty, value);
		}
		#endregion

		public bool ShouldSerializeColumnDefinitions ()
		{
			return column_definitions.Count != 0;
		}

		public bool ShouldSerializeRowDefinitions ()
		{
			return row_definitions.Count != 0;
		}
		#endregion

		#region Protected Methods
		protected override Size ArrangeOverride (Size finalSize)
		{
			bool has_row_definitions = row_definitions.Count != 0;
			bool has_column_definitions = column_definitions.Count != 0;
			if ((has_row_definitions || has_column_definitions) && !measure_called)
				throw new NullReferenceException ();
			int row_count = GetRowColumnCount (row_definitions.Count);
			int column_count = GetRowColumnCount (column_definitions.Count);
			double [] row_heights = new double [row_count];
			double [] column_widths = new double [column_count];
			int index;
			#region Compute initial row and column size
			if (has_row_definitions)
				for (index = 0; index < row_count; index++) {
					GridLength row_definition_height = row_definitions [index].Height;
					row_heights [index] = row_definition_height.IsAbsolute ? row_definition_height.Value : double.PositiveInfinity;
				} else
				row_heights [0] = finalSize.Height;
			if (has_column_definitions)
				for (index = 0; index < column_count; index++) {
					GridLength column_definition_width = column_definitions [index].Width;
					column_widths [index] = column_definition_width.IsAbsolute ? column_definition_width.Value : double.PositiveInfinity;
				} else
				column_widths [0] = finalSize.Width;
			#endregion
			if (has_row_definitions || has_column_definitions) {
				double [] desired_row_heights = new double [row_count];
				double [] desired_column_widths = new double [column_count];
				foreach (UIElement child in InternalChildren) {
					if (has_row_definitions) {
						int child_row = Math.Min (GetRow (child), row_count - 1);
						if (!row_definitions [child_row].Height.IsAbsolute && GetRowSpan (child) == 1)
							desired_row_heights [child_row] = Math.Max (desired_row_heights [child_row], child.DesiredSize.Height);
					}
					if (has_column_definitions) {
						int child_column = Math.Min (GetColumn (child), column_count - 1);
						if (!column_definitions [child_column].Width.IsAbsolute && GetColumnSpan (child) == 1)
							desired_column_widths [child_column] = Math.Max (desired_column_widths [child_column], child.DesiredSize.Width);
					}
				}
				if (has_row_definitions)
					for (index = 0; index < row_count; index++)
						if (row_definitions [index].Height.IsAuto)
							row_heights [index] = desired_row_heights [index];
				if (has_column_definitions)
					for (index = 0; index < column_count; index++)
						if (column_definitions [index].Width.IsAuto)
							column_widths [index] = desired_column_widths [index];
				#region Apply definition minimum and maximum
				if (has_row_definitions)
					for (index = 0; index < row_count; index++) {
						RowDefinition row_definition = row_definitions [index];
						row_heights [index] = AdjustToBeInRange (row_heights [index], row_definition.MinHeight, row_definition.MaxHeight);
					}
				if (has_column_definitions)
					for (index = 0; index < column_count; index++) {
						ColumnDefinition column_definition = column_definitions [index];
						column_widths [index] = AdjustToBeInRange (column_widths [index], column_definition.MinWidth, column_definition.MaxWidth);
					}
				#endregion
				#region Distribute remaining space to rows/columns with star size
				double total_star;
				double remaining_lenght;
				double star_ratio;
				bool [] uses_star_sizing;
				bool current_star_sizing_valid;
				if (has_row_definitions) {
					uses_star_sizing = new bool [row_count];
					for (index = 0; index < row_count; index++)
						uses_star_sizing [index] = row_definitions [index].Height.IsStar;
					do {
						current_star_sizing_valid = true;
						total_star = 0;
						remaining_lenght = finalSize.Height;
						for (index = 0; index < row_count; index++) {
							if (uses_star_sizing [index])
								total_star += row_definitions [index].Height.Value;
							else
								remaining_lenght -= row_heights [index];
						}
						if (remaining_lenght > 0 && total_star != 0) {
							star_ratio = remaining_lenght / total_star;
							for (index = 0; index < row_count; index++)
								if (uses_star_sizing [index]) {
									RowDefinition definition = row_definitions [index];
									double proposed_lenght = Math.Max (definition.Height.Value * star_ratio, desired_row_heights [index]);
									if (proposed_lenght < definition.MinHeight) {
										row_heights [index] = definition.MinHeight;
										uses_star_sizing [index] = false;
										current_star_sizing_valid = false;
										break;
									} else if (proposed_lenght > definition.MaxHeight) {
										row_heights [index] = definition.MaxHeight;
										uses_star_sizing [index] = false;
										current_star_sizing_valid = false;
										break;
									} else
										row_heights [index] = proposed_lenght;
								}
						}
					} while (!current_star_sizing_valid);
				}
				if (has_column_definitions) {
					uses_star_sizing = new bool [column_count];
					for (index = 0; index < column_count; index++)
						uses_star_sizing [index] = column_definitions [index].Width.IsStar;
					do {
						current_star_sizing_valid = true;
						total_star = 0;
						remaining_lenght = finalSize.Width;
						for (index = 0; index < column_count; index++) {
							if (uses_star_sizing [index])
								total_star += column_definitions [index].Width.Value;
							else
								remaining_lenght -= column_widths [index];
						}
						if (remaining_lenght > 0 && total_star != 0) {
							star_ratio = remaining_lenght / total_star;
							for (index = 0; index < column_count; index++)
								if (uses_star_sizing [index]) {
									ColumnDefinition definition = column_definitions [index];
									double proposed_lenght = Math.Max (definition.Width.Value * star_ratio, desired_column_widths [index]);
									if (proposed_lenght < definition.MinWidth) {
										column_widths [index] = definition.MinWidth;
										uses_star_sizing [index] = false;
										current_star_sizing_valid = false;
										break;
									} else if (proposed_lenght > definition.MaxWidth) {
										column_widths [index] = definition.MaxWidth;
										uses_star_sizing [index] = false;
										current_star_sizing_valid = false;
										break;
									} else
										column_widths [index] = proposed_lenght;
								}
						}
					} while (!current_star_sizing_valid);
				}
				#endregion
				#region Set row/column definitions ActualWidth/Height and Offset values
				double offset;
				if (has_row_definitions) {
					remaining_lenght = finalSize.Height;
					offset = 0;
					for (index = 0; index < row_count; index++) {
						if (row_heights [index] > remaining_lenght)
							row_heights [index] = remaining_lenght;
						remaining_lenght -= row_heights [index];
						RowDefinition definition = row_definitions [index];
						definition.Offset = offset;
						offset += definition.ActualHeight = row_heights [index];
					}
				}
				if (has_column_definitions) {
					remaining_lenght = finalSize.Width;
					offset = 0;
					for (index = 0; index < column_count; index++) {
						if (column_widths [index] > remaining_lenght)
							column_widths [index] = remaining_lenght;
						remaining_lenght -= column_widths [index];
						ColumnDefinition definition = column_definitions [index];
						definition.Offset = offset;
						offset += definition.ActualWidth = column_widths [index];
					}
				}
				#endregion
			}
			#region Arrange children
			foreach (UIElement child in InternalChildren) {
				Rect rect = new Rect ();
				int row_column = Math.Min (GetRow (child), row_count - 1);
				for (index = 0; index < row_column; index++)
					rect.Y += row_heights [index];
				int maximum = Math.Min (row_column + GetRowSpan (child), row_count);
				for (index = row_column; index < maximum; index++)
					rect.Height += row_heights [index];
				row_column = Math.Min (GetColumn (child), column_count - 1);
				for (index = 0; index < row_column; index++)
					rect.X += column_widths [index];
				maximum = Math.Min (row_column + GetColumnSpan (child), column_count);
				for (index = row_column; index < maximum; index++)
					rect.Width += column_widths [index];
				child.Arrange (rect);
			}
			#endregion
			#region Create or update GridLinesRenderer
			if (grid_lines_renderer == null) {
				if (ShowGridLines && (has_row_definitions || has_column_definitions)) {
					grid_lines_renderer = new GridLinesRenderer (this);
					AddVisualChild (grid_lines_renderer);
					grid_lines_renderer.Rebuild ();
				}
			} else
				grid_lines_renderer.Rebuild ();
			#endregion
			return finalSize;
		}

		protected override Visual GetVisualChild (int index)
		{
			if (grid_lines_renderer != null && index == InternalChildren.Count)
				return grid_lines_renderer;
			return base.GetVisualChild (index);
		}

		protected override Size MeasureOverride (Size availableSize)
		{
			measure_called = true;
			bool has_row_definitions = row_definitions.Count != 0;
			bool has_column_definitions = column_definitions.Count != 0;
			int row_count = GetRowColumnCount (row_definitions.Count);
			int column_count = GetRowColumnCount (column_definitions.Count);
			double [] row_heights = new double [row_count];
			double [] column_widths = new double [column_count];
			int index;
			#region Compute initial row and column size
			if (has_row_definitions)
				for (index = 0; index < row_definitions.Count; index++) {
					GridLength row_definition_height = row_definitions [index].Height;
					row_heights [index] = row_definition_height.IsAbsolute ? row_definition_height.Value : double.PositiveInfinity;
				} else
				row_heights [0] = double.PositiveInfinity;
			if (has_column_definitions)
				for (index = 0; index < column_definitions.Count; index++) {
					GridLength column_definition_width = column_definitions [index].Width;
					column_widths [index] = column_definition_width.IsAbsolute ? column_definition_width.Value : double.PositiveInfinity;
				} else
				column_widths [0] = double.PositiveInfinity;
			#endregion
			#region Apply definition minimum and maximum
			if (has_row_definitions)
				for (index = 0; index < row_count; index++) {
					RowDefinition row_definition = row_definitions [index];
					row_heights [index] = AdjustToBeInRange (row_heights [index], row_definition.MinHeight, row_definition.MaxHeight);
				}
			if (has_column_definitions)
				for (index = 0; index < column_count; index++) {
					ColumnDefinition column_definition = column_definitions [index];
					column_widths [index] = AdjustToBeInRange (column_widths [index], column_definition.MinWidth, column_definition.MaxWidth);
				}
			#endregion
			double [] desired_row_heights = new double [row_count];
			double [] desired_column_widths = new double [column_count];
			double remaining_lenght;
			#region Measure children
			List<UIElement> children_in_auto_size_definitions = new List<UIElement> ();
			List<UIElement> other_children = new List<UIElement> ();
			foreach (UIElement child in InternalChildren) {
				if (has_row_definitions) {
					if (row_definitions [Math.Min (GetRow (child), row_count - 1)].Height.IsAuto) {
						children_in_auto_size_definitions.Add (child);
						continue;
					}
				}
				if (has_column_definitions) {
					if (column_definitions [Math.Min (GetColumn (child), column_count - 1)].Width.IsAuto) {
						children_in_auto_size_definitions.Add (child);
						continue;
					}
				}
				other_children.Add (child);
			}
			children_in_auto_size_definitions.AddRange (other_children);
			foreach (UIElement child in children_in_auto_size_definitions) {
				#region Distribute remaining space to rows/columns with star size
				double [] row_star_heights = new double [row_count];
				Array.Copy (row_heights, row_star_heights, row_count);
				double [] column_star_widths = new double [column_count];
				Array.Copy (column_widths, column_star_widths, column_count);
				double total_star;
				double star_ratio;
				bool [] uses_star_sizing;
				bool current_star_sizing_valid;
				GridLength lenght;
				if (has_row_definitions && !double.IsPositiveInfinity (availableSize.Height)) {
					uses_star_sizing = new bool [row_count];
					for (index = 0; index < row_count; index++)
						uses_star_sizing [index] = row_definitions [index].Height.IsStar;
					do {
						current_star_sizing_valid = true;
						total_star = 0;
						remaining_lenght = availableSize.Height;
						for (index = 0; index < row_count; index++) {
							lenght = row_definitions [index].Height;
							if (uses_star_sizing [index])
								total_star += lenght.Value;
							else
								if (lenght.IsAuto)
									remaining_lenght -= desired_row_heights [index];
								else if (!double.IsPositiveInfinity (row_heights [index]))
									remaining_lenght -= row_heights [index];
						}
						if (remaining_lenght > 0 && total_star != 0) {
							star_ratio = remaining_lenght / total_star;
							for (index = 0; index < row_count; index++)
								if (uses_star_sizing [index]) {
									RowDefinition definition = row_definitions [index];
									double proposed_lenght = definition.Height.Value * star_ratio;
									if (proposed_lenght < definition.MinHeight) {
										row_star_heights [index] = definition.MinHeight;
										uses_star_sizing [index] = false;
										current_star_sizing_valid = false;
										break;
									} else if (proposed_lenght > definition.MaxHeight) {
										row_star_heights [index] = definition.MaxHeight;
										uses_star_sizing [index] = false;
										current_star_sizing_valid = false;
										break;
									} else
										row_star_heights [index] = proposed_lenght;
								}
						}
					} while (!current_star_sizing_valid);
					remaining_lenght = availableSize.Height;
					for (index = 0; index < row_count; index++) {
						if (row_definitions [index].Height.IsAuto)
							continue;
						if (row_star_heights [index] > remaining_lenght)
							row_star_heights [index] = remaining_lenght;
						remaining_lenght -= row_star_heights [index];
					}
				}
				if (has_column_definitions && !double.IsPositiveInfinity (availableSize.Width)) {
					uses_star_sizing = new bool [column_count];
					for (index = 0; index < column_count; index++)
						uses_star_sizing [index] = column_definitions [index].Width.IsStar;
					do {
						current_star_sizing_valid = true;
						total_star = 0;
						remaining_lenght = availableSize.Width;
						for (index = 0; index < column_count; index++) {
							lenght = column_definitions [index].Width;
							if (uses_star_sizing [index])
								total_star += lenght.Value;
							else
								if (lenght.IsAuto)
									remaining_lenght -= desired_column_widths [index];
								else if (!double.IsPositiveInfinity (column_widths [index]))
									remaining_lenght -= column_widths [index];
						}
						if (remaining_lenght > 0 && total_star != 0) {
							star_ratio = remaining_lenght / total_star;
							for (index = 0; index < column_count; index++)
								if (uses_star_sizing [index]) {
									ColumnDefinition definition = column_definitions [index];
									double proposed_lenght = definition.Width.Value * star_ratio;
									if (proposed_lenght < definition.MinWidth) {
										column_star_widths [index] = definition.MinWidth;
										uses_star_sizing [index] = false;
										current_star_sizing_valid = false;
										break;
									} else if (proposed_lenght > definition.MaxWidth) {
										column_star_widths [index] = definition.MaxWidth;
										uses_star_sizing [index] = false;
										current_star_sizing_valid = false;
										break;
									} else
										column_star_widths [index] = proposed_lenght;
								}
						}
					} while (!current_star_sizing_valid);
					remaining_lenght = availableSize.Width;
					for (index = 0; index < column_count; index++) {
						if (column_definitions [index].Width.IsAuto)
							continue;
						if (column_star_widths [index] > remaining_lenght)
							column_star_widths [index] = remaining_lenght;
						remaining_lenght -= column_star_widths [index];
					}
				}
				#endregion
				int child_row = Math.Min (GetRow (child), row_count - 1);
				int child_column = Math.Min (GetColumn (child), column_count - 1);
				int child_row_span = GetRowSpan (child);
				int child_column_span = GetColumnSpan (child);
				Size child_constraint = new Size ();
				int maximum;
				if (has_row_definitions) {
					maximum = Math.Min (child_row + child_row_span, row_count);
					for (index = child_row; index < maximum; index++)
						child_constraint.Height += row_definitions [index].Height.IsStar ? row_star_heights [index] : row_heights [index];
					if (double.IsPositiveInfinity (child_constraint.Height) && child_row_span == 1 && row_definitions [child_row].Height.IsStar)
						child_constraint.Height = availableSize.Height;
				} else
					child_constraint.Height = availableSize.Height;
				if (has_column_definitions) {
					maximum = Math.Min (child_column + child_column_span, column_count);
					for (index = child_column; index < maximum; index++)
						child_constraint.Width += column_definitions [index].Width.IsStar ? column_star_widths [index] : column_widths [index];
					if (double.IsPositiveInfinity (child_constraint.Width) && child_column_span == 1 && column_definitions [child_column].Width.IsStar)
						child_constraint.Width = availableSize.Width;
				} else
					child_constraint.Width = availableSize.Width;
				child.Measure (child_constraint);
				if (child_row_span == 1)
					desired_row_heights [child_row] = Math.Max (desired_row_heights [child_row], child.DesiredSize.Height);
				if (child_column_span == 1)
					desired_column_widths [child_column] = Math.Max (desired_column_widths [child_column], child.DesiredSize.Width);
			}
			#endregion
			for (index = 0; index < row_count; index++)
				if (double.IsPositiveInfinity (row_heights [index]))
					row_heights [index] = desired_row_heights [index];
			for (index = 0; index < column_count; index++)
				if (double.IsPositiveInfinity (column_widths [index]))
					column_widths [index] = desired_column_widths [index];
			#region Apply definition minimum and maximum
			if (has_row_definitions)
				for (index = 0; index < row_count; index++) {
					RowDefinition row_definition = row_definitions [index];
					row_heights [index] = AdjustToBeInRange (row_heights [index], row_definition.MinHeight, row_definition.MaxHeight);
				}
			if (has_column_definitions)
				for (index = 0; index < column_count; index++) {
					ColumnDefinition column_definition = column_definitions [index];
					column_widths [index] = AdjustToBeInRange (column_widths [index], column_definition.MinWidth, column_definition.MaxWidth);
				}
			#endregion
			if (has_row_definitions) {
				remaining_lenght = availableSize.Height;
				for (index = 0; index < row_count; index++) {
					if (row_heights [index] > remaining_lenght)
						row_heights [index] = remaining_lenght;
					remaining_lenght -= row_heights [index];
				}
			}
			if (has_column_definitions) {
				remaining_lenght = availableSize.Width;
				for (index = 0; index < column_count; index++) {
					if (column_widths [index] > remaining_lenght)
						column_widths [index] = remaining_lenght;
					remaining_lenght -= column_widths [index];
				}
			}
			Size result = new Size ();
			for (index = 0; index < row_count; index++)
				result.Height += row_heights [index];
			for (index = 0; index < column_count; index++)
				result.Width += column_widths [index];
			return result;
		}

		protected override void OnVisualChildrenChanged (DependencyObject visualAdded, DependencyObject visualRemoved)
		{
			base.OnVisualChildrenChanged (visualAdded, visualRemoved);
			//WDTDH
		}
		#endregion

		#region Private Methods
		static int GetRowColumnCount (int definitionsCount)
		{
			return definitionsCount == 0 ? 1 : definitionsCount;
		}

		static double AdjustToBeInRange (double value, double minimum, double maximum)
		{
			if (value < minimum)
				return minimum;
			if (value > maximum)
				return maximum;
			return value;
		}

		static void InvalidateGridMeasure (DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Grid grid = VisualTreeHelper.GetParent (d) as Grid;
			if (grid == null)
				return;
			grid.InvalidateMeasure ();
		}

		static bool ValidateRowColumn (object value)
		{
			return (int)value >= 0;
		}

		static bool ValidateSpan (object value)
		{
			return (int)value > 0;
		}
		#endregion

		#region Classes
		class GridLinesRenderer : DrawingVisual
		{
			Grid grid;
			int row_count;
			int column_count;
			double [] row_offsets;
			double [] column_offsets;

			public GridLinesRenderer (Grid grid)
			{
				this.grid = grid;
			}

			public void Rebuild ()
			{
				bool changed = false;
				int definition_index;
				if (grid.row_definitions.Count != row_count) {
					changed = true;
					row_count = grid.row_definitions.Count;
					row_offsets = new double [row_count];
					for (definition_index = 1; definition_index < row_count; definition_index++)
						row_offsets [definition_index] = grid.row_definitions [definition_index].Offset;
				} else
					for (definition_index = 1; definition_index < row_count; definition_index++) {
						if (row_offsets [definition_index] != grid.row_definitions [definition_index].Offset)
							changed = true;
						row_offsets [definition_index] = grid.row_definitions [definition_index].Offset;
					}
				if (grid.column_definitions.Count != column_count) {
					changed = true;
					column_count = grid.column_definitions.Count;
					column_offsets = new double [column_count];
					for (definition_index = 1; definition_index < column_count; definition_index++)
						column_offsets [definition_index] = grid.column_definitions [definition_index].Offset;
				} else
					for (definition_index = 1; definition_index < column_count; definition_index++) {
						if (column_offsets [definition_index] != grid.column_definitions [definition_index].Offset)
							changed = true;
						column_offsets [definition_index] = grid.column_definitions [definition_index].Offset;
					}

				//FIXME: I don't know why I can't do this only when "changed".
				//if (changed) {
				DrawingContext drawing_context = RenderOpen ();
				for (definition_index = 1; definition_index < row_count; definition_index++)
					DrawLine (drawing_context, grid.row_definitions [definition_index].Offset, true);
				for (definition_index = 1; definition_index < column_count; definition_index++)
					DrawLine (drawing_context, grid.column_definitions [definition_index].Offset, false);
				drawing_context.Close ();
				//}

				if (changed)
					grid.InvalidateMeasure ();
			}

			void DrawLine (DrawingContext drawingContext, double position, bool horizontal)
			{
				Point point1 = new Point (horizontal ? 0 : position, horizontal ? position : 0);
				Point point2 = new Point (horizontal ? grid.ActualWidth : position, horizontal ? position : grid.ActualHeight);
				const int DashSize = 4;
				Pen pen = new Pen (Brushes.Blue, 1);
				pen.DashStyle = new DashStyle (new double [] { DashSize, DashSize }, 0);
				drawingContext.DrawLine (pen, point1, point2);
				pen = new Pen (Brushes.Yellow, 1);
				pen.DashStyle = new DashStyle (new double [] { DashSize, DashSize }, DashSize);
				drawingContext.DrawLine (pen, point1, point2);
			}
		}
		#endregion
	}
}