// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using DevExpress.XamarinForms.DataGrid.Localization;
    using System;
    
    public class DateQuarterRowData : TransformedRowData
    {
        public override object GetGroupValue(object transformedValue)
        {
            if (transformedValue == null)
            {
                return null;
            }
            DateTime time = (DateTime) transformedValue;
            int num = ((time.Month - 1) / 3) + 1;
            return string.Format(GridLocalizer.GetString(GridStringId.GroupIntervalQuarterDisplayFormat), (int) num, (int) time.Year);
        }
        
        protected override object TransformValue(object value)
        {
            if (value == null)
            {
                return null;
            }
            DateTime time = (DateTime) value;
            return new DateTime(time.Year, (((time.Month - 1) / 3) * 3) + 1, 1);
        }
    }
}
