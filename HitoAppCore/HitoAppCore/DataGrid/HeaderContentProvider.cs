using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class HeaderContentProvider : BaseContentProvider
    {
        // Methods
        public HeaderContentProvider(double rowHeight, IHorizontalScrollingData horizontalScrollingData) : base(rowHeight, horizontalScrollingData)
        {
        }

        protected override BaseCellView CreateView(int cellHandle)
        {
            if (cellHandle >= base.VisibleColumns.Count)
            {
                return null;
            }
            HeaderView headerView = new HeaderView();
            this.SetHeaderCellSettingsCore(headerView, cellHandle);
            return headerView;
        }

        protected override CellData GetCellData(int rowHandle, int cellHandle, CellData reuseCellData)
        {
            CellData local1 = (reuseCellData != null) ? reuseCellData : new CellData();
            local1.Value = base.VisibleColumns[cellHandle];
            return local1;
        }

        protected override object GetCellIdentifier(int cellHandle)
        {
            GridColumn column = base.VisibleColumns[cellHandle];
            return ((column.HeaderTemplate != null) ? ((object)((int)column.HeaderTemplate.GetHashCode())) : ((object)column.GetType()));
        }

        //protected override void HandleDataChanged(object sender, GridDataControllerDataChangedEventArgs args)
        //{
        //    bool hardDropCaches = (args.ChangeType == GridDataControllerDataChangedType.DataSourceChanged) || (args.ChangeType == GridDataControllerDataChangedType.GroupingChanged);
        //    if (hardDropCaches)
        //    {
        //        base.DelegateInvalidateContent(hardDropCaches, false);
        //    }
        //}

        protected override void RestoreCellSettings(BaseCellView cellView, int cellHandle)
        {
            this.SetHeaderCellSettingsCore(cellView as HeaderView, cellHandle);
        }

        private void SetHeaderCellSettingsCore(HeaderView headerView, int cellHandle)
        {
            TextAlignment textContentAlignment = base.VisibleColumns[cellHandle].GetTextContentAlignment();
            headerView.ContentAlignment = textContentAlignment;
            //headerView.set_Padding(base.CurrentTheme.CellCustomizer.Padding);
            headerView.BindingContext = new GridColumnData(base.VisibleColumns[cellHandle], headerView);
        }

        // Properties
        protected override int RowCount =>
            1;

        protected override int GroupCount =>
            0;
    }
}
