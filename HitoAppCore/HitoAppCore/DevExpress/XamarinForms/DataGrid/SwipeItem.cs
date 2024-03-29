// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid
{
    using DevExpress.XamarinForms.Core.Internal;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Input;
    using Xamarin.Forms;
    
    public class SwipeItem : BindableObject
    {
        public static readonly BindableProperty CaptionProperty;
        public static readonly BindableProperty ColorProperty;
        public static readonly BindableProperty ImageProperty;
        public static readonly BindableProperty CommandProperty;
        [CompilerGenerated]
        private EventHandler<SwipeItemTapEventArgs> Tap;
        
        static SwipeItem()
        {
            ParameterExpression expression = Expression.Parameter(typeof(SwipeItem), "o");
            ParameterExpression[] expressionArray1 = new ParameterExpression[] { expression };
            CaptionProperty = BindingUtils.Instance.CreateBindableProperty<SwipeItem, string>(Expression.Lambda<Func<SwipeItem, string>>((Expression) Expression.Property((Expression) expression, typeof(SwipeItem).GetProperty("Caption")), expressionArray1), string.Empty, BindingMode.OneWay, null, null, null, null, null);
            expression = Expression.Parameter(typeof(SwipeItem), "o");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            ColorProperty = BindingUtils.Instance.CreateBindableProperty<SwipeItem, Xamarin.Forms.Color>(Expression.Lambda<Func<SwipeItem, Xamarin.Forms.Color>>((Expression) Expression.Property((Expression) expression, typeof(SwipeItem).GetProperty("Color")), expressionArray2), Xamarin.Forms.Color.Default, BindingMode.OneWay, null, null, null, null, null);
            expression = Expression.Parameter(typeof(SwipeItem), "o");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            ImageProperty = BindingUtils.Instance.CreateBindableProperty<SwipeItem, ImageSource>(Expression.Lambda<Func<SwipeItem, ImageSource>>((Expression) Expression.Property((Expression) expression, typeof(SwipeItem).GetProperty("Image")), expressionArray3), null, BindingMode.OneWay, null, null, null, null, null);
            expression = Expression.Parameter(typeof(SwipeItem), "o");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            CommandProperty = BindingUtils.Instance.CreateBindableProperty<SwipeItem, ICommand>(Expression.Lambda<Func<SwipeItem, ICommand>>((Expression) Expression.Property((Expression) expression, typeof(SwipeItem).GetProperty("Command")), expressionArray4), null, BindingMode.OneWay, null, null, null, null, null);
        }
        
        private SwipeItemTapEventArgs GetTapArgs(int visibleRowIndex)
        {
            int sourceIndex = -1;
            int rowHandle = -2147483648;
            object dataObject = null;
            if (this.Owner != null)
            {
                rowHandle = this.Owner.GetRowHandleByVisibleIndex(visibleRowIndex);
                if ((rowHandle != -2147483648) && (rowHandle >= 0))
                {
                    sourceIndex = this.Owner.GetSourceRowIndex(rowHandle);
                    dataObject = this.Owner.GetRow(rowHandle).DataObject;
                }
            }
            return new SwipeItemTapEventArgs(rowHandle, sourceIndex, dataObject);
        }
        
        internal DataGridView Owner { get; set; }
        
        internal SwipeButtonLocation Location { get; set; }
        
        public string Caption
        {
            get => 
                ((string) ((string) base.GetValue(CaptionProperty)));
            set => 
                base.SetValue(CaptionProperty, value);
        }
        
        public Xamarin.Forms.Color Color
        {
            get => 
                ((Xamarin.Forms.Color) base.GetValue(ColorProperty));
            set => 
                base.SetValue(ColorProperty, value);
        }
        
        public ImageSource Image
        {
            get => 
                ((ImageSource) base.GetValue(ImageProperty));
            set => 
                base.SetValue(ImageProperty, value);
        }
        
        public ICommand Command
        {
            get => 
                ((ICommand) base.GetValue(CommandProperty));
            set => 
                base.SetValue(CommandProperty, value);
        }
        
        internal Func<int, bool> CanExecuteDelegate =>
            delegate (int visibleRowIndex) {
                return this.Command.CanExecute(this.GetTapArgs(visibleRowIndex));
            };
        
        internal Action<int> Handler =>
            delegate (int visibleRowIndex) {
                SwipeItemTapEventArgs tapArgs = this.GetTapArgs(visibleRowIndex);
                if ((this.Command != null) && this.Command.CanExecute(tapArgs))
                {
                    this.Command.Execute(tapArgs);
                }
                else if (this.Tap == null)
                {
                    EventHandler<SwipeItemTapEventArgs> tap = this.Tap;
                }
                else
                {
                    this.Tap(this, tapArgs);
                }
            };
    }
}
