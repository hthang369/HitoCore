using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace HitoAppCore.DataGrid
{
    public abstract class BaseContentProvider : IRowContentProvider
    {
        // Fields
        private GridColumnCollection columns;
        private IReadOnlyList<GridColumn> visibleColumns;
        private IHorizontalScrollingData horizontalScrollingData;
        private bool customizable;
        private double rowWidth;
        private double rowHeight;
        // Methods
        public BaseContentProvider(double rowHeight, IHorizontalScrollingData horizontalScrollingData)
        {
            //this.dataController = dataController;
            this.SubscribeDataControllerEvents();
            this.customizable = false;
            this.rowHeight = rowHeight;
            this.horizontalScrollingData = horizontalScrollingData;
        }

        protected virtual bool ApplyConditionalFormatting(CellView cell) =>
            false;

        protected BaseCellView CreateEditViewCore(int cellHandle, int edititngRowHandle)
        {
            GridColumn column = this.VisibleColumns[cellHandle];
            //EditCellView view = new EditCellView
            //{
            //    Editor = ColumnEditorFactory.Create(column)
            //};
            //if (view.Editor is IBindableEditValue)
            //{
            //    (view.Editor as IBindableEditValue).CreateEditValueBinding(EditValuesContainer.GetBindingPath(column.FieldName), valuesContainer);
            //}
            //else
            //{
            //    CellData data = this.GetCellData(edititngRowHandle, cellHandle, null);
            //    view.set_BindingContext(data);
            //    if (data != null)
            //    {
            //        data.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
            //            CellData data = sender as CellData;
            //            if (data != null)
            //            {
            //                valuesContainer.Values[data.Index.FieldName] = data.Value;
            //            }
            //        };
            //    }
            //}
            //view.set_HorizontalOptions(LayoutOptions.FillAndExpand);
            //view.set_VerticalOptions(LayoutOptions.FillAndExpand);
            //view.set_Padding(this.CurrentTheme.CellCustomizer.Padding);
            return null;//view;
        }

        protected abstract BaseCellView CreateView(int cellHandle);
        protected void DelegateArrangeContent()
        {
            //if (this.providerDelegate != null)
            //{
            //    this.providerDelegate.ContentArranged();
            //}
        }

        protected void DelegateInvalidateContent(bool hardDropCaches, bool softDropCaches)
        {
            //if (this.providerDelegate != null)
            //{
            //    this.providerDelegate.ContentInvalidated(hardDropCaches, softDropCaches);
            //}
        }

        protected void DelegateUpdateContent()
        {
            //if (this.providerDelegate != null)
            //{
            //    this.providerDelegate.ContentUpdated();
            //}
        }

        bool IRowContentProvider.ApplyConditionalFormatting(CellView cell) =>
            this.ApplyConditionalFormatting(cell);

        BaseCellView IRowContentProvider.CreateView(int cellHandle) =>
            this.CreateView(cellHandle);

        CellData IRowContentProvider.GetCellData(int rowHandle, int cellHandle, CellData reuseCellData) =>
            this.GetCellData(rowHandle, cellHandle, reuseCellData);

        FixedStyle IRowContentProvider.GetCellFixedStyle(int cellHandle) =>
            this.GetFixedStyle(cellHandle);

        object IRowContentProvider.GetCellIdentifier(int cellHandle) =>
            this.GetCellIdentifier(cellHandle);

        double IRowContentProvider.GetCellWidth(int cellHandle) =>
            this.GetCellWidth(cellHandle);

        GridColumn IRowContentProvider.GetColumn(int cellHandle)
        {
            if ((cellHandle < 0) || (cellHandle >= this.VisibleColumns.Count))
            {
                return null;
            }
            return this.VisibleColumns[cellHandle];
        }

        //IGroupInfo IRowContentProvider.GetGroup(int groupHandle) =>
        //    this.dataController.GetGroupInfo(groupHandle);

        //GroupData IRowContentProvider.GetGroupData(int rowHandle)
        //{
        //    IGroupInfo groupInfo = this.dataController.GetGroupInfo(rowHandle);
        //    return ((groupInfo == null) ? null : new GroupData(groupInfo, this.Columns, this.VisibleColumns));
        //}

        double IRowContentProvider.GetRowHeight(int rowHandle) =>
            this.GetRowHeight(rowHandle);

        //void IRowContentProvider.RaiseCustomizeCell(CustomizeCellEventArgs args)
        //{
        //    this.RaiseCustomizeCell(args);
        //}

        //void IRowContentProvider.RegisterDelegate(IRowContentProviderDelegate providerDelegate)
        //{
        //    this.providerDelegate = providerDelegate;
        //}

        void IRowContentProvider.RestoreCellSettings(BaseCellView cellView, int cellHandle)
        {
            this.RestoreCellSettings(cellView, cellHandle);
        }

        bool IRowContentProvider.SupportsFontConditionalFormatting(CellView cell) =>
            this.SupportsFontConditionalFormatting(cell);

        protected abstract CellData GetCellData(int rowHandle, int cellHandle, CellData reuseCellData);
        protected virtual object GetCellIdentifier(int cellHandle) =>
            this.visibleColumns[cellHandle].GetType();

        protected virtual int GetCellsCount() =>
            ((this.visibleColumns == null) ? 0 : this.visibleColumns.Count);

        protected virtual double GetCellWidth(int cellHandle) =>
            this.visibleColumns[cellHandle].ActualWidth;

        protected virtual FixedStyle GetFixedStyle(int cellHandle) =>
            (this.HorizontalScrollingData.ColumnsAutoWidth ? FixedStyle.None : this.visibleColumns[cellHandle].FixedStyle);

        protected virtual double GetHorizontalOffset() =>
            this.HorizontalScrollingData.HorizontalScrollOffset;

        protected double GetRowHeight(int rowHandle) =>
            this.rowHeight;

        //protected virtual void HandleDataChanged(object sender, GridDataControllerDataChangedEventArgs args)
        //{
        //    int num1;
        //    int num2;
        //    if (((args.ChangeType == GridDataControllerDataChangedType.DataSourceChanged) || ((args.ChangeType == GridDataControllerDataChangedType.GroupingChanged) || (args.ChangeType == GridDataControllerDataChangedType.DataSourceRowInserted))) || (args.ChangeType == GridDataControllerDataChangedType.DataSourceRowDeleted))
        //    {
        //        num1 = 1;
        //    }
        //    else
        //    {
        //        num1 = (args.ChangeType != GridDataControllerDataChangedType.DataSourceRowChanged) ? 0 : (this.DataController.Predicate != null);
        //    }
        //    bool hardDropCaches = num1;
        //    if ((args.ChangeType == GridDataControllerDataChangedType.DataSourceRowInserted) || (args.ChangeType == GridDataControllerDataChangedType.DataSourceRowDeleted))
        //    {
        //        num2 = 1;
        //    }
        //    else
        //    {
        //        num2 = (args.ChangeType != GridDataControllerDataChangedType.DataSourceRowChanged) ? 0 : (this.DataController.Predicate != null);
        //    }
        //    this.DelegateInvalidateContent(hardDropCaches, num2);
        //}

        //protected internal virtual void RaiseCustomizeCell(CustomizeCellEventArgs args)
        //{
        //    if (this.CustomizeCell != null)
        //    {
        //        this.CustomizeCell(args);
        //    }
        //}

        public void RecreateLayout()
        {
            this.UpdateLayoutCore(new Action(this.Refresh), true);
        }

        public void Refresh()
        {
            this.DelegateInvalidateContent(true, false);
        }

        public virtual void ResetConditionalFormatting()
        {
        }

        protected abstract void RestoreCellSettings(BaseCellView cellView, int cellHandle);
        public void SetColumns(GridColumnCollection columns)
        {
            this.columns = columns;
        }

        public virtual void SetRowHeight(double rowHeight)
        {
            this.rowHeight = rowHeight;
        }

        public virtual void SetVisibleColumns(IReadOnlyList<GridColumn> columns)
        {
            if (!object.ReferenceEquals(this.visibleColumns, columns))
            {
                this.visibleColumns = columns;
                this.RecreateLayout();
            }
        }

        public void SubscribeDataControllerEvents()
        {
            //this.dataController.DataChanged += new GridDataControllerDataChangedEventHandler(this.HandleDataChanged);
        }

        protected virtual bool SupportsFontConditionalFormatting(CellView cell) =>
            false;

        public void UnsubscribeDataControllerEvents()
        {
            //this.dataController.DataChanged -= new GridDataControllerDataChangedEventHandler(this.HandleDataChanged);
        }

        public void UpdateLayout()
        {
            this.UpdateLayoutCore(new Action(this.DelegateUpdateContent), false);
        }

        private void UpdateLayoutCore(Action action, bool forceAction)
        {
            if ((this.visibleColumns == null) || (this.visibleColumns.Count <= 0))
            {
                if (forceAction)
                {
                    action();
                }
            }
            else
            {
                this.rowWidth = 0.0;
                foreach (GridColumn column in this.visibleColumns)
                {
                    this.rowWidth += column.ActualWidth;
                }
                action();
            }
        }

        // Properties
        //protected IRowContentProviderDelegate ProviderDelegate =>
        //    this.providerDelegate;

        protected GridColumnCollection Columns =>
            this.columns;

        protected IReadOnlyList<GridColumn> VisibleColumns =>
            this.visibleColumns;

        //protected IGridDataController DataController =>
        //    this.dataController;

        //protected ThemeBase CurrentTheme =>
        //    ThemeManager.Theme;

        protected IHorizontalScrollingData HorizontalScrollingData =>
            this.horizontalScrollingData;

        protected abstract int RowCount { get; }

        protected abstract int GroupCount { get; }

        int IRowContentProvider.CellsCount =>
            this.GetCellsCount();

        double IRowContentProvider.RowWidth =>
            this.rowWidth;

        int IRowContentProvider.GroupCount =>
            this.GroupCount;

        int IRowContentProvider.RowCount =>
            this.RowCount;

        bool IRowContentProvider.Customizable
        {
            get =>
                this.customizable;
            set =>
                this.customizable = value;
        }

        double IRowContentProvider.HorizontalScrollOffset =>
            this.GetHorizontalOffset();

        double IRowContentProvider.VisibleRowWidth =>
            this.HorizontalScrollingData.VisibleRowWidth;

        bool IRowContentProvider.AllowHorizontalScrollingVirtualization =>
            this.HorizontalScrollingData.AllowHorizontalScrollingVirtualization;
    }
}
