using System;
using System.Collections.Generic;
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
        private readonly BindableProperty IsReadOnlyProperty;
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

        }
        public GridControl()
        {
            this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Star) });
            this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Auto) });

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
        public bool IsReadOnly
        {
            get => (bool)base.GetValue(IsReadOnlyProperty);
            set => base.SetValue(IsReadOnlyProperty, value);
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
