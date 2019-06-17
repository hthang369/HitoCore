using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HitoAppCore.DataGrid
{
    public class GridColumnCollection : ObservableCollection<GridColumn>
    {
        // Fields
        private readonly Dictionary<string, GridColumn> fieldNameMap = new Dictionary<string, GridColumn>();
        
        // Events
        [CompilerGenerated]
        public event PropertyChangedEventHandler ColumnPropertyChanged;

        // Methods
        protected override void ClearItems()
        {
            for (int i = 0; i < base.Count; i++)
            {
                this.UnsubscribeItemEvents(base[i]);
            }
            base.ClearItems();
            this.ResetColumnsByFieldsNameMap();
        }

        public GridColumn GetColumnByFieldName(string fieldName)
        {
            GridColumn column;
            if (string.IsNullOrEmpty(fieldName) || (base.Count == 0))
            {
                return null;
            }
            if (!this.fieldNameMap.TryGetValue(fieldName, out column))
            {
                using (IEnumerator<GridColumn> enumerator = base.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        GridColumn current = enumerator.Current;
                        if (current.FieldName == fieldName)
                        {
                            this.fieldNameMap.Add(fieldName, current);
                            return current;
                        }
                    }
                }
            }
            return column;
        }

        protected override void InsertItem(int index, GridColumn item)
        {
            this.SubscribeItemEvents(item);
            base.InsertItem(index, item);
        }

        private void OnColumnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.ColumnPropertyChanged != null)
            {
                this.ColumnPropertyChanged(sender, e);
            }
        }

        protected override void RemoveItem(int index)
        {
            this.UnsubscribeItemEvents(base[index]);
            base.RemoveItem(index);
            this.ResetColumnsByFieldsNameMap();
        }

        protected void ResetColumnsByFieldsNameMap()
        {
            this.fieldNameMap.Clear();
        }

        protected override void SetItem(int index, GridColumn item)
        {
            this.UnsubscribeItemEvents(base[index]);
            base.SetItem(index, item);
            this.SubscribeItemEvents(item);
        }

        private void SubscribeItemEvents(GridColumn column)
        {
            if (column != null)
            {
                column.AfterPropertyChanged += new PropertyChangedEventHandler(this.OnColumnPropertyChanged);
            }
        }

        private void UnsubscribeItemEvents(GridColumn column)
        {
            if (column != null)
            {
                column.AfterPropertyChanged -= new PropertyChangedEventHandler(this.OnColumnPropertyChanged);
            }
        }

        // Properties
        public GridColumn this[string fieldName] =>
            this.GetColumnByFieldName(fieldName);
    }

    public enum UnboundColumnType
    {
        Bound,
        Integer,
        Decimal,
        DateTime,
        String,
        Boolean,
        Object
    }

    public enum ColumnContentAlignment
    {
        Start,
        Center,
        End
    }

    public enum ColumnSortOrder
    {
        None,
        Ascending,
        Descending
    }

    public enum DefaultBoolean
    {
        True,
        False,
        Default
    }

    public enum ColumnFilterMode
    {
        Value,
        DisplayText
    }

    public enum ColumnSortMode
    {
        Value,
        DisplayText
    }

    public enum ColumnGroupInterval
    {
        Default,
        Value,
        Date,
        DateMonth,
        DateQuarter,
        DateYear,
        DateRange,
        Alphabetical,
        DisplayText
    }

    public enum FixedStyle
    {
        None,
        Left,
        Right
    }
}
