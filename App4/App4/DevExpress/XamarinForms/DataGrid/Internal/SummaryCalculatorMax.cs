// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using System;
    
    public class SummaryCalculatorMax : SummaryCalculatorMinMax
    {
        protected override bool ShouldAssignSummaryValue(int compareResult) => 
            (compareResult > 0);
    }
}