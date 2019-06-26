using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class HeaderFilterView : ContentView
    {
        #region Fields
        private List<GridColumn> columns;
        private Grid grid;
        public Dictionary<string, object> FieldNameSelected { get; private set; }
        public static readonly BindableProperty DataGridProperty;
        public static readonly BindableProperty TextChangedProperty;
        public static readonly BindableProperty TextProperty;
        private ICommand OnTextCommand;
        #endregion

        #region Event
        public event EventHandler<TextChangedEventArgs> textChanged;
        public event EventHandler<DateChangedEventArgs> dateChanged;
        #endregion

        #region Contructor
        static HeaderFilterView()
        {
            DataGridProperty = BindingUtils.CreateProperty<RowView, GridControl>(nameof(DataGrid));
            TextChangedProperty = BindingUtils.CreateProperty<RowView, object>(nameof(TextChanged), null, OnTextChanged);
            TextProperty = BindingUtils.CreateProperty<RowView, string>(nameof(Text), null, OnTextChanged);
        }

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }

        public HeaderFilterView()
        {
            this.grid = new Grid();
            FieldNameSelected = new Dictionary<string, object>();
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
                        entry.Text = Text;
                        //entry.ReturnCommand = OnTextCommand;
                        entry.TextChanged += Entry_TextChanged;
                        entry.Triggers.Add(new EventTrigger()
                        {
                            Event = "TextChanged",
                            BindingContext = TextChangedProperty
                        });
                        g.Children.Add(entry);
                    }
                    contentView.Content = g;
                }
                grid.Children.Add(contentView);
                Grid.SetColumn(contentView, (item.SortIndex < 0) ? grid.Children.IndexOf(contentView) : item.SortIndex);
            }
            base.Content = grid;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //(sender as Entry).TextChanged -= Entry_TextChanged;
                //this.FieldNameSelected.AddItem((sender as Entry).ClassId, e.NewTextValue);
                //if (this.DataGrid.DataSource != null)
                //{
                //    List<object> lstSources = new List<object>();
                //    this.DataGrid.listViewSource.ItemsSource = GetDataSource(FieldNameSelected, this.DataGrid.DataSource, 0, ref lstSources);
                //}
            }
            catch
            {

            }
            finally
            {

            }
        }

        private List<object> GetDataSource(Dictionary<string, object> lstColumns, List<object> lstDatas, int idx, ref List<object> lstSources)
        {
            string col = lstColumns.Keys.ElementAtOrDefault(idx);
            if (!string.IsNullOrEmpty(col))
            {
                object val = lstColumns[col];
                lstSources.AddRange(lstDatas.Where(x => Convert.ToString(ObjectUtils.GetPropertyValue(x, col)).Contains(Convert.ToString(val))));
                GetDataSource(lstColumns, lstSources, idx + 1, ref lstSources);
            }
            return lstSources;
        }

        private void Picker_DateSelected(object sender, DateChangedEventArgs e)
        {
            this.FieldNameSelected.AddItem((sender as DatePicker).ClassId, e.NewDate);
            dateChanged?.Invoke(this, e);
        }
        public GridControl DataGrid
        {
            get => (GridControl)base.GetValue(DataGridProperty);
            set => base.SetValue(DataGridProperty, value);
        }
        public object TextChanged
        {
            get => base.GetValue(TextChangedProperty);
            set => base.SetValue(TextChangedProperty, value);
        }
        public string Text
        {
            get => (string)base.GetValue(TextProperty);
            set => base.SetValue(TextProperty, value);
        }
        #endregion
    }
}
