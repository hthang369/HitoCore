// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid
{
    using DevExpress.XamarinForms.DataGrid.Internal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    
    internal class ObjectListGridDataSource : IGridDataSource, IGridColumnsProvider
    {
        private readonly PropertyAccessorCache accessorCache = new PropertyAccessorCache();
        private IList list;
        private int selectedRow = -1;
        private NotifyCollectionChangedEventHandler onGroupCollectionChanged;
        private NotifyCollectionChangedEventHandler onRowCollectionChanged;
        
        event NotifyCollectionChangedEventHandler IGridDataSource.GroupCollectionChanged
        {
            add
            {
                this.onGroupCollectionChanged = (NotifyCollectionChangedEventHandler) Delegate.Combine((Delegate) this.onGroupCollectionChanged, (Delegate) value);
            }
            remove
            {
                this.onGroupCollectionChanged = (NotifyCollectionChangedEventHandler) Delegate.Remove((Delegate) this.onGroupCollectionChanged, (Delegate) value);
            }
        }
        
        event NotifyCollectionChangedEventHandler IGridDataSource.RowCollectionChanged
        {
            add
            {
                this.onRowCollectionChanged = (NotifyCollectionChangedEventHandler) Delegate.Combine((Delegate) this.onRowCollectionChanged, (Delegate) value);
            }
            remove
            {
                this.onRowCollectionChanged = (NotifyCollectionChangedEventHandler) Delegate.Remove((Delegate) this.onRowCollectionChanged, (Delegate) value);
            }
        }
        
        public int AddNewRow(IEditableRowData rowData)
        {
            this.List.Add(rowData.DataObject);
            return (this.List.Count - 1);
        }
        
        public IEditableRowData CreateNewRow() => 
            ((this.List?.Count > 0) ? ObjectRowData.Create(Activator.CreateInstance(this.List.get_Item(0).GetType()), this.List?.Count, this.accessorCache) : null);
        
        public int DeleteRow(int rowHandle)
        {
            this.List.RemoveAt(rowHandle);
            return rowHandle;
        }
        
        IGroupInfo IGridDataSource.GetGroup(int groupHandle) => 
            null;
        
        public IList<GridColumn> GenerateColumns()
        {
            if (this.RowCount <= 0)
            {
                return null;
            }
            object obj2 = this.List.get_Item(0);
            return ((obj2 != null) ? new GridColumnAutoGenerator().GenerateColumns(obj2.GetType()) : null);
        }
        
        public IRowData GetRow(int rowHandle, IRowData reuseRow)
        {
            if (this.List == null)
            {
                return null;
            }
            if ((rowHandle < 0) || (rowHandle >= this.List.Count))
            {
                return null;
            }
            ObjectRowData data = reuseRow as ObjectRowData;
            if (data == null)
            {
                return ObjectRowData.Create(this.List[rowHandle], rowHandle, this.accessorCache);
            }
            data.RowHandle = rowHandle;
            data.DataObject = this.List[rowHandle];
            return data;
        }
        
        private void OnDataSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.RaiseRowCollectionChanged(e);
        }
        
        private void OnDataSourceCollectionChanged(object sender, ListChangedEventArgs e)
        {
            IBindingList bindingList = sender as IBindingList;
            if (bindingList != null)
            {
                this.RaiseRowCollectionChanged(EventArgsConverter.Convert(e, bindingList));
            }
        }
        
        protected virtual void RaiseRowCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.onRowCollectionChanged != null)
            {
                this.onRowCollectionChanged(this, e);
            }
        }
        
        private void SubscribeDataSourceEvents()
        {
            INotifyCollectionChanged changed = this.list as INotifyCollectionChanged;
            if (changed != null)
            {
                changed.add_CollectionChanged(new NotifyCollectionChangedEventHandler(this.OnDataSourceCollectionChanged));
            }
            IBindingList list = this.list as IBindingList;
            if (list != null)
            {
                list.add_ListChanged(new ListChangedEventHandler(this.OnDataSourceCollectionChanged));
            }
        }
        
        private void UnsubscribeDataSourceEvents()
        {
            INotifyCollectionChanged changed = this.list as INotifyCollectionChanged;
            if (changed != null)
            {
                changed.remove_CollectionChanged(new NotifyCollectionChangedEventHandler(this.OnDataSourceCollectionChanged));
            }
            IBindingList list = this.list as IBindingList;
            if (list != null)
            {
                list.remove_ListChanged(new ListChangedEventHandler(this.OnDataSourceCollectionChanged));
            }
        }
        
        public IList List
        {
            get => 
                this.list;
            set
            {
                if (!object.ReferenceEquals(this.list, value))
                {
                    this.UnsubscribeDataSourceEvents();
                    this.list = value;
                    this.SelectedRow = 0;
                    this.SubscribeDataSourceEvents();
                }
            }
        }
        
        public bool SupportsSorting =>
            false;
        
        public bool SupportsGrouping =>
            false;
        
        public bool SupportsFiltering =>
            false;
        
        public int SelectedRow
        {
            get
            {
                int rowCount = this.RowCount;
                return ((rowCount > 0) ? ((this.selectedRow < rowCount) ? ((this.selectedRow >= 0) ? this.selectedRow : -1) : (rowCount - 1)) : -1);
            }
            set => 
                (this.selectedRow = value);
        }
        
        public int RowCount =>
            ((this.List == null) ? 0 : this.List.Count);
        
        int IGridDataSource.GroupCount =>
            0;
        
        public Type ActualDataSourceType =>
            base.GetType();
    }
}
