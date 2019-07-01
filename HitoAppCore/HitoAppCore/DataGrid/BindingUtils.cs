using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.Forms.DataGrid
{
    public class BindingUtils
    {
        public static BindableProperty CreateProperty<TDeclares, TProperty>(string propertyName, object defaultValue = null, BindableProperty.BindingPropertyChangedDelegate<TProperty> propertyChanged = null, BindableProperty.CoerceValueDelegate<TProperty> coerceValue = null, BindableProperty.ValidateValueDelegate<TProperty> validateValue = null) where TDeclares : BindableObject
        {
            try
            {
                return BindableProperty.Create(propertyName, typeof(TProperty), typeof(TDeclares), defaultValue, BindingMode.OneWay,
                    validateValue: (bindable, value) => { if (validateValue != null) { validateValue(bindable, (TProperty)value); } return true; },
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
