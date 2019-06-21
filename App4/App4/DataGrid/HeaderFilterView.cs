using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class HeaderFilterView : ContentView
    {
        #region Fields
        private List<GridColumn> columns;
        private Grid grid;
        #endregion

        #region Contructor
        public HeaderFilterView(IEnumerable<GridColumn> gridColumns)
        {
            this.grid = new Grid();
            this.columns = gridColumns as List<GridColumn>;
            this.InitializeContent();
        }
        #endregion

        #region Methods
        private void InitializeContent()
        {
            foreach (GridColumn item in columns)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                
                //grid.Children.Add(cell);
                //Grid.SetColumn(cell, (item.SortIndex < 0) ? grid.Children.IndexOf(cell) : item.SortIndex);
            }
            base.Content = grid;
        }
        #endregion
    }
}
