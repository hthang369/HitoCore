using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Xamarin.Forms.DataGrid
{
    public class ObjectUtils
    {
        public static object GetPropertyValue(object obj, string strPropertyName)
        {
            PropertyInfo property = obj.GetType().GetProperty(strPropertyName);
            if (property != null)
            {
                return property.GetValue(obj, null);
            }
            return null;
        }
        public static void SetPropertyValue(object obj, string strPropertyName, object value)
        {
            PropertyInfo property = obj.GetType().GetProperty(strPropertyName);
            if (property != null)
            {
                property.SetValue(obj, value, null);
            }
        }
    }
}
