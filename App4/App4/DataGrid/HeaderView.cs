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
        #endregion

        #region Contructor
        public HeaderView(IEnumerable<GridColumn> gridColumns)
        {
            this.grid = new Grid();
            this.columns = gridColumns as List<GridColumn>;
            this.InitDefaultHeaderContent();
            this.InitializeContent();
        }
        #endregion

        #region Methods
        private void InitDefaultHeaderContent()
        {

        }
        private void InitializeContent()
        {
            foreach (GridColumn item in columns)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                CellView cell = new CellView(item.Caption, item.ContentAlignment);
                //cell.BackgroundColor = Color.Red;
                grid.Children.Add(cell);
                Grid.SetColumn(cell, (item.SortIndex < 0) ? grid.Children.IndexOf(cell) : item.SortIndex);
            }
            base.Content = grid;
        }
        #endregion
    }
}
