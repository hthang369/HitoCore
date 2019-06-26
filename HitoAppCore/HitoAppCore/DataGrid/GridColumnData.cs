using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class GridColumnData : DataTemplateSelector
    {
        private static DataTemplate _dataGridRowTemplate;
        public GridColumnData()
        {
            _dataGridRowTemplate = new DataTemplate(typeof(RowView));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ListView listView = container as ListView;
            GridControl dataGrid = listView.Parent as GridControl;
            _dataGridRowTemplate.SetValue(RowView.DataGridProperty, dataGrid);
            _dataGridRowTemplate.SetValue(RowView.RowContextProperty, item);

            return _dataGridRowTemplate;
        }
    }
}
