// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using System;
    using System.Collections.Generic;
    
    public class PropertyAccessorCache
    {
        private readonly Dictionary<Type, Dictionary<string, PropertyAccessor>> cache = new Dictionary<Type, Dictionary<string, PropertyAccessor>>();
        
        public PropertyAccessor GetAccessor(Type objectType, string propertyName)
        {
            Dictionary<string, PropertyAccessor> dictionary;
            PropertyAccessor accessor;
            if (!this.cache.TryGetValue(objectType, out dictionary))
            {
                dictionary = new Dictionary<string, PropertyAccessor>();
                this.cache.SetItem(objectType, dictionary);
            }
            if (!dictionary.TryGetValue(propertyName, out accessor))
            {
                accessor = PropertyAccessor.Create(objectType, propertyName, null);
                dictionary.SetItem(propertyName, accessor);
            }
            return accessor;
        }
    }
}
