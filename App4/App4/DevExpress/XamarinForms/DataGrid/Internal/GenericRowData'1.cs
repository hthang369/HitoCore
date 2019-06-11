// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using DevExpress.XamarinForms.DataGrid;
    using System;
    
    public class GenericRowData<T> : IEditableRowData, IRowData
    {
        private T obj;
        private int rowHandle;
        private readonly PropertyAccessorCache cache;
        
        private GenericRowData(T obj, int rowHandle, PropertyAccessorCache cache)
        {
            this.cache = new PropertyAccessorCache();
            this.obj = obj;
            this.rowHandle = rowHandle;
        }
        
        public static GenericRowData<T> Create(T obj, int rowHandle, PropertyAccessorCache cache) => 
            new GenericRowData<T>(obj, rowHandle, cache);
        
        public object GetFieldValue(string fieldName) => 
            this.cache.GetAccessor(this.obj.GetType(), fieldName)?.GetValue(this.obj);
        
        public TValue GetFieldValueGeneric<TValue>(string fieldName)
        {
            PropertyAccessor accessor = this.cache.GetAccessor(this.obj.GetType(), fieldName);
            if (accessor != null)
            {
                return accessor.GetValueGeneric<TValue>(this.obj);
            }
            return default(TValue);
        }
        
        public void SetFieldValue(string fieldName, object value)
        {
            PropertyAccessor accessor = this.cache.GetAccessor(this.obj.GetType(), fieldName);
            if (accessor != null)
            {
                accessor.SetValue(this.obj, value);
            }
        }
        
        public int RowHandle
        {
            get => 
                this.rowHandle;
            set => 
                this.rowHandle = value;
        }
        
        public object DataObject
        {
            get => 
                this.obj;
            internal set => 
                this.obj = (T) value;
        }
    }
}
