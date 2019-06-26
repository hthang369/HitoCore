using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public abstract class RowContainerBase : Layout<View>
    {
        // Fields
        private readonly IRowContentProvider rowContentProvider;
        private readonly List<BaseCellView> cells;
        //private Locker updateLayoutLocker;
        //private TouchLockView touchFilter;
        protected Dictionary<object, Stack<BaseCellView>> CellsCache = new Dictionary<object, Stack<BaseCellView>>();
        private double leftFixedWidth;
        private double rightFixedWidth;
        private int leftFixedColumnsCount;
        private int rightFixedColumnsCount;
        private double scrollAreaWidth;

        // Methods
        public RowContainerBase(IRowContentProvider rowContentProvider)
        {
            this.rowContentProvider = rowContentProvider;
            this.cells = new List<BaseCellView>();
            //this.updateLayoutLocker = new Locker();
            //this.ScrollView = new ScrollAreaView();
            //this.ScrollView.set_IsClippedToBounds(true);
        }

        protected virtual void AddChildToContent(View item)
        {
            base.Children.Add(item);
        }

        protected virtual void AddScrollCellToContent(BaseCellView cell)
        {
            //this.ScrollView.get_Children().Add(cell);
        }

        protected void ArrangeChildren()
        {
            //if (!this.updateLayoutLocker.IsLocked && (this.Cells.Count != 0))
            //{
            //    //this.ScrollView.Layout(new Rectangle(this.leftFixedWidth, 0.0, this.scrollAreaWidth, base.get_Height()));
            //    CellsArrangeHelper.ArrangeCells(this);
            //    this.ArrangeFinished();
            //}
        }

        protected virtual void ArrangeFinished()
        {
        }

        private bool CheckColumnVisibility(double cellStartPosition, double cellWidth) =>
            (this.RowContentProvider.AllowHorizontalScrollingVirtualization ? (((cellStartPosition + cellWidth) >= -10.0) ? (cellStartPosition <= (this.scrollAreaWidth + 50.0)) : false) : true);

        protected virtual void ClearChildren()
        {
            //this.ScrollView.get_Children().Clear();
            base.Children.Clear();
        }

        private void ClearRow()
        {
            this.UnsubscribeCellsEvents();
            this.ClearChildren();
            this.cells.Clear();
            using (Dictionary<object, Stack<BaseCellView>>.ValueCollection.Enumerator enumerator = this.CellsCache.Values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    enumerator.Current.Clear();
                }
            }
            this.CellsCache.Clear();
        }

        protected virtual List<BaseCellView> CreateLeftFixedCells()
        {
            List<BaseCellView> list = new List<BaseCellView>();
            this.leftFixedWidth = 0.0;
            this.leftFixedColumnsCount = 0;
            for (int i = 0; (i < this.ViewsCount) && (this.RowContentProvider.GetCellFixedStyle(i) == FixedStyle.Left); i++)
            {
                this.leftFixedColumnsCount++;
                this.leftFixedWidth += this.RowContentProvider.GetCellWidth(i);
                BaseCellView cellView = this.GetCellView(i);
                list.Add(cellView);
                if (cellView != null)
                {
                    this.AddChildToContent(cellView);
                }
            }
            return list;
        }

        protected virtual List<BaseCellView> CreateRightFixedCells()
        {
            List<BaseCellView> list = new List<BaseCellView>();
            this.rightFixedWidth = 0.0;
            this.rightFixedColumnsCount = 0;
            for (int i = this.ViewsCount - 1; (i >= 0) && (this.RowContentProvider.GetCellFixedStyle(i) == FixedStyle.Right); i--)
            {
                this.rightFixedColumnsCount++;
                this.rightFixedWidth += this.RowContentProvider.GetCellWidth(i);
                BaseCellView cellView = this.GetCellView(i);
                list.Insert(0, cellView);
                if (cellView != null)
                {
                    this.AddChildToContent(cellView);
                }
            }
            return list;
        }

        protected abstract void CustomizeAndLocateItem(View item, Rectangle itemRect, bool isFirst, bool isLast, bool isLeftFixedColumnBorder, bool isRightFixedColumnBorder);
        internal void DisablePanelForTouch()
        {
            //if (this.touchFilter == null)
            //{
            //    //this.touchFilter = new TouchLockView();
            //    this.touchFilter.set_InputTransparent(false);
            //    base.Children.Add(this.touchFilter);
            //    this.touchFilter.Layout(new Rectangle(0.0, 0.0, base.Width, base.Height));
            //}
        }

        internal void EnablePanelForTouch()
        {
            //if (this.touchFilter != null)
            //{
            //    base.Children.Remove(this.touchFilter);
            //    this.touchFilter = null;
            //}
        }

        protected virtual BaseCellView GetCellFromCache(int cellIndex, object cellIdentifier)
        {
            BaseCellView cellView = this.CellsCache[cellIdentifier].Pop();
            this.RowContentProvider.RestoreCellSettings(cellView, cellIndex);
            this.UpdateCellData(cellView, cellIndex);
            return cellView;
        }

        public GridCellInfo GetCellInfoByPosition(double x)
        {
            if ((x < 0.0) || (x >= base.Width))
            {
                return null;
            }
            return this.GetCellInfoByPositionCore(x);
        }

        protected virtual GridCellInfo GetCellInfoByPositionCore(double x) =>
            CellInfoHelper.GetCellInfoByPosition(this, x);

        protected abstract Size GetCellSize(int cellIndex);
        protected abstract BaseCellView GetCellView(int cellIndex);
        protected double GetCellXPosition(View cell, double cellX)
        {
            double num = cellX;
            //if (cell.Parent == this.ScrollView)
            //{
            //    num += this.ScrollView.get_X();
            //}
            return num;
        }

        public GridColumn GetColumn(int cellIndex) =>
            this.RowContentProvider.GetColumn(cellIndex);

        protected virtual double GetStartPosition() =>
            (0.0 - this.RowContentProvider.HorizontalScrollOffset);

        protected virtual void HideScrollCell(BaseCellView cell)
        {
            cell.Layout(new Rectangle(0.0, 0.0, 0.0, 0.0));
        }

        protected override void InvalidateLayout()
        {
        }

        protected override void InvalidateMeasure()
        {
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            //if ((width != this.ScrollView.get_Width()) || (height != this.ScrollView.get_Height()))
            //{
            //    this.UpdateScrollAreaWidth();
            //    //this.ScrollView.Layout(new Rectangle(this.leftFixedWidth, 0.0, this.scrollAreaWidth, base.get_Height()));
            //}
        }

        protected void RecreateLayout()
        {
            //this.updateLayoutLocker.Lock();
            this.ClearRow();
            //this.AddChildToContent(this.ScrollView);
            this.cells.AddRange((IEnumerable<BaseCellView>)this.CreateLeftFixedCells());
            List<BaseCellView> list = this.CreateRightFixedCells();
            this.UpdateScrollAreaWidth();
            double startPosition = this.GetStartPosition();
            for (int i = this.leftFixedColumnsCount; i < (this.ViewsCount - this.rightFixedColumnsCount); i++)
            {
                double cellWidth = this.RowContentProvider.GetCellWidth(i);
                BaseCellView item = null;
                if (this.CheckColumnVisibility(startPosition, cellWidth))
                {
                    item = this.GetCellView(i);
                }
                this.cells.Add(item);
                if (item != null)
                {
                    this.AddScrollCellToContent(item);
                }
                startPosition += cellWidth;
            }
            this.cells.AddRange((IEnumerable<BaseCellView>)list);
            //this.updateLayoutLocker.Unlock();
            this.ArrangeChildren();
        }

        protected virtual void RemoveChildFromContent(View item)
        {
            base.Children.Remove(item);
        }

        protected virtual void UnsubscribeCellsEvents()
        {
            foreach (BaseCellView view in this.cells)
            {
                if (view != null)
                {
                    view.UnsubscribeEvents();
                }
            }
        }

        protected abstract void UpdateCellData(BaseCellView cellView, int cellIndex);
        protected void UpdateLayout()
        {
            //this.updateLayoutLocker.Lock();
            double startPosition = this.GetStartPosition();
            this.UpdateScrollAreaWidth();
            //this.ScrollView.BatchBegin();
            int num2 = this.ViewsCount - this.rightFixedColumnsCount;
            for (int i = this.leftFixedColumnsCount; i < num2; i++)
            {
                double cellWidth = this.RowContentProvider.GetCellWidth(i);
                if (!this.CheckColumnVisibility(startPosition, cellWidth))
                {
                    if (this.Cells[i] != null)
                    {
                        object cellIdentifier = this.RowContentProvider.GetCellIdentifier(i);
                        BaseCellView cell = this.cells[i];
                        cell.UnsubscribeEvents();
                        this.HideScrollCell(cell);
                        if (!this.CellsCache.ContainsKey(cellIdentifier))
                        {
                            this.CellsCache.Add(cellIdentifier, new Stack<BaseCellView>());
                        }
                        this.CellsCache[cellIdentifier].Push(cell);
                        this.cells[i] = null;
                    }
                }
                else if (this.Cells[i] == null)
                {
                    BaseCellView cellFromCache;
                    object cellIdentifier = this.RowContentProvider.GetCellIdentifier(i);
                    if (this.CellsCache.ContainsKey(cellIdentifier) && (this.CellsCache[cellIdentifier].Count > 0))
                    {
                        cellFromCache = this.GetCellFromCache(i, cellIdentifier);
                    }
                    else
                    {
                        cellFromCache = this.GetCellView(i);
                        if (cellFromCache != null)
                        {
                            this.AddScrollCellToContent(cellFromCache);
                        }
                    }
                    this.cells[i] = cellFromCache;
                }
                startPosition += cellWidth;
            }
            //this.ScrollView.BatchCommit();
            //this.updateLayoutLocker.Unlock();
            this.ArrangeChildren();
        }

        private void UpdateScrollAreaWidth()
        {
            this.scrollAreaWidth = (this.RowContentProvider.VisibleRowWidth - this.leftFixedWidth) - this.rightFixedWidth;
        }

        internal abstract void UpdateTheme();

        // Properties
        //protected ScrollAreaView ScrollView { get; set; }

        protected IRowContentProvider RowContentProvider =>
            this.rowContentProvider;

        //protected ThemeBase CurrentTheme =>
        //    ThemeManager.Theme;

        protected abstract int ViewsCount { get; }

        public IReadOnlyList<BaseCellView> Cells =>
            ((IReadOnlyList<BaseCellView>)this.cells);

        // Nested Types
        protected class CellInfoHelper : RowContainerBase.CellsVisitorBase
        {
            // Fields
            private static double xTapPosition;
            private static GridCellInfo result;

            // Methods
            public static bool CheckPositionInChild(double x, View child, double cellStartX, double cellWidth)
            {
                double cellXPosition = RowContainerBase.CellsVisitorBase.rowcontainer.GetCellXPosition(child, cellStartX);
                double num2 = cellXPosition + cellWidth;
                if ((x < cellXPosition) || (x > num2))
                {
                    return false;
                }
                double num3 = RowContainerBase.CellsVisitorBase.rowcontainer.RowContentProvider.VisibleRowWidth - RowContainerBase.CellsVisitorBase.rowcontainer.rightFixedWidth;
                return ((((cellXPosition >= num3) || (num2 <= num3)) || (x < num3)) && ((cellXPosition < num3))); // || (child.Parent != RowContainerBase.CellsVisitorBase.rowcontainer.ScrollView)));
            }

            public static GridCellInfo GetCellInfoByPosition(RowContainerBase _rowContaiter, double x)
            {
                RowContainerBase.CellsVisitorBase.rowcontainer = _rowContaiter;
                xTapPosition = x;
                DoActionsWithCells(new RowContainerBase.CellsVisitorBase.CellAction(RowContainerBase.CellInfoHelper.GetGridCellInfo));
                GridCellInfo result = RowContainerBase.CellInfoHelper.result;
                RowContainerBase.CellInfoHelper.result = null;
                xTapPosition = -1.0;
                return ((result == null) ? new GridCellInfo(-1, null, null, Rectangle.Zero) : result);
            }

            private static void GetGridCellInfo(BaseCellView view, Size size, double startPosition, int cellIndex)
            {
                if ((result == null) && CheckPositionInChild(xTapPosition, view, startPosition, size.Width))
                {
                    result = new GridCellInfo(cellIndex, view, RowContainerBase.CellsVisitorBase.rowcontainer.GetColumn(cellIndex), new Rectangle(RowContainerBase.CellsVisitorBase.rowcontainer.GetCellXPosition(view, startPosition), view.Y, view.Width, view.Height));
                }
            }
        }

        protected class CellsArrangeHelper : RowContainerBase.CellsVisitorBase
        {
            // Methods
            private static void ArrangeCell(BaseCellView view, Size size, double startPosition, int cellIndex)
            {
                Rectangle itemRect = new Rectangle(startPosition, 0.0, size.Width, size.Height);
                RowContainerBase.CellsVisitorBase.rowcontainer.CustomizeAndLocateItem(view, itemRect, cellIndex == 0, cellIndex == (RowContainerBase.CellsVisitorBase.rowcontainer.RowContentProvider.CellsCount - 1), cellIndex == (RowContainerBase.CellsVisitorBase.rowcontainer.leftFixedColumnsCount - 1), cellIndex == (RowContainerBase.CellsVisitorBase.rowcontainer.ViewsCount - RowContainerBase.CellsVisitorBase.rowcontainer.rightFixedColumnsCount));
            }

            public static void ArrangeCells(RowContainerBase _rowContainer)
            {
                RowContainerBase.CellsVisitorBase.rowcontainer = _rowContainer;
                DoActionsWithCells(new RowContainerBase.CellsVisitorBase.CellAction(RowContainerBase.CellsArrangeHelper.ArrangeCell));
            }
        }

        protected class CellsVisitorBase
        {
            // Fields
            protected static RowContainerBase rowcontainer;

            // Methods
            protected static void DoActionsWithCells(CellAction action)
            {
                double startPosition = 0.0;
                for (int i = 0; i < rowcontainer.ViewsCount; i++)
                {
                    if (i == rowcontainer.leftFixedColumnsCount)
                    {
                        startPosition = rowcontainer.GetStartPosition();
                    }
                    if (i == (rowcontainer.ViewsCount - rowcontainer.rightFixedColumnsCount))
                    {
                        startPosition = rowcontainer.RowContentProvider.VisibleRowWidth - rowcontainer.rightFixedWidth;
                    }
                    BaseCellView view = rowcontainer.Cells[i];
                    Size cellSize = rowcontainer.GetCellSize(i);
                    if (view != null)
                    {
                        action(view, cellSize, startPosition, i);
                    }
                    startPosition += cellSize.Width;
                }
            }

            // Nested Types
            protected delegate void CellAction(BaseCellView view, Size size, double startPosition, int cellIndex);
        }
    }
}
