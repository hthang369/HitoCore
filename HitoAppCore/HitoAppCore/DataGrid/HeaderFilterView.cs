using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Xamarin.Forms.DataGrid
{
    public class HeaderFilterView : ContentView
    {
        #region Fields
        private List<GridColumn> columns;
        private Grid g;
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
            DataGridProperty = BindingUtils.CreateProperty<RowView, DataGrid>(nameof(GridControl));
            TextChangedProperty = BindingUtils.CreateProperty<RowView, object>(nameof(TextChanged), null, OnTextChanged);
            TextProperty = BindingUtils.CreateProperty<RowView, string>(nameof(Text), null, OnTextChanged);
        }

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }

        public HeaderFilterView()
        {
            FieldNameSelected = new Dictionary<string, object>();
            g = new Grid();
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
            base.Padding = 0;
            base.Margin = 0;
            BackgroundColor = Color.BlueViolet;
            HeightRequest = 20;
            foreach (GridColumn item in columns)
            {
                g.ColumnDefinitions.Add(new ColumnDefinition());
                g.VerticalOptions = LayoutOptions.Center;
                g.HorizontalOptions = LayoutOptions.FillAndExpand;
                if (item.AllowAutoFilter)
                {
                    View ctrl = null;
                    if (item.GetPreferredDataType() == typeof(DatePicker))
                    {
                        ctrl = new DatePicker();
                        ctrl.ClassId = item.FieldName;
                        //ctrl.DateSelected += Picker_DateSelected;
                    }
                    else
                    {
                        ctrl = new Entry();
                        ctrl.ClassId = item.FieldName;
                        (ctrl as Entry).Text = Text;
                        (ctrl as Entry).VerticalOptions = LayoutOptions.CenterAndExpand;
                        (ctrl as Entry).Unfocused += HeaderFilterView_Unfocused;
                        //entry.ReturnCommand = OnTextCommand;
                        //entry.TextChanged += Entry_TextChanged;
                        //entry.Triggers.Add(new EventTrigger()
                        //{
                        //    Event = "TextChanged",
                        //    BindingContext = TextChangedProperty
                        //});
                    }
                    //contentView.Content = g;
                    g.Children.Add(ctrl);
                    Grid.SetColumn(ctrl, (item.SortIndex < 0) ? g.Children.IndexOf(ctrl) : item.SortIndex);
                }
                base.Content = g;
            }
        }

        private void HeaderFilterView_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                Entry entry = sender as Entry;
                this.FieldNameSelected.AddItem(entry.ClassId, entry.Text);
                if (this.GridControl.InternalItems != null)
                {
                    List<object> lstSources = new List<object>();
                    IEnumerable<object> lst = (IEnumerable<object>)this.GridControl.ItemsSource;
                    this.GridControl.InternalItems = GetDataSource(FieldNameSelected, lst.ToList(), 0, ref lstSources);
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //(sender as Entry).TextChanged -= Entry_TextChanged;
                //
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
        public DataGrid GridControl
        {
            get => (DataGrid)base.GetValue(DataGridProperty);
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
