// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid
{
    using DevExpress.XamarinForms.Core.Internal;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Xamarin.Forms;
    
    public class TemplateColumn : GridColumn
    {
        public static readonly BindableProperty DisplayTemplateProperty;
        public static readonly BindableProperty EditTemplateProperty;
        
        static TemplateColumn()
        {
            ParameterExpression expression = Expression.Parameter(typeof(TemplateColumn), "o");
            ParameterExpression[] expressionArray1 = new ParameterExpression[] { expression };
            DisplayTemplateProperty = BindingUtils.Instance.CreateBindableProperty<TemplateColumn, DataTemplate>(Expression.Lambda<Func<TemplateColumn, DataTemplate>>((Expression) Expression.Property((Expression) expression, typeof(TemplateColumn).GetProperty("DisplayTemplate")), expressionArray1), null, BindingMode.OneWay, null, null, null, null, null);
            expression = Expression.Parameter(typeof(TemplateColumn), "o");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            EditTemplateProperty = BindingUtils.Instance.CreateBindableProperty<TemplateColumn, DataTemplate>(Expression.Lambda<Func<TemplateColumn, DataTemplate>>((Expression) Expression.Property((Expression) expression, typeof(TemplateColumn).GetProperty("EditTemplate")), expressionArray2), null, BindingMode.OneWay, null, null, null, null, null);
        }
        
        public DataTemplate DisplayTemplate
        {
            get => 
                ((DataTemplate) base.GetValue(DisplayTemplateProperty));
            set => 
                base.SetValue(DisplayTemplateProperty, value);
        }
        
        public DataTemplate EditTemplate
        {
            get => 
                ((DataTemplate) base.GetValue(EditTemplateProperty));
            set => 
                base.SetValue(EditTemplateProperty, value);
        }
    }
}
