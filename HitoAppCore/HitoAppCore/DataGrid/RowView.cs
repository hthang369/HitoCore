using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xamarin.Forms.DataGrid
{
    internal sealed class RowView : ViewCell
    {
        #region Fields
        Grid rowLayout;
        private static int _rowHandle;
        private bool _hasSelected;
        Color _bgColor;
        Color _textColor;
        public static readonly BindableProperty DataGridProperty;
        public static readonly BindableProperty RowContextProperty;
        public static readonly BindableProperty RowIndexProperty;
        #endregion

        #region Contructor
        static RowView()
        {
            _rowHandle++;
            DataGridProperty = BindingUtils.CreateProperty<RowView, DataGrid>(nameof(GridControl), null, OnDataGridChanged);
            RowContextProperty = BindingUtils.CreateProperty<RowView, object>(nameof(RowContext), null, OnRowContextChanged);
            RowIndexProperty = BindingUtils.CreateProperty<RowView, int>(nameof(RowIndex), 0, OnRowIndexChanged);
        }
        #endregion

        #region Methods
        private void CreateRowView()
        {
            UpdateBackgroundColor();
            rowLayout = new Grid()
            {
                RowSpacing = 0,
                ColumnSpacing = GridControl.BorderThickness.HorizontalThickness / 2,
                Padding = new Thickness(GridControl.BorderThickness.HorizontalThickness / 2, GridControl.BorderThickness.VerticalThickness / 2)
            };
            GridControl.VisibleColumns.ToList().ForEach(c =>
            {
                ColumnDefinition definition = new ColumnDefinition();
                if (!double.IsNaN(c.Width)) definition.Width = c.Width;
                rowLayout.ColumnDefinitions.Add(definition);
                ContentView cell = new ContentView()
                {
                    Padding = 0
                };
                CellData data = new CellData()
                {
                    DisplayText = c.FieldName,
                    Value = c.FieldName,
                    Index = new CellIndex(0, c.FieldName),
                    Source = c.FieldName,
                    TypeData = c.GetPreferredDataType(),
                    DataAlignment = c.ContentAlignment
                };
                View ctrl = GetControl(data);
                ctrl.HorizontalOptions = c.GetControlContentAlignment(true);
                ctrl.VerticalOptions = c.GetControlContentAlignment(true);
                ctrl.SetBinding(GetControlProperty(ctrl), new Binding(c.FieldName, BindingMode.Default, stringFormat: c.DisplayFormat));
                BindableProperty property = GetFontSizeProperty(ctrl);
                if (property != null)
                    ctrl.SetBinding(property, new Binding(DataGrid.FontSizeProperty.PropertyName, BindingMode.Default, source: GridControl));
                property = GetFontFamilyProperty(ctrl);
                if (property != null)
                    ctrl.SetBinding(property, new Binding(DataGrid.FontFamilyProperty.PropertyName, BindingMode.Default, source: GridControl));
                property = GetFontFamilyProperty(ctrl);
                if (property != null)
                    ctrl.SetValue(property, FontAttributes.None);
                cell.Content = ctrl;
                rowLayout.Children.Add(cell);
                Grid.SetColumn(cell, c.SortIndex);
            });
            View = rowLayout;
        }
        private BindableProperty GetControlProperty(View ctrl)
        {
            if (ctrl is Label) return Label.TextProperty;
            else if(ctrl is Entry) return Label.TextProperty;
            else if (ctrl is Switch) return Switch.IsToggledProperty;
            else if (ctrl is DatePicker) return DatePicker.DateProperty;
            else return Label.TextProperty;
        }
        private BindableProperty GetFontSizeProperty(View ctrl)
        {
            if (ctrl is Label) return Label.FontSizeProperty;
            else if (ctrl is Entry) return Entry.FontSizeProperty;
            else if (ctrl is Switch) return null;
            else if (ctrl is DatePicker) return DatePicker.FontSizeProperty;
            else return Label.FontSizeProperty;
        }
        private BindableProperty GetFontFamilyProperty(View ctrl)
        {
            if (ctrl is Label) return Label.FontFamilyProperty;
            else if (ctrl is Entry) return Entry.FontFamilyProperty;
            else if (ctrl is Switch) return null;
            else if (ctrl is DatePicker) return DatePicker.FontFamilyProperty;
            else return Label.FontFamilyProperty;
        }
        private BindableProperty GetFontAttributesProperty(View ctrl)
        {
            if (ctrl is Label) return Label.FontAttributesProperty;
            else if (ctrl is Entry) return Entry.FontAttributesProperty;
            else if (ctrl is Switch) return null;
            else if (ctrl is DatePicker) return DatePicker.FontAttributesProperty;
            else return Label.FontAttributesProperty;
        }
        private View GetControl(CellData cell)
        {
            switch (Type.GetTypeCode(cell.TypeData))
            {
                case TypeCode.String:
                    return new Label()
                    {
                        LineBreakMode = LineBreakMode.WordWrap,
                        HorizontalTextAlignment = cell.DataAlignment
                    };
                case TypeCode.DateTime:
                    return new DatePicker();
                case TypeCode.Decimal:
                case TypeCode.Int32:
                    return new Entry();
                case TypeCode.Boolean:
                    return new Switch();
            }
            return new Label()
            {
                LineBreakMode = LineBreakMode.WordWrap,
                HorizontalTextAlignment = cell.DataAlignment
            };
        }
        private void UpdateBackgroundColor()
        {
            _hasSelected = GridControl.SelectedItem == RowContext;
            int actualIndex = GridControl?.InternalItems?.IndexOf(BindingContext) ?? -1;
            if (actualIndex > -1)
            {
                _bgColor = (GridControl.SelectionEnabled && GridControl.SelectedItem != null && GridControl.SelectedItem == RowContext) ?
                    GridControl.ActiveRowColor : GridControl.RowsBackgroundColorPalette.GetColor(RowIndex, BindingContext);
                _textColor = GridControl.RowsTextColorPalette.GetColor(actualIndex, BindingContext);

                ChangeColor(_bgColor);
            }
        }
        private void ChangeColor(Color color)
        {
            foreach (var v in rowLayout.Children)
            {
                v.BackgroundColor = color;
                var contentView = v as ContentView;
                if (contentView?.Content is Label)
                    ((Label)contentView.Content).TextColor = _textColor;
            }
        }
        static void OnDataGridChanged(BindableObject bindable, DataGrid oldValue, DataGrid newValue)
        {
            ((RowView)bindable).CreateRowView();
        }
        private static void OnRowContextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as RowView).UpdateBackgroundColor();
        }
        private static void OnRowIndexChanged(BindableObject bindable, int oldValue, int newValue)
        {
            (bindable as RowView).UpdateBackgroundColor();
        }
        #endregion

        #region Property
        public DataGrid GridControl
        {
            get => (DataGrid)base.GetValue(DataGridProperty);
            set => base.SetValue(DataGridProperty, value);
        }
        public object RowContext
        {
            get => base.GetValue(RowContextProperty);
            set => base.SetValue(RowContextProperty, value);
        }
        public int RowIndex
        {
            get => (int)base.GetValue(RowIndexProperty);
            set => base.SetValue(RowIndexProperty, value);
        }
        #endregion

    }
}
