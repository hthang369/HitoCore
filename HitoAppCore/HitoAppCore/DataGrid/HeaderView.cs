using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class HeaderView : ContentView
    {
        #region Fields
        private List<GridColumn> columns;
        private Grid grid;
        public double ColumnSpacing { get; set; }
        #endregion

        #region Contructor
        public HeaderView()
        {
            this.grid = new Grid();
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
            grid.ColumnSpacing = ColumnSpacing;
            grid.Padding = Padding;
            foreach (GridColumn item in columns)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                CellView cell = new CellView(item);
                grid.Children.Add(cell);
                Grid.SetColumn(cell, (item.SortIndex < 0) ? grid.Children.IndexOf(cell) : item.SortIndex);
            }
            base.Content = grid;
        }
        #endregion
    }
}
