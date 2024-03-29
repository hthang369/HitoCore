// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid
{
    using DevExpress.Data;
    using DevExpress.XamarinForms.Core.Internal;
    using DevExpress.XamarinForms.DataGrid.Internal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Xamarin.Forms;
    
    public class PickerColumn : TextColumn
    {
        private List<object> autoValues;
        private Dictionary<object, string> formattedDisplayTexts;
        private Dictionary<object, string> unformattedDisplayTexts;
        private PropertyAccessor valueAccessor;
        private PropertyAccessor displayTextAccessor;
        public static readonly BindableProperty ItemsSourceProperty;
        public static readonly BindableProperty DisplayMemberProperty;
        public static readonly BindableProperty ValueMemberProperty;
        
        static PickerColumn()
        {
            ParameterExpression expression = Expression.Parameter(typeof(PickerColumn), "o");
            ParameterExpression[] expressionArray1 = new ParameterExpression[] { expression };
            ItemsSourceProperty = BindingUtils.Instance.CreateBindableProperty<PickerColumn, object>(Expression.Lambda<Func<PickerColumn, object>>((Expression) Expression.Property((Expression) expression, typeof(PickerColumn).GetProperty("ItemsSource")), expressionArray1), null, BindingMode.OneWay, null, OnItemsSourceChanged, null, null, null);
            expression = Expression.Parameter(typeof(PickerColumn), "o");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            DisplayMemberProperty = BindingUtils.Instance.CreateBindableProperty<PickerColumn, string>(Expression.Lambda<Func<PickerColumn, string>>((Expression) Expression.Property((Expression) expression, typeof(PickerColumn).GetProperty("DisplayMember")), expressionArray2), null, BindingMode.OneWay, null, OnDisplayMemberChanged, null, null, null);
            expression = Expression.Parameter(typeof(PickerColumn), "o");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            ValueMemberProperty = BindingUtils.Instance.CreateBindableProperty<PickerColumn, string>(Expression.Lambda<Func<PickerColumn, string>>((Expression) Expression.Property((Expression) expression, typeof(PickerColumn).GetProperty("ValueMember")), expressionArray3), null, BindingMode.OneWay, null, OnValueMemberChanged, null, null, null);
        }
        
        private Type CalculateActualPropertyType()
        {
            if (base.PropertyAccessor != null)
            {
                return base.PropertyAccessor.PropertyType;
            }
            switch (base.UnboundType)
            {
                case UnboundColumnType.Integer:
                    return typeof(int);
                
                case UnboundColumnType.Decimal:
                    return typeof(decimal);
                
                case UnboundColumnType.DateTime:
                    return typeof(DateTime);
                
                case UnboundColumnType.String:
                    return typeof(string);
                
                case UnboundColumnType.Boolean:
                    return typeof(bool);
            }
            return typeof(object);
        }
        
        internal object CorrectCellValue(object obj)
        {
            if (string.IsNullOrEmpty(this.ValueMember))
            {
                object result = null;
                if (this.TryGetEnumValue(obj, ref result))
                {
                    return result;
                }
            }
            return obj;
        }
        
        private Dictionary<object, string> CreateDisplayTextTable(bool format)
        {
            Dictionary<object, string> dictionary = new Dictionary<object, string>();
            if (this.ActualValues != null)
            {
                foreach (object obj2 in this.ActualValues)
                {
                    dictionary.Add(this.GetActualValue(obj2), this.GetDisplayValue(obj2, format));
                }
            }
            return dictionary;
        }
        
        internal string GetActualDisplayText(object obj) => 
            this.GetActualDisplayText(obj, true);
        
        internal string GetActualDisplayText(object obj, bool format)
        {
            if (obj != null)
            {
                string str;
                if (string.IsNullOrEmpty(this.DisplayMember))
                {
                    object result = null;
                    if (!this.TryGetEnumValue(obj, ref result))
                    {
                        return DisplayTextHelper.Instance.GetDisplayText(base.DisplayFormat, obj);
                    }
                    obj = result;
                }
                if (format)
                {
                    if (this.FormattedDisplayTexts.TryGetValue(obj, out str))
                    {
                        return str;
                    }
                }
                else if (this.UnformattedDisplayTexts.TryGetValue(obj, out str))
                {
                    return str;
                }
            }
            return string.Empty;
        }
        
        internal object GetActualValue(object obj)
        {
            if (string.IsNullOrEmpty(this.ValueMember))
            {
                return obj;
            }
            if (this.valueAccessor == null)
            {
                this.valueAccessor = PropertyAccessor.Create(obj.GetType(), this.ValueMember, null);
            }
            return ((this.valueAccessor == null) ? obj : this.valueAccessor.GetValue(obj));
        }
        
        protected override Type GetComparerPropertyType() => 
            ((this.GetActualSortModeForVisitor() != ColumnSortMode.DisplayText) ? base.GetComparerPropertyTypeCore() : typeof(string));
        
        protected override ColumnSortMode GetDefaultSortMode() => 
            ColumnSortMode.DisplayText;
        
        private string GetDisplayValue(object obj, bool format)
        {
            if (string.IsNullOrEmpty(this.DisplayMember))
            {
                return (!format ? obj.ToString() : DisplayTextHelper.Instance.GetDisplayText(base.DisplayFormat, obj));
            }
            if (this.displayTextAccessor == null)
            {
                this.displayTextAccessor = PropertyAccessor.Create(obj.GetType(), this.DisplayMember, null);
            }
            return ((this.displayTextAccessor == null) ? null : (!format ? this.displayTextAccessor.GetValue(obj).ToString() : DisplayTextHelper.Instance.GetDisplayText(base.DisplayFormat, this.displayTextAccessor.GetValue(obj))));
        }
        
        internal override Type GetFieldTypeForVisitor() => 
            ((this.GetActualSortModeForVisitor() != ColumnSortMode.DisplayText) ? base.GetFieldTypeForVisitor() : typeof(string));
        
        internal override object GetFieldValueForVisitor(IRowData rowData, bool format)
        {
            if (base.SortMode != ColumnSortMode.DisplayText)
            {
                return null;
            }
            if (this.ItemsSource == null)
            {
                return base.GetFieldValueForVisitor(rowData, format);
            }
            string actualDisplayText = this.GetActualDisplayText(rowData.GetFieldValue(base.FieldName), format);
            return (!string.IsNullOrWhiteSpace(actualDisplayText) ? actualDisplayText : null);
        }
        
        private static void OnDisplayMemberChanged(BindableObject obj, object oldValue, object newValue)
        {
            PickerColumn column1 = obj as PickerColumn;
            column1.ResetCache();
            column1.OnDisplayFormatChanged();
            column1.RaiseAfterPropertyChanged(DisplayMemberProperty.PropertyName);
        }
        
        protected virtual void OnItemsSourceChanged(object oldValue, object newValue)
        {
            this.ResetCache();
            base.OnFieldNameChanged();
        }
        
        private static void OnItemsSourceChanged(BindableObject obj, object oldValue, object newValue)
        {
            PickerColumn column1 = obj as PickerColumn;
            column1.OnItemsSourceChanged(oldValue, newValue);
            column1.RaiseAfterPropertyChanged(ItemsSourceProperty.PropertyName);
        }
        
        protected override void OnUnboundTypeChanged()
        {
            this.ResetCache();
            this.TryUpdateAutoValues();
            base.OnUnboundTypeChanged();
        }
        
        private static void OnValueMemberChanged(BindableObject obj, object oldValue, object newValue)
        {
            PickerColumn column1 = obj as PickerColumn;
            column1.ResetCache();
            column1.RaiseAfterPropertyChanged(ValueMemberProperty.PropertyName);
        }
        
        private void ResetCache()
        {
            this.formattedDisplayTexts = null;
            this.valueAccessor = null;
            this.displayTextAccessor = null;
        }
        
        protected internal override void SetPropertyAccessor(PropertyAccessor accessor)
        {
            base.SetPropertyAccessor(accessor);
            this.ResetCache();
            this.TryUpdateAutoValues();
            base.RaiseAfterPropertyChanged("PropertyAccessor");
        }
        
        private bool TryGetEnumValue(object value, ref object result)
        {
            bool flag;
            try
            {
                Type comparerPropertyTypeCore = base.GetComparerPropertyTypeCore();
                Type type2 = value.GetType();
                if (!comparerPropertyTypeCore.IsEnum || type2.IsEnum)
                {
                    flag = false;
                }
                else
                {
                    result = Enum.Parse(comparerPropertyTypeCore, value.ToString());
                    flag = true;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
        
        private void TryUpdateAutoValues()
        {
            Type type = this.CalculateActualPropertyType();
            if (!IntrospectionExtensions.GetTypeInfo(type).IsEnum)
            {
                this.autoValues = null;
            }
            else
            {
                this.autoValues = new List<object>();
                foreach (object obj2 in Enum.GetValues(type))
                {
                    this.autoValues.Add(obj2);
                }
            }
        }
        
        public object ItemsSource
        {
            get => 
                base.GetValue(ItemsSourceProperty);
            set
            {
                if (value == null)
                {
                    base.ClearValue(ItemsSourceProperty);
                }
                else
                {
                    base.SetValue(ItemsSourceProperty, value);
                }
            }
        }
        
        public string DisplayMember
        {
            get => 
                ((string) ((string) base.GetValue(DisplayMemberProperty)));
            set => 
                base.SetValue(DisplayMemberProperty, value);
        }
        
        public string ValueMember
        {
            get => 
                ((string) ((string) base.GetValue(ValueMemberProperty)));
            set => 
                base.SetValue(ValueMemberProperty, value);
        }
        
        protected internal Dictionary<object, string> FormattedDisplayTexts
        {
            get
            {
                if (this.formattedDisplayTexts == null)
                {
                    this.formattedDisplayTexts = this.CreateDisplayTextTable(true);
                }
                return this.formattedDisplayTexts;
            }
        }
        
        protected internal Dictionary<object, string> UnformattedDisplayTexts
        {
            get
            {
                if (this.unformattedDisplayTexts == null)
                {
                    this.unformattedDisplayTexts = this.CreateDisplayTextTable(false);
                }
                return this.unformattedDisplayTexts;
            }
        }
        
        internal IEnumerable ActualValues
        {
            get
            {
                if ((this.ItemsSource != null) || (this.autoValues == null))
                {
                    return (this.ItemsSource as IEnumerable);
                }
                return (IEnumerable) this.autoValues;
            }
        }
        
        protected override AutoFilterCondition DefaultAutoFilterCondition =>
            AutoFilterCondition.Equals;
    }
}
