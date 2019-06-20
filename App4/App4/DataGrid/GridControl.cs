using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class GridControl : GridLayout, IDisposable, IServiceProvider
    {
        #region Fields
        private HeaderView headerView;
        private GridColumnCollection columns;
        private List<GridColumn> visibleColumns;
        private List<object> dataSource;
        ListView listViewSource;
        public const double DefaultColumnHeaderHeight = 44;
        public const double DefaultRowHeight = 44;
        public const double DefaultColumnWidth = 120.0;
        public static readonly BindableProperty ItemsSourceProperty;
        public static readonly BindableProperty IsReadOnlyProperty;
        public static readonly BindableProperty BackgroundHeaderColorProperty;
        public static readonly BindableProperty BackgroundDataColorProperty;
        #endregion

        #region Events
        private void SubscribeColumnEvents()
        {
            if(this.columns != null)
            {
                this.columns.CollectionChanged += Columns_CollectionChanged;
                this.columns.ColumnPropertyChanged += Columns_ColumnPropertyChanged;
            }
        }
        #endregion

        #region Contructor
        static GridControl()
        {
            ItemsSourceProperty = BindingUtils.CreateProperty<GridControl, object>(nameof(ItemsSource), null, OnItemsSourceChanged);
            IsReadOnlyProperty = BindingUtils.CreateProperty<GridControl, bool>(nameof(IsReadOnly), false, OnReadOnlyChanged);
            BackgroundHeaderColorProperty = BindingUtils.CreateProperty<GridControl, Color>(nameof(BackgroundHeaderColor), Color.Transparent, OnBackgroundHeaderColorChanged);
            BackgroundDataColorProperty = BindingUtils.CreateProperty<GridControl, Color>(nameof(BackgroundDataColor), Color.Transparent, OnBackgroundDataColorChanged);
        }

        public GridControl()
        {
            this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(DefaultColumnHeaderHeight, GridUnitType.Star) });
            this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(DefaultRowHeight, GridUnitType.Auto) });
            visibleColumns = new List<GridColumn>();
            dataSource = new List<object>();
            listViewSource = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                ItemTemplate = new GridColumnData()
            };
        }
        #endregion

        #region Methods
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        protected override void OnParentSet()
        {
            if (base.Parent != null)
            {
                UpdateVisibleColumns();
                Children.Add(listViewSource);
                Grid.SetRow(listViewSource, 1);
            }
            base.OnParentSet();
        }
        protected virtual GridColumnCollection CreateColumns() => new GridColumnCollection();
        private void UpdateVisibleColumns()
        {
            List<GridColumn> lst = new List<GridColumn>();
            List<GridColumn> lst1 = new List<GridColumn>();
            List<GridColumn> lst2 = new List<GridColumn>();
            this.Columns.ToList().ForEach(x =>
            {
                if (x.IsVisible)
                {
                    x.SortIndex = Columns.IndexOf(x);
                    if (x.FixedStyle == FixedStyle.Left) lst1.Add(x);
                    else if (x.FixedStyle == FixedStyle.Right) lst2.Add(x);
                    else lst.Add(x);
                }
            });
            lst.InsertRange(0, lst1);
            lst.AddRange(lst2);
            if (!ListHelper.AreEqual<GridColumn>(visibleColumns, lst))
            {
                visibleColumns = lst;
                headerView = new HeaderView(visibleColumns);
                headerView.BackgroundColor = BackgroundHeaderColor;
                this.Children.Add(headerView);
                Grid.SetRow(headerView, 0);
            }
        }
        #endregion

        #region Method Event
        private void Columns_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null)
            {
                foreach (GridColumn item in e.NewItems)
                {
                    item.BindingContext = base.BindingContext;
                    item.IsParentReadOnly = this.IsReadOnly;
                }
            }
        }
        private void Columns_ColumnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsVisible")
            {

            }
            if(e.PropertyName == "IsReadOnly")
            {

            }
            if(e.PropertyName == GridColumn.WidthProperty.PropertyName)
            {

            }
        }
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                ((GridControl)bindable).dataSource = new List<object>(((IEnumerable<object>)newValue).Cast<object>());
            }
            catch
            {

            }
        }
        private static void OnReadOnlyChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            ((GridControl)bindable).Columns.ToList().ForEach(x => x.IsParentReadOnly = newValue);
        }
        
        private static void OnBackgroundDataColorChanged(BindableObject bindable, Color oldValue, Color newValue)
        {
            
        }

        private static void OnBackgroundHeaderColorChanged(BindableObject bindable, Color oldValue, Color newValue)
        {
            
        }
        #endregion

        #region Properties
        public GridColumnCollection Columns
        {
            get
            {
                if(this.columns == null)
                {
                    this.columns = this.CreateColumns();
                }
                return this.columns;
            }
        }
        public object ItemsSource
        {
            get => base.GetValue(ItemsSourceProperty);
            set
            {
                if (value == null) base.ClearValue(ItemsSourceProperty);
                else base.SetValue(ItemsSourceProperty, value);
            }
        }
        public bool IsReadOnly
        {
            get => (bool)base.GetValue(IsReadOnlyProperty);
            set => base.SetValue(IsReadOnlyProperty, value);
        }
        public IReadOnlyList<GridColumn> VisibleColumns => (IReadOnlyList<GridColumn>)this.visibleColumns;
        public Color BackgroundHeaderColor
        {
            get => (Color)base.GetValue(BackgroundHeaderColorProperty);
            set => base.SetValue(BackgroundHeaderColorProperty, value);
        }
        public Color BackgroundDataColor
        {
            get => (Color)base.GetValue(BackgroundDataColorProperty);
            set => base.SetValue(BackgroundDataColorProperty, value);
        }
        #endregion
        private enum RowDragDirection
        {
            None,
            Right,
            Left
        }
    }
}
