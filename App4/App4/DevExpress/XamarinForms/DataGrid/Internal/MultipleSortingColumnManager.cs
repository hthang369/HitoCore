// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using DevExpress.Data;
    using DevExpress.XamarinForms.DataGrid;
    using System;
    using System.Collections.Generic;
    
    internal class MultipleSortingColumnManager : SortingColumnManager
    {
        private readonly SortIndexComparer columnsComparer;
        private List<GridColumn> sortByIndexColumns;
        
        public MultipleSortingColumnManager(ISortGroupDataSettings dataController) : base(dataController)
        {
            this.sortByIndexColumns = new List<GridColumn>();
            this.columnsComparer = new SortIndexComparer();
        }
        
        private void CorrectSortIndex()
        {
            for (int i = 0; i < (this.sortByIndexColumns.get_Count() - 1); i++)
            {
                if ((this.sortByIndexColumns.get_Item(i).SortIndex != -1) && (this.sortByIndexColumns.get_Item(i).SortIndex == this.sortByIndexColumns.get_Item((int) (i + 1)).SortIndex))
                {
                    List<GridColumn> list = new List<GridColumn>();
                    int num2 = i;
                    while (true)
                    {
                        if ((i >= (this.sortByIndexColumns.get_Count() - 1)) || (this.sortByIndexColumns.get_Item(i).SortIndex != this.sortByIndexColumns.get_Item((int) (i + 1)).SortIndex))
                        {
                            list.Add(this.sortByIndexColumns.get_Item(i));
                            this.sortByIndexColumns.RemoveRange(num2, list.get_Count());
                            foreach (GridColumn column in base.Columns)
                            {
                                if (list.IndexOf(column) >= 0)
                                {
                                    num2++;
                                    this.sortByIndexColumns.Insert(num2, column);
                                }
                            }
                            break;
                        }
                        list.Add(this.sortByIndexColumns.get_Item(i));
                        i++;
                    }
                }
            }
        }
        
        protected override SortDescriptor<IRowData> CreateSortComparer()
        {
            ComplexSortDescriptor<IRowData> descriptor = new ComplexSortDescriptor<IRowData>();
            foreach (GridColumn column in this.sortByIndexColumns)
            {
                if (column.SortIndex <= -1)
                {
                    continue;
                }
                if ((column.SortOrder != ColumnSortOrder.None) && !column.IsGrouped)
                {
                    descriptor.Add(column.SortComparer);
                }
            }
            return ((descriptor.Descriptors.get_Count() <= 0) ? null : descriptor.TryConvertToSingle());
        }
        
        private int GetMaxSortIndex()
        {
            int sortIndex = -1;
            if (this.sortByIndexColumns.get_Count() > 0)
            {
                sortIndex = this.sortByIndexColumns.get_Item((int) (this.sortByIndexColumns.get_Count() - 1)).SortIndex;
                if (sortIndex < -1)
                {
                    sortIndex = -1;
                }
            }
            return sortIndex;
        }
        
        protected override void PrepareSorting(GridColumn addedColumn)
        {
            if (base.Columns != null)
            {
                int maxSortIndex = this.GetMaxSortIndex();
                foreach (GridColumn column in base.Columns)
                {
                    if (this.UpdateSortIndex(column, maxSortIndex + 1))
                    {
                        maxSortIndex++;
                    }
                }
                this.sortByIndexColumns.Clear();
                this.sortByIndexColumns.AddRange((IEnumerable<GridColumn>) base.Columns);
                this.sortByIndexColumns.Sort(this.columnsComparer);
                this.CorrectSortIndex();
            }
        }
        
        protected override void SortIndexChanged(GridColumn sender, int sortIndex)
        {
            this.sortByIndexColumns.Sort(this.columnsComparer);
            this.CorrectSortIndex();
            base.GroupAndSortData();
        }
        
        protected override void SortOrderChanged(GridColumn sender, ColumnSortOrder sortOrder)
        {
            this.UpdateSortIndex(sender, this.GetMaxSortIndex() + 1);
            this.sortByIndexColumns.Sort(this.columnsComparer);
            base.GroupAndSortData();
        }
        
        private bool UpdateSortIndex(GridColumn column, int newSortIndex)
        {
            bool flag;
            if (base.IsProcessing)
            {
                return false;
            }
            base.IsProcessing = true;
            if (column.SortOrder == ColumnSortOrder.None)
            {
                column.SortIndex = -1;
                flag = false;
            }
            else if ((column.SortIndex > -1) || (newSortIndex == -1))
            {
                flag = false;
            }
            else
            {
                column.SortIndex = newSortIndex;
                flag = true;
            }
            base.IsProcessing = false;
            return flag;
        }
        
        private class SortIndexComparer : IComparer<GridColumn>
        {
            public int Compare(GridColumn x, GridColumn y) => 
                ((int) x.SortIndex).CompareTo(y.SortIndex);
        }
    }
}