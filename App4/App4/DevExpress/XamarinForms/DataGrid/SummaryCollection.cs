// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    
    public class SummaryCollection : ObservableCollection<GridColumnSummary>
    {
        [CompilerGenerated]
        private InternalCollectionChangedEventHandler InternalCollectionChanged;
        
        internal event InternalCollectionChangedEventHandler InternalCollectionChanged
        {
            [CompilerGenerated] add
            {
                InternalCollectionChangedEventHandler internalCollectionChanged = this.InternalCollectionChanged;
                while (true)
                {
                    InternalCollectionChangedEventHandler comparand = internalCollectionChanged;
                    InternalCollectionChangedEventHandler handler3 = (InternalCollectionChangedEventHandler) Delegate.Combine((Delegate) comparand, (Delegate) value);
                    internalCollectionChanged = Interlocked.CompareExchange<InternalCollectionChangedEventHandler>(ref this.InternalCollectionChanged, handler3, comparand);
                    if (object.ReferenceEquals(internalCollectionChanged, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                InternalCollectionChangedEventHandler internalCollectionChanged = this.InternalCollectionChanged;
                while (true)
                {
                    InternalCollectionChangedEventHandler comparand = internalCollectionChanged;
                    InternalCollectionChangedEventHandler handler3 = (InternalCollectionChangedEventHandler) Delegate.Remove((Delegate) comparand, (Delegate) value);
                    internalCollectionChanged = Interlocked.CompareExchange<InternalCollectionChangedEventHandler>(ref this.InternalCollectionChanged, handler3, comparand);
                    if (object.ReferenceEquals(internalCollectionChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }
        
        protected override void ClearItems()
        {
            List<GridColumnSummary> oldItems = new List<GridColumnSummary>((IEnumerable<GridColumnSummary>) base.get_Items());
            base.ClearItems();
            this.RaiseInternalCollectionChanged(null, oldItems);
        }
        
        protected override void InsertItem(int index, GridColumnSummary item)
        {
            base.InsertItem(index, item);
            List<GridColumnSummary> newItems = new List<GridColumnSummary>();
            newItems.Add(item);
            this.RaiseInternalCollectionChanged(newItems, null);
        }
        
        private void RaiseInternalCollectionChanged(List<GridColumnSummary> newItems, List<GridColumnSummary> oldItems)
        {
            if (this.InternalCollectionChanged != null)
            {
                InternalCollectionChangedEventArgs e = new InternalCollectionChangedEventArgs();
                e.NewItems = newItems;
                e.OldItems = oldItems;
                this.InternalCollectionChanged(this, e);
            }
        }
        
        protected override void RemoveItem(int index)
        {
            GridColumnSummary summary = base.get_Items().get_Item(index);
            base.RemoveItem(index);
            List<GridColumnSummary> oldItems = new List<GridColumnSummary>();
            oldItems.Add(summary);
            this.RaiseInternalCollectionChanged(null, oldItems);
        }
    }
}