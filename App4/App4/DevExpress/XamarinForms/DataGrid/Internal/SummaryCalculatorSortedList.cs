// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using DevExpress.Collections;
    using DevExpress.XamarinForms.Core.ConditionalFormatting.Native;
    using DevExpress.XamarinForms.DataGrid;
    using System;
    using System.Collections.Generic;
    
    public class SummaryCalculatorSortedList : SummaryCalculator
    {
        private List<int> indices;
        private List<object> values;
        private SortedIndices result;
        
        public SummaryCalculatorSortedList(IGridDataController controller, string fieldName, GridColumnSummary summary)
        {
        }
        
        public override bool Begin()
        {
            this.result = null;
            this.indices = new List<int>();
            this.values = new List<object>();
            return true;
        }
        
        public override object End()
        {
            MergeSort.Sort<int>((IList<int>) this.indices, 0, this.indices.Count - 1, new SortComparer(this.values));
            this.result = new SortedIndices(this.indices.ToArray());
            return this.result;
        }
        
        public override object GetFinalResult() => 
            this.result;
        
        public override void ProcessValue(IRowData row, object value, IToDecimalConverter converter)
        {
            this.indices.Add(row.RowHandle);
            this.values.Add(value);
        }
        
        private class SortComparer : IComparer<int>
        {
            private readonly List<object> values;
            
            public SortComparer(List<object> values)
            {
                this.values = values;
            }
            
            public int Compare(int x, int y) => 
                Comparer.Default.Compare(this.values.GetItem(x), this.values.GetItem(y));
        }
    }
}
