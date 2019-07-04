using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.DataGrid
{
    public class DataGridColumn : GridColumn
    {
        public static readonly BindableProperty DataTypeProperty;
        static DataGridColumn()
        {
            DataTypeProperty = BindingUtils.CreateProperty<DataGridColumn, Type>(nameof(DataType), typeof(string));
        }
        
        protected override Type GetComparerPropertyType()
        {
            return DataType;
        }

        public Type DataType
        {
            get => (Type)base.GetValue(DataTypeProperty);
            set => base.SetValue(DataTypeProperty, value);
        }
    }
}
