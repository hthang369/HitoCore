// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using DevExpress.XamarinForms.DataGrid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    
    public class FilteredGridDataSource : TransformationGridDataSource
    {
        private Predicate<IRowData> predicate;
        
        protected override CreateIndexMapResult CreateIndexMap()
        {
            int count = (base.DataSource != null) ? base.DataSource.RowCount : 0;
            List<int> list = new List<int>(count);
            CreateIndexMapResult result = new CreateIndexMapResult {
                IndexMap = (IList<int>) list,
                ShouldResetSelection = true
            };
            if (this.Predicate == null)
            {
                this.PopulateNonFilteredIndexMap(result, count);
            }
            else
            {
                this.PopulateFilteredIndexMap(result, count);
            }
            if (result.ShouldResetSelection)
            {
                result.ShouldResetSelection = false;
                result.NewSelectionRow = list.Count - 1;
            }
            return result;
        }
        
        public override int GetRowHandle(int sourceRowIndex)
        {
            int rowHandle = base.DataSource.GetRowHandle(sourceRowIndex);
            int num2 = base.IndexMap.BinarySearch<int>(rowHandle);
            return ((num2 >= 0) ? num2 : -2147483648);
        }
        
        private void IncrementIndices(int from, int increment)
        {
            int num = base.IndexMap.Count;
            for (int i = from; i < num; i++)
            {
                IList<int> indexMap = base.IndexMap;
                int num3 = i;
                indexMap.set_Item(num3, indexMap.get_Item(num3) + increment);
            }
        }
        
        protected override int OnDataSourceCollectionChangedCore(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (base.IndexMap != null)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        return this.OnRowsAdded(e);
                    
                    case NotifyCollectionChangedAction.Remove:
                        return this.OnRowsRemoved(e);
                    
                    case NotifyCollectionChangedAction.Replace:
                        return this.OnRowReplace(e);
                }
            }
            return base.OnDataSourceCollectionReset();
        }
        
        private int OnRowReplace(NotifyCollectionChangedEventArgs e)
        {
            IRowData row;
            if (e.NewItems.Count > 1)
            {
                return base.OnDataSourceCollectionReset();
            }
            int startingIndex = base.IndexMap.BinarySearch<int>(e.NewStartingIndex);
            if (startingIndex < 0)
            {
                startingIndex = ~startingIndex;
                if (this.Predicate != null)
                {
                    row = base.DataSource.GetRow(e.NewStartingIndex, null);
                    if (this.Predicate(row))
                    {
                        base.IndexMap.Insert(startingIndex, e.NewStartingIndex);
                        List<int> list = new List<int> {
                            startingIndex
                        };
                        CustomNotifyCollectionChangedEventArgs args = new CustomNotifyCollectionChangedEventArgs((NotifyCollectionChangedAction) NotifyCollectionChangedAction.Add, (IList) list, startingIndex) {
                            RowHandle = startingIndex
                        };
                        this.RaiseRowCollectionChanged(args);
                        return args.RowHandle;
                    }
                }
            }
            if (startingIndex >= base.IndexMap.Count)
            {
                return -2147483648;
            }
            if (this.Predicate == null)
            {
                int[] newItems = new int[] { base.IndexMap.get_Item(startingIndex) };
                int[] oldItems = new int[] { base.IndexMap.get_Item(startingIndex) };
                CustomNotifyCollectionChangedEventArgs args2 = new CustomNotifyCollectionChangedEventArgs((NotifyCollectionChangedAction) NotifyCollectionChangedAction.Replace, newItems, oldItems, startingIndex) {
                    RowHandle = startingIndex
                };
                this.RaiseRowCollectionChanged(args2);
                return args2.RowHandle;
            }
            row = base.DataSource.GetRow(base.IndexMap.get_Item(startingIndex), null);
            if (this.Predicate(row))
            {
                int[] newItems = new int[] { base.IndexMap.get_Item(startingIndex) };
                int[] oldItems = new int[] { base.IndexMap.get_Item(startingIndex) };
                CustomNotifyCollectionChangedEventArgs args3 = new CustomNotifyCollectionChangedEventArgs((NotifyCollectionChangedAction) NotifyCollectionChangedAction.Replace, newItems, oldItems, startingIndex) {
                    RowHandle = startingIndex
                };
                this.RaiseRowCollectionChanged(args3);
                return args3.RowHandle;
            }
            if (base.SelectedRow >= startingIndex)
            {
                base.SelectedRow = Math.Max(0, base.SelectedRow - 1);
            }
            int num2 = base.IndexMap.get_Item(startingIndex);
            base.IndexMap.RemoveAt(startingIndex);
            int[] changedItems = new int[] { (int) num2 };
            CustomNotifyCollectionChangedEventArgs args4 = new CustomNotifyCollectionChangedEventArgs((NotifyCollectionChangedAction) NotifyCollectionChangedAction.Remove, changedItems, startingIndex) {
                RowHandle = startingIndex
            };
            this.RaiseRowCollectionChanged(args4);
            return args4.RowHandle;
        }
        
        private int OnRowsAdded(NotifyCollectionChangedEventArgs e)
        {
            List<int> list = new List<int>();
            int from = e.NewStartingIndex;
            int num2 = (from + e.NewItems.Count) - 1;
            IRowData reuseRow = null;
            for (int i = from; i <= num2; i++)
            {
                if (this.Predicate == null)
                {
                    list.Add(i);
                }
                else
                {
                    reuseRow = base.DataSource.GetRow(i, reuseRow);
                    if (this.Predicate(reuseRow))
                    {
                        list.Add(i);
                    }
                }
            }
            from = base.IndexMap.BinarySearch<int>(e.NewStartingIndex);
            if (from < 0)
            {
                from = ~from;
            }
            this.IncrementIndices(from, e.NewItems.Count);
            int num3 = list.Count;
            if (num3 <= 0)
            {
                return -2147483648;
            }
            base.IndexMap.InsertRange<int>(from, (IEnumerable<int>) list);
            if (base.SelectedRow >= from)
            {
                base.SelectedRow += num3;
            }
            CustomNotifyCollectionChangedEventArgs args = new CustomNotifyCollectionChangedEventArgs((NotifyCollectionChangedAction) NotifyCollectionChangedAction.Add, (IList) list, from) {
                RowHandle = from
            };
            this.RaiseRowCollectionChanged(args);
            return args.RowHandle;
        }
        
        private int OnRowsRemoved(NotifyCollectionChangedEventArgs e)
        {
            int from = base.IndexMap.BinarySearch<int>(e.OldStartingIndex);
            if (from < 0)
            {
                from = ~from;
            }
            if (from >= base.IndexMap.Count)
            {
                return -2147483648;
            }
            List<int> list = new List<int>();
            int index = -1;
            int count = 0;
            int num4 = e.OldItems.Count;
            for (int i = 0; (i < num4) && (from < base.IndexMap.Count); i++)
            {
                if (base.IndexMap.get_Item(from) != (e.OldStartingIndex + i))
                {
                    if ((e.OldStartingIndex + i) > base.IndexMap.get_Item(from))
                    {
                        from++;
                        i--;
                    }
                }
                else
                {
                    list.Add(base.IndexMap[i]);
                    count++;
                    if (index < 0)
                    {
                        index = from;
                    }
                    from++;
                }
            }
            this.IncrementIndices(from, -e.OldItems.Count);
            if ((index < 0) || (count <= 0))
            {
                return -2147483648;
            }
            base.IndexMap.RemoveRange<int>(index, count);
            if (base.SelectedRow >= index)
            {
                base.SelectedRow = Math.Max(0, base.SelectedRow - count);
            }
            CustomNotifyCollectionChangedEventArgs args = new CustomNotifyCollectionChangedEventArgs((NotifyCollectionChangedAction) NotifyCollectionChangedAction.Remove, (IList) list, index) {
                RowHandle = index
            };
            this.RaiseRowCollectionChanged(args);
            return args.RowHandle;
        }
        
        private void PopulateFilteredIndexMap(CreateIndexMapResult result, int count)
        {
            IList<int> indexMap = result.IndexMap;
            if (count > 0)
            {
                IRowData reuseRow = null;
                for (int i = 0; i < count; i++)
                {
                    reuseRow = base.DataSource.GetRow(i, reuseRow);
                    if (this.Predicate(reuseRow))
                    {
                        if (result.ShouldResetSelection && (i >= base.Selection.SourceIndex.Value))
                        {
                            result.ShouldResetSelection = false;
                            result.NewSelectionRow = indexMap.Count;
                        }
                        indexMap.Add(i);
                    }
                }
            }
        }
        
        private void PopulateNonFilteredIndexMap(CreateIndexMapResult result, int count)
        {
            IList<int> indexMap = result.IndexMap;
            for (int i = 0; i < count; i++)
            {
                if (result.ShouldResetSelection && (i >= base.Selection.SourceIndex.Value))
                {
                    result.ShouldResetSelection = false;
                    result.NewSelectionRow = indexMap.Count;
                }
                indexMap.Add(i);
            }
        }
        
        public override bool SupportsFiltering =>
            (base.DataSource != null);
        
        public Predicate<IRowData> Predicate
        {
            get => 
                this.predicate;
            set
            {
                if (!object.ReferenceEquals(this.predicate, value))
                {
                    this.predicate = value;
                    this.UpdateIndexMap();
                }
            }
        }
    }
}
