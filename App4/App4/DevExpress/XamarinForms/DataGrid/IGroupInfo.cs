// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid
{
    using System;
    using System.Collections.Generic;
    
    public interface IGroupInfo
    {
        string FieldName { get; }
        
        object Value { get; }
        
        IReadOnlyList<GridColumnSummary> Summaries { get; }
        
        bool IsCollapsed { get; }
        
        int GroupRowHandle { get; }
        
        int FirstRowHandle { get; }
        
        int LastRowHandle { get; }
        
        int RowCount { get; }
    }
}
