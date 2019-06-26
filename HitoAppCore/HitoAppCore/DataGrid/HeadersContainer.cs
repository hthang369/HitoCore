using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class HeadersContainer : RowContainerBase//, IRowContentProviderDelegate, IHitTestAccess
    {
        // Methods
        public HeadersContainer(IRowContentProvider gridCellFactory) : base(gridCellFactory)
        {
            //base.BackgroundColor = base.CurrentTheme.HeaderCustomizer.BorderColor);
            base.RecreateLayout();
            //gridCellFactory.RegisterDelegate(this);
        }

        protected override void CustomizeAndLocateItem(View item, Rectangle itemRect, bool isFirst, bool isLast, bool isLeftFixedColumnBorder, bool isRightFixedColumnBorder)
        {
            //item.set_BackgroundColor(base.CurrentTheme.HeaderCustomizer.BackgroundColor);
            //base.CurrentTheme.HeaderCustomizer.LocateHeaderViewInPanel(item, itemRect, isFirst, isLast, isLeftFixedColumnBorder, isRightFixedColumnBorder);
        }

        //GridHitInfo IHitTestAccess.HitTest(Point location)
        //{
        //    if (((location.get_Y() < base.get_Y()) || ((location.get_X() < base.get_X()) || (location.get_X() > (base.get_Width() + base.get_X())))) || (location.get_Y() > (base.get_Height() + base.get_Y())))
        //    {
        //        return null;
        //    }
        //    return new GridHitInfo(this, location, -1, base.GetCellInfoByPosition(location.get_X()), this, GridElementType.Header);
        //}

        //void IRowContentProviderDelegate.ContentArranged()
        //{
        //    base.ArrangeChildren();
        //}

        //void IRowContentProviderDelegate.ContentInvalidated(bool hardDropCaches, bool softDropCaches)
        //{
        //    base.RecreateLayout();
        //}

        //void IRowContentProviderDelegate.ContentUpdated()
        //{
        //    base.UpdateLayout();
        //}

        protected override Size GetCellSize(int cellIndex) =>
            new Size(base.RowContentProvider.GetCellWidth(cellIndex), base.RowContentProvider.GetRowHeight(cellIndex));

        protected override BaseCellView GetCellView(int cellIndex) =>
            base.RowContentProvider.CreateView(cellIndex);

        protected override void UpdateCellData(BaseCellView cellView, int cellIndex)
        {
        }

        internal override void UpdateTheme()
        {
            //base.set_BackgroundColor(base.CurrentTheme.HeaderCustomizer.BorderColor);
            base.RecreateLayout();
        }

        // Properties
        protected override int ViewsCount =>
            base.RowContentProvider.CellsCount;
    }
}
