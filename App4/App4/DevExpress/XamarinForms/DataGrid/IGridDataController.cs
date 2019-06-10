// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid
{
    using DevExpress.XamarinForms.DataGrid.Internal;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    
    public interface IGridDataController : IReadWriteDataProcessingLocker
    {
        event CustomSummaryEventHandler CalculateCustomSummary;
        
        event GridDataControllerDataChangedEventHandler DataChanged;
        
        IEditableRowData AddNewRow();
        IEditableRowData BeginEditRow(int rowHandle);
        int EndEditNewRow(IEditableRowData rowData);
        void EndEditRow(IEditableRowData rowData);
        IList<GridColumn> GenerateColumns();
        int GetGroupedRowCount(int rowHandle);
        IGroupInfo GetGroupInfo(int rowHandle);
        object GetGroupValue(int rowHandle);
        IRowData GetRow(int rowHandle, IRowData reuseRow);
        bool IsGroupCollapsed(int rowHandle);
        bool IsGroupRow(int rowHandle);
        void RaiseCalculateCustomSummary(CustomSummaryEventArgs args);
        void RefreshData();
        int RevealRowHandle(int rowHandle);
        
        bool AreGroupsInitiallyCollapsed { get; set; }
        
        Predicate<IRowData> Predicate { get; set; }
        
        int RowCount { get; }
        
        int VisibleRowCount { get; }
        
        int GroupCount { get; }
        
        IGridDataSource DataSource { get; set; }
    }
}
