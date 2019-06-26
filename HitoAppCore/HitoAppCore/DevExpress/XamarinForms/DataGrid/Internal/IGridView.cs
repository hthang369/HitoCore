// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using DevExpress.XamarinForms.DataGrid;
    using System;
    
    public interface IGridView
    {
        void CloseInplaceEditor(bool applyChanges);
        void EndRefreshing();
        void OpenInplaceEditor(EditRowViewModel viewModel, int rowVisibleIndex, int columnIndex);
        void Rebuild(DataGridView grid);
        void ScrollToRow(int index);
        void SetFocusedRowIndex(int visibleIndex);
        void UpdateAppearance(DataGridView grid);
        void UpdateGridStyle(DataGridView grid);
        void UpdateHeaders(DataGridView grid);
        void UpdateHeaderSettings(DataGridView grid);
        void UpdateLoadMore(bool isActive);
        void UpdatePullToRefresh(bool isActive);
        void UpdateRows();
        void UpdateRowSettings(DataGridView grid);
        void UpdateTotalSummaries();
        void UpdateTotalSummaryPanel(DataGridView grid);
    }
}
