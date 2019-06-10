// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    
    public class ReverseOrderedList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        private readonly IList<T> originalList;
        
        public ReverseOrderedList(IList<T> originalList)
        {
            this.originalList = originalList;
        }
        
        internal int BinarySearchOriginalComparer(int index, int count, T item, IComparer<T> comparer)
        {
            if (count <= 0)
            {
                return ~(index + count);
            }
            int num = this.ConvertIndex((index + count) - 1);
            int num2 = this.originalList.BinarySearch<T>(num, count, item, comparer);
            if (num2 >= 0)
            {
                return this.ConvertIndex(num2);
            }
            int num3 = ~num2;
            return ((num3 < (num + count)) ? ((num3 >= num) ? ~this.ConvertInsertionIndex(num3) : ~(index + count)) : ~index);
        }
        
        private int ConvertIndex(int index) => 
            ((this.originalList.get_Count() - index) - 1);
        
        private int ConvertInsertionIndex(int index) => 
            (this.originalList.get_Count() - index);
        
        void ICollection<T>.Add(T item)
        {
            this.originalList.Insert(0, item);
        }
        
        void ICollection<T>.Clear()
        {
            this.originalList.Clear();
        }
        
        bool ICollection<T>.Contains(T item) => 
            this.originalList.Contains(item);
        
        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            this.originalList.CopyTo(array, arrayIndex);
        }
        
        bool ICollection<T>.Remove(T item) => 
            this.originalList.Remove(item);
        
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => 
            this.originalList.GetEnumerator();
        
        int IList<T>.IndexOf(T item) => 
            this.ConvertIndex(this.originalList.IndexOf(item));
        
        void IList<T>.Insert(int index, T item)
        {
            this.originalList.Insert(this.ConvertInsertionIndex(index), item);
        }
        
        void IList<T>.RemoveAt(int index)
        {
            this.originalList.RemoveAt(this.ConvertIndex(index));
        }
        
        IEnumerator IEnumerable.GetEnumerator() => 
            ((IEnumerator) this.originalList.GetEnumerator());
        
        public IList<T> OriginalList =>
            this.originalList;
        
        T IList<T>.this[int index]
        {
            get => 
                this.originalList.get_Item(this.ConvertIndex(index));
            set => 
                this.originalList.set_Item(this.ConvertIndex(index), value);
        }
        
        int ICollection<T>.this[] =>
            this.originalList.get_Count();
        
        bool ICollection<T>.this[] =>
            this.originalList.get_IsReadOnly();
    }
}