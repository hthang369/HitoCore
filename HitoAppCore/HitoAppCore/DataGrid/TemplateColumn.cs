using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class TemplateColumn : GridColumn
    {
        #region Fields
        public static readonly BindableProperty EditTemplateProperty;
        public static readonly BindableProperty DisplayTemplateProperty;
        #endregion

        #region Contructor
        static TemplateColumn()
        {
            EditTemplateProperty = BindingUtils.CreateProperty<TemplateColumn, DataTemplate>(nameof(EditTemplate));
            DisplayTemplateProperty = BindingUtils.CreateProperty<TemplateColumn, DataTemplate>(nameof(DisplayTemplate));
        }
        #endregion

        #region Methods
        protected override Type GetComparerPropertyType()
        {
            return typeof(DataTemplate);
        }
        #endregion

        #region Properties
        public DataTemplate DisplayTemplate
        {
            get => (DataTemplate)base.GetValue(DisplayTemplateProperty);
            set => base.SetValue(DisplayTemplateProperty, value);
        }
        public DataTemplate EditTemplate
        {
            get => (DataTemplate)base.GetValue(EditTemplateProperty);
            set => base.SetValue(EditTemplateProperty, value);
        }
        #endregion
    }
}
