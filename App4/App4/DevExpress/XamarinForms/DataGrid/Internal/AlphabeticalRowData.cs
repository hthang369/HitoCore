// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using System;
    
    public class AlphabeticalRowData : TransformedRowData
    {
        protected override object TransformValue(object value)
        {
            string str = value.ToString();
            return (string.IsNullOrEmpty(str) ? ((object) string.Empty) : ((object) ((char) str.get_Chars(0))));
        }
    }
}
