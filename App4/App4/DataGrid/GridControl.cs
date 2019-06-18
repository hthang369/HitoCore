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
        public const double DefaultColumnHeaderHeight = 44;
        public const double DefaultRowHeight = 44;
        public const double DefaultColumnWidth = 120.0;
        public static readonly BindableProperty ItemsSourceProperty;
        public static readonly BindableProperty IsReadOnlyProperty;
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
        }
        public GridControl()
        {
            this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(DefaultColumnHeaderHeight, GridUnitType.Star) });
            this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(DefaultRowHeight, GridUnitType.Auto) });
            visibleColumns = new List<GridColumn>();
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
            }
            base.OnParentSet();
        }
        protected virtual GridColumnCollection CreateColumns() => new GridColumnCollection();
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

        }
        private static void OnReadOnlyChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            ((GridControl)bindable).Columns.ToList().ForEach(x => x.IsParentReadOnly = newValue);
        }
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
            if(!ListHelper.AreEqual<GridColumn>(visibleColumns, lst))
            {
                visibleColumns = lst;
                headerView = new HeaderView(visibleColumns);
                this.Children.Add(headerView);
                Grid.SetRow(headerView, 0);
            }
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
        #endregion
        private enum RowDragDirection
        {
            None,
            Right,
            Left
        }
    }
}
