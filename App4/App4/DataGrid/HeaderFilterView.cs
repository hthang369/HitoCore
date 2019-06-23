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
        public List<string> FieldNameSelected { get; private set; }
        #endregion

        #region Event
        public event EventHandler<TextChangedEventArgs> textChanged;
        public event EventHandler<DateChangedEventArgs> dateChanged;
        #endregion

        #region Contructor
        public HeaderFilterView()
        {
            this.grid = new Grid();
            FieldNameSelected = new List<string>();
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
            foreach (GridColumn item in columns)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                ContentView contentView = new ContentView();
                if (item.AllowAutoFilter)
                {
                    Grid g = new Grid();
                    g.ColumnDefinitions.Add(new ColumnDefinition());
                    if (item.GetPreferredDataType() == typeof(DatePicker))
                    {
                        DatePicker picker = new DatePicker();
                        picker.ClassId = item.FieldName;
                        picker.DateSelected += Picker_DateSelected;
                        g.Children.Add(picker);
                    }
                    else
                    {
                        Entry entry = new Entry();
                        entry.ClassId = item.FieldName;
                        entry.TextChanged += Entry_TextChanged;
                        g.Children.Add(entry);
                    }
                    contentView.Content = g;
                }
                grid.Children.Add(contentView);
                Grid.SetColumn(contentView, (item.SortIndex < 0) ? grid.Children.IndexOf(contentView) : item.SortIndex);
            }
            base.Content = grid;
        }

        private void Picker_DateSelected(object sender, DateChangedEventArgs e)
        {
            this.FieldNameSelected.AddItem((sender as DatePicker).ClassId);
            dateChanged?.Invoke(this, e);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.FieldNameSelected.AddItem((sender as Entry).ClassId);
            textChanged?.Invoke(this, e);
        }
        #endregion
    }
}
