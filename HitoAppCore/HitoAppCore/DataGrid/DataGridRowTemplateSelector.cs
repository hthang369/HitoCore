﻿namespace Xamarin.Forms.DataGrid
{
	internal class DataGridRowTemplateSelector : DataTemplateSelector
	{
		private static DataTemplate _dataGridRowTemplate;

		public DataGridRowTemplateSelector()
		{
			_dataGridRowTemplate = new DataTemplate(typeof(RowView));
		}

		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			var listView = container as ListView;
			var dataGrid = listView.Parent as DataGrid;
			var items = dataGrid.InternalItems;

			_dataGridRowTemplate.SetValue(RowView.DataGridProperty, dataGrid);
			_dataGridRowTemplate.SetValue(RowView.RowContextProperty, item);

			if (items != null)
				_dataGridRowTemplate.SetValue(RowView.RowIndexProperty, items.IndexOf(item));

			return _dataGridRowTemplate;
		}
	}
}
