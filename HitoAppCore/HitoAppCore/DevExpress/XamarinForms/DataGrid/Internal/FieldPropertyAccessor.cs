// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using System;
    using System.Reflection;
    
    public class FieldPropertyAccessor : PropertyAccessor
    {
        private readonly FieldInfo property;
        
        public FieldPropertyAccessor(Type objectType, string propertyName, FieldInfo property) : base(objectType, property.Name)
        {
            this.property = property;
        }
        
        public override object GetValue(object obj) => 
            this.property.GetValue(obj);
        
        public override T GetValueGeneric<T>(object obj) => 
            ((T) this.GetValue(obj));
        
        internal static void SetFieldValue(object obj, object value, FieldInfo field)
        {
            if ((value == null) || IntrospectionExtensions.GetTypeInfo(field.FieldType).IsAssignableFrom(IntrospectionExtensions.GetTypeInfo(value.GetType())))
            {
                field.SetValue(obj, value);
            }
            else
            {
                object obj1 = Convert.ChangeType(value, field.FieldType);
                value = obj1;
                field.SetValue(obj, value);
            }
        }
        
        public override void SetValue(object obj, object value)
        {
            SetFieldValue(obj, value, this.property);
        }
        
        public override Type PropertyType =>
            this.property.FieldType;
    }
}
