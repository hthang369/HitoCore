using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    internal sealed class RowView : ViewCell
    {
        #region Fields
        Grid rowLayout;
        private static int _rowHandle;
        public static readonly BindableProperty DataGridProperty;
        public static readonly BindableProperty RowContextProperty;
        public static readonly BindableProperty RowIndexProperty;
        #endregion

        #region Contructor
        static RowView()
        {
            _rowHandle++;
            DataGridProperty = BindingUtils.CreateProperty<RowView, GridControl>(nameof(DataGrid), null, OnDataGridChanged);
            RowContextProperty = BindingUtils.CreateProperty<RowView, object>(nameof(RowContext), null, OnRowContextChanged);
            RowIndexProperty = BindingUtils.CreateProperty<RowView, int>(nameof(RowIndex), 0, OnRowIndexChanged);
        }
        #endregion

        #region Methods
        private void CreateRowView()
        {
            rowLayout = new Grid()
            {
                RowSpacing = 0,
                ColumnSpacing = DataGrid.BorderThickness.HorizontalThickness / 2,
                Padding = new Thickness(DataGrid.BorderThickness.HorizontalThickness / 2, DataGrid.BorderThickness.VerticalThickness / 2)
            };
            DataGrid.VisibleColumns.ToList().ForEach(c =>
            {
                rowLayout.ColumnDefinitions.Add(new ColumnDefinition());
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
                ctrl.SetBinding(GetControlProperty(ctrl), new Binding(c.FieldName, BindingMode.Default));
                cell.Content = ctrl;
                rowLayout.Children.Add(cell);
                Grid.SetColumn(cell, c.SortIndex);
            });
            View = rowLayout;
        }
        private BindableProperty GetControlProperty(View ctrl)
        {
            if (ctrl is Label) return Label.TextProperty;
            else if (ctrl is Entry) return Entry.TextProperty;
            else if (ctrl is Switch) return Switch.IsToggledProperty;
            else if (ctrl is DatePicker) return DatePicker.DateProperty;
            else return Label.TextProperty;
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
        static void OnDataGridChanged(BindableObject bindable, GridControl oldValue, GridControl newValue)
        {
            ((RowView)bindable).CreateRowView();
        }
        private static void OnRowContextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }
        private static void OnRowIndexChanged(BindableObject bindable, int oldValue, int newValue)
        {
            
        }
        #endregion

        #region Property
        public GridControl DataGrid
        {
            get => (GridControl)base.GetValue(DataGridProperty);
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
