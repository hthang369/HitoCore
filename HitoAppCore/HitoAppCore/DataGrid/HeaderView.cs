using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.DataGrid
{
    public class HeaderView : GridLayout
    {
        #region Fields
        private List<GridColumn> columns;
        public static readonly BindableProperty DataGridProperty;
        #endregion

        #region Contructor
        static HeaderView()
        {
            DataGridProperty = BindingUtils.CreateProperty<RowView, DataGrid>(nameof(GridControl));
        }
        public HeaderView(DataGrid ctrl)
        {
            GridControl = ctrl;
        }
        #endregion

        #region Methods
        public void InitColumns(IEnumerable<GridColumn> gridColumns)
        {
            this.columns = gridColumns as List<GridColumn>;
            this.InitializeContent();
        }
        private void InitializeContent()
        {
            ColumnSpacing = GridControl.BorderThickness.HorizontalThickness / 2;
            Padding = new Thickness(GridControl.BorderThickness.Left, GridControl.BorderThickness.Top, GridControl.BorderThickness.Right, 0);
            foreach (GridColumn item in columns)
            {
                ColumnDefinition definition = new ColumnDefinition();
                if (item.Width != double.NaN) definition.Width = item.Width;
                ColumnDefinitions.Add(definition);
                CellView cell = new CellView(item, GridControl);
                grid.Children.Add(cell);
                Grid.SetColumn(cell, (item.SortIndex < 0) ? grid.Children.IndexOf(cell) : item.SortIndex);
            }
        }
        #endregion

        #region Properties
        public DataGrid GridControl
        {
            get => (DataGrid)base.GetValue(DataGridProperty);
            set => base.SetValue(DataGridProperty, value);
        }
        #endregion
    }
}
