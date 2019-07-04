// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using DevExpress.XamarinForms.DataGrid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    
    public interface IInnerGridDataSource : IGridDataSource, IGridDataSourceSupportSortCache
    {
        event EventHandler SelectionChanged;
        
        void ForceSetSelectedRow(int rowHandle);
        IList<GridColumn> GenerateColumns();
        int GetRowHandle(int sourceRowIndex);
        int GetSourceRowHandle(int rowHandle);
        int NotifyRowReplaced(int rowHandle);
        void PopulateSourceRowHandles(int[] rowHandles);
        void RefreshData();
        void ResetSelection();
        
        GridDataSourceSelection Selection { get; }
        
        bool IsRowCountReady { get; }
        
        bool IsGetRowReady { get; }
    }
}