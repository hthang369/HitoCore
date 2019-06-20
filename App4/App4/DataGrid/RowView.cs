using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    internal sealed class RowView : ViewCell
    {
        Grid rowLayout;
        public static readonly BindableProperty DataGridProperty;
        static RowView()
        {
            DataGridProperty = BindingUtils.CreateProperty<RowView, GridControl>(nameof(DataGrid), null, OnDataGridChanged);
        }
        private void CreateRowView()
        {
            rowLayout = new Grid()
            {
                RowSpacing = 0,
                //ColumnSpacing = new Thickness().HorizontalThickness / 2
            };
            DataGrid.VisibleColumns.ToList().ForEach(c =>
            {
                rowLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = c.Width });
                ContentView cell = new ContentView()
                {
                    Padding = 0
                };

                Label text = new Label
                {
                    LineBreakMode = LineBreakMode.WordWrap
                };
                text.SetBinding(Label.TextProperty, new Binding(c.FieldName, BindingMode.Default));
                cell.Content = text;
                rowLayout.Children.Add(cell);
                Grid.SetColumn(cell, c.SortIndex);
            });
            View = rowLayout;
        }
        static void OnDataGridChanged(BindableObject bindable, GridControl oldValue, GridControl newValue)
        {
            ((RowView)bindable).CreateRowView();
        }

        public GridControl DataGrid
        {
            get => (GridControl)base.GetValue(DataGridProperty);
            set => base.SetValue(DataGridProperty, value);
        }
    }
}
