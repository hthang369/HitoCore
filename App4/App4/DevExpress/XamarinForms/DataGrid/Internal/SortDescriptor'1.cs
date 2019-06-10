// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    
    public class SortDescriptor<T>
    {
        private IComparer<T> comparer;
        private IComparer<T> ascendingComparer;
        private string fieldName;
        private Type fieldType;
        private ColumnSortOrder sortOrder;
        private ColumnSortMode sortMode;
        
        public SortDescriptor<T> Clone()
        {
            SortDescriptor<T> descriptor1 = new SortDescriptor<T>();
            descriptor1.AscendingComparer = this.AscendingComparer;
            descriptor1.SortMode = this.SortMode;
            descriptor1.SortOrder = this.SortOrder;
            descriptor1.FieldName = this.FieldName;
            descriptor1.FieldValueVisitor = this.FieldValueVisitor;
            return descriptor1;
        }
        
        public virtual int Compare(T firstValue, T secondValue)
        {
            if (this.Comparer == null)
            {
                throw new InvalidOperationException();
            }
            return this.Comparer.Compare(firstValue, secondValue);
        }
        
        protected virtual IComparer<T> CreateComparer()
        {
            if (this.AscendingComparer != null)
            {
                switch (this.SortOrder)
                {
                    case ColumnSortOrder.Ascending:
                        return this.AscendingComparer;
                    
                    case ColumnSortOrder.Descending:
                        return new ReversedComparer<T>(this.AscendingComparer);
                }
            }
            return null;
        }
        
        public override bool Equals(object obj)
        {
            SortDescriptor<T> descriptor = obj as SortDescriptor<T>;
            return ((descriptor != null) ? (object.ReferenceEquals(descriptor.AscendingComparer, this.AscendingComparer) && ((descriptor.SortOrder == this.SortOrder) && (descriptor.FieldName == this.FieldName))) : false);
        }
        
        public override int GetHashCode()
        {
            int hashCode = 0;
            if (this.AscendingComparer != null)
            {
                hashCode = this.AscendingComparer.GetHashCode();
            }
            if (this.FieldName != null)
            {
                hashCode ^= this.FieldName.GetHashCode();
            }
            return (hashCode ^ this.SortOrder.GetHashCode());
        }
        
        private void ResetComparer()
        {
            this.comparer = null;
        }
        
        public virtual IComparer<T> AscendingComparer
        {
            get => 
                this.ascendingComparer;
            set
            {
                if (!object.ReferenceEquals(this.AscendingComparer, value))
                {
                    this.ascendingComparer = value;
                    this.ResetComparer();
                }
            }
        }
        
        public string FieldName
        {
            get => 
                this.fieldName;
            set
            {
                if (this.FieldName != value)
                {
                    this.fieldName = value;
                    this.ResetComparer();
                }
            }
        }
        
        public Type FieldType
        {
            get => 
                this.fieldType;
            set
            {
                if (this.FieldType != value)
                {
                    this.fieldType = value;
                    this.ResetComparer();
                }
            }
        }
        
        public virtual ColumnSortOrder SortOrder
        {
            get => 
                this.sortOrder;
            set
            {
                if (this.SortOrder != value)
                {
                    this.sortOrder = value;
                    this.ResetComparer();
                }
            }
        }
        
        public ColumnSortMode SortMode
        {
            get => 
                this.sortMode;
            set => 
                (this.sortMode = value);
        }
        
        public IFieldValueVisitor FieldValueVisitor { get; set; }
        
        public virtual bool IsSingleColumn =>
            true;
        
        public IComparer<T> Comparer
        {
            get
            {
                if (this.comparer == null)
                {
                    this.comparer = this.CreateComparer();
                }
                return this.comparer;
            }
        }
    }
}
