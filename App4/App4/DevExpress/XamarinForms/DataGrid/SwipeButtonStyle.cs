// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid
{
    using DevExpress.XamarinForms.Core.Internal;
    using DevExpress.XamarinForms.Core.Themes;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Xamarin.Forms;
    
    public class SwipeButtonStyle : StyleBase
    {
        private const double defaultWidth = 100.0;
        public static readonly BindableProperty WidthProperty;
        public static readonly BindableProperty ContentAlignmentProperty;
        
        static SwipeButtonStyle()
        {
            ParameterExpression expression = Expression.Parameter(typeof(SwipeButtonStyle), "o");
            ParameterExpression[] expressionArray1 = new ParameterExpression[] { expression };
            WidthProperty = BindingUtils.Instance.CreateBindableProperty<SwipeButtonStyle, double>(Expression.Lambda<Func<SwipeButtonStyle, double>>((Expression) Expression.Property((Expression) expression, typeof(SwipeButtonStyle).GetProperty("Width")), expressionArray1), 100.0, BindingMode.OneWay, null, null, null, null, null);
            expression = Expression.Parameter(typeof(SwipeButtonStyle), "o");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            ContentAlignmentProperty = BindingUtils.Instance.CreateBindableProperty<SwipeButtonStyle, TextAlignment>(Expression.Lambda<Func<SwipeButtonStyle, TextAlignment>>((Expression) Expression.Property((Expression) expression, typeof(SwipeButtonStyle).GetProperty("ContentAlignment")), expressionArray2), TextAlignment.Center, BindingMode.OneWay, null, null, null, null, null);
        }
        
        public SwipeButtonStyle()
        {
            ThemeManager.Initialize(this);
        }
        
        public SwipeButtonStyle(IStyledElement styledElement) : base(styledElement)
        {
            ThemeManager.Initialize(this);
        }
        
        protected override StyleBase CreateCloneInstance()
        {
            SwipeButtonStyle style1 = new SwipeButtonStyle();
            style1.Width = this.Width;
            style1.ContentAlignment = this.ContentAlignment;
            return style1;
        }
        
        protected override void OnStylePropertyChanged()
        {
            IStyledElement styledElement = base.StyledElement;
            if (styledElement == null)
            {
                IStyledElement local1 = styledElement;
            }
            else
            {
                styledElement.OnCellStyleChanged();
            }
        }
        
        public double Width
        {
            get => 
                ((double) ((double) base.GetValue(WidthProperty)));
            set => 
                base.SetValue(WidthProperty, (double) value);
        }
        
        public TextAlignment ContentAlignment
        {
            get => 
                ((TextAlignment) base.GetValue(ContentAlignmentProperty));
            set => 
                base.SetValue(ContentAlignmentProperty, value);
        }
    }
}