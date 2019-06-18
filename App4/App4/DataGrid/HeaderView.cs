using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class HeaderView : ContentView
    {
        #region Fields
        private GridColumnCollection columns;
        private Grid grid;
        #endregion

        #region Contructor
        public HeaderView()
        {
            this.grid = new Grid();
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
                grid.Children.Add(cell, 0, 0);
                Grid.SetColumn(cell, item.SortIndex);
            }
            base.Content = grid;
        }
        #endregion
    }
}
