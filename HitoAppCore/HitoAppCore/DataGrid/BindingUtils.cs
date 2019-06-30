using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.Forms.DataGrid
{
    public class BindingUtils
    {
        public static BindableProperty CreateProperty<TDeclares, TProperty>(string propertyName, object defaultValue = null, BindableProperty.BindingPropertyChangedDelegate<TProperty> propertyChanged = null, BindableProperty.CoerceValueDelegate<TProperty> coerceValue = null) where TDeclares : BindableObject
        {
            try
            {
                return BindableProperty.Create(propertyName, typeof(TProperty), typeof(TDeclares), defaultValue, BindingMode.OneWay, null,
                    propertyChanged: (bindable, oldValue, newValue) => { if (propertyChanged != null) propertyChanged(bindable, (TProperty)oldValue, (TProperty)newValue); },
                    coerceValue: (bindable, value) => { if (coerceValue != null) { coerceValue(bindable, (TProperty)value); } return value; });
            }
            catch
            {
            }
            return null;
        }
        public static BindablePropertyKey CreateReadOnlyProperty<TDeclares, TProperty>(string propertyName, object defaultValue = null, BindableProperty.BindingPropertyChangedDelegate<TProperty> propertyChanged = null)
        {
            return BindableProperty.CreateReadOnly(propertyName, typeof(TProperty), typeof(TDeclares), defaultValue, BindingMode.OneWayToSource, null,
                propertyChanged: (bindable, oldValue, newValue) => { if (propertyChanged != null) propertyChanged(bindable, (TProperty)oldValue, (TProperty)newValue); });
        }
    }
}
