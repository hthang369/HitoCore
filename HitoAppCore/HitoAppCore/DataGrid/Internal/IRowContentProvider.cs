using System;
using System.Collections.Generic;
using System.Text;

namespace HitoAppCore.DataGrid
{
    public interface IRowContentProvider
    {
        int RowCount { get; }
        int GroupCount { get; }
        int CellsCount { get; }
        double RowWidth { get; }
        bool Customizable { get; set; }
        double HorizontalScrollOffset { get; }
        double VisibleRowWidth { get; }
        bool AllowHorizontalScrollingVirtualization { get; }

        bool ApplyConditionalFormatting(CellView cell);
        BaseCellView CreateView(int cellHandle);
        CellData GetCellData(int rowHandle, int cellHandle, CellData reuseCellData);
        FixedStyle GetCellFixedStyle(int cellHandle);
        object GetCellIdentifier(int cellHandle);
        double GetCellWidth(int cellHandle);
        GridColumn GetColumn(int cellHandle);
        //IGroupInfo GetGroup(int groupHandle);
        //GroupData GetGroupData(int rowHandle);
        double GetRowHeight(int rowHandle);
        //void RaiseCustomizeCell(CustomizeCellEventArgs args);
        //void RegisterDelegate(IRowContentProviderDelegate providerDelegate);
        void RestoreCellSettings(BaseCellView cellView, int cellHandle);
        bool SupportsFontConditionalFormatting(CellView cell);
    }
}
