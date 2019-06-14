using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class GridControl : GridLayout, ICommandAwareControl<GridCommandId>, IFormatConditionCollectionOwner, IGestureRecognizerDelegate, ISelectedRowHandleProvider, IHitTestAccess, IDisposable, IThemeChangingHandler, IAndroidThemeChanger, IXtraSerializableLayoutEx, IHorizontalScrollingData, ICustomCellTextProvider, IServiceProvider
    {
        // Fields
        private GestureCommandBindings commandBindings;
        private EventHandler onUpdateUI;
        private ConditionalFormattingEngine conditionalFormatEngine;
        private ContentPage formEditorContentPage;
        private DialogForm formEditorDialogForm;
        private EditValuesContainer editingValuesContainer;
        public static readonly BindableProperty OptionsExportXlsxProperty;
        public static readonly BindableProperty OptionsExportXlsProperty;
        public static readonly BindableProperty OptionsExportCsvProperty;
        internal const int AutoFilterRowHandle = -999_997;
        internal const double DefaultAutoFilterHeight = 0x2c;
        public static readonly BindableProperty FilterPanelHeightProperty;
        public static readonly BindableProperty FilterStringProperty;
        private static readonly BindablePropertyKey ActualFilterStringPropertyKey;
        public static readonly BindableProperty ActualFilterStringProperty;
        public static readonly BindableProperty FilterPanelVisibilityProperty;
        public static readonly BindableProperty AutoFilterPanelHeightProperty;
        public static readonly BindableProperty AutoFilterPanelVisibilityProperty;
        private GridFilter filter;
        private FilterPanelContentProvider filterPanelContentProvider;
        private FilterPanelContainer filterPanelContainer;
        private AutoFilterContentProvider autoFilterPanelContentProvider;
        private AutoFilterPanelContainer autoFilterPanelContainer;
        private bool lockFilterStringChanged;
        private GridGestureHandler gestureHandler;
        private ScrollHitTestAccessor scrollHitTestAccessor;
        private const string VisibleColumnsPropertyName = "VisibleColumns";
        internal const int NewItemRowHandle = -2_147_483_647;
        public const double DefaultColumnHeaderHeight = 0x2c;
        public const double DefaultRowHeight = 0x2c;
        public const double DefaultColumnWidth = 120.0;
        private const int GRID_HEADER_ROW = 0;
        private const int GRID_SAVE_CANCEL_BUTTONS_ROW = 1;
        private const int GRID_AUTO_FILTER_ROW = 2;
        private const int GRID_NEW_ITEM_ROW = 3;
        private const int GRID_SCROLL_ROW = 4;
        private const int GRID_TOTAL_SUMMARY_ROW = 5;
        private const int GRID_FILTER_ROW = 6;
        private const int GRID_COLUMN_CHOOSER_COLUMN = 1;
        public const int InvalidRowHandle = -2_147_483_648;
        public static readonly BindableProperty ItemsSourceProperty;
        public static readonly BindableProperty ColumnHeadersHeightProperty;
        public static readonly BindableProperty ColumnHeadersVisibilityProperty;
        public static readonly BindableProperty RowHeightProperty;
        public static readonly BindableProperty SelectedRowHandleProperty;
        public static readonly BindableProperty SelectedDataObjectProperty;
        public static readonly BindableProperty SortModeProperty;
        public static readonly BindableProperty IsReadOnlyProperty;
        public static readonly BindableProperty AllowSortProperty;
        public static readonly BindableProperty AllowEditRowsProperty;
        public static readonly BindableProperty AllowDeleteRowsProperty;
        public static readonly BindableProperty AllowResizeColumnsProperty;
        public static readonly BindableProperty AllowGroupProperty;
        public static readonly BindableProperty AllowGroupCollapseProperty;
        public static readonly BindableProperty IsRowCellMenuEnabledProperty;
        public static readonly BindableProperty IsTotalSummaryMenuEnabledProperty;
        public static readonly BindableProperty IsColumnMenuEnabledProperty;
        public static readonly BindableProperty IsGroupRowMenuEnabledProperty;
        public static readonly BindableProperty HighlightMenuTargetElementsProperty;
        public static readonly BindableProperty AutoGenerateColumnsModeProperty;
        public static readonly BindableProperty IsPullToRefreshEnabledProperty;
        public static readonly BindableProperty PullToRefreshCommandProperty;
        public static readonly BindableProperty LoadMoreCommandProperty;
        public static readonly BindableProperty RowTapCommandProperty;
        public static readonly BindableProperty IsLoadMoreEnabledProperty;
        public static readonly BindableProperty IsColumnChooserEnabledProperty;
        public static readonly BindableProperty GroupsInitiallyExpandedProperty;
        public static readonly BindableProperty RowEditModeProperty;
        public static readonly BindableProperty ColumnsAutoWidthProperty;
        private static bool platformInitialized;
        private double horizontalScrollOffsetCore;
        private bool allowHorizontalScrollingVirtualization = true;
        private readonly RowVirtualizer rowVirtualizer;
        private readonly ScrollContentLayout scrollContent;
        private readonly RowContentProvider dataRowContentProvider;
        private readonly HeaderContentProvider headerContentProvider;
        private readonly HeadersContainer headers;
        private readonly PullToRefreshView refreshView;
        private readonly GridDataController dataController;
        private readonly RowsLayout rowsLayout;
        private readonly GridColumnChooser columnChooser;
        private readonly ExtendedScrollView scroller;
        private readonly AdornerView adorner;
        private bool isDisposed;
        internal bool isColumnResizing;
        private int selectedRowSourceIndex = -1;
        private double totalColumnsWidth;
        private SortingColumnManager sortingColumnManager;
        private List<GridColumn> visibleColumns;
        private GridColumnCollection columns;
        private IList<GridColumn> autoGeneratedColumns;
        private bool? isDevExpressNotRegistered;
        private bool isRendererShouldBeLoaded;
        [CompilerGenerated]
        private EventHandler PullToRefresh;
        [CompilerGenerated]
        private EventHandler LoadMore;
        [CompilerGenerated]
        private CustomizeCellEventHandler customizeCell;
        [CompilerGenerated]
        private CustomizeCellDisplayTextEventHandler customizeCellDisplayText;
        [CompilerGenerated]
        private AutoGeneratingColumnEventHandler AutoGeneratingColumn;
        [CompilerGenerated]
        private GridColumnDataEventHandler CustomUnboundColumnData;
        [CompilerGenerated]
        private CustomSummaryEventHandler CalculateCustomSummary;
        [CompilerGenerated]
        private RowAllowEventHandler GroupRowCollapsing;
        [CompilerGenerated]
        private RowAllowEventHandler GroupRowExpanding;
        [CompilerGenerated]
        private RowEventHandler GroupRowCollapsed;
        [CompilerGenerated]
        private RowEventHandler GroupRowExpanded;
        [CompilerGenerated]
        private RowEventHandler SelectionChanged;
        [CompilerGenerated]
        private RowTapEventHandler RowTap;
        [CompilerGenerated]
        private RowDoubleTapEventHandler RowDoubleTap;
        [CompilerGenerated]
        private RowEditingEventHandler EndRowEdit;
        [CompilerGenerated]
        private PopupMenuEventHandler PopupMenuCustomization;
        [CompilerGenerated]
        private HorizontalScrollOffsetEventHandler HorizontalScrollOffsetChanged;
        [CompilerGenerated]
        private EventHandler FilterApplied;
        private Size size = Size.Zero;
        private bool isThemeChanged;
        private EditRowContentProvider editRowContentProvider;
        private EditRowLayout editRowLayout;
        private RowContainer underEditorRowContainer;
        private SaveCancelEditingRowControl saveCancelEditingRowControl;
        private int openedRowHandle = -2_147_483_648;
        internal const double SaveCancelRowHeight = 50.0;
        private CellIndex openEditorCellIndex;
        public static readonly BindableProperty NewItemRowVisibilityProperty;
        [CompilerGenerated]
        private InitNewRowEventHandler InitNewRow;
        private NewItemRowContainer newItemRowContainer;
        private NewItemRowContentProvider newItemRowContentProvider;
        private GridPopupMenuView popupMenu = new GridPopupMenuView();
        private readonly List<BoxView> menuContext = new List<BoxView>();
        private bool isPopupMenuOpened;
        private VirtualRowContainer draggedRow;
        private int currentDraggedRowHandle = -2_147_483_648;
        private double sumDistance;
        private RowDragDirection dragDirection;
        public static readonly BindableProperty SwipeButtonCommandProperty;
        [CompilerGenerated]
        private SwipeButtonEventHandler SwipeButtonClick;
        [CompilerGenerated]
        private SwipeButtonShowingEventHandler SwipeButtonShowing;
        private ServiceContainer serviceContainer;
        public static readonly BindableProperty TotalSummaryHeightProperty;
        public static readonly BindableProperty TotalSummaryVisibilityProperty;
        private TotalSummaryContentProvider totalSummaryContentProvider;
        private TotalSummaryContainer totalSummary;

        // Events
        public event AutoGeneratingColumnEventHandler AutoGeneratingColumn
        {
            [CompilerGenerated]
            add
            {
                AutoGeneratingColumnEventHandler autoGeneratingColumn = this.AutoGeneratingColumn;
                while (true)
                {
                    AutoGeneratingColumnEventHandler comparand = autoGeneratingColumn;
                    AutoGeneratingColumnEventHandler handler3 = (AutoGeneratingColumnEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    autoGeneratingColumn = Interlocked.CompareExchange<AutoGeneratingColumnEventHandler>(ref this.AutoGeneratingColumn, handler3, comparand);
                    if (object.ReferenceEquals(autoGeneratingColumn, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                AutoGeneratingColumnEventHandler autoGeneratingColumn = this.AutoGeneratingColumn;
                while (true)
                {
                    AutoGeneratingColumnEventHandler comparand = autoGeneratingColumn;
                    AutoGeneratingColumnEventHandler handler3 = (AutoGeneratingColumnEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    autoGeneratingColumn = Interlocked.CompareExchange<AutoGeneratingColumnEventHandler>(ref this.AutoGeneratingColumn, handler3, comparand);
                    if (object.ReferenceEquals(autoGeneratingColumn, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event CustomSummaryEventHandler CalculateCustomSummary
        {
            [CompilerGenerated]
            add
            {
                CustomSummaryEventHandler calculateCustomSummary = this.CalculateCustomSummary;
                while (true)
                {
                    CustomSummaryEventHandler comparand = calculateCustomSummary;
                    CustomSummaryEventHandler handler3 = (CustomSummaryEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    calculateCustomSummary = Interlocked.CompareExchange<CustomSummaryEventHandler>(ref this.CalculateCustomSummary, handler3, comparand);
                    if (object.ReferenceEquals(calculateCustomSummary, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                CustomSummaryEventHandler calculateCustomSummary = this.CalculateCustomSummary;
                while (true)
                {
                    CustomSummaryEventHandler comparand = calculateCustomSummary;
                    CustomSummaryEventHandler handler3 = (CustomSummaryEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    calculateCustomSummary = Interlocked.CompareExchange<CustomSummaryEventHandler>(ref this.CalculateCustomSummary, handler3, comparand);
                    if (object.ReferenceEquals(calculateCustomSummary, comparand))
                    {
                        return;
                    }
                }
            }
        }

        private event CustomizeCellEventHandler customizeCell
        {
            [CompilerGenerated]
            add
            {
                CustomizeCellEventHandler customizeCell = this.customizeCell;
                while (true)
                {
                    CustomizeCellEventHandler comparand = customizeCell;
                    CustomizeCellEventHandler handler3 = (CustomizeCellEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    customizeCell = Interlocked.CompareExchange<CustomizeCellEventHandler>(ref this.customizeCell, handler3, comparand);
                    if (object.ReferenceEquals(customizeCell, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                CustomizeCellEventHandler customizeCell = this.customizeCell;
                while (true)
                {
                    CustomizeCellEventHandler comparand = customizeCell;
                    CustomizeCellEventHandler handler3 = (CustomizeCellEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    customizeCell = Interlocked.CompareExchange<CustomizeCellEventHandler>(ref this.customizeCell, handler3, comparand);
                    if (object.ReferenceEquals(customizeCell, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event CustomizeCellEventHandler CustomizeCell
        {
            add
            {
                this.customizeCell += value;
                this.dataRowContentProvider.Customizable = this.customizeCell != null;
            }
            remove
            {
                this.customizeCell -= value;
                this.dataRowContentProvider.Customizable = this.customizeCell != null;
            }
        }

        private event CustomizeCellDisplayTextEventHandler customizeCellDisplayText
        {
            [CompilerGenerated]
            add
            {
                CustomizeCellDisplayTextEventHandler customizeCellDisplayText = this.customizeCellDisplayText;
                while (true)
                {
                    CustomizeCellDisplayTextEventHandler comparand = customizeCellDisplayText;
                    CustomizeCellDisplayTextEventHandler handler3 = (CustomizeCellDisplayTextEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    customizeCellDisplayText = Interlocked.CompareExchange<CustomizeCellDisplayTextEventHandler>(ref this.customizeCellDisplayText, handler3, comparand);
                    if (object.ReferenceEquals(customizeCellDisplayText, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                CustomizeCellDisplayTextEventHandler customizeCellDisplayText = this.customizeCellDisplayText;
                while (true)
                {
                    CustomizeCellDisplayTextEventHandler comparand = customizeCellDisplayText;
                    CustomizeCellDisplayTextEventHandler handler3 = (CustomizeCellDisplayTextEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    customizeCellDisplayText = Interlocked.CompareExchange<CustomizeCellDisplayTextEventHandler>(ref this.customizeCellDisplayText, handler3, comparand);
                    if (object.ReferenceEquals(customizeCellDisplayText, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event CustomizeCellDisplayTextEventHandler CustomizeCellDisplayText
        {
            add
            {
                this.customizeCellDisplayText += value;
                this.ResetColumnsComparers();
                this.UpdateVisibleColumns();
            }
            remove
            {
                this.customizeCellDisplayText -= value;
                this.ResetColumnsComparers();
                this.UpdateVisibleColumns();
            }
        }

        public event GridColumnDataEventHandler CustomUnboundColumnData
        {
            [CompilerGenerated]
            add
            {
                GridColumnDataEventHandler customUnboundColumnData = this.CustomUnboundColumnData;
                while (true)
                {
                    GridColumnDataEventHandler comparand = customUnboundColumnData;
                    GridColumnDataEventHandler handler3 = (GridColumnDataEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    customUnboundColumnData = Interlocked.CompareExchange<GridColumnDataEventHandler>(ref this.CustomUnboundColumnData, handler3, comparand);
                    if (object.ReferenceEquals(customUnboundColumnData, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                GridColumnDataEventHandler customUnboundColumnData = this.CustomUnboundColumnData;
                while (true)
                {
                    GridColumnDataEventHandler comparand = customUnboundColumnData;
                    GridColumnDataEventHandler handler3 = (GridColumnDataEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    customUnboundColumnData = Interlocked.CompareExchange<GridColumnDataEventHandler>(ref this.CustomUnboundColumnData, handler3, comparand);
                    if (object.ReferenceEquals(customUnboundColumnData, comparand))
                    {
                        return;
                    }
                }
            }
        }

        event EventHandler ICommandAwareControl<GridCommandId>.UpdateUI
        {
            add
            {
                this.onUpdateUI = (EventHandler)Delegate.Combine((Delegate)this.onUpdateUI, (Delegate)value);
            }
            remove
            {
                this.onUpdateUI = (EventHandler)Delegate.Remove((Delegate)this.onUpdateUI, (Delegate)value);
            }
        }

        public event RowEditingEventHandler EndRowEdit
        {
            [CompilerGenerated]
            add
            {
                RowEditingEventHandler endRowEdit = this.EndRowEdit;
                while (true)
                {
                    RowEditingEventHandler comparand = endRowEdit;
                    RowEditingEventHandler handler3 = (RowEditingEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    endRowEdit = Interlocked.CompareExchange<RowEditingEventHandler>(ref this.EndRowEdit, handler3, comparand);
                    if (object.ReferenceEquals(endRowEdit, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                RowEditingEventHandler endRowEdit = this.EndRowEdit;
                while (true)
                {
                    RowEditingEventHandler comparand = endRowEdit;
                    RowEditingEventHandler handler3 = (RowEditingEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    endRowEdit = Interlocked.CompareExchange<RowEditingEventHandler>(ref this.EndRowEdit, handler3, comparand);
                    if (object.ReferenceEquals(endRowEdit, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event EventHandler FilterApplied
        {
            [CompilerGenerated]
            add
            {
                EventHandler filterApplied = this.FilterApplied;
                while (true)
                {
                    EventHandler comparand = filterApplied;
                    EventHandler handler3 = (EventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    filterApplied = Interlocked.CompareExchange<EventHandler>(ref this.FilterApplied, handler3, comparand);
                    if (object.ReferenceEquals(filterApplied, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                EventHandler filterApplied = this.FilterApplied;
                while (true)
                {
                    EventHandler comparand = filterApplied;
                    EventHandler handler3 = (EventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    filterApplied = Interlocked.CompareExchange<EventHandler>(ref this.FilterApplied, handler3, comparand);
                    if (object.ReferenceEquals(filterApplied, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event RowEventHandler GroupRowCollapsed
        {
            [CompilerGenerated]
            add
            {
                RowEventHandler groupRowCollapsed = this.GroupRowCollapsed;
                while (true)
                {
                    RowEventHandler comparand = groupRowCollapsed;
                    RowEventHandler handler3 = (RowEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    groupRowCollapsed = Interlocked.CompareExchange<RowEventHandler>(ref this.GroupRowCollapsed, handler3, comparand);
                    if (object.ReferenceEquals(groupRowCollapsed, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                RowEventHandler groupRowCollapsed = this.GroupRowCollapsed;
                while (true)
                {
                    RowEventHandler comparand = groupRowCollapsed;
                    RowEventHandler handler3 = (RowEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    groupRowCollapsed = Interlocked.CompareExchange<RowEventHandler>(ref this.GroupRowCollapsed, handler3, comparand);
                    if (object.ReferenceEquals(groupRowCollapsed, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event RowAllowEventHandler GroupRowCollapsing
        {
            [CompilerGenerated]
            add
            {
                RowAllowEventHandler groupRowCollapsing = this.GroupRowCollapsing;
                while (true)
                {
                    RowAllowEventHandler comparand = groupRowCollapsing;
                    RowAllowEventHandler handler3 = (RowAllowEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    groupRowCollapsing = Interlocked.CompareExchange<RowAllowEventHandler>(ref this.GroupRowCollapsing, handler3, comparand);
                    if (object.ReferenceEquals(groupRowCollapsing, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                RowAllowEventHandler groupRowCollapsing = this.GroupRowCollapsing;
                while (true)
                {
                    RowAllowEventHandler comparand = groupRowCollapsing;
                    RowAllowEventHandler handler3 = (RowAllowEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    groupRowCollapsing = Interlocked.CompareExchange<RowAllowEventHandler>(ref this.GroupRowCollapsing, handler3, comparand);
                    if (object.ReferenceEquals(groupRowCollapsing, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event RowEventHandler GroupRowExpanded
        {
            [CompilerGenerated]
            add
            {
                RowEventHandler groupRowExpanded = this.GroupRowExpanded;
                while (true)
                {
                    RowEventHandler comparand = groupRowExpanded;
                    RowEventHandler handler3 = (RowEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    groupRowExpanded = Interlocked.CompareExchange<RowEventHandler>(ref this.GroupRowExpanded, handler3, comparand);
                    if (object.ReferenceEquals(groupRowExpanded, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                RowEventHandler groupRowExpanded = this.GroupRowExpanded;
                while (true)
                {
                    RowEventHandler comparand = groupRowExpanded;
                    RowEventHandler handler3 = (RowEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    groupRowExpanded = Interlocked.CompareExchange<RowEventHandler>(ref this.GroupRowExpanded, handler3, comparand);
                    if (object.ReferenceEquals(groupRowExpanded, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event RowAllowEventHandler GroupRowExpanding
        {
            [CompilerGenerated]
            add
            {
                RowAllowEventHandler groupRowExpanding = this.GroupRowExpanding;
                while (true)
                {
                    RowAllowEventHandler comparand = groupRowExpanding;
                    RowAllowEventHandler handler3 = (RowAllowEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    groupRowExpanding = Interlocked.CompareExchange<RowAllowEventHandler>(ref this.GroupRowExpanding, handler3, comparand);
                    if (object.ReferenceEquals(groupRowExpanding, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                RowAllowEventHandler groupRowExpanding = this.GroupRowExpanding;
                while (true)
                {
                    RowAllowEventHandler comparand = groupRowExpanding;
                    RowAllowEventHandler handler3 = (RowAllowEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    groupRowExpanding = Interlocked.CompareExchange<RowAllowEventHandler>(ref this.GroupRowExpanding, handler3, comparand);
                    if (object.ReferenceEquals(groupRowExpanding, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event HorizontalScrollOffsetEventHandler HorizontalScrollOffsetChanged
        {
            [CompilerGenerated]
            add
            {
                HorizontalScrollOffsetEventHandler horizontalScrollOffsetChanged = this.HorizontalScrollOffsetChanged;
                while (true)
                {
                    HorizontalScrollOffsetEventHandler comparand = horizontalScrollOffsetChanged;
                    HorizontalScrollOffsetEventHandler handler3 = (HorizontalScrollOffsetEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    horizontalScrollOffsetChanged = Interlocked.CompareExchange<HorizontalScrollOffsetEventHandler>(ref this.HorizontalScrollOffsetChanged, handler3, comparand);
                    if (object.ReferenceEquals(horizontalScrollOffsetChanged, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                HorizontalScrollOffsetEventHandler horizontalScrollOffsetChanged = this.HorizontalScrollOffsetChanged;
                while (true)
                {
                    HorizontalScrollOffsetEventHandler comparand = horizontalScrollOffsetChanged;
                    HorizontalScrollOffsetEventHandler handler3 = (HorizontalScrollOffsetEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    horizontalScrollOffsetChanged = Interlocked.CompareExchange<HorizontalScrollOffsetEventHandler>(ref this.HorizontalScrollOffsetChanged, handler3, comparand);
                    if (object.ReferenceEquals(horizontalScrollOffsetChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event InitNewRowEventHandler InitNewRow
        {
            [CompilerGenerated]
            add
            {
                InitNewRowEventHandler initNewRow = this.InitNewRow;
                while (true)
                {
                    InitNewRowEventHandler comparand = initNewRow;
                    InitNewRowEventHandler handler3 = (InitNewRowEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    initNewRow = Interlocked.CompareExchange<InitNewRowEventHandler>(ref this.InitNewRow, handler3, comparand);
                    if (object.ReferenceEquals(initNewRow, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                InitNewRowEventHandler initNewRow = this.InitNewRow;
                while (true)
                {
                    InitNewRowEventHandler comparand = initNewRow;
                    InitNewRowEventHandler handler3 = (InitNewRowEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    initNewRow = Interlocked.CompareExchange<InitNewRowEventHandler>(ref this.InitNewRow, handler3, comparand);
                    if (object.ReferenceEquals(initNewRow, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event EventHandler LoadMore
        {
            [CompilerGenerated]
            add
            {
                EventHandler loadMore = this.LoadMore;
                while (true)
                {
                    EventHandler comparand = loadMore;
                    EventHandler handler3 = (EventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    loadMore = Interlocked.CompareExchange<EventHandler>(ref this.LoadMore, handler3, comparand);
                    if (object.ReferenceEquals(loadMore, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                EventHandler loadMore = this.LoadMore;
                while (true)
                {
                    EventHandler comparand = loadMore;
                    EventHandler handler3 = (EventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    loadMore = Interlocked.CompareExchange<EventHandler>(ref this.LoadMore, handler3, comparand);
                    if (object.ReferenceEquals(loadMore, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event PopupMenuEventHandler PopupMenuCustomization
        {
            [CompilerGenerated]
            add
            {
                PopupMenuEventHandler popupMenuCustomization = this.PopupMenuCustomization;
                while (true)
                {
                    PopupMenuEventHandler comparand = popupMenuCustomization;
                    PopupMenuEventHandler handler3 = (PopupMenuEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    popupMenuCustomization = Interlocked.CompareExchange<PopupMenuEventHandler>(ref this.PopupMenuCustomization, handler3, comparand);
                    if (object.ReferenceEquals(popupMenuCustomization, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                PopupMenuEventHandler popupMenuCustomization = this.PopupMenuCustomization;
                while (true)
                {
                    PopupMenuEventHandler comparand = popupMenuCustomization;
                    PopupMenuEventHandler handler3 = (PopupMenuEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    popupMenuCustomization = Interlocked.CompareExchange<PopupMenuEventHandler>(ref this.PopupMenuCustomization, handler3, comparand);
                    if (object.ReferenceEquals(popupMenuCustomization, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event EventHandler PullToRefresh
        {
            [CompilerGenerated]
            add
            {
                EventHandler pullToRefresh = this.PullToRefresh;
                while (true)
                {
                    EventHandler comparand = pullToRefresh;
                    EventHandler handler3 = (EventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    pullToRefresh = Interlocked.CompareExchange<EventHandler>(ref this.PullToRefresh, handler3, comparand);
                    if (object.ReferenceEquals(pullToRefresh, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                EventHandler pullToRefresh = this.PullToRefresh;
                while (true)
                {
                    EventHandler comparand = pullToRefresh;
                    EventHandler handler3 = (EventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    pullToRefresh = Interlocked.CompareExchange<EventHandler>(ref this.PullToRefresh, handler3, comparand);
                    if (object.ReferenceEquals(pullToRefresh, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event RowDoubleTapEventHandler RowDoubleTap
        {
            [CompilerGenerated]
            add
            {
                RowDoubleTapEventHandler rowDoubleTap = this.RowDoubleTap;
                while (true)
                {
                    RowDoubleTapEventHandler comparand = rowDoubleTap;
                    RowDoubleTapEventHandler handler3 = (RowDoubleTapEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    rowDoubleTap = Interlocked.CompareExchange<RowDoubleTapEventHandler>(ref this.RowDoubleTap, handler3, comparand);
                    if (object.ReferenceEquals(rowDoubleTap, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                RowDoubleTapEventHandler rowDoubleTap = this.RowDoubleTap;
                while (true)
                {
                    RowDoubleTapEventHandler comparand = rowDoubleTap;
                    RowDoubleTapEventHandler handler3 = (RowDoubleTapEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    rowDoubleTap = Interlocked.CompareExchange<RowDoubleTapEventHandler>(ref this.RowDoubleTap, handler3, comparand);
                    if (object.ReferenceEquals(rowDoubleTap, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event RowTapEventHandler RowTap
        {
            [CompilerGenerated]
            add
            {
                RowTapEventHandler rowTap = this.RowTap;
                while (true)
                {
                    RowTapEventHandler comparand = rowTap;
                    RowTapEventHandler handler3 = (RowTapEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    rowTap = Interlocked.CompareExchange<RowTapEventHandler>(ref this.RowTap, handler3, comparand);
                    if (object.ReferenceEquals(rowTap, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                RowTapEventHandler rowTap = this.RowTap;
                while (true)
                {
                    RowTapEventHandler comparand = rowTap;
                    RowTapEventHandler handler3 = (RowTapEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    rowTap = Interlocked.CompareExchange<RowTapEventHandler>(ref this.RowTap, handler3, comparand);
                    if (object.ReferenceEquals(rowTap, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event RowEventHandler SelectionChanged
        {
            [CompilerGenerated]
            add
            {
                RowEventHandler selectionChanged = this.SelectionChanged;
                while (true)
                {
                    RowEventHandler comparand = selectionChanged;
                    RowEventHandler handler3 = (RowEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    selectionChanged = Interlocked.CompareExchange<RowEventHandler>(ref this.SelectionChanged, handler3, comparand);
                    if (object.ReferenceEquals(selectionChanged, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                RowEventHandler selectionChanged = this.SelectionChanged;
                while (true)
                {
                    RowEventHandler comparand = selectionChanged;
                    RowEventHandler handler3 = (RowEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    selectionChanged = Interlocked.CompareExchange<RowEventHandler>(ref this.SelectionChanged, handler3, comparand);
                    if (object.ReferenceEquals(selectionChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event SwipeButtonEventHandler SwipeButtonClick
        {
            [CompilerGenerated]
            add
            {
                SwipeButtonEventHandler swipeButtonClick = this.SwipeButtonClick;
                while (true)
                {
                    SwipeButtonEventHandler comparand = swipeButtonClick;
                    SwipeButtonEventHandler handler3 = (SwipeButtonEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    swipeButtonClick = Interlocked.CompareExchange<SwipeButtonEventHandler>(ref this.SwipeButtonClick, handler3, comparand);
                    if (object.ReferenceEquals(swipeButtonClick, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                SwipeButtonEventHandler swipeButtonClick = this.SwipeButtonClick;
                while (true)
                {
                    SwipeButtonEventHandler comparand = swipeButtonClick;
                    SwipeButtonEventHandler handler3 = (SwipeButtonEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    swipeButtonClick = Interlocked.CompareExchange<SwipeButtonEventHandler>(ref this.SwipeButtonClick, handler3, comparand);
                    if (object.ReferenceEquals(swipeButtonClick, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event SwipeButtonShowingEventHandler SwipeButtonShowing
        {
            [CompilerGenerated]
            add
            {
                SwipeButtonShowingEventHandler swipeButtonShowing = this.SwipeButtonShowing;
                while (true)
                {
                    SwipeButtonShowingEventHandler comparand = swipeButtonShowing;
                    SwipeButtonShowingEventHandler handler3 = (SwipeButtonShowingEventHandler)Delegate.Combine((Delegate)comparand, (Delegate)value);
                    swipeButtonShowing = Interlocked.CompareExchange<SwipeButtonShowingEventHandler>(ref this.SwipeButtonShowing, handler3, comparand);
                    if (object.ReferenceEquals(swipeButtonShowing, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated]
            remove
            {
                SwipeButtonShowingEventHandler swipeButtonShowing = this.SwipeButtonShowing;
                while (true)
                {
                    SwipeButtonShowingEventHandler comparand = swipeButtonShowing;
                    SwipeButtonShowingEventHandler handler3 = (SwipeButtonShowingEventHandler)Delegate.Remove((Delegate)comparand, (Delegate)value);
                    swipeButtonShowing = Interlocked.CompareExchange<SwipeButtonShowingEventHandler>(ref this.SwipeButtonShowing, handler3, comparand);
                    if (object.ReferenceEquals(swipeButtonShowing, comparand))
                    {
                        return;
                    }
                }
            }
        }

        // Methods
        static GridControl()
        {
            ParameterExpression expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray1 = new ParameterExpression[] { expression };
            OptionsExportXlsxProperty = BindingUtils.CreateProperty<GridControl, XlsxExportOptions>(Expression.Lambda<Func<GridControl, XlsxExportOptions>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_OptionsExportXlsx)), expressionArray1), null);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            OptionsExportXlsProperty = BindingUtils.CreateProperty<GridControl, XlsExportOptions>(Expression.Lambda<Func<GridControl, XlsExportOptions>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_OptionsExportXls)), expressionArray2), null);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            OptionsExportCsvProperty = BindingUtils.CreateProperty<GridControl, CsvExportOptions>(Expression.Lambda<Func<GridControl, CsvExportOptions>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_OptionsExportCsv)), expressionArray3), null);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            FilterPanelHeightProperty = BindingUtils.CreateProperty<GridControl, double>(Expression.Lambda<Func<GridControl, double>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_FilterPanelHeight)), expressionArray4), DefaultFilterPanelHeight, new BindableProperty.BindingPropertyChangedDelegate<double>(null, OnFilterPanelHeightChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            FilterStringProperty = BindingUtils.CreateProperty<GridControl, string>(Expression.Lambda<Func<GridControl, string>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_FilterString)), expressionArray5), string.Empty, new BindableProperty.BindingPropertyChangedDelegate<string>(null, OnFilterStringChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            ActualFilterStringPropertyKey = BindingUtils.CreateReadOnlyProperty<GridControl, string>(Expression.Lambda<Func<GridControl, string>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_ActualFilterString)), expressionArray6), string.Empty);
            ActualFilterStringProperty = ActualFilterStringPropertyKey.get_BindableProperty();
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            FilterPanelVisibilityProperty = BindingUtils.CreateProperty<GridControl, VisibilityState>(Expression.Lambda<Func<GridControl, VisibilityState>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_FilterPanelVisibility)), expressionArray7), VisibilityState.Default, new BindableProperty.BindingPropertyChangedDelegate<VisibilityState>(null, OnFilterPanelVisibilityChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            AutoFilterPanelHeightProperty = BindingUtils.CreateProperty<GridControl, double>(Expression.Lambda<Func<GridControl, double>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_AutoFilterPanelHeight)), expressionArray8), 0x2c, new BindableProperty.BindingPropertyChangedDelegate<double>(null, OnAutoFilterPanelHeightChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray9 = new ParameterExpression[] { expression };
            AutoFilterPanelVisibilityProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_AutoFilterPanelVisibility)), expressionArray9), false, new BindableProperty.BindingPropertyChangedDelegate<bool>(null, OnAutoFilterPanelVisibilityChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray10 = new ParameterExpression[] { expression };
            NewItemRowVisibilityProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_NewItemRowVisibility)), expressionArray10), false, new BindableProperty.BindingPropertyChangedDelegate<bool>(null, OnNewItemRowVisibilityChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray11 = new ParameterExpression[] { expression };
            SwipeButtonCommandProperty = BindingUtils.CreateProperty<GridControl, ICommand>(Expression.Lambda<Func<GridControl, ICommand>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_SwipeButtonCommand)), expressionArray11), null);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray12 = new ParameterExpression[] { expression };
            TotalSummaryHeightProperty = BindingUtils.CreateProperty<GridControl, double>(Expression.Lambda<Func<GridControl, double>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_TotalSummaryHeight)), expressionArray12), DefaultTotalSummaryHeight, new BindableProperty.BindingPropertyChangedDelegate<double>(null, OnTotalSummaryHeightChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray13 = new ParameterExpression[] { expression };
            TotalSummaryVisibilityProperty = BindingUtils.CreateProperty<GridControl, VisibilityState>(Expression.Lambda<Func<GridControl, VisibilityState>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_TotalSummaryVisibility)), expressionArray13), VisibilityState.Default, new BindableProperty.BindingPropertyChangedDelegate<VisibilityState>(null, OnTotalSummaryVisibilityChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray14 = new ParameterExpression[] { expression };
            ItemsSourceProperty = BindingUtils.CreateProperty<GridControl, object>(Expression.Lambda<Func<GridControl, object>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_ItemsSource)), expressionArray14), null, new BindableProperty.BindingPropertyChangedDelegate<object>(null, OnItemsSourceChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray15 = new ParameterExpression[] { expression };
            ColumnHeadersHeightProperty = BindingUtils.CreateProperty<GridControl, double>(Expression.Lambda<Func<GridControl, double>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_ColumnHeadersHeight)), expressionArray15), 0x2c, new BindableProperty.BindingPropertyChangedDelegate<double>(null, OnColumnHeadersHeightChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray16 = new ParameterExpression[] { expression };
            ColumnHeadersVisibilityProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_ColumnHeadersVisibility)), expressionArray16), true, new BindableProperty.BindingPropertyChangedDelegate<bool>(null, OnColumnHeadersVisibilityChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray17 = new ParameterExpression[] { expression };
            RowHeightProperty = BindingUtils.CreateProperty<GridControl, double>(Expression.Lambda<Func<GridControl, double>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_RowHeight)), expressionArray17), 0x2c, new BindableProperty.BindingPropertyChangedDelegate<double>(null, OnRowHeightChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray18 = new ParameterExpression[] { expression };
            SelectedRowHandleProperty = BindingUtils.CreateProperty<GridControl, int>(Expression.Lambda<Func<GridControl, int>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_SelectedRowHandle)), expressionArray18), 0, new BindableProperty.BindingPropertyChangedDelegate<int>(null, OnSelectedRowHandleChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray19 = new ParameterExpression[] { expression };
            SelectedDataObjectProperty = BindingUtils.CreateProperty<GridControl, object>(Expression.Lambda<Func<GridControl, object>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_SelectedDataObject)), expressionArray19), null, new BindableProperty.BindingPropertyChangedDelegate<object>(null, OnSelectedDataObjectChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray20 = new ParameterExpression[] { expression };
            SortModeProperty = BindingUtils.CreateProperty<GridControl, GridSortMode>(Expression.Lambda<Func<GridControl, GridSortMode>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_SortMode)), expressionArray20), GridSortMode.Single, new BindableProperty.BindingPropertyChangedDelegate<GridSortMode>(null, OnSortModeChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray21 = new ParameterExpression[] { expression };
            IsReadOnlyProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_IsReadOnly)), expressionArray21), false, new BindableProperty.BindingPropertyChangedDelegate<bool>(null, OnReadOnlyChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray22 = new ParameterExpression[] { expression };
            AllowSortProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_AllowSort)), expressionArray22), true);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray23 = new ParameterExpression[] { expression };
            AllowEditRowsProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_AllowEditRows)), expressionArray23), true);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray24 = new ParameterExpression[] { expression };
            AllowDeleteRowsProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_AllowDeleteRows)), expressionArray24), true);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray25 = new ParameterExpression[] { expression };
            AllowResizeColumnsProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_AllowResizeColumns)), expressionArray25), true);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray26 = new ParameterExpression[] { expression };
            AllowGroupProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_AllowGroup)), expressionArray26), true);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray27 = new ParameterExpression[] { expression };
            AllowGroupCollapseProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_AllowGroupCollapse)), expressionArray27), true, new BindableProperty.BindingPropertyChangedDelegate<bool>(null, OnAllowGroupCollapseChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray28 = new ParameterExpression[] { expression };
            IsRowCellMenuEnabledProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_IsRowCellMenuEnabled)), expressionArray28), true);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray29 = new ParameterExpression[] { expression };
            IsTotalSummaryMenuEnabledProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_IsTotalSummaryMenuEnabled)), expressionArray29), true);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray30 = new ParameterExpression[] { expression };
            IsColumnMenuEnabledProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_IsColumnMenuEnabled)), expressionArray30), true);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray31 = new ParameterExpression[] { expression };
            IsGroupRowMenuEnabledProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_IsGroupRowMenuEnabled)), expressionArray31), true);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray32 = new ParameterExpression[] { expression };
            HighlightMenuTargetElementsProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_HighlightMenuTargetElements)), expressionArray32), true);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray33 = new ParameterExpression[] { expression };
            AutoGenerateColumnsModeProperty = BindingUtils.CreateProperty<GridControl, AutoGenerateColumnsMode>(Expression.Lambda<Func<GridControl, AutoGenerateColumnsMode>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_AutoGenerateColumnsMode)), expressionArray33), AutoGenerateColumnsMode.Auto);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray34 = new ParameterExpression[] { expression };
            IsPullToRefreshEnabledProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_IsPullToRefreshEnabled)), expressionArray34), false, new BindableProperty.BindingPropertyChangedDelegate<bool>(null, OnIsPullToRefreshEnabledPropertyChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray35 = new ParameterExpression[] { expression };
            PullToRefreshCommandProperty = BindingUtils.CreateProperty<GridControl, ICommand>(Expression.Lambda<Func<GridControl, ICommand>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_PullToRefreshCommand)), expressionArray35), null, new BindableProperty.BindingPropertyChangedDelegate<ICommand>(null, OnPullToRefreshCommandPropertyChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray36 = new ParameterExpression[] { expression };
            LoadMoreCommandProperty = BindingUtils.CreateProperty<GridControl, ICommand>(Expression.Lambda<Func<GridControl, ICommand>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_LoadMoreCommand)), expressionArray36), null, new BindableProperty.BindingPropertyChangedDelegate<ICommand>(null, OnLoadMoreCommandChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray37 = new ParameterExpression[] { expression };
            RowTapCommandProperty = BindingUtils.CreateProperty<GridControl, ICommand>(Expression.Lambda<Func<GridControl, ICommand>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_RowTapCommand)), expressionArray37), null);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray38 = new ParameterExpression[] { expression };
            IsLoadMoreEnabledProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_IsLoadMoreEnabled)), expressionArray38), false, new BindableProperty.BindingPropertyChangedDelegate<bool>(null, OnIsLoadMoreEnabledPropertyChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray39 = new ParameterExpression[] { expression };
            IsColumnChooserEnabledProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_IsColumnChooserEnabled)), expressionArray39), true, null);
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray40 = new ParameterExpression[] { expression };
            GroupsInitiallyExpandedProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_GroupsInitiallyExpanded)), expressionArray40), true, new BindableProperty.BindingPropertyChangedDelegate<bool>(null, OnGroupsInitiallyExpandedPropertyChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray41 = new ParameterExpression[] { expression };
            RowEditModeProperty = BindingUtils.CreateProperty<GridControl, RowEditMode>(Expression.Lambda<Func<GridControl, RowEditMode>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_RowEditMode)), expressionArray41), RowEditMode.Inplace, new BindableProperty.BindingPropertyChangedDelegate<RowEditMode>(null, OnRowEditModePropertyChanged));
            expression = Expression.Parameter((Type)typeof(GridControl), "o");
            ParameterExpression[] expressionArray42 = new ParameterExpression[] { expression };
            ColumnsAutoWidthProperty = BindingUtils.CreateProperty<GridControl, bool>(Expression.Lambda<Func<GridControl, bool>>((Expression)Expression.Property((Expression)expression, (MethodInfo)methodof(GridControl.get_ColumnsAutoWidth)), expressionArray42), true, new BindableProperty.BindingPropertyChangedDelegate<bool>(null, OnColumnsAutoWidthPropertyChanged));
            GridLocalizer.GetString(GridStringId.GroupNameNull);
        }

        public GridControl()
        {
            ThemeManager.AddThemeChangedHandler(this);
            base.set_BackgroundColor(this.CurrentTheme.GridControlCustomizer.BorderColor);
            ConsoleLog.IsEnabled = false;
            ConsoleLog.PrintLine("Grid Control constuctor", Array.Empty<object>());
            this.IsLoaded = false;
            this.InitServices();
            this.visibleColumns = new List<GridColumn>();
            this.dataController = new GridDataController();
            this.dataController.SelectionChanged += new EventHandler(this.OnDataControllerSelectionChanged);
            this.dataController.DataChanged += new GridDataControllerDataChangedEventHandler(this.OnDataControllerDataChanged);
            this.dataController.CalculateCustomSummary += new CustomSummaryEventHandler(this.OnDataControllerCalculateCustomSummary);
            this.dataController.GroupRowCollapsing += new RowAllowEventHandler(this.OnDataControllerGroupRowCollapsing);
            this.dataController.GroupRowCollapsed += new RowEventHandler(this.OnDataControllerGroupRowCollapsed);
            this.dataController.GroupRowExpanding += new RowAllowEventHandler(this.OnDataControllerGroupRowExpanding);
            this.dataController.GroupRowExpanded += new RowEventHandler(this.OnDataControllerGroupRowExpanded);
            this.dataController.AreGroupsInitiallyCollapsed = !this.GroupsInitiallyExpanded;
            this.SortingColumnManager = SortingColumnManager.Create(this.Columns, this.dataController, this.SortMode, this.IsLoaded);
            this.InitFiltering();
            this.InitNewItemRow();
            base.set_ColumnSpacing(0.0);
            base.set_RowSpacing(0.0);
            this.InitSummaries();
            this.InitConditionalFormattings();
            this.headerContentProvider = new HeaderContentProvider(this.dataController, this.ActualColumnHeadersHeight, this);
            HeadersContainer container1 = new HeadersContainer(this.headerContentProvider);
            container1.set_VerticalOptions(LayoutOptions.FillAndExpand);
            container1.set_HorizontalOptions(LayoutOptions.FillAndExpand);
            container1.set_IsClippedToBounds(true);
            this.headers = container1;
            this.refreshView = new PullToRefreshView();
            this.refreshView.set_IsClippedToBounds(true);
            this.InitTotalSummaryUI();
            this.columnChooser = new GridColumnChooser();
            this.columnChooser.GridControl = this;
            this.columnChooser.set_IsVisible(false);
            ExtendedScrollView view1 = new ExtendedScrollView();
            view1.set_VerticalOptions(LayoutOptions.FillAndExpand);
            view1.set_HorizontalOptions(LayoutOptions.FillAndExpand);
            this.scroller = view1;
            this.scroller.set_BackgroundColor(this.CurrentTheme.GridControlCustomizer.BackgroundColor);
            this.scroller.set_IsClippedToBounds(true);
            this.rowsLayout = new RowsLayout(ActionDispatcher.BackgroudDispatcher);
            this.rowsLayout.set_IsClippedToBounds(true);
            this.editRowContentProvider = new EditRowContentProvider(this.dataController, this.RowHeight, this);
            this.editRowContentProvider.SetVisibleColumns(this.VisibleColumns);
            this.editRowLayout = new EditRowLayout(this.editRowContentProvider, this);
            this.dataRowContentProvider = new RowContentProvider(this.dataController, this.RowHeight, this, this.conditionalFormatEngine, this);
            this.dataRowContentProvider.CustomizeCell += new CustomizeCellEventHandler(this.OnCustomizeCell);
            this.rowVirtualizer = new RowVirtualizer(ActionDispatcher.BackgroudDispatcher, this.dataRowContentProvider, this.rowsLayout, this, 5);
            this.rowVirtualizer.AllowGroupCollapse = this.AllowGroupCollapse;
            this.scrollContent = new ScrollContentLayout(this.scroller, this.rowsLayout, this.editRowLayout, this.rowVirtualizer, this.RowHeight);
            this.scroller.set_Content(this.scrollContent);
            this.InitFilteringUI();
            this.scroller.Delegate = this.scrollContent;
            this.ConfigureInternalGridStructure();
            this.adorner = new AdornerView();
            this.adorner.SetValue(Grid.RowSpanProperty, (int)base.get_RowDefinitions().get_Count());
            this.adorner.SetValue(Grid.ColumnSpanProperty, (int)base.get_ColumnDefinitions().get_Count());
            if (PlatformHelper.Platform != PlatformHelper.Platforms.WinPhone)
            {
                this.adorner.set_BackgroundColor(Color.FromRgba(0, 0, 0, 0));
            }
            this.adorner.InputTransparent = true;
            this.adorner.set_IsClippedToBounds(true);
            this.InitPopupMenu();
            this.InitCommandBindings();
            this.InitSwipeButtons();
            this.InitPullToRefreshView();
            this.InitLoadMore();
            this.InitRowEditing();
            this.InitGestureHandler();
            base.get_Children().Add(this.scroller, 0, 4);
            base.get_Children().Add(this.saveCancelEditingRowControl, 0, 1);
            base.get_Children().Add(this.headers, 0, 0);
            base.get_Children().Add(this.autoFilterPanelContainer, 0, 2);
            base.get_Children().Add(this.newItemRowContainer, 0, 3);
            base.get_Children().Add(this.totalSummary, 0, 5);
            base.get_Children().Add(this.filterPanelContainer, 0, 6);
            base.get_Children().Add(this.adorner, 0, 0);
            this.adorner.SetValue(Grid.RowSpanProperty, (int)base.get_RowDefinitions().get_Count());
            this.adorner.SetValue(Grid.ColumnSpanProperty, (int)base.get_ColumnDefinitions().get_Count());
            this.adorner.get_Children().Add(this.columnChooser);
            this.SelectedRowHandle = -2_147_483_648;
            base.set_IsClippedToBounds(true);
            base.set_Padding(this.CurrentTheme.GridControlCustomizer.BorderThickness);
        }

        private void ActivateNewItemRow()
        {
            if (!this.IsReadOnly)
            {
                View view = null;
                IEditableRowData editableRowData = this.dataController.AddNewRow();
                if (editableRowData != null)
                {
                    this.RaiseInitNewRow(editableRowData);
                    if (this.RowEditMode != RowEditMode.Inplace)
                    {
                        view = this.GetContentForEditForm(editableRowData, ColumnsFilterCreterias.VisibleColumns, true);
                    }
                    else
                    {
                        this.editingValuesContainer = this.GetEditingValuesContainer(editableRowData, true);
                        this.newItemRowContentProvider.ValuesContainer = this.editingValuesContainer;
                        this.SubscribeEditFormValuesChangeEvent();
                        this.newItemRowContentProvider.SubscribeDataControllerEvents();
                    }
                    this.editingValuesContainer.IsNewRow = true;
                    switch (this.RowEditMode)
                    {
                        case RowEditMode.Inplace:
                            this.newItemRowContentProvider.ActivateRow();
                            this.PrepareGridForInplaceRowEditing();
                            return;

                        case RowEditMode.Popup:
                            this.OpenFormEditorPopoup(view);
                            return;

                        case RowEditMode.GridArea:
                            this.OpenFormEditorFullscreen(view);
                            return;

                        case RowEditMode.ScreenArea:
                            this.OpenFormEditorFullscreenWithNavBar(view);
                            return;
                    }
                }
            }
        }

        internal PopupMenuItem AddButtonItem(GridPopupMenuView menu, GridCommandId commandId) =>
            this.AddItemCore(menu, commandId, false);

        private void AddCancelButton()
        {
            if ((PlatformHelper.Platform == PlatformHelper.Platforms.iOS) && (Device.get_Idiom() == 1))
            {
                this.AddButtonItem(this.popupMenu, GridCommandId.CloseMenu);
            }
        }

        internal PopupMenuItem AddCheckItem(GridPopupMenuView menu, GridCommandId commandId) =>
            this.AddItemCore(menu, commandId, true);

        private PopupMenuItem AddItemCore(GridPopupMenuView menu, GridCommandId commandId, bool isCheckable)
        {
            Command command = this.CreateCommand(commandId);
            if (command == null)
            {
                return null;
            }
            GridPopupMenuItem item = new GridPopupMenuItem
            {
                Caption = command.MenuCaption,
                CommandId = commandId,
                IsCheckable = isCheckable
            };
            menu.Items.Add(item);
            return item;
        }

        private void AddServices()
        {
            this.ServiceContainer.AddService<IGridCommandFactoryService>(new GridCommandFactoryService(this));
            this.ServiceContainer.AddService<IGestureHandlerService>(new GridGestureHandlerService(this));
        }

        private void AppendAutoGeneratedColumns()
        {
            if (this.autoGeneratedColumns == null)
            {
                this.autoGeneratedColumns = this.DataController.GenerateColumns();
                if (this.autoGeneratedColumns == null)
                {
                    this.autoGeneratedColumns = (IList<GridColumn>)new List<GridColumn>();
                }
                else
                {
                    int count = this.autoGeneratedColumns.Count;
                    for (int i = 0; i < count; i++)
                    {
                        GridColumn gridColumn = this.autoGeneratedColumns[i];
                        if ((this.Columns[gridColumn.FieldName] == null) && this.RaiseAutoGeneratingColumn(gridColumn))
                        {
                            this.Columns.Add(gridColumn);
                        }
                    }
                }
            }
        }

        private void ApplyEditorResults()
        {
            try
            {
                IEditableRowData data;
                int rowHandle = this.editingValuesContainer.RowData.RowHandle;
                if (!(this.editingValuesContainer.RowData is IEditableRowData))
                {
                    data = this.dataController.BeginEditRow(rowHandle);
                    if (data == null)
                    {
                        return;
                    }
                }
                RowContainer rowByRowHandle = this.rowVirtualizer.GetRowByRowHandle(rowHandle);
                foreach (string str in this.editingValuesContainer.Values.get_Keys())
                {
                    if (!this.Columns.GetColumnByFieldName(str).ActualIsReadOnly)
                    {
                        object obj2 = this.editingValuesContainer.Values[str];
                        try
                        {
                            data.SetFieldValue(str, obj2);
                            if (rowByRowHandle == null)
                            {
                                continue;
                            }
                            CellView cellByFieldName = rowByRowHandle.GetCellByFieldName(str);
                            if (cellByFieldName == null)
                            {
                                continue;
                            }
                            if (!(cellByFieldName.get_BindingContext() is CellData))
                            {
                                continue;
                            }
                            (cellByFieldName.get_BindingContext() as CellData).Value = obj2;
                        }
                        catch
                        {
                        }
                    }
                }
                if (!this.editingValuesContainer.IsNewRow)
                {
                    this.dataController.EndEditRow(data);
                }
                else
                {
                    this.dataController.EndEditNewRow(data);
                    ((IRowContentProviderDelegate)this.rowVirtualizer).ContentInvalidated(true, false);
                }
            }
            catch
            {
            }
        }

        private void AttachDataSource(object newDataSource)
        {
            this.UnsubscribeDataSourceEvents();
            if (newDataSource == null)
            {
                this.dataController.DataSource = null;
            }
            else
            {
                IGridDataSource dataSource = this.TryCreateDataSource(newDataSource);
                if (this.HasUnboundFields())
                {
                    UnboundColumnsDataSource source2 = new UnboundColumnsDataSource(dataSource);
                    source2.UpdateUnboundFields(this.Columns);
                    this.SubscribeDataSourceEventsCore(source2);
                    dataSource = source2;
                }
                this.dataController.DataSource = dataSource;
            }
            this.SubscribeDataSourceEvents();
        }

        private void AttachUnboundDataSource()
        {
            if ((this.dataController.DataSource != null) && this.HasUnboundFields())
            {
                if (!(this.dataController.DataSource is UnboundColumnsDataSource))
                {
                    this.UnsubscribeDataSourceEvents();
                    UnboundColumnsDataSource dataSource = new UnboundColumnsDataSource(this.dataController.DataSource);
                    dataSource.UpdateUnboundFields(this.Columns);
                    this.SubscribeDataSourceEventsCore(dataSource);
                    this.dataController.DataSource = dataSource;
                    this.SubscribeDataSourceEvents();
                }
                else
                {
                    this.UpdateUnboundFields();
                }
            }
        }

        private IDataAwareExportOptions CalculateSourceOptions(IDataAwareExportOptions options) =>
            (!(options is XlsxExportOptions) ? (!(options is XlsExportOptions) ? (!(options is CsvExportOptions) ? null : this.OptionsExportCsv) : this.OptionsExportXls) : this.OptionsExportXlsx);

        private int CalculateVisibleItemCount()
        {
            int num = 0;
            foreach (PopupMenuItem item in this.popupMenu.Items)
            {
                if (this.IsItemVisible(item))
                {
                    num++;
                }
            }
            return num;
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            this.UnsubscribeDataControllerEvents();
            this.CloseEditor(false);
        }

        internal virtual bool CanGroupByColumn(GridColumn groupingColumn) =>
            ((this.VisibleColumns.Count != 1) || (this.VisibleColumns[0] != groupingColumn));

        private bool CheckActivedSwipeButtons()
        {
            if (this.currentDraggedRowHandle == -2_147_483_648)
            {
                return true;
            }
            this.FinishCurrentDragging();
            return false;
        }

        private void CheckDevExpressPlatformRegistered()
        {
            if (!this.isDevExpressNotRegistered.HasValue && this.isRendererShouldBeLoaded)
            {
                if (PlatformHelper.IsDevExpressPlatformInitialized())
                {
                    this.isDevExpressNotRegistered = false;
                }
                else
                {
                    this.isDevExpressNotRegistered = true;
                    Device.BeginInvokeOnMainThread(delegate {
                        PlatformHelper.AppendWarningMessage(this);
                    });
                }
            }
        }

        internal bool CheckExecuteTapEventPossibility(GridHitInfo hitInfo)
        {
            if (this.IsEditingRowOpened)
            {
                if (hitInfo.GridElementType == GridElementType.SaveCancelButtonsPanel)
                {
                    this.SendTapToSaveCancelButtonsControl(hitInfo);
                }
                return false;
            }
            if (this.columnChooser.get_IsVisible())
            {
                return false;
            }
            if (this.IsPopoverEditorOpened)
            {
                return false;
            }
            if (this.CheckSwipeButtonForHitInfo(hitInfo))
            {
                return false;
            }
            this.TapInsideGridControl(hitInfo);
            return (hitInfo.GridElementType != GridElementType.Unknown);
        }

        private void CheckGroupingOnVisibleColumns()
        {
            if (this.visibleColumns.Count == 0)
            {
                foreach (GridColumn column in this.Columns)
                {
                    if (column.IsGrouped)
                    {
                        column.IsGrouped = false;
                    }
                }
            }
            else if ((this.visibleColumns.Count == 1) && this.visibleColumns[0].IsGrouped)
            {
                this.visibleColumns[0].IsGrouped = false;
            }
        }

        private bool CheckHorizontalScrollingPossibility(float distance)
        {
            if (!this.ColumnsAutoWidth)
            {
                if (this.currentDraggedRowHandle != -2_147_483_648)
                {
                    return false;
                }
                if ((distance <= 0f) || (this.HorizontalScrollOffset > 0.0))
                {
                    return ((distance >= 0f) || ((this.HorizontalScrollOffset + base.get_Width()) < this.totalColumnsWidth));
                }
            }
            return false;
        }

        private void CheckNewItemRowHandle(int oldValue, int newValue)
        {
            if (newValue == -2_147_483_647)
            {
                this.ActivateNewItemRow();
            }
        }

        private bool CheckSwipeButtonForHitInfo(GridHitInfo hitInfo) =>
            ((hitInfo != null) && ((hitInfo.CellInfo != null) && (hitInfo.CellInfo.CellView is SwipeButton)));

        public void ClearFilter()
        {
            this.filter.ClearFilter();
        }

        public void ClearGrouping()
        {
            using (IEnumerator<GridColumn> enumerator = this.Columns.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    enumerator.Current.IsGrouped = false;
                }
            }
        }

        public void ClearSorting()
        {
            using (IEnumerator<GridColumn> enumerator = this.Columns.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    enumerator.Current.SortOrder = ColumnSortOrder.None;
                }
            }
        }

        private void CloseActivatedSwipeButtons(GridHitInfo hitInfo)
        {
            if (!this.CheckSwipeButtonForHitInfo(hitInfo) && (this.currentDraggedRowHandle != -2_147_483_648))
            {
                this.FinishCurrentDragging();
            }
        }

        private void CloseEditingRow()
        {
            if (this.openedRowHandle != -2_147_483_648)
            {
                this.editRowLayout.CloseEditRow();
                this.underEditorRowContainer = null;
                this.ExitFromInplaceEditMode();
                this.UpdateEditRowLayoutInputTransparent(true);
            }
        }

        public void CloseEditor(bool applyChanges)
        {
            this.CloseEditorOfConcreteType(applyChanges, this.RowEditMode);
        }

        private void CloseEditorOfConcreteType(bool applyChanges, RowEditMode rowEditMode)
        {
            this.UnsubscribeEditFormValuesChangeEvent();
            int sourceRowIndex = this.GetSourceRowIndex(this.openedRowHandle);
            if (applyChanges)
            {
                this.ApplyEditorResults();
            }
            switch (rowEditMode)
            {
                case RowEditMode.Inplace:
                    this.CloseInplaceEditor();
                    break;

                case RowEditMode.Popup:
                    this.HidePopupEditor();
                    break;

                case RowEditMode.GridArea:
                    this.HidePopupEditor();
                    break;

                case RowEditMode.ScreenArea:
                    this.HideFullscreenEditor();
                    break;

                default:
                    break;
            }
            if ((this.formEditorDialogForm != null) && this.Adorner.get_Children().Contains(this.formEditorDialogForm))
            {
                this.Adorner.get_Children().Remove(this.formEditorDialogForm);
            }
            this.formEditorDialogForm = null;
            int rowHandle = this.DataController.GetRowHandle(sourceRowIndex);
            this.openedRowHandle = -2_147_483_648;
            if (this.editingValuesContainer != null)
            {
                if (this.editingValuesContainer.IsNewRow)
                {
                    this.SelectedRowHandle = (this.dataController.RowCount > 1) ? 0 : -2_147_483_648;
                }
                else
                {
                    this.RaiseEndRowEdit(rowHandle, applyChanges ? EditingRowAction.Apply : EditingRowAction.Cancel);
                }
            }
        }

        private void CloseInplaceEditor()
        {
            if (this.openedRowHandle != -2_147_483_648)
            {
                this.CloseEditingRow();
            }
            if (this.newItemRowContentProvider.IsActive)
            {
                this.CloseNewItemRow();
            }
        }

        internal void CloseNewItemRow()
        {
            this.newItemRowContentProvider.CloseRow();
            this.ExitFromInplaceEditMode();
        }

        public void CollapseAllGroups()
        {
            this.FinishCurrentDragging();
            this.DataController.CollapseAllGroups(true);
            this.rowVirtualizer.Invalidate(false, true);
        }

        public void CollapseGroupRow(int rowHandle)
        {
            this.FinishCurrentDragging();
            this.DataController.CollapseGroup(rowHandle, true);
            this.rowVirtualizer.Invalidate(false, true);
        }

        private bool CompareDoubleValues(double v1, double v2) =>
            (!double.IsNaN(v1) && (!double.IsNaN(v2) && (!double.IsInfinity(v1) && (!double.IsInfinity(v2) && (Math.Abs((double)(v1 - v2)) < 0.001)))));

        private void ConfigureInternalGridStructure()
        {
            RowDefinition definition1 = new RowDefinition();
            definition1.set_Height(new GridLength(this.ActualColumnHeadersHeight, 0));
            base.get_RowDefinitions().Add(definition1);
            RowDefinition definition2 = new RowDefinition();
            definition2.set_Height(new GridLength(this.GetActualSaveCancelRowHeight(), 0));
            base.get_RowDefinitions().Add(definition2);
            RowDefinition definition3 = new RowDefinition();
            definition3.set_Height(new GridLength(this.GetAutoFilterPanelHeight(), 0));
            base.get_RowDefinitions().Add(definition3);
            RowDefinition definition4 = new RowDefinition();
            definition4.set_Height(new GridLength(this.GetActualNewItemRowHeight(), 0));
            base.get_RowDefinitions().Add(definition4);
            RowDefinition definition5 = new RowDefinition();
            definition5.set_Height(new GridLength(1.0, 1));
            base.get_RowDefinitions().Add(definition5);
            RowDefinition definition6 = new RowDefinition();
            definition6.set_Height(new GridLength(this.ActualTotalSummaryHeight, 0));
            base.get_RowDefinitions().Add(definition6);
            RowDefinition definition7 = new RowDefinition();
            definition7.set_Height(new GridLength(this.ActualFilterPanelHeight, 0));
            base.get_RowDefinitions().Add(definition7);
            ColumnDefinition definition8 = new ColumnDefinition();
            definition8.set_Width(new GridLength(1.0, 1));
            base.get_ColumnDefinitions().Add(definition8);
        }

        protected virtual GridColumnCollection CreateColumns() =>
            new GridColumnCollection();

        private Command CreateCommand(PopupMenuItem item)
        {
            GridPopupMenuItem item2 = item as GridPopupMenuItem;
            return ((item2 != null) ? this.CreateCommand(item2.CommandId) : null);
        }

        internal Command CreateCommand(GridCommandId commandId)
        {
            IGridCommandFactoryService service = this.GetService(typeof(IGridCommandFactoryService)) as IGridCommandFactoryService;
            return service?.CreateCommand(commandId);
        }

        private BoxView CreateContextAdorner()
        {
            BoxView item = new BoxView();
            item.set_IsVisible(false);
            item.set_Color(this.CurrentTheme.GridControlCustomizer.PopupMenuHighlightColor);
            this.Adorner.get_Children().Add(item);
            this.menuContext.Add(item);
            return item;
        }

        private CsvExportOptions CreateCsvExportOptions()
        {
            CsvExportOptions options = new CsvExportOptions(",", Encoding.get_UTF8());
            if (this.OptionsExportCsv != null)
            {
                options.Assign(this.OptionsExportCsv);
            }
            return options;
        }

        private IDataAwareExportOptions CreateExcelExportOptions(ExportTarget format)
        {
            switch (format)
            {
                case ExportTarget.Xls:
                    return this.CreateXlsExportOptions();

                case ExportTarget.Xlsx:
                    return this.CreateXlsxExportOptions();

                case ExportTarget.Csv:
                    return this.CreateCsvExportOptions();
            }
            return null;
        }

        private void CreateFormEditorDialog()
        {
            this.formEditorDialogForm = new DialogForm();
            this.formEditorDialogForm.OnCancelPressed += new EventHandler(this.OnCancelFormEditorDialog);
            this.formEditorDialogForm.OnDonePressed += new EventHandler(this.OnOKFormEditorDialog);
            this.LocalizeFormEditorDialog();
        }

        private XlsExportOptions CreateXlsExportOptions()
        {
            XlsExportOptions options = new XlsExportOptions();
            if (this.OptionsExportXls != null)
            {
                options.Assign(this.OptionsExportXls);
            }
            return options;
        }

        private XlsxExportOptions CreateXlsxExportOptions()
        {
            XlsxExportOptions options = new XlsxExportOptions();
            if (this.OptionsExportXlsx != null)
            {
                options.Assign(this.OptionsExportXlsx);
            }
            return options;
        }

        public void DeleteRow(int rowHandle)
        {
            this.DataController.DeleteRow(rowHandle);
        }

        bool IGestureRecognizerDelegate.GestureRecognized(Gesture gesture, GestureData data)
        {
            IGestureHandlerService service = this.GetService<IGestureHandlerService>();
            return ((service != null) && service.HandleGesture(gesture, data));
        }

        void IAndroidThemeChanger.RefreshTheme()
        {
            this.UpdateTheme();
        }

        string ICustomCellTextProvider.Customize(object value, string formattedText, int rowHandle, string fieldName)
        {
            if (!((ICustomCellTextProvider)this).CanCustomize)
            {
                return formattedText;
            }
            CustomizeCellDisplayTextEventArgs args = new CustomizeCellDisplayTextEventArgs
            {
                DisplayText = formattedText,
                Index = new CellIndex(rowHandle, fieldName),
                Value = value
            };
            this.RaiseCustomizeCellDisplayText(args);
            return args.DisplayText;
        }

        void IFormatConditionCollectionOwner.OnFormatConditionCollectionChanged(FormatConditionChangeType changeType)
        {
            this.ResetConditionalFormatEngine();
        }

        void IFormatConditionCollectionOwner.SyncFormatCondtitionCollectionWithDetails(NotifyCollectionChangedEventArgs e)
        {
        }

        void IFormatConditionCollectionOwner.SyncFormatCondtitionPropertyWithDetails(FormatConditionBase item, BindableProperty property, object oldValue, object newValue)
        {
        }

        GridHitInfo IHitTestAccess.HitTest(Point location) =>
            (!this.scroller.get_Bounds().Contains(location) ? new GridHitInfo(this, location, -2_147_483_648, null, null, GridElementType.Unknown) : ((IHitTestAccess)this.rowVirtualizer).HitTest(this.ProjectToView(location, this.scroller)));

        void IThemeChangingHandler.OnThemeChanged()
        {
            this.UpdateTheme();
        }

        Command ICommandAwareControl<GridCommandId>.CreateCommand(GridCommandId commandId) =>
            this.CreateCommand(commandId);

        void ICommandAwareControl<GridCommandId>.Focus()
        {
        }

        bool ICommandAwareControl<GridCommandId>.HandleException(Exception e) =>
            false;

        bool IXtraSerializableLayoutEx.AllowProperty(OptionsLayoutBase options, string propertyName, int id) =>
            true;

        void IXtraSerializableLayoutEx.ResetProperties(OptionsLayoutBase options)
        {
            this.ResetProperties(options);
        }

        private void DisableTouchInActiveGridPanels()
        {
            this.autoFilterPanelContainer.DisablePanelForTouch();
            this.filterPanelContainer.DisablePanelForTouch();
        }

        public void Dispose()
        {
            if (!this.isDisposed)
            {
                this.UnregisterGestureHandlers();
                this.isDisposed = true;
            }
        }

        private bool DoHorizontalScrolling(float distance)
        {
            if (!this.CheckHorizontalScrollingPossibility(distance))
            {
                return false;
            }
            this.SetHorizontalScrollOffsetCore(this.HorizontalScrollOffset - distance);
            return true;
        }

        internal void DragRow(int rowHandle, float distance, GestureState state)
        {
            if (!this.DoHorizontalScrolling(distance))
            {
                if (state == GestureState.Begin)
                {
                    this.PrepareGridForDragging(rowHandle);
                }
                if (this.draggedRow != null)
                {
                    if (state == GestureState.End)
                    {
                        this.StopRowDragging();
                    }
                    else
                    {
                        this.DragRowCore((double)distance);
                    }
                }
            }
        }

        private void DragRowCore(double distance)
        {
            double num = this.sumDistance + distance;
            this.DragDirection = (num > 0.0) ? RowDragDirection.Right : RowDragDirection.Left;
            double num2 = distance;
            if (Math.Abs(num) > this.SwipeButtonsStategy.MaxButtonsWidth)
            {
                num2 = (num2 / Math.Abs(num2)) * (this.SwipeButtonsStategy.MaxButtonsWidth - Math.Abs(this.sumDistance));
            }
            if (num2 != 0.0)
            {
                this.sumDistance += num2;
                this.draggedRow.MoveRowToDistance(num2);
                this.SwipeButtonsStategy.LayoutButtons(Math.Abs(this.sumDistance), this.draggedRow.get_Bounds().get_Height(), base.get_Width());
            }
        }

        private void EnableTouchInActiveGridPanels()
        {
            this.autoFilterPanelContainer.EnablePanelForTouch();
            this.filterPanelContainer.EnablePanelForTouch();
        }

        internal bool ExecuteCommand(GridCommandId commandId)
        {
            Command command = this.CreateCommand(commandId);
            if (command == null)
            {
                return false;
            }
            command.Execute();
            return true;
        }

        private void ExitFromInplaceEditMode()
        {
            this.UnsubscribeEditFormValuesChangeEvent();
            this.EnableTouchInActiveGridPanels();
            this.IsEditingRowOpened = false;
            this.UpdateSaveCancelRowHeight();
        }

        public void ExpandAllGroups()
        {
            this.FinishCurrentDragging();
            this.DataController.CollapseAllGroups(false);
            this.rowVirtualizer.Invalidate(false, true);
        }

        public void ExpandGroupRow(int rowHandle)
        {
            this.FinishCurrentDragging();
            this.DataController.CollapseGroup(rowHandle, false);
            this.rowVirtualizer.Invalidate(false, true);
        }

        public bool ExportToExcel(Stream stream, ExportTarget format)
        {
            if (!this.IsExcelFormat(format))
            {
                return false;
            }
            IDataAwareExportOptions options = this.CreateExcelExportOptions(format);
            if (options == null)
            {
                return false;
            }
            options.InitDefaults();
            OptionsEventRouter router1 = new OptionsEventRouter(options, this.CalculateSourceOptions(options));
            router1.Attach();
            new GridViewExcelExporter<ColumnImplementer, DataRowImplementer>(new GridViewImplementer<ColumnImplementer, DataRowImplementer>(this, format), options).Export(stream);
            router1.Detach();
            return true;
        }

        internal IHitTestAccess FindHitTestAccessor(View view)
        {
            IHitTestAccess access = view as IHitTestAccess;
            if (access != null)
            {
                return access;
            }
            if ((view is RowsLayout) || (view is ExtendedScrollView))
            {
                return this.scrollHitTestAccessor;
            }
            return null;
        }

        public int FindRow(Predicate<IRowData> predicate) =>
            this.DataController.FindRow(predicate);

        protected int FindRowByDataObject(object dataObject)
        {
            Predicate<IRowData> predicate = delegate (IRowData row) {
                return object.Equals(row.DataObject, dataObject);
            };
            return this.FindRow(predicate);
        }

        public int FindRowByValue(GridColumn column, object value) =>
            ((column != null) ? this.FindRowByValue(column.FieldName, value) : -2_147_483_648);

        public int FindRowByValue(string fieldName, object value)
        {
            Predicate<IRowData> predicate = delegate (IRowData row) {
                return object.Equals(row.GetFieldValue(fieldName), value);
            };
            return this.FindRow(predicate);
        }

        private void FinishCurrentDragging()
        {
            this.sumDistance = 0.0;
            this.currentDraggedRowHandle = -2_147_483_648;
            if (this.draggedRow != null)
            {
                this.draggedRow.MoveRowToStart();
                this.draggedRow.SwipeButtons.Clear();
                this.draggedRow = null;
            }
            this.DragDirection = RowDragDirection.None;
        }

        private double GetActualNewItemRowHeight()
        {
            if (!this.NewItemRowVisibility || this.IsReadOnly)
            {
                return 0.0;
            }
            return this.RowHeight;
        }

        private double GetActualSaveCancelRowHeight() =>
            (this.IsEditingRowOpened ? 50.0 : 0.0);

        private double GetAutoFilterPanelHeight() =>
            (!this.AutoFilterPanelVisibility ? 0.0 : this.AutoFilterPanelHeight);

        public string GetCellDisplayText(int rowHandle, GridColumn column)
        {
            if (column == null)
            {
                return string.Empty;
            }
            object cellValue = this.GetCellValue(rowHandle, column);
            return DisplayTextHelper.Instance.GetDisplayText(column.DisplayFormat, cellValue);
        }

        public string GetCellDisplayText(int rowHandle, string fieldName) =>
            this.GetCellDisplayText(rowHandle, this.Columns[fieldName]);

        public object GetCellValue(int rowHandle, GridColumn column) =>
            this.GetCellValue(rowHandle, column.FieldName);

        public object GetCellValue(int rowHandle, string fieldName) =>
            this.DataController.GetRow(rowHandle, null)?.GetFieldValue(fieldName);

        private View GetContentForEditForm(IRowData rowData, Predicate<GridColumn> criteria, bool isNewRow)
        {
            View view = null;
            IReadOnlyList<GridColumn> editableColumns = this.GetEditableColumns(criteria);
            this.editingValuesContainer = this.GetEditingValuesContainer(rowData, isNewRow);
            this.SubscribeEditFormValuesChangeEvent();
            if (this.EditFormContent != null)
            {
                view = this.EditFormContent.CreateContent() as View;
            }
            if (view == null)
            {
                view = new DefaultEditFormContent(this, editableColumns, this.editingValuesContainer);
            }
            view.set_BindingContext(this.editingValuesContainer);
            return view;
        }

        private IReadOnlyList<GridColumn> GetEditableColumns(Predicate<GridColumn> criteria)
        {
            List<GridColumn> list = new List<GridColumn>();
            foreach (GridColumn column in this.Columns)
            {
                if (criteria(column))
                {
                    list.Add(column);
                }
            }
            return (IReadOnlyList<GridColumn>)list;
        }

        private EditValuesContainer GetEditingValuesContainer(IRowData rowData, bool isNewRow)
        {
            EditValuesContainer container = new EditValuesContainer
            {
                IsNewRow = isNewRow
            };
            foreach (GridColumn column in this.Columns)
            {
                if (!(column is TemplateColumn))
                {
                    container.Values[column.FieldName] = rowData.GetFieldValue(column.FieldName);
                }
            }
            container.RowData = rowData;
            return container;
        }

        internal GridPopupMenuView GetEmptyPopupMenu()
        {
            this.popupMenu.Items.Clear();
            return this.popupMenu;
        }

        private FieldInfo GetField(TypeInfo type, string name)
        {
            FieldInfo declaredField = type.GetDeclaredField(name);
            if (declaredField != null)
            {
                return declaredField;
            }
            Type type2 = type.get_BaseType();
            if (((type2 == null) || object.ReferenceEquals(type2, Type.Missing)) || (type2 == typeof(BindableObject)))
            {
                return null;
            }
            return this.GetField(IntrospectionExtensions.GetTypeInfo(type2), name);
        }

        public IGroupInfo GetGroupInfo(int rowHandle) =>
            this.DataController.GetGroupInfo(rowHandle);

        public object GetGroupRowValue(int rowHandle) =>
            this.DataController.GetGroupValue(rowHandle);

        private Rectangle GetRealRowBoundsInGridControl(RowContainerBase rowContainer) =>
            this.GetRealRowBoundsInGridControlCore(rowContainer, new Rectangle(0.0, 0.0, rowContainer.get_Bounds().get_Width(), rowContainer.get_Bounds().get_Height()));

        private Rectangle GetRealRowBoundsInGridControlCore(View view, Rectangle currentRect)
        {
            if (view == null)
            {
                return currentRect;
            }
            Rectangle rectangle = new Rectangle(currentRect.get_X() + view.get_Bounds().get_X(), currentRect.get_Y() + view.get_Bounds().get_Y(), currentRect.get_Width(), currentRect.get_Height());
            if (view.get_Parent() is GridControl)
            {
                return rectangle;
            }
            ExtendedScrollView view2 = view.get_Parent() as ExtendedScrollView;
            if (view2 == null)
            {
                return this.GetRealRowBoundsInGridControlCore(view.get_Parent() as View, rectangle);
            }
            Rectangle rectangle3 = new Rectangle(rectangle.get_X() - view2.Offset.get_X(), rectangle.get_Y() - view2.Offset.get_Y(), rectangle.get_Width(), rectangle.get_Height());
            return this.GetRealRowBoundsInGridControlCore(view.get_Parent() as View, rectangle3);
        }

        public IRowData GetRow(int rowHandle) =>
            this.DataController.GetRow(rowHandle, null);

        public int GetRowHandle(int sourceRowIndex) =>
            this.dataController.GetRowHandle(sourceRowIndex);

        public T GetService<T>() where T : class
        {
            T service = this.ServiceContainer.GetService(typeof(T)) as T;
            if (service == null)
            {
                service = GlobalServices.Instance.GetService<T>();
            }
            return service;
        }

        public object GetService(Type serviceType)
        {
            object service = this.ServiceContainer.GetService(serviceType);
            if (service == null)
            {
                service = GlobalServices.Instance.GetService(serviceType);
            }
            return service;
        }

        public int GetSourceRowIndex(int rowHandle) =>
            this.dataController.GetSourceRowIndex(rowHandle);

        internal IList<GridColumn> GetVisibleColumns() =>
            ((IList<GridColumn>)this.visibleColumns);

        public void GroupBy(GridColumn column)
        {
            if ((column != null) && this.CanGroupByColumn(column))
            {
                column.IsGrouped = true;
            }
        }

        public void GroupBy(string fieldName)
        {
            this.GroupBy(this.Columns[fieldName]);
        }

        private bool HasUnboundFields()
        {
            using (IEnumerator<GridColumn> enumerator = this.Columns.GetEnumerator())
            {
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    if (enumerator.Current.IsUnbound)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void HideColumnChooser()
        {
            this.HideColumnChooser(false);
        }

        public void HideColumnChooser(bool ApplyChanges)
        {
            this.columnChooser.Hide(ApplyChanges);
        }

        private void HideContextAdorners()
        {
            if (this.HighlightMenuTargetElements && this.isPopupMenuOpened)
            {
                this.HideContextAdornersCore();
                this.isPopupMenuOpened = false;
            }
        }

        private void HideContextAdornersCore()
        {
            using (List<BoxView>.Enumerator enumerator = this.menuContext.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    enumerator.Current.set_IsVisible(false);
                }
            }
        }

        private void HideFullscreenEditor()
        {
            if ((this.formEditorContentPage != null) && this.formEditorContentPage.get_IsVisible())
            {
                this.formEditorContentPage.get_Content().set_IsVisible(false);
                base.get_Navigation().PopModalAsync(false);
            }
            this.formEditorContentPage = null;
        }

        private void HidePopupEditor()
        {
            if ((this.formEditorDialogForm != null) && this.formEditorDialogForm.get_IsVisible())
            {
                this.formEditorDialogForm.Hide();
                this.Adorner.SetInputTransparent(true);
            }
        }

        private void InitCommandBindings()
        {
            this.commandBindings = new GestureCommandBindings();
            this.commandBindings.DataRowSelect = this.commandBindings.Create(GridElementType.Row, Gesture.SingleTap, GridCommandId.DataRowSelect);
            this.commandBindings.DataRowShowPopupMenu = this.commandBindings.Create(GridElementType.Row, Gesture.LongTap, GridCommandId.DataRowShowPopupMenu);
            this.commandBindings.DataRowInplaceEditorActivate = this.commandBindings.Create(GridElementType.Row, Gesture.DoubleTap, GridCommandId.DataRowActivateInplaceEditor);
            this.commandBindings.ColumnToggleSort = this.commandBindings.Create(GridElementType.Header, Gesture.SingleTap, GridCommandId.ColumnToggleSort);
            this.commandBindings.ColumnShowPopupMenu = this.commandBindings.Create(GridElementType.Header, Gesture.LongTap, GridCommandId.ColumnShowPopupMenu);
            this.commandBindings.GroupRowToggleCollapsed = this.commandBindings.Create(GridElementType.GroupRow, Gesture.SingleTap, GridCommandId.GroupRowToggleCollapsed);
            this.commandBindings.GroupRowShowPopupMenu = this.commandBindings.Create(GridElementType.GroupRow, Gesture.LongTap, GridCommandId.GroupRowShowPopupMenu);
            this.commandBindings.TotalSummaryShowPopupMenu = this.commandBindings.Create(GridElementType.TotalSummary, Gesture.LongTap, GridCommandId.TotalSummaryShowPopupMenu);
            this.commandBindings.PopulateList();
        }

        private void InitConditionalFormattings()
        {
            this.conditionalFormatEngine = new ConditionalFormattingEngine(this);
            this.FormatConditions = new FormatConditionCollection(this);
            this.FormatConditions.add_CollectionChanged(new NotifyCollectionChangedEventHandler(this.OnFormatConditionsCollectionChanged));
        }

        private void InitFiltering()
        {
            this.filter = new GridFilter(this.Columns);
            this.SubscribeFilterEvents();
        }

        private void InitFilteringUI()
        {
            this.filterPanelContentProvider = new FilterPanelContentProvider(this.ActualFilterPanelHeight, this.filter, this.Columns);
            this.filterPanelContainer = new FilterPanelContainer(this.filterPanelContentProvider);
            this.filterPanelContainer.set_IsClippedToBounds(true);
            this.autoFilterPanelContentProvider = new AutoFilterContentProvider(this.dataController, this.GetAutoFilterPanelHeight(), this);
            this.autoFilterPanelContainer = new AutoFilterPanelContainer(this.autoFilterPanelContentProvider);
            this.autoFilterPanelContainer.set_IsClippedToBounds(true);
        }

        private void InitGestureHandler()
        {
            this.gestureHandler = new GridGestureHandler(this);
            this.scrollHitTestAccessor = new ScrollHitTestAccessor(this.rowVirtualizer, this.headers);
            this.RegisterGestureHandlers();
        }

        private void InitLoadMore()
        {
            this.scrollContent.LoadMore.LoadMoreStart += new EventHandler(this.OnLoadMore);
        }

        private void InitNewItemRow()
        {
            this.newItemRowContentProvider = new NewItemRowContentProvider(this.dataController, this.GetActualNewItemRowHeight(), this);
            this.newItemRowContainer = new NewItemRowContainer(this.newItemRowContentProvider);
            this.newItemRowContainer.set_IsClippedToBounds(true);
        }

        private void InitPopupMenu()
        {
            this.popupMenu = new GridPopupMenuView();
            this.Adorner.get_Children().Add(this.popupMenu);
        }

        private void InitPullToRefreshView()
        {
            if (PlatformHelper.Platform == PlatformHelper.Platforms.Android)
            {
                this.refreshView.PullToRefresh += new EventHandler(this.OnPullToRefresh);
            }
            else if (PlatformHelper.Platform == PlatformHelper.Platforms.iOS)
            {
                this.scroller.RefreshStarted += new EventHandler(this.OnPullToRefresh);
            }
        }

        private void InitRowEditing()
        {
            this.IsEditingRowOpened = false;
            this.saveCancelEditingRowControl = new SaveCancelEditingRowControl();
            this.saveCancelEditingRowControl.SaveClicked += new EventHandler(this.SaveClicked);
            this.saveCancelEditingRowControl.CancelClicked += new EventHandler(this.CancelClicked);
        }

        private void InitServices()
        {
            this.serviceContainer = new ServiceContainer();
            this.AddServices();
        }

        private void InitSummaries()
        {
            this.TotalSummaries = new ObservableCollection<GridColumnSummary>();
            this.TotalSummaries.add_CollectionChanged(new NotifyCollectionChangedEventHandler(this.OnTotalSummariesCollectionChanged));
            this.GroupSummaries = new ObservableCollection<GridColumnSummary>();
            this.GroupSummaries.add_CollectionChanged(new NotifyCollectionChangedEventHandler(this.OnGroupSummariesCollectionChanged));
        }

        private void InitSwipeButtons()
        {
            this.RightSwipeButtons = new ObservableCollection<SwipeButtonInfo>();
            this.LeftSwipeButtons = new ObservableCollection<SwipeButtonInfo>();
            this.rowVirtualizer.ViewOffsetChanged = (EventHandler)Delegate.Combine((Delegate)this.rowVirtualizer.ViewOffsetChanged, (Delegate)new EventHandler(this.OnViewOffsetChanged));
        }

        private void InitTotalSummaryUI()
        {
            this.totalSummaryContentProvider = new TotalSummaryContentProvider(this.dataController, this.ActualTotalSummaryHeight, this);
            TotalSummaryContainer container1 = new TotalSummaryContainer(this.totalSummaryContentProvider);
            container1.set_VerticalOptions(LayoutOptions.FillAndExpand);
            container1.set_HorizontalOptions(LayoutOptions.FillAndExpand);
            this.totalSummary = container1;
        }

        private bool IsColumnVisibleInHeaders(GridColumn column) =>
            !column.IsGrouped;

        private bool IsExcelFormat(ExportTarget format) =>
            ((format == ExportTarget.Xlsx) || ((format == ExportTarget.Xls) || (format == ExportTarget.Csv)));

        public bool IsGroupCollapsed(int rowHandle) =>
            this.DataController.IsGroupCollapsed(rowHandle);

        public bool IsGroupRow(int rowHandle) =>
            this.DataController.IsGroupRow(rowHandle);

        private bool IsItemVisible(PopupMenuItem item)
        {
            Command command = this.CreateCommand(item);
            if (command == null)
            {
                return true;
            }
            ICommandUIState state = command.CreateDefaultCommandUIState();
            command.UpdateUIState(state);
            return state.Visible;
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            this.isRendererShouldBeLoaded = true;
            this.CheckDevExpressPlatformRegistered();
            base.LayoutChildren(x, y, width, height);
        }

        public void LoadInVisualTree()
        {
            if (this.isDisposed)
            {
                this.RegisterGestureHandlers();
                this.isDisposed = false;
            }
        }

        private void LoadMoreCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            if (!this.isDisposed)
            {
                this.scrollContent.LoadMoreSettingsChanged(this.CanShowLoadMorePanel, this.LoadMoreCommand);
            }
        }

        private void LocalizeFormEditorDialog()
        {
            if (this.formEditorDialogForm != null)
            {
                if (this.formEditorDialogForm.DialogContent is DefaultEditFormContent)
                {
                    (this.formEditorDialogForm.DialogContent as DefaultEditFormContent).Localize();
                }
                this.formEditorDialogForm.Caption = GridLocalizer.GetString(GridStringId.EditingForm_LabelCaption);
                this.formEditorDialogForm.Localize();
            }
        }

        private void OnAllowGroupCollapseChanged(bool newValue)
        {
            this.rowVirtualizer.AllowGroupCollapse = newValue;
        }

        private static void OnAllowGroupCollapseChanged(BindableObject obj, bool oldValue, bool newValue)
        {
            ((GridControl)obj).OnAllowGroupCollapseChanged(newValue);
        }

        private void OnAutoFilterPanelHeightChanged(double oldValue, double newValue)
        {
            this.autoFilterPanelContentProvider.SetRowHeight(this.GetAutoFilterPanelHeight());
            this.AutoFilterRowDefinition.set_Height((GridLength)this.GetAutoFilterPanelHeight());
            this.RelayoutDataRows();
        }

        private static void OnAutoFilterPanelHeightChanged(BindableObject obj, double oldValue, double newValue)
        {
            ((GridControl)obj).OnAutoFilterPanelHeightChanged(oldValue, newValue);
        }

        private void OnAutoFilterPanelVisibilityChanged(bool oldValue, bool newValue)
        {
            this.autoFilterPanelContentProvider.SetRowHeight(this.GetAutoFilterPanelHeight());
            this.AutoFilterRowDefinition.set_Height((GridLength)this.GetAutoFilterPanelHeight());
            this.RelayoutDataRows();
        }

        private static void OnAutoFilterPanelVisibilityChanged(BindableObject obj, bool oldValue, bool newValue)
        {
            ((GridControl)obj).OnAutoFilterPanelVisibilityChanged(oldValue, newValue);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            using (IEnumerator<GridColumn> enumerator = this.Columns.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    enumerator.Current.set_BindingContext(base.get_BindingContext());
                }
            }
        }

        private void OnCancelFormEditorDialog(object sender, EventArgs e)
        {
            this.CloseEditor(false);
        }

        private void OnColumnHeadersHeightChanged(double oldValue, double newValue)
        {
            this.UpdateColumnHeadersHeight();
        }

        private static void OnColumnHeadersHeightChanged(BindableObject obj, double oldValue, double newValue)
        {
            ((GridControl)obj).OnColumnHeadersHeightChanged(oldValue, newValue);
        }

        private void OnColumnHeadersVisibilityChanged(bool oldValue, bool newValue)
        {
            this.UpdateColumnHeadersHeight();
        }

        private static void OnColumnHeadersVisibilityChanged(BindableObject obj, bool oldValue, bool newValue)
        {
            ((GridControl)obj).OnColumnHeadersVisibilityChanged(oldValue, newValue);
        }

        private void OnColumnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (GridColumn.IsPropertyAffectsUnboundFields(e.PropertyName))
            {
                this.UpdateUnboundFields();
            }
            if (e.PropertyName == "IsVisible")
            {
                this.UpdateVisibleColumns();
                this.UpdateUnboundFields();
            }
            if (e.PropertyName == "IsReadOnly")
            {
                this.Redraw(true);
            }
            if ((e.PropertyName == GridColumn.WidthProperty.get_PropertyName()) && !this.isColumnResizing)
            {
                this.SetColumnWidth(sender as GridColumn, false);
            }
            if (e.PropertyName == GridColumn.AllowAutoFilterProperty.get_PropertyName())
            {
                this.autoFilterPanelContainer.UpdateTheme();
            }
            if (e.PropertyName == GridColumn.FixedStyleProperty.get_PropertyName())
            {
                this.UpdateVisibleColumns(true);
            }
        }

        private void OnColumnsAutoWidthPropertyChanged()
        {
            this.FinishCurrentDragging();
            this.CloseEditingRow();
            this.RecalculateVisibleColumnsWidth();
            this.UpdateVisibleColumns(true);
        }

        private static void OnColumnsAutoWidthPropertyChanged(BindableObject obj, bool oldValue, bool newValue)
        {
            ((GridControl)obj).OnColumnsAutoWidthPropertyChanged();
        }

        private void OnColumnsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.get_NewItems() != null)
            {
                foreach (GridColumn column1 in e.get_NewItems())
                {
                    column1.set_BindingContext(base.get_BindingContext());
                    column1.IsParentReadOnly = this.IsReadOnly;
                    column1.CustomCellTextProvider = this;
                }
            }
            this.UpdateVisibleColumns();
            this.AttachUnboundDataSource();
        }

        private void OnCreateUnboundFieldFunction(object sender, CreateUnboundFieldFunctionEventArgs e)
        {
            GridColumn column = this.columns[e.FieldName];
            if (column != null)
            {
                e.Function = column.CreateUnboundFieldFunction();
                if (e.Function == null)
                {
                    e.Function = delegate (IRowData row) {
                        return this.RaiseGetCustomUnboundColumnData(row, column);
                    };
                }
            }
        }

        private void OnCreateUnboundFieldSetter(object sender, CreateUnboundFieldFunctionEventArgs e)
        {
            GridColumn column = this.columns[e.FieldName];
            if (column != null)
            {
                e.Setter = delegate (IRowData row, object value) {
                    this.RaiseSetCustomUnboundColumnData(row, column, value);
                };
            }
        }

        private void OnCustomizeCell(CustomizeCellEventArgs e)
        {
            this.RaiseCustomizeCell(e);
        }

        private void OnDataControllerCalculateCustomSummary(object sender, CustomSummaryEventArgs e)
        {
            if (this.CalculateCustomSummary != null)
            {
                this.CalculateCustomSummary(this, e);
            }
        }

        private void OnDataControllerDataChanged(object sender, GridDataControllerDataChangedEventArgs args)
        {
            this.FinishCurrentDragging();
            if (args.ChangeType == GridDataControllerDataChangedType.GroupingChanged)
            {
                this.UpdateVisibleColumns();
            }
            else if (args.ChangeType == GridDataControllerDataChangedType.FilterChanged)
            {
                this.rowVirtualizer.Invalidate(true, true);
                this.RaiseFilterApplied();
            }
            else if (args.ChangeType == GridDataControllerDataChangedType.DataSourceChanged)
            {
                this.InvalidateMeasure();
            }
        }

        private void OnDataControllerGroupRowCollapsed(object sender, RowEventArgs e)
        {
            this.RaiseGroupRowCollapsed(e);
        }

        private void OnDataControllerGroupRowCollapsing(object sender, RowAllowEventArgs e)
        {
            this.RaiseGroupRowCollapsing(e);
        }

        private void OnDataControllerGroupRowExpanded(object sender, RowEventArgs e)
        {
            this.RaiseGroupRowExpanded(e);
        }

        private void OnDataControllerGroupRowExpanding(object sender, RowAllowEventArgs e)
        {
            this.RaiseGroupRowExpanding(e);
        }

        private void OnDataControllerSelectionChanged(object sender, EventArgs e)
        {
            this.SelectedRowHandle = (this.dataController.RowCount > 0) ? this.dataController.SelectedRow : -2_147_483_648;
            this.UpdateSelectedDataObject();
        }

        private void OnDragDirectionChanged()
        {
            if (this.SwipeButtonsStategy != null)
            {
                this.SwipeButtonsStategy.RemoveButtons();
                this.SwipeButtonsStategy.ButtonClick -= new EventHandler(this.OnSwipeButtonClick);
            }
            switch (this.DragDirection)
            {
                case RowDragDirection.None:
                    this.SwipeButtonsStategy = null;
                    break;

                case RowDragDirection.Right:
                    this.SwipeButtonsStategy = new RightSwipeButtonsStategy(this, this.LeftSwipeButtons, this.draggedRow);
                    break;

                case RowDragDirection.Left:
                    this.SwipeButtonsStategy = new LeftSwipeButtonsStategy(this, this.RightSwipeButtons, this.draggedRow);
                    break;

                default:
                    break;
            }
            if (this.SwipeButtonsStategy != null)
            {
                this.SwipeButtonsStategy.ButtonClick += new EventHandler(this.OnSwipeButtonClick);
            }
        }

        private void OnFilterPanelHeightChanged(double OldValue, double newValue)
        {
            this.UpdateFilterPanelHeight();
        }

        private static void OnFilterPanelHeightChanged(BindableObject obj, double oldValue, double newValue)
        {
            ((GridControl)obj).OnFilterPanelHeightChanged(oldValue, newValue);
        }

        private void OnFilterPanelVisibilityChanged(VisibilityState oldValue, VisibilityState newValue)
        {
            this.UpdateFilterPanelHeight();
        }

        private static void OnFilterPanelVisibilityChanged(BindableObject obj, VisibilityState oldValue, VisibilityState newValue)
        {
            ((GridControl)obj).OnFilterPanelVisibilityChanged(oldValue, newValue);
        }

        private void OnFilterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CriteriaOperator actualFilterExpression = this.filter.ActualFilterExpression;
            if (e.PropertyName != "IsActive")
            {
                this.lockFilterStringChanged = true;
                try
                {
                    if (object.Equals(actualFilterExpression, null))
                    {
                        this.ActualFilterString = string.Empty;
                        this.FilterString = string.Empty;
                    }
                    else
                    {
                        this.ActualFilterString = actualFilterExpression.ToString();
                        this.FilterString = object.Equals(this.filter.FilterExpression, null) ? string.Empty : this.filter.FilterExpression.ToString();
                    }
                }
                finally
                {
                    this.lockFilterStringChanged = false;
                }
            }
            this.UpdateFilter(actualFilterExpression, this.filter.IsActive);
            this.UpdateFilterPanelHeight();
        }

        private void OnFilterStringChanged(string oldValue, string newValue)
        {
            if (!this.lockFilterStringChanged)
            {
                this.lockFilterStringChanged = true;
                try
                {
                    OperandValue[] valueArray;
                    this.FilterExpression = CriteriaParser.Parse(this.FilterString, out valueArray);
                }
                finally
                {
                    this.lockFilterStringChanged = false;
                }
            }
        }

        private static void OnFilterStringChanged(BindableObject obj, string oldValue, string newValue)
        {
            ((GridControl)obj).OnFilterStringChanged(oldValue, newValue);
        }

        private void OnFormatConditionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.ResetConditionalFormatEngine();
        }

        private void OnGroupsInitiallyExpandedPropertyChanged(bool oldValue, bool newValue)
        {
            this.DataController.AreGroupsInitiallyCollapsed = !this.GroupsInitiallyExpanded;
        }

        private static void OnGroupsInitiallyExpandedPropertyChanged(BindableObject obj, bool oldValue, bool newValue)
        {
            ((GridControl)obj).OnGroupsInitiallyExpandedPropertyChanged(oldValue, newValue);
        }

        private void OnGroupSummariesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.dataController.GroupSummaries.Clear();
            foreach (GridColumnSummary summary in this.GroupSummaries)
            {
                summary.IsGroupSummary = true;
                this.dataController.GroupSummaries.Add(summary);
            }
            this.dataController.UpdateGroupSummaries();
            this.rowVirtualizer.Invalidate(false, true);
        }

        private void OnIsLoadMoreEnabledPropertyChanged(bool oldValue, bool newValue)
        {
            this.scrollContent.LoadMoreSettingsChanged(this.CanShowLoadMorePanel, this.LoadMoreCommand);
        }

        private static void OnIsLoadMoreEnabledPropertyChanged(BindableObject obj, bool oldValue, bool newValue)
        {
            ((GridControl)obj).OnIsLoadMoreEnabledPropertyChanged(oldValue, newValue);
        }

        private void OnIsPullToRefreshEnabledPropertyChanged(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                if (PlatformHelper.Platform == PlatformHelper.Platforms.iOS)
                {
                    this.scroller.RefreshColor = this.CurrentTheme.IOSRefreshCustomizer.RefreshColor;
                    this.scroller.IsPullToRefreshEnabled = newValue;
                }
                else
                {
                    this.refreshView.BarColor = this.CurrentTheme.AndroidRefreshCustomizer.BarColor;
                    this.refreshView.set_VerticalOptions(LayoutOptions.StartAndExpand);
                    base.get_Children().Add(this.refreshView, 0, 4);
                }
            }
        }

        private static void OnIsPullToRefreshEnabledPropertyChanged(BindableObject obj, bool oldValue, bool newValue)
        {
            ((GridControl)obj).OnIsPullToRefreshEnabledPropertyChanged(oldValue, newValue);
        }

        private void OnIsReadOnlyChanged(bool oldValue, bool newValue)
        {
            using (IEnumerator<GridColumn> enumerator = this.Columns.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    enumerator.Current.IsParentReadOnly = newValue;
                }
            }
            this.Redraw(true);
            this.UpdateNewItemRow();
        }

        protected virtual void OnItemsSourceChanged(object oldValue, object newValue)
        {
            this.dataRowContentProvider.ResetCachedRow();
            this.AttachDataSource(newValue);
            this.ResetSelection();
            this.ResetAutoGeneratedColumns();
            this.TryAutoGenerateColumns();
            this.SortingColumnManager.SetColumnsCollection(this.columns, this.IsLoaded);
        }

        private static void OnItemsSourceChanged(BindableObject obj, object oldValue, object newValue)
        {
            ((GridControl)obj).OnItemsSourceChanged(oldValue, newValue);
        }

        private void OnLoadMore(object sender, EventArgs args)
        {
            this.StartLoadMore();
        }

        private void OnLoadMoreCommandChanged(ICommand oldValue, ICommand newValue)
        {
            this.scrollContent.LoadMoreSettingsChanged(this.CanShowLoadMorePanel, this.LoadMoreCommand);
            if (oldValue != null)
            {
                oldValue.remove_CanExecuteChanged(new EventHandler(this.LoadMoreCommand_CanExecuteChanged));
            }
            if (newValue != null)
            {
                newValue.add_CanExecuteChanged(new EventHandler(this.LoadMoreCommand_CanExecuteChanged));
            }
        }

        private static void OnLoadMoreCommandChanged(BindableObject obj, ICommand oldValue, ICommand newValue)
        {
            ((GridControl)obj).OnLoadMoreCommandChanged(oldValue, newValue);
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            double num = (((((this.ActualColumnHeadersHeight + this.GetActualSaveCancelRowHeight()) + this.GetAutoFilterPanelHeight()) + this.GetActualNewItemRowHeight()) + this.ActualFilterPanelHeight) + this.ActualTotalSummaryHeight) + (this.RowCount * this.RowHeight);
            if ((!double.IsInfinity(heightConstraint) && !double.IsNaN(heightConstraint)) && (num > heightConstraint))
            {
                num = heightConstraint;
            }
            return new SizeRequest(new Size(widthConstraint, num));
        }

        private void OnNewItemRowVisibilityChanged(bool oldValue, bool newValue)
        {
            this.UpdateNewItemRow();
        }

        private static void OnNewItemRowVisibilityChanged(BindableObject obj, bool oldValue, bool newValue)
        {
            ((GridControl)obj).OnNewItemRowVisibilityChanged(oldValue, newValue);
        }

        private void OnOKFormEditorDialog(object sender, EventArgs e)
        {
            this.CloseEditor(true);
        }

        private void OnOptionsCustomizeCell(CustomizeCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnParentSet()
        {
            if (base.get_Parent() != null)
            {
                this.IsLoaded = true;
                this.UpdateVisibleColumns();
                this.SortingColumnManager = SortingColumnManager.Create(this.Columns, this.dataController, this.SortMode, this.IsLoaded);
                this.SortingColumnManager.GroupAndSortData();
                this.ResetSelection();
            }
            base.OnParentSet();
        }

        private void OnPopupMenuClosed(object sender, EventArgs e)
        {
            GridPopupMenuView view = sender as GridPopupMenuView;
            if (view != null)
            {
                this.HideContextAdorners();
                view.Closed -= new EventHandler(this.OnPopupMenuClosed);
                view.MenuItemClick -= new PopupMenuItemClickEventHandler(this.OnPopupMenuMenuItemClick);
                view.UpdateUI -= new EventHandler<MenuItemUpdateUIEventArgs>(this.OnPopupMenuUpdateUI);
            }
        }

        private void OnPopupMenuMenuItemClick(object sender, PopupMenuItemClickEventArgs e)
        {
            this.HideContextAdorners();
            Command command = this.CreateCommand(e.Item);
            if (command != null)
            {
                command.Execute();
            }
        }

        private void OnPopupMenuUpdateUI(object sender, MenuItemUpdateUIEventArgs e)
        {
            Command command = this.CreateCommand(e.Item);
            if (command != null)
            {
                command.UpdateUIState(e.State);
            }
            else
            {
                e.Item.UpdateUIState(e.State);
            }
        }

        private void OnPullToRefresh(object sender, EventArgs args)
        {
            this.StartPullToRefresh();
        }

        private void OnPullToRefreshCommandPropertyChanged(ICommand oldValue, ICommand newValue)
        {
            if (this.IsPullToRefreshEnabled)
            {
                if (PlatformHelper.Platform == PlatformHelper.Platforms.iOS)
                {
                    this.scroller.PullToRefreshCommand = newValue;
                }
                else
                {
                    this.refreshView.PullToRefreshCommand = newValue;
                }
            }
        }

        private static void OnPullToRefreshCommandPropertyChanged(BindableObject obj, ICommand oldValue, ICommand newValue)
        {
            ((GridControl)obj).OnPullToRefreshCommandPropertyChanged(oldValue, newValue);
        }

        private static void OnReadOnlyChanged(BindableObject obj, bool oldValue, bool newValue)
        {
            ((GridControl)obj).OnIsReadOnlyChanged(oldValue, newValue);
        }

        private static void OnRowEditModePropertyChanged(BindableObject obj, RowEditMode oldValue, RowEditMode newValue)
        {
            ((GridControl)obj).CloseEditorOfConcreteType(false, oldValue);
        }

        private void OnRowHeightChanged(double oldValue, double newValue)
        {
            this.dataRowContentProvider.SetRowHeight(newValue);
            this.SetNewItemRowHeight(this.GetActualNewItemRowHeight());
            this.editRowContentProvider.SetRowHeight(newValue);
            this.scrollContent.LoadMore.SetHeight(newValue);
        }

        private static void OnRowHeightChanged(BindableObject obj, double oldValue, double newValue)
        {
            ((GridControl)obj).OnRowHeightChanged(oldValue, newValue);
        }

        private void OnSelectedDataObjectChanged(object oldValue, object newValue)
        {
            this.SelectedRowHandle = this.FindRowByDataObject(newValue);
            if (this.SelectedRowHandle == -2_147_483_648)
            {
                this.SelectedDataObject = null;
            }
        }

        private static void OnSelectedDataObjectChanged(BindableObject obj, object oldValue, object newValue)
        {
            ((GridControl)obj).OnSelectedDataObjectChanged(oldValue, newValue);
        }

        private void OnSelectedRowHandleChanged(int oldValue, int newValue)
        {
            this.dataController.SelectedRow = newValue;
            RowContainer rowByRowHandle = this.rowVirtualizer.GetRowByRowHandle(oldValue) as RowContainer;
            if (rowByRowHandle != null)
            {
                rowByRowHandle.UpdateRowSelection();
            }
            rowByRowHandle = this.rowVirtualizer.GetRowByRowHandle(newValue) as RowContainer;
            if (rowByRowHandle != null)
            {
                rowByRowHandle.UpdateRowSelection();
            }
            this.CheckNewItemRowHandle(oldValue, newValue);
            this.UpdateSelectedDataObject();
            int sourceRowIndex = this.dataController.GetSourceRowIndex(newValue);
            if (sourceRowIndex != this.selectedRowSourceIndex)
            {
                this.selectedRowSourceIndex = sourceRowIndex;
                this.RaiseSelectionChanged();
            }
        }

        private static void OnSelectedRowHandleChanged(BindableObject obj, int oldValue, int newValue)
        {
            ((GridControl)obj).OnSelectedRowHandleChanged(oldValue, newValue);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if ((this.size.get_IsZero() || (!this.CompareDoubleValues(this.size.get_Width(), width) || (!this.CompareDoubleValues(this.size.get_Height(), height) || (this.size.get_Width() <= 0.0)))) || (this.size.get_Height() <= 0.0))
            {
                this.size = new Size(base.get_Width(), base.get_Height());
                if (this.columnChooser != null)
                {
                    this.columnChooser.Layout(new Rectangle(0.0, 0.0, base.get_Width(), base.get_Height()));
                }
                if (this.IsPopoverEditorOpened)
                {
                    this.formEditorDialogForm.Layout(new Rectangle(0.0, 0.0, base.get_Width(), base.get_Height()));
                }
                this.RecalculateVisibleColumnsWidth();
                this.UpdateCells(false);
                this.RelayoutDataRows();
            }
        }

        private void OnSortModeChanged(GridSortMode oldValue, GridSortMode newValue)
        {
            this.SortingColumnManager = SortingColumnManager.Create(this.Columns, this.dataController, newValue, this.IsLoaded);
        }

        private static void OnSortModeChanged(BindableObject obj, GridSortMode oldValue, GridSortMode newValue)
        {
            ((GridControl)obj).OnSortModeChanged(oldValue, newValue);
        }

        private void OnSwipeButtonClick(object sender, EventArgs e)
        {
            SwipeButton button = sender as SwipeButton;
            if (button != null)
            {
                this.SendSwipeButtonClick(button.ButtonInfo);
                if (button.ButtonInfo.AutoCloseOnTap)
                {
                    this.FinishCurrentDragging();
                }
            }
        }

        private void OnTotalSummariesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.dataController.TotalSummaries.Clear();
            foreach (GridColumnSummary summary in this.TotalSummaries)
            {
                this.dataController.TotalSummaries.Add(summary);
            }
            this.totalSummaryContentProvider.SetTotalSummariesCollection((IList<GridColumnSummary>)this.TotalSummaries);
            this.UpdateTotalSummaryHeight();
        }

        private void OnTotalSummaryHeightChanged(double OldValue, double newValue)
        {
            this.UpdateTotalSummaryHeight();
        }

        private static void OnTotalSummaryHeightChanged(BindableObject obj, double oldValue, double newValue)
        {
            ((GridControl)obj).OnTotalSummaryHeightChanged(oldValue, newValue);
        }

        private void OnTotalSummaryVisibilityChanged(VisibilityState oldValue, VisibilityState newValue)
        {
            this.UpdateTotalSummaryHeight();
        }

        private static void OnTotalSummaryVisibilityChanged(BindableObject obj, VisibilityState oldValue, VisibilityState newValue)
        {
            ((GridControl)obj).OnTotalSummaryVisibilityChanged(oldValue, newValue);
        }

        private void OnValueChanged(object sender, ObservableDictionary<string, object>.ObservableDictionaryEventHandlerArgs e)
        {
            if ((this.openedRowHandle != -2_147_483_648) && (this.dataController.DataSource is UnboundColumnsDataSource))
            {
                this.UnsubscribeEditFormValuesChangeEvent();
                try
                {
                    string key = e.Key;
                    foreach (string str2 in new List<string>((IEnumerable<string>)this.editingValuesContainer.Values.get_Keys()))
                    {
                        if (str2 == key)
                        {
                            this.SyncUnboundValue(str2);
                            continue;
                        }
                        GridColumn column = this.Columns[str2];
                        if ((column != null) && column.IsUnbound)
                        {
                            this.UpdateUnboundValue(str2);
                        }
                    }
                }
                finally
                {
                    this.SubscribeEditFormValuesChangeEvent();
                }
            }
        }

        private void OnViewOffsetChanged(object sender, EventArgs e)
        {
            if (this.DragDirection != RowDragDirection.None)
            {
                this.FinishCurrentDragging();
            }
        }

        public void OpenEditor(CellIndex cellIndex)
        {
            if (!cellIndex.Equals(CellIndex.InvalidIndex))
            {
                this.ScrollToRow(cellIndex.RowHandle);
                this.OpenEditorCore(cellIndex);
            }
        }

        internal void OpenEditorCommandExecuted(CellIndex cellIndex)
        {
            if (!this.RaiseRowDoubleTap(cellIndex))
            {
                this.OpenEditor(cellIndex);
            }
        }

        private void OpenEditorCore(CellIndex cellIndex)
        {
            if (this.RowEditMode == RowEditMode.Inplace)
            {
                this.OpenInplaceEditor(cellIndex);
            }
            else
            {
                this.OpenFormEditorCore(cellIndex);
            }
        }

        private void OpenFormEditorCore(CellIndex cellIndex)
        {
            View view = this.GetContentForEditForm(this.dataController.GetRow(cellIndex.RowHandle, null), ColumnsFilterCreterias.CurrentCriteria, false);
            this.SubscribeEditFormValuesChangeEvent();
            if (this.RowEditMode == RowEditMode.GridArea)
            {
                this.OpenFormEditorFullscreen(view);
            }
            if (this.RowEditMode == RowEditMode.ScreenArea)
            {
                this.OpenFormEditorFullscreenWithNavBar(view);
            }
            if (this.RowEditMode == RowEditMode.Popup)
            {
                this.OpenFormEditorPopoup(view);
            }
            this.openedRowHandle = cellIndex.RowHandle;
        }

        private void OpenFormEditorFormInAdorner(View view, DialogFormStyle dialogFormStyle)
        {
            if (this.formEditorDialogForm == null)
            {
                this.CreateFormEditorDialog();
                this.formEditorDialogForm.DialogStyle = dialogFormStyle;
                this.Adorner.get_Children().Add(this.formEditorDialogForm);
            }
            this.formEditorDialogForm.DialogContent = view;
            this.Adorner.SetInputTransparent(false);
            this.formEditorDialogForm.Show();
            this.formEditorDialogForm.Layout(new Rectangle(0.0, 0.0, base.get_Width(), base.get_Height()));
        }

        private void OpenFormEditorFullscreen(View view)
        {
            this.OpenFormEditorFormInAdorner(view, DialogFormStyle.Fullscreen);
        }

        private void OpenFormEditorFullscreenWithNavBar(View view)
        {
            this.formEditorContentPage = new ContentPage();
            IDialogFormCustomizer dialogFormCustomizer = this.CurrentTheme.DialogFormCustomizer;
            this.formEditorContentPage.set_BackgroundColor(dialogFormCustomizer.FullScreenBackgroundColor);
            view.set_BackgroundColor(dialogFormCustomizer.FullScreenBackgroundColor);
            ScrollView view1 = new ScrollView();
            view1.set_Content(view);
            this.formEditorContentPage.set_Content(view1);
            ToolbarItem item = new ToolbarItem();
            item.set_Text(GridLocalizer.GetString(GridStringId.DialogForm_ButtonCancel));
            item.set_Order(1);
            item.set_Command(new Command(delegate {
                this.OnCancelFormEditorDialog(this, EventArgs.Empty);
            }));
            item.set_Priority(0);
            this.formEditorContentPage.get_ToolbarItems().Add(item);
            ToolbarItem item2 = new ToolbarItem();
            item2.set_Text(GridLocalizer.GetString(GridStringId.DialogForm_ButtonOk));
            item2.set_Order(1);
            item2.set_Command(new Command(delegate {
                this.OnOKFormEditorDialog(this, EventArgs.Empty);
            }));
            item2.set_Priority(0);
            this.formEditorContentPage.get_ToolbarItems().Add(item2);
            this.formEditorContentPage.set_Title(GridLocalizer.GetString(GridStringId.EditingForm_LabelCaption));
            NavigationPage page = new NavigationPage(this.formEditorContentPage);
            page.set_Title(GridLocalizer.GetString(GridStringId.EditingForm_LabelCaption));
            base.get_Navigation().PushModalAsync(page);
        }

        private void OpenFormEditorPopoup(View view)
        {
            this.OpenFormEditorFormInAdorner(view, DialogFormStyle.Popup);
        }

        private void OpenInplaceEditor(CellIndex cellIndex)
        {
            if (double.IsNaN(this.rowVirtualizer.CalculateOffsetForScrollingToRow(cellIndex.RowHandle)))
            {
                this.OpenInplaceEditorCore(cellIndex);
            }
            else
            {
                this.openEditorCellIndex = cellIndex;
                this.rowVirtualizer.ScrollingFinished += new EventHandler(this.ScrollToRowFinished);
                this.rowVirtualizer.ScrollToRow(cellIndex.RowHandle);
            }
        }

        private void OpenInplaceEditorCore(CellIndex cellIndex)
        {
            this.editRowContentProvider.UnsubscribeDataControllerEvents();
            this.editRowContentProvider.SubscribeDataControllerEvents();
            GridColumn item = this.Columns[cellIndex.FieldName];
            if (!item.ActualIsReadOnly)
            {
                this.underEditorRowContainer = this.rowVirtualizer.GetRowByRowHandle(cellIndex.RowHandle);
                if (this.underEditorRowContainer != null)
                {
                    this.editingValuesContainer = this.GetEditingValuesContainer(this.DataController.GetRow(cellIndex.RowHandle, null), false);
                    this.SubscribeEditFormValuesChangeEvent();
                    this.editRowLayout.SetEditRowBounds(this.underEditorRowContainer.get_Bounds());
                    this.editRowLayout.OpenEditRow(cellIndex.RowHandle, this.visibleColumns.IndexOf(item), this.editingValuesContainer);
                    this.PrepareGridForInplaceRowEditing();
                    this.openedRowHandle = cellIndex.RowHandle;
                    this.UpdateEditRowLayoutInputTransparent(false);
                }
            }
        }

        private void PrepareGridForDragging(int rowHandle)
        {
            if ((this.currentDraggedRowHandle != rowHandle) && (this.currentDraggedRowHandle != -2_147_483_648))
            {
                this.FinishCurrentDragging();
            }
            if (this.currentDraggedRowHandle == -2_147_483_648)
            {
                this.draggedRow = this.rowVirtualizer.GetRowByRowHandle(rowHandle);
                this.currentDraggedRowHandle = rowHandle;
                this.scroller.PreventAndroidScrolling = true;
            }
        }

        private void PrepareGridForInplaceRowEditing()
        {
            this.IsEditingRowOpened = true;
            this.UpdateSaveCancelRowHeight();
            this.DisableTouchInActiveGridPanels();
        }

        private bool RaiseAutoGeneratingColumn(GridColumn gridColumn)
        {
            if (this.AutoGeneratingColumn == null)
            {
                return true;
            }
            AutoGeneratingColumnEventArgs e = new AutoGeneratingColumnEventArgs(gridColumn);
            this.AutoGeneratingColumn(this, e);
            return !e.Cancel;
        }

        protected internal virtual void RaiseCustomizeCell(CustomizeCellEventArgs args)
        {
            if (this.customizeCell != null)
            {
                this.customizeCell(args);
            }
        }

        protected internal virtual void RaiseCustomizeCellDisplayText(CustomizeCellDisplayTextEventArgs args)
        {
            if ((this.customizeCellDisplayText != null) && (args != null))
            {
                args.Source = this;
                this.customizeCellDisplayText(args);
            }
        }

        private void RaiseEndRowEdit(int rowHandle, EditingRowAction action)
        {
            if (this.EndRowEdit != null)
            {
                RowEditingEventArgs e = new RowEditingEventArgs(rowHandle, this.GetSourceRowIndex(rowHandle), action);
                this.EndRowEdit(this, e);
            }
        }

        private void RaiseFilterApplied()
        {
            if (this.FilterApplied != null)
            {
                this.FilterApplied(this, EventArgs.Empty);
            }
        }

        private object RaiseGetCustomUnboundColumnData(IRowData row, GridColumn column)
        {
            if (this.CustomUnboundColumnData == null)
            {
                return null;
            }
            GridColumnDataEventArgs e = new GridColumnDataEventArgs(this, column, row, null, true);
            this.CustomUnboundColumnData(this, e);
            return e.Value;
        }

        protected virtual void RaiseGroupRowCollapsed(RowEventArgs args)
        {
            if (this.GroupRowCollapsed != null)
            {
                this.GroupRowCollapsed(this, args);
            }
        }

        protected virtual bool RaiseGroupRowCollapsing(RowAllowEventArgs args)
        {
            if (this.GroupRowCollapsing == null)
            {
                return true;
            }
            this.GroupRowCollapsing(this, args);
            return args.Allow;
        }

        protected virtual void RaiseGroupRowExpanded(RowEventArgs args)
        {
            if (this.GroupRowExpanded != null)
            {
                this.GroupRowExpanded(this, args);
            }
        }

        protected virtual bool RaiseGroupRowExpanding(RowAllowEventArgs args)
        {
            if (this.GroupRowExpanding == null)
            {
                return true;
            }
            this.GroupRowExpanding(this, args);
            return args.Allow;
        }

        private void RaiseHorizontalScrollOffsetChanged(double offset)
        {
            if (this.HorizontalScrollOffsetChanged != null)
            {
                this.HorizontalScrollOffsetChanged(this, new HorizontalScrollOffsetEventArgs(offset));
            }
        }

        private void RaiseInitNewRow(IEditableRowData editableRowData)
        {
            if (this.InitNewRow != null)
            {
                this.InitNewRow(this, new InitNewRowEventArgs(editableRowData));
            }
        }

        private void RaisePopupMenuCustomization(GridPopupMenuView menu, GridPopupMenuType menuType, int rowHandle, GridColumn column)
        {
            if (this.PopupMenuCustomization != null)
            {
                PopupMenuEventArgs e = new PopupMenuEventArgs(menu, menuType, rowHandle, column);
                this.PopupMenuCustomization(this, e);
            }
        }

        private bool RaiseRowDoubleTap(CellIndex cellIndex)
        {
            if (this.RowDoubleTap == null)
            {
                return false;
            }
            RowDoubleTapEventArgs e = new RowDoubleTapEventArgs(cellIndex.RowHandle, cellIndex.FieldName);
            this.RowDoubleTap(this, e);
            return e.Handled;
        }

        private void RaiseRowTap(int rowHandle, string fieldName)
        {
            if (this.RowTap != null)
            {
                RowTapEventArgs e = new RowTapEventArgs(rowHandle, fieldName);
                this.RowTap(this, e);
            }
        }

        private void RaiseSelectionChanged()
        {
            if (this.SelectionChanged != null)
            {
                RowEventArgs e = new RowEventArgs(this.SelectedRowHandle);
                this.SelectionChanged(this, e);
            }
        }

        private void RaiseSetCustomUnboundColumnData(IRowData row, GridColumn column, object value)
        {
            if (this.CustomUnboundColumnData != null)
            {
                GridColumnDataEventArgs e = new GridColumnDataEventArgs(this, column, row, value, false);
                this.CustomUnboundColumnData(this, e);
            }
        }

        internal void RaiseSwipeButtonShowing(SwipeButtonShowingEventArgs e)
        {
            if (this.SwipeButtonShowing != null)
            {
                this.SwipeButtonShowing(this, e);
            }
        }

        internal void RearrangeColumns(ScaleGestureData data, GridColumn fixedColumn)
        {
            this.SetColumnWidth(fixedColumn, true);
        }

        private void RecalculateVisibleColumnsWidth()
        {
            this.isColumnResizing = true;
            this.horizontalScrollOffsetCore = 0.0;
            if (this.ColumnsAutoWidth)
            {
                ColumnsLayoutCalculator.CalculateColumnsWidth(this.VisibleColumns as IList, base.get_Width());
            }
            else
            {
                this.totalColumnsWidth = ColumnsLayoutCalculator.SetDefaultWidthToColumnsAndReturnTotalWidth(this.VisibleColumns as IList, 120.0);
            }
            this.isColumnResizing = false;
        }

        public void Redraw(bool forceLayout)
        {
            this.FinishCurrentDragging();
            if (!forceLayout)
            {
                this.rowsLayout.UpdateTheme();
            }
            else
            {
                if (this.rowVirtualizer != null)
                {
                    this.rowVirtualizer.Invalidate(true, true);
                }
                this.OnTotalSummariesCollectionChanged(null, new NotifyCollectionChangedEventArgs((NotifyCollectionChangedAction)NotifyCollectionChangedAction.Reset));
                if (this.newItemRowContentProvider != null)
                {
                    this.newItemRowContentProvider.Refresh();
                }
                if (this.saveCancelEditingRowControl != null)
                {
                    this.saveCancelEditingRowControl.UpdateContent();
                }
                if (this.filterPanelContentProvider != null)
                {
                    this.filterPanelContentProvider.UpdateContent();
                }
                if (this.columnChooser.get_IsVisible())
                {
                    this.columnChooser.UpdateContent();
                }
                this.LocalizeFormEditorDialog();
            }
        }

        public void RefreshData()
        {
            this.FinishCurrentDragging();
            this.DataController.RefreshData();
            this.rowVirtualizer.Invalidate(true, true);
        }

        internal void RegisterGestureHandlers()
        {
            IGestureRecognizerService service = this.GetService<IGestureRecognizerService>();
            if (service != null)
            {
                service.Register(this.headers, this, Gesture.AllWithoutDrag);
                service.Register(this.autoFilterPanelContainer, this, Gesture.AllWithoutDrag);
                service.Register(this.newItemRowContainer, this, Gesture.AllWithoutDrag);
                service.Register(this.scroller, this, Gesture.AllWithoutDrag);
                service.Register(this.totalSummary, this, Gesture.AllWithoutDrag);
                service.Register(this.saveCancelEditingRowControl, this, Gesture.SingleTap);
                service.Register(this, this, Gesture.HorizontalDrag | Gesture.LongTap | Gesture.SingleTap);
            }
        }

        internal void RegisterTapGestureForView(View view)
        {
            IGestureRecognizerService service = this.GetService<IGestureRecognizerService>();
            if (service != null)
            {
                service.Register(view, this, Gesture.SingleTap);
            }
        }

        private void RelayoutDataRows()
        {
            this.scroller.Redraw(this.scroller.get_Width(), this.scroller.get_Height());
        }

        private void ResetAutoGeneratedColumns()
        {
            this.UnsubscribeColumnsEvents();
            try
            {
                this.autoGeneratedColumns = null;
                for (int i = this.Columns.Count - 1; i >= 0; i--)
                {
                    if (this.Columns[i].IsAutoGenerated)
                    {
                        this.Columns.RemoveAt(i);
                    }
                }
            }
            finally
            {
                this.SubscribeColumnsEvents();
            }
        }

        private void ResetColumnsComparers()
        {
            using (IEnumerator<GridColumn> enumerator = this.Columns.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    enumerator.Current.ResetComparers();
                }
            }
        }

        private void ResetConditionalFormatEngine()
        {
            this.conditionalFormatEngine.Reset();
            this.DataController.ConditionalFormatSummaries.Clear();
            this.Redraw(true);
        }

        private void ResetProperties(OptionsLayoutBase options)
        {
            foreach (PropertyInfo info in RuntimeReflectionExtensions.GetRuntimeProperties(base.GetType()))
            {
                XtraSerializableProperty customAttribute = CustomAttributeExtensions.GetCustomAttribute((MemberInfo)info, typeof(XtraSerializableProperty)) as XtraSerializableProperty;
                if ((customAttribute != null) && ((customAttribute.Visibility == XtraSerializationVisibility.Visible) && info.CanWrite))
                {
                    FieldInfo field = this.GetField(IntrospectionExtensions.GetTypeInfo(base.GetType()), info.Name + "Property");
                    if ((field != null) && (field.get_FieldType() == typeof(BindableProperty)))
                    {
                        info.SetValue(this, ((BindableProperty)field.GetValue(this)).get_DefaultValue());
                    }
                }
            }
        }

        protected virtual void ResetSelection()
        {
            this.dataController.ResetSelection();
            if (this.RowCount <= 0)
            {
                this.SelectedRowHandle = -2_147_483_648;
            }
            else if (this.SelectedRowHandle == 0)
            {
                this.UpdateSelectedDataObject();
            }
            else
            {
                this.SelectedRowHandle = 0;
            }
        }

        public void RestoreLayoutFromStream(Stream stream)
        {
            base.BeginUpdate();
            try
            {
                new XmlXtraSerializer().DeserializeObject(this, stream, string.Empty);
            }
            finally
            {
                base.CancelUpdate();
            }
            this.Redraw(true);
        }

        public void RestoreLayoutFromXml(string xmlContent)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.get_UTF8().GetBytes(xmlContent)))
            {
                this.RestoreLayoutFromStream((Stream)stream);
            }
        }

        private void SaveClicked(object sender, EventArgs e)
        {
            this.UnsubscribeDataControllerEvents();
            this.CloseEditor(true);
        }

        public void SaveLayoutToStream(Stream stream)
        {
            new XmlXtraSerializer().SerializeObject(this, stream, string.Empty);
        }

        public string SaveLayoutToXml()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                this.SaveLayoutToStream((Stream)stream);
                byte[] bytes = stream.ToArray();
                return Encoding.get_UTF8().GetString(bytes, 0, bytes.Length);
            }
        }

        public void ScrollToRow(int rowHandle)
        {
            this.rowVirtualizer.ScrollToRow(rowHandle);
        }

        private void ScrollToRowFinished(object sender, EventArgs e)
        {
            this.rowVirtualizer.ScrollingFinished -= new EventHandler(this.ScrollToRowFinished);
            this.OpenInplaceEditorCore(this.openEditorCellIndex);
            this.openEditorCellIndex = CellIndex.InvalidIndex;
        }

        private void SendSwipeButtonClick(SwipeButtonInfo buttonInfo)
        {
            SwipeButtonEventArgs e = new SwipeButtonEventArgs(buttonInfo, this.currentDraggedRowHandle, this.dataController.GetSourceRowIndex(this.currentDraggedRowHandle));
            if (this.SwipeButtonClick != null)
            {
                this.SwipeButtonClick(this, e);
            }
            if (this.SwipeButtonCommand != null)
            {
                this.SwipeButtonCommand.Execute(e);
            }
        }

        private void SendTapToSaveCancelButtonsControl(GridHitInfo hitInfo)
        {
            this.saveCancelEditingRowControl.TapInsideControl(hitInfo.Location);
        }

        public void SetCellValue(int rowHandle, GridColumn column, object value)
        {
            this.SetCellValue(rowHandle, column.FieldName, value);
        }

        public void SetCellValue(int rowHandle, string fieldName, object value)
        {
            IEditableRowData rowData = this.DataController.BeginEditRow(rowHandle);
            if (rowData != null)
            {
                rowData.SetFieldValue(fieldName, value);
                this.DataController.EndEditRow(rowData);
            }
        }

        internal void SetColumnWidth(GridColumn column, bool fixIncorrectWidth)
        {
            if (this.ColumnsAutoWidth)
            {
                ColumnsLayoutCalculator.ModifyOneColumnWidth(column, this.VisibleColumns as IList, base.get_Width(), fixIncorrectWidth);
            }
            else
            {
                this.totalColumnsWidth = ColumnsLayoutCalculator.SetDefaultWidthToColumnsAndReturnTotalWidth(this.VisibleColumns as IList, 120.0);
            }
            this.UpdateCells(true);
        }

        private void SetHorizontalScrollOffsetCore(double value)
        {
            this.horizontalScrollOffsetCore = value;
            if (this.horizontalScrollOffsetCore < 0.0)
            {
                this.horizontalScrollOffsetCore = 0.0;
            }
            if ((this.horizontalScrollOffsetCore + base.get_Width()) > this.totalColumnsWidth)
            {
                this.horizontalScrollOffsetCore = this.totalColumnsWidth - base.get_Width();
            }
            this.UpdateCells(false);
            this.RaiseHorizontalScrollOffsetChanged(this.horizontalScrollOffsetCore);
        }

        private void SetNewItemRowHeight(double height)
        {
            this.newItemRowContentProvider.SetRowHeight(height);
            this.NewItemRowDefinition.set_Height((GridLength)height);
        }

        public void ShowColumnChooser()
        {
            this.columnChooser.Show();
        }

        private void ShowContextAdorners(Rectangle[] contextBounds)
        {
            if (this.HighlightMenuTargetElements)
            {
                int length = contextBounds.Length;
                for (int i = 0; i < length; i++)
                {
                    Rectangle rectangle = contextBounds[i];
                    if ((rectangle.get_Width() > 0.0) && (rectangle.get_Height() > 0.0))
                    {
                        BoxView view = (i < this.menuContext.Count) ? this.menuContext[i] : this.CreateContextAdorner();
                        view.Layout(rectangle);
                        view.set_IsVisible(true);
                    }
                }
            }
        }

        internal void ShowPopupMenu(GridHitInfo hitInfo, GridPopupMenuType menuType)
        {
            this.ShowPopupMenu(hitInfo, menuType, Rectangle.Zero);
        }

        internal void ShowPopupMenu(GridHitInfo hitInfo, GridPopupMenuType menuType, Rectangle contextBounds)
        {
            Rectangle[] rectangleArray1 = new Rectangle[] { contextBounds };
            this.ShowPopupMenu(hitInfo, menuType, rectangleArray1);
        }

        internal void ShowPopupMenu(GridHitInfo hitInfo, GridPopupMenuType menuType, params Rectangle[] contextBounds)
        {
            if (this.CalculateVisibleItemCount() > 0)
            {
                this.AddCancelButton();
                this.RaisePopupMenuCustomization(this.popupMenu, menuType, hitInfo.RowHandle, hitInfo.CellInfo?.Column);
                if (this.popupMenu.Items.Count != 0)
                {
                    this.ShowContextAdorners(contextBounds);
                    this.popupMenu.Layout(new Rectangle(hitInfo.LocationInView(this), Size.Zero));
                    this.popupMenu.Closed += new EventHandler(this.OnPopupMenuClosed);
                    this.popupMenu.MenuItemClick += new PopupMenuItemClickEventHandler(this.OnPopupMenuMenuItemClick);
                    this.popupMenu.UpdateUI += new EventHandler<MenuItemUpdateUIEventArgs>(this.OnPopupMenuUpdateUI);
                    this.popupMenu.Show();
                    this.isPopupMenuOpened = true;
                }
            }
        }

        public void SortBy(GridColumn column)
        {
            this.SortBy(column, ColumnSortOrder.Ascending);
        }

        public void SortBy(string fieldName)
        {
            this.SortBy(this.Columns[fieldName], ColumnSortOrder.Ascending);
        }

        public void SortBy(GridColumn column, ColumnSortOrder sortOrder)
        {
            this.SortBy(column, sortOrder, 0);
        }

        public void SortBy(GridColumn column, ColumnSortOrder sortOrder, int sortIndex)
        {
            if (column != null)
            {
                column.SortOrder = sortOrder;
                column.SortIndex = sortIndex;
            }
        }

        private void StartLoadMore()
        {
            if (this.LoadMore != null)
            {
                this.LoadMore(this, EventArgs.Empty);
            }
        }

        private void StartPullToRefresh()
        {
            if (this.PullToRefresh != null)
            {
                this.PullToRefresh(this, EventArgs.Empty);
            }
        }

        private void StopRowDragging()
        {
            if (Math.Abs(this.sumDistance) <= (this.SwipeButtonsStategy.MaxButtonsWidth / 2.0))
            {
                this.FinishCurrentDragging();
            }
            else
            {
                double num = this.sumDistance / Math.Abs(this.sumDistance);
                this.draggedRow.MoveRowToDistance(num * (this.SwipeButtonsStategy.MaxButtonsWidth - Math.Abs(this.sumDistance)));
                this.SwipeButtonsStategy.LayoutButtons(this.SwipeButtonsStategy.MaxButtonsWidth, this.draggedRow.get_Bounds().get_Height(), base.get_Width());
                this.sumDistance = num * this.SwipeButtonsStategy.MaxButtonsWidth;
            }
            this.scroller.PreventAndroidScrolling = false;
        }

        private void SubscribeColumnsEvents()
        {
            if (this.columns != null)
            {
                this.columns.add_CollectionChanged(new NotifyCollectionChangedEventHandler(this.OnColumnsCollectionChanged));
                this.columns.ColumnPropertyChanged += new PropertyChangedEventHandler(this.OnColumnPropertyChanged);
            }
        }

        private void SubscribeDataSourceEvents()
        {
            UnboundColumnsDataSource dataSource = this.dataController.DataSource as UnboundColumnsDataSource;
            if (dataSource != null)
            {
                this.SubscribeDataSourceEventsCore(dataSource);
            }
        }

        private void SubscribeDataSourceEventsCore(UnboundColumnsDataSource dataSource)
        {
            dataSource.CreateUnboundFieldFunction += new CreateUnboundFieldFunctionEventHandler(this.OnCreateUnboundFieldFunction);
            dataSource.CreateUnboundFieldSetter += new CreateUnboundFieldFunctionEventHandler(this.OnCreateUnboundFieldSetter);
        }

        private void SubscribeEditFormValuesChangeEvent()
        {
            if (this.editingValuesContainer != null)
            {
                this.editingValuesContainer.Values.OnValueChanged += new EventHandler<ObservableDictionary<string, object>.ObservableDictionaryEventHandlerArgs>(this.OnValueChanged);
            }
        }

        private void SubscribeFilterEvents()
        {
            this.filter.PropertyChanged += new PropertyChangedEventHandler(this.OnFilterPropertyChanged);
        }

        private void SyncUnboundValue(string fieldName)
        {
            UnboundColumnsDataSource dataSource = this.dataController.DataSource as UnboundColumnsDataSource;
            if ((dataSource != null) && (this.openedRowHandle != -2_147_483_648))
            {
                object obj2 = null;
                if (this.editingValuesContainer.Values.TryGetValue(fieldName, out obj2))
                {
                    Action<IRowData, object> action = dataSource.TryGetUnboundFieldSetter(fieldName);
                    EditorRowData data = new EditorRowData(this.GetRow(this.openedRowHandle), this.editingValuesContainer.Values);
                    if ((action != null) && (data != null))
                    {
                        action(data, obj2);
                    }
                }
            }
        }

        private void TapInsideGridControl(GridHitInfo hitInfo)
        {
            this.CloseActivatedSwipeButtons(hitInfo);
        }

        internal void TapIntoRowToSelect(int rowHandle, string fieldName)
        {
            this.SelectedRowHandle = rowHandle;
            if (this.RowTapCommand != null)
            {
                int sourceRowIndex = this.GetSourceRowIndex(rowHandle);
                if (this.RowTapCommand.CanExecute((int)sourceRowIndex))
                {
                    this.RowTapCommand.Execute((int)sourceRowIndex);
                }
            }
            this.RaiseRowTap(rowHandle, fieldName);
        }

        internal bool ToggleGroupRowCollapsed(int rowHandle)
        {
            if (!this.dataController.IsGroupRow(rowHandle))
            {
                return false;
            }
            this.dataController.CollapseGroup(rowHandle, !this.dataController.IsGroupCollapsed(rowHandle));
            this.rowVirtualizer.Invalidate(false, true);
            return true;
        }

        private void TryAutoGenerateColumns()
        {
            if ((this.AutoGenerateColumnsMode != AutoGenerateColumnsMode.None) && (this.autoGeneratedColumns == null))
            {
                this.UnsubscribeColumnsEvents();
                try
                {
                    if (this.AutoGenerateColumnsMode == AutoGenerateColumnsMode.Auto)
                    {
                        if (this.Columns.Count <= 0)
                        {
                            this.AppendAutoGeneratedColumns();
                        }
                        else
                        {
                            this.autoGeneratedColumns = (IList<GridColumn>)new List<GridColumn>();
                        }
                    }
                    else if (this.AutoGenerateColumnsMode == AutoGenerateColumnsMode.Add)
                    {
                        this.AppendAutoGeneratedColumns();
                    }
                    else if (this.AutoGenerateColumnsMode == AutoGenerateColumnsMode.Replace)
                    {
                        this.Columns.Clear();
                        this.AppendAutoGeneratedColumns();
                    }
                }
                finally
                {
                    this.SubscribeColumnsEvents();
                }
                using (IEnumerator<GridColumn> enumerator = this.Columns.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.IsParentReadOnly = this.IsReadOnly;
                    }
                }
                this.UpdateUnboundFields();
                this.UpdateVisibleColumns();
            }
        }

        private IGridDataSource TryCreateDataSource(object newDataSource)
        {
            IGridDataSource source = GridDataSourceFactory.Instance.Create(newDataSource);
            if (source != null)
            {
                return source;
            }
            return GlobalServices.Instance.GetService<IGridDataSourceFactoryService>()?.TryCreateDataSource(newDataSource);
        }

        internal void UnregisterGestureHandlers()
        {
            IGestureRecognizerService service = this.GetService<IGestureRecognizerService>();
            if (service != null)
            {
                service.Unregister(this.headers);
                service.Unregister(this.autoFilterPanelContainer);
                service.Unregister(this.newItemRowContainer);
                service.Unregister(this.scroller);
                service.Unregister(this.totalSummary);
                service.Unregister(this.saveCancelEditingRowControl);
                service.Unregister(this);
            }
        }

        internal void UnregisterTapGestureForView(View view)
        {
            IGestureRecognizerService service = this.GetService<IGestureRecognizerService>();
            if (service != null)
            {
                service.Unregister(view);
            }
        }

        private void UnsubscribeColumnsEvents()
        {
            if (this.columns != null)
            {
                this.columns.remove_CollectionChanged(new NotifyCollectionChangedEventHandler(this.OnColumnsCollectionChanged));
                this.columns.ColumnPropertyChanged -= new PropertyChangedEventHandler(this.OnColumnPropertyChanged);
            }
        }

        private void UnsubscribeDataControllerEvents()
        {
            if (this.newItemRowContentProvider.IsActive && (this.newItemRowContentProvider != null))
            {
                this.newItemRowContentProvider.UnsubscribeDataControllerEvents();
            }
            this.editRowContentProvider.UnsubscribeDataControllerEvents();
        }

        private void UnsubscribeDataSourceEvents()
        {
            UnboundColumnsDataSource dataSource = this.dataController.DataSource as UnboundColumnsDataSource;
            if (dataSource != null)
            {
                dataSource.CreateUnboundFieldFunction -= new CreateUnboundFieldFunctionEventHandler(this.OnCreateUnboundFieldFunction);
                dataSource.CreateUnboundFieldSetter -= new CreateUnboundFieldFunctionEventHandler(this.OnCreateUnboundFieldSetter);
            }
        }

        private void UnsubscribeEditFormValuesChangeEvent()
        {
            if (this.editingValuesContainer != null)
            {
                this.editingValuesContainer.Values.OnValueChanged -= new EventHandler<ObservableDictionary<string, object>.ObservableDictionaryEventHandlerArgs>(this.OnValueChanged);
            }
        }

        private void UnsubscribeFilterEvents()
        {
            this.filter.PropertyChanged -= new PropertyChangedEventHandler(this.OnFilterPropertyChanged);
        }

        private void UpdateCells(bool rearrangeRowsOnly)
        {
            if (rearrangeRowsOnly)
            {
                this.rowVirtualizer.UpdateRowsLayout();
            }
            else
            {
                this.dataRowContentProvider.UpdateLayout();
            }
            this.headerContentProvider.UpdateLayout();
            this.totalSummaryContentProvider.UpdateLayout();
            this.autoFilterPanelContentProvider.UpdateLayout();
            this.newItemRowContentProvider.UpdateLayout();
            if (this.IsEditingRowOpened)
            {
                this.editRowContentProvider.UpdateLayout();
            }
        }

        private void UpdateColumnHeadersHeight()
        {
            this.headerContentProvider.SetRowHeight(this.ActualColumnHeadersHeight);
            base.get_RowDefinitions().get_Item(0).set_Height((GridLength)this.ActualColumnHeadersHeight);
            this.RelayoutDataRows();
        }

        private void UpdateEditRowLayoutInputTransparent(bool value)
        {
            this.editRowLayout.BatchBegin();
            this.editRowLayout.set_InputTransparent(value);
            this.editRowLayout.BatchCommit();
        }

        private void UpdateFilter(CriteriaOperator actualFilterExpression, bool isActive)
        {
            this.filterPanelContentProvider.UpdateContent();
            try
            {
                if (object.Equals(actualFilterExpression, null) || !isActive)
                {
                    this.dataController.Predicate = null;
                }
                else
                {
                    ExpressionEvaluator evaluator = new ExpressionEvaluator(new CustomEvaluatorContextDescriptor((IEnumerable<GridColumn>)this.Columns, this), actualFilterExpression, false);
                    this.dataController.Predicate = delegate (IRowData row) {
                        return evaluator.Fit(row);
                    };
                }
            }
            catch
            {
                this.dataController.Predicate = null;
                this.filterPanelContentProvider.UpdateContent();
            }
        }

        private void UpdateFilterPanelHeight()
        {
            this.FilterPanelRowDefinition.set_Height((GridLength)this.ActualFilterPanelHeight);
            this.filterPanelContentProvider.SetRowHeight(this.ActualFilterPanelHeight);
            this.RelayoutDataRows();
        }

        private void UpdateNewItemRow()
        {
            this.NewItemRowDefinition.set_Height((GridLength)this.GetActualNewItemRowHeight());
            this.newItemRowContentProvider.SetRowHeight(this.GetActualNewItemRowHeight());
            this.RelayoutDataRows();
        }

        private void UpdateSaveCancelRowHeight()
        {
            this.SaveCancelRowDefinition.set_Height(new GridLength(this.GetActualSaveCancelRowHeight(), 0));
            this.saveCancelEditingRowControl.set_IsVisible(this.GetActualSaveCancelRowHeight() > 0.0);
            this.RelayoutDataRows();
        }

        private void UpdateSelectedDataObject()
        {
            IRowData row = this.GetRow(this.SelectedRowHandle);
            if (row != null)
            {
                this.SelectedDataObject = row.DataObject;
            }
            else
            {
                this.SelectedDataObject = null;
            }
        }

        private void UpdateTheme()
        {
            if (!LayoutHelper.CheckElementIsInLogicalTree(this))
            {
                this.isThemeChanged = false;
            }
            else
            {
                base.set_BackgroundColor(this.CurrentTheme.GridControlCustomizer.BorderColor);
                base.set_Padding(this.CurrentTheme.GridControlCustomizer.BorderThickness);
                this.headers.UpdateTheme();
                this.totalSummary.UpdateTheme();
                this.rowsLayout.UpdateTheme();
                this.filterPanelContainer.UpdateTheme();
                this.autoFilterPanelContainer.UpdateTheme();
                this.newItemRowContainer.UpdateTheme();
                this.scroller.set_BackgroundColor(this.CurrentTheme.GridControlCustomizer.BackgroundColor);
                this.columnChooser.UpdateTheme();
                this.saveCancelEditingRowControl.UpdateTheme();
                this.scrollContent.LoadMore.UpdateTheme();
                if (this.IsPullToRefreshEnabled)
                {
                    if (PlatformHelper.Platform == PlatformHelper.Platforms.Android)
                    {
                        this.refreshView.BarColor = this.CurrentTheme.AndroidRefreshCustomizer.BarColor;
                    }
                    else if (PlatformHelper.Platform == PlatformHelper.Platforms.iOS)
                    {
                        this.scroller.RefreshColor = this.CurrentTheme.IOSRefreshCustomizer.RefreshColor;
                    }
                }
                this.isThemeChanged = true;
            }
        }

        private void UpdateTotalSummaryHeight()
        {
            this.TotalSummaryRowDefinition.set_Height((GridLength)this.ActualTotalSummaryHeight);
            this.totalSummaryContentProvider.SetRowHeight(this.ActualTotalSummaryHeight);
            this.RelayoutDataRows();
        }

        private void UpdateUnboundFields()
        {
            UnboundColumnsDataSource dataSource = this.dataController.DataSource as UnboundColumnsDataSource;
            if (dataSource != null)
            {
                dataSource.UpdateUnboundFields(this.Columns);
            }
        }

        private void UpdateUnboundValue(string fieldName)
        {
            UnboundColumnsDataSource dataSource = this.dataController.DataSource as UnboundColumnsDataSource;
            if ((dataSource != null) && (this.openedRowHandle != -2_147_483_648))
            {
                object objA = dataSource.TryGetUnboundField(fieldName)(new EditorRowData(this.GetRow(this.openedRowHandle), this.editingValuesContainer.Values));
                if (!object.Equals(objA, this.editingValuesContainer.Values[fieldName]))
                {
                    this.editingValuesContainer.Values[fieldName] = objA;
                    this.editingValuesContainer.Raise();
                }
            }
        }

        protected void UpdateVisibleColumns()
        {
            this.UpdateVisibleColumns(false);
        }

        protected virtual void UpdateVisibleColumns(bool forceUpdateRows)
        {
            if (this.IsLoaded)
            {
                this.horizontalScrollOffsetCore = 0.0;
                this.FinishCurrentDragging();
                this.CloseEditingRow();
                this.TryAutoGenerateColumns();
                GridColumnCollection columns = this.Columns;
                List<GridColumn> list = new List<GridColumn>();
                List<GridColumn> list2 = new List<GridColumn>();
                List<GridColumn> list3 = new List<GridColumn>();
                int count = columns.Count;
                for (int i = 0; i < count; i++)
                {
                    GridColumn column = columns[i];
                    if (column.IsVisible && this.IsColumnVisibleInHeaders(column))
                    {
                        if (column.FixedStyle == FixedStyle.Left)
                        {
                            list2.Add(column);
                        }
                        else if (column.FixedStyle == FixedStyle.Right)
                        {
                            list3.Add(column);
                        }
                        else
                        {
                            list.Add(column);
                        }
                    }
                }
                list.InsertRange(0, (IEnumerable<GridColumn>)list2);
                list.AddRange((IEnumerable<GridColumn>)list3);
                if (forceUpdateRows || !ListHelper.AreEqual<GridColumn>((IList<GridColumn>)this.visibleColumns, (IList<GridColumn>)list))
                {
                    this.visibleColumns = list;
                    this.CheckGroupingOnVisibleColumns();
                    this.RecalculateVisibleColumnsWidth();
                    this.UpdateColumnHeadersHeight();
                    this.dataRowContentProvider.SetColumns(this.Columns);
                    this.headerContentProvider.SetColumns(this.Columns);
                    this.totalSummaryContentProvider.SetColumns(this.Columns);
                    this.autoFilterPanelContentProvider.SetColumns(this.Columns);
                    this.newItemRowContentProvider.SetColumns(this.Columns);
                    this.editRowContentProvider.SetColumns(this.Columns);
                    this.dataRowContentProvider.SetVisibleColumns((IReadOnlyList<GridColumn>)this.visibleColumns);
                    this.headerContentProvider.SetVisibleColumns((IReadOnlyList<GridColumn>)this.visibleColumns);
                    this.totalSummaryContentProvider.SetVisibleColumns((IReadOnlyList<GridColumn>)this.visibleColumns);
                    this.autoFilterPanelContentProvider.SetVisibleColumns((IReadOnlyList<GridColumn>)this.visibleColumns);
                    this.newItemRowContentProvider.SetVisibleColumns((IReadOnlyList<GridColumn>)this.visibleColumns);
                    this.editRowContentProvider.SetVisibleColumns(this.VisibleColumns);
                }
            }
        }

        internal object XtraCreateColumnsItem(XtraItemEventArgs e)
        {
            if (e.Item.ChildProperties == null)
            {
                return null;
            }
            XtraPropertyInfo info = e.Item.ChildProperties["SerializationTypeName"];
            if ((info == null) || (info.Value == null))
            {
                return null;
            }
            string str = info.Value.ToString();
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            Type type = Type.GetType(str, false);
            return ((type != null) ? (Activator.CreateInstance(type) as GridColumn) : null);
        }

        internal object XtraCreateFormatConditionsItem(XtraItemEventArgs e)
        {
            if (e.Item.ChildProperties == null)
            {
                return null;
            }
            XtraPropertyInfo info = e.Item.ChildProperties["TypeName"];
            if ((info == null) || (info.Value == null))
            {
                return null;
            }
            string str = info.Value.ToString();
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            Type type = Type.GetType(typeof(FormatConditionBase).Namespace + "." + str, false);
            return ((type != null) ? (Activator.CreateInstance(type) as FormatConditionBase) : null);
        }

        internal object XtraCreateGroupSummariesItem(XtraItemEventArgs e) =>
            new GridColumnSummary();

        internal object XtraCreateTotalSummariesItem(XtraItemEventArgs e) =>
            new GridColumnSummary();

        internal void XtraSetIndexColumnsItem(XtraSetItemIndexEventArgs e)
        {
            GridColumn item = e.Item.Value as GridColumn;
            if (item != null)
            {
                this.Columns.Add(item);
            }
        }

        internal void XtraSetIndexFormatConditionsItem(XtraSetItemIndexEventArgs e)
        {
            FormatConditionBase item = e.Item.Value as FormatConditionBase;
            if (item != null)
            {
                this.FormatConditions.Add(item);
            }
        }

        internal void XtraSetIndexGroupSummariesItem(XtraSetItemIndexEventArgs e)
        {
            GridColumnSummary item = e.Item.Value as GridColumnSummary;
            if (item != null)
            {
                this.GroupSummaries.Add(item);
            }
        }

        internal void XtraSetIndexTotalSummariesItem(XtraSetItemIndexEventArgs e)
        {
            GridColumnSummary item = e.Item.Value as GridColumnSummary;
            if (item != null)
            {
                this.TotalSummaries.Add(item);
            }
        }

        // Properties
        internal GestureCommandBindings CommandBindings =>
            this.commandBindings;

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public FormatConditionCollection FormatConditions { get; private set; }

        internal bool IsPopoverEditorOpened =>
            ((this.RowEditMode == RowEditMode.Popup) && ((this.formEditorDialogForm != null) && this.formEditorDialogForm.get_IsVisible()));

        public DataTemplate EditFormContent { get; set; }

        public XlsxExportOptions OptionsExportXlsx
        {
            get =>
                ((XlsxExportOptions)base.GetValue(OptionsExportXlsxProperty));
            set =>
                base.SetValue(OptionsExportXlsxProperty, value);
        }

        public XlsExportOptions OptionsExportXls
        {
            get =>
                ((XlsExportOptions)base.GetValue(OptionsExportXlsProperty));
            set =>
                base.SetValue(OptionsExportXlsProperty, value);
        }

        public CsvExportOptions OptionsExportCsv
        {
            get =>
                ((CsvExportOptions)base.GetValue(OptionsExportCsvProperty));
            set =>
                base.SetValue(OptionsExportCsvProperty, value);
        }

        private RowDefinition AutoFilterRowDefinition =>
            base.get_RowDefinitions().get_Item(2);

        internal static double DefaultFilterPanelHeight
        {
            get
            {
                OnIdiom<double> idiom1 = new OnIdiom<double>();
                idiom1.set_Phone(50.0);
                idiom1.set_Tablet(70.0);
                return idiom1;
            }
        }

        private RowDefinition FilterPanelRowDefinition =>
            base.get_RowDefinitions().get_Item(6);

        [XtraSerializableProperty]
        public double FilterPanelHeight
        {
            get =>
                ((double)((double)base.GetValue(FilterPanelHeightProperty)));
            set =>
                base.SetValue(FilterPanelHeightProperty, (double)value);
        }

        [XtraSerializableProperty]
        public string FilterString
        {
            get =>
                ((string)((string)base.GetValue(FilterStringProperty)));
            set =>
                base.SetValue(FilterStringProperty, value);
        }

        public string ActualFilterString
        {
            get =>
                ((string)((string)base.GetValue(ActualFilterStringProperty)));
            private set =>
                base.SetValue(ActualFilterStringPropertyKey, value);
        }

        public CriteriaOperator FilterExpression
        {
            get =>
                this.filter.FilterExpression;
            set =>
                (this.filter.FilterExpression = value);
        }

        [XtraSerializableProperty]
        public VisibilityState FilterPanelVisibility
        {
            get =>
                ((VisibilityState)base.GetValue(FilterPanelVisibilityProperty));
            set =>
                base.SetValue(FilterPanelVisibilityProperty, value);
        }

        [XtraSerializableProperty]
        public double AutoFilterPanelHeight
        {
            get =>
                ((double)((double)base.GetValue(AutoFilterPanelHeightProperty)));
            set =>
                base.SetValue(AutoFilterPanelHeightProperty, (double)value);
        }

        [XtraSerializableProperty]
        public bool AutoFilterPanelVisibility
        {
            get =>
                ((bool)((bool)base.GetValue(AutoFilterPanelVisibilityProperty)));
            set =>
                base.SetValue(AutoFilterPanelVisibilityProperty, (bool)value);
        }

        protected double ActualFilterPanelHeight
        {
            get
            {
                if ((this.FilterPanelVisibility != VisibilityState.Never) && ((this.FilterPanelVisibility != VisibilityState.Default) || !object.Equals(this.filter.ActualFilterExpression, null)))
                {
                    return this.FilterPanelHeight;
                }
                return 0.0;
            }
        }

        internal GridGestureHandler GestureHandler =>
            this.gestureHandler;

        internal static bool PlatformInitialized
        {
            get =>
                platformInitialized;
            set =>
                (platformInitialized = value);
        }

        internal static bool RendererLoaded
        {
            get =>


                < RendererLoaded > k__BackingField;
            set =>
                (< RendererLoaded > k__BackingField = value);
        }

        public double HorizontalScrollOffset
        {
            get =>
                this.horizontalScrollOffsetCore;
            set
            {
                if ((this.horizontalScrollOffsetCore != value) && this.CheckHorizontalScrollingPossibility((float)(this.horizontalScrollOffsetCore - value)))
                {
                    this.SetHorizontalScrollOffsetCore(value);
                }
            }
        }

        public bool AllowHorizontalScrollingVirtualization
        {
            get =>
                this.allowHorizontalScrollingVirtualization;
            set
            {
                if (this.allowHorizontalScrollingVirtualization != value)
                {
                    this.allowHorizontalScrollingVirtualization = value;
                    this.UpdateVisibleColumns(true);
                }
            }
        }

        private RowDefinition ColumnHeadersRowDefinition =>
            base.get_RowDefinitions().get_Item(0);

        private ThemeBase CurrentTheme =>
            ThemeManager.Theme;

        internal GridColumn CommandColumn { get; set; }

        internal int CommandRowIndex { get; set; }

        internal HeadersContainer Headers =>
            this.headers;

        internal AdornerView Adorner =>
            this.adorner;

        private bool IsLoaded { get; set; }

        private Command SingleTapInGridCommand { get; set; }

        private Command DoubleTapInGridCommand { get; set; }

        private Command LongTapInGridCommand { get; set; }

        internal GridDataController DataController =>
            this.dataController;

        [XtraSerializableProperty]
        public double ColumnHeadersHeight
        {
            get =>
                ((double)((double)base.GetValue(ColumnHeadersHeightProperty)));
            set =>
                base.SetValue(ColumnHeadersHeightProperty, (double)value);
        }

        [XtraSerializableProperty]
        public bool ColumnHeadersVisibility
        {
            get =>
                ((bool)((bool)base.GetValue(ColumnHeadersVisibilityProperty)));
            set =>
                base.SetValue(ColumnHeadersVisibilityProperty, (bool)value);
        }

        protected double ActualColumnHeadersHeight
        {
            get
            {
                if (!this.ColumnHeadersVisibility || (this.VisibleColumns.Count == 0))
                {
                    return 0.0;
                }
                return this.ColumnHeadersHeight;
            }
        }

        [XtraSerializableProperty]
        public double RowHeight
        {
            get =>
                ((double)((double)base.GetValue(RowHeightProperty)));
            set =>
                base.SetValue(RowHeightProperty, (double)value);
        }

        public object ItemsSource
        {
            get =>
                base.GetValue(ItemsSourceProperty);
            set
            {
                if (value == null)
                {
                    base.ClearValue(ItemsSourceProperty);
                }
                else
                {
                    base.SetValue(ItemsSourceProperty, value);
                }
            }
        }

        public int SelectedRowHandle
        {
            get =>
                ((int)((int)base.GetValue(SelectedRowHandleProperty)));
            set =>
                base.SetValue(SelectedRowHandleProperty, (int)value);
        }

        public object SelectedDataObject
        {
            get =>
                base.GetValue(SelectedDataObjectProperty);
            set =>
                base.SetValue(SelectedDataObjectProperty, value);
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public GridColumnCollection Columns
        {
            get
            {
                if (this.columns == null)
                {
                    this.columns = this.CreateColumns();
                    this.SubscribeColumnsEvents();
                }
                return this.columns;
            }
        }

        public IReadOnlyList<GridColumn> VisibleColumns =>
            ((IReadOnlyList<GridColumn>)this.visibleColumns);

        public GridSortMode SortMode
        {
            get =>
                ((GridSortMode)base.GetValue(SortModeProperty));
            set =>
                base.SetValue(SortModeProperty, value);
        }

        [XtraSerializableProperty]
        public bool IsReadOnly
        {
            get =>
                ((bool)((bool)base.GetValue(IsReadOnlyProperty)));
            set =>
                base.SetValue(IsReadOnlyProperty, (bool)value);
        }

        [XtraSerializableProperty]
        public bool AllowSort
        {
            get =>
                ((bool)((bool)base.GetValue(AllowSortProperty)));
            set =>
                base.SetValue(AllowSortProperty, (bool)value);
        }

        [XtraSerializableProperty]
        public bool AllowEditRows
        {
            get =>
                ((bool)((bool)base.GetValue(AllowEditRowsProperty)));
            set =>
                base.SetValue(AllowEditRowsProperty, (bool)value);
        }

        [XtraSerializableProperty]
        public bool AllowDeleteRows
        {
            get =>
                ((bool)((bool)base.GetValue(AllowDeleteRowsProperty)));
            set =>
                base.SetValue(AllowDeleteRowsProperty, (bool)value);
        }

        [XtraSerializableProperty]
        public bool AllowResizeColumns
        {
            get =>
                ((bool)((bool)base.GetValue(AllowResizeColumnsProperty)));
            set =>
                base.SetValue(AllowResizeColumnsProperty, (bool)value);
        }

        [XtraSerializableProperty]
        public bool AllowGroup
        {
            get =>
                ((bool)((bool)base.GetValue(AllowGroupProperty)));
            set =>
                base.SetValue(AllowGroupProperty, (bool)value);
        }

        [XtraSerializableProperty]
        public bool AllowGroupCollapse
        {
            get =>
                ((bool)((bool)base.GetValue(AllowGroupCollapseProperty)));
            set =>
                base.SetValue(AllowGroupCollapseProperty, (bool)value);
        }

        [XtraSerializableProperty]
        public bool IsRowCellMenuEnabled
        {
            get =>
                ((bool)((bool)base.GetValue(IsRowCellMenuEnabledProperty)));
            set =>
                base.SetValue(IsRowCellMenuEnabledProperty, (bool)value);
        }

        [XtraSerializableProperty]
        public bool IsTotalSummaryMenuEnabled
        {
            get =>
                ((bool)((bool)base.GetValue(IsTotalSummaryMenuEnabledProperty)));
            set =>
                base.SetValue(IsTotalSummaryMenuEnabledProperty, (bool)value);
        }

        [XtraSerializableProperty]
        public bool IsColumnMenuEnabled
        {
            get =>
                ((bool)((bool)base.GetValue(IsColumnMenuEnabledProperty)));
            set =>
                base.SetValue(IsColumnMenuEnabledProperty, (bool)value);
        }

        [XtraSerializableProperty]
        public bool IsGroupRowMenuEnabled
        {
            get =>
                ((bool)((bool)base.GetValue(IsGroupRowMenuEnabledProperty)));
            set =>
                base.SetValue(IsGroupRowMenuEnabledProperty, (bool)value);
        }

        [XtraSerializableProperty]
        public bool HighlightMenuTargetElements
        {
            get =>
                ((bool)((bool)base.GetValue(HighlightMenuTargetElementsProperty)));
            set =>
                base.SetValue(HighlightMenuTargetElementsProperty, (bool)value);
        }

        [XtraSerializableProperty]
        public AutoGenerateColumnsMode AutoGenerateColumnsMode
        {
            get =>
                ((AutoGenerateColumnsMode)base.GetValue(AutoGenerateColumnsModeProperty));
            set =>
                base.SetValue(AutoGenerateColumnsModeProperty, value);
        }

        [XtraSerializableProperty]
        public bool IsPullToRefreshEnabled
        {
            get =>
                ((bool)((bool)base.GetValue(IsPullToRefreshEnabledProperty)));
            set =>
                base.SetValue(IsPullToRefreshEnabledProperty, (bool)value);
        }

        public ICommand PullToRefreshCommand
        {
            get =>
                ((ICommand)base.GetValue(PullToRefreshCommandProperty));
            set =>
                base.SetValue(PullToRefreshCommandProperty, value);
        }

        public ICommand LoadMoreCommand
        {
            get =>
                ((ICommand)base.GetValue(LoadMoreCommandProperty));
            set =>
                base.SetValue(LoadMoreCommandProperty, value);
        }

        public ICommand RowTapCommand
        {
            get =>
                ((ICommand)base.GetValue(RowTapCommandProperty));
            set =>
                base.SetValue(RowTapCommandProperty, value);
        }

        [XtraSerializableProperty]
        public bool IsLoadMoreEnabled
        {
            get =>
                ((bool)((bool)base.GetValue(IsLoadMoreEnabledProperty)));
            set =>
                base.SetValue(IsLoadMoreEnabledProperty, (bool)value);
        }

        [XtraSerializableProperty]
        public bool IsColumnChooserEnabled
        {
            get =>
                ((bool)((bool)base.GetValue(IsColumnChooserEnabledProperty)));
            set =>
                base.SetValue(IsColumnChooserEnabledProperty, (bool)value);
        }

        [XtraSerializableProperty]
        public bool GroupsInitiallyExpanded
        {
            get =>
                ((bool)((bool)base.GetValue(GroupsInitiallyExpandedProperty)));
            set =>
                base.SetValue(GroupsInitiallyExpandedProperty, (bool)value);
        }

        [XtraSerializableProperty]
        public RowEditMode RowEditMode
        {
            get =>
                ((RowEditMode)base.GetValue(RowEditModeProperty));
            set =>
                base.SetValue(RowEditModeProperty, value);
        }

        [XtraSerializableProperty]
        public bool ColumnsAutoWidth
        {
            get =>
                ((bool)((bool)base.GetValue(ColumnsAutoWidthProperty)));
            set =>
                base.SetValue(ColumnsAutoWidthProperty, (bool)value);
        }

        internal bool CanShowLoadMorePanel =>
            (this.IsLoadMoreEnabled && ((this.LoadMoreCommand == null) || ((this.LoadMoreCommand != null) && this.LoadMoreCommand.CanExecute(null))));

        bool ICustomCellTextProvider.CanCustomize =>
            (this.customizeCellDisplayText != null);

        private SortingColumnManager SortingColumnManager
        {
            get =>
                this.sortingColumnManager;
            set
            {
                if (!object.ReferenceEquals(this.sortingColumnManager, value))
                {
                    if (this.sortingColumnManager != null)
                    {
                        this.sortingColumnManager.Dispose();
                    }
                    this.sortingColumnManager = value;
                }
            }
        }

        public int RowCount =>
            this.dataController.RowCount;

        public bool IsGrouped =>
            this.DataController.IsGrouped;

        double IHorizontalScrollingData.HorizontalScrollOffset =>
            this.horizontalScrollOffsetCore;

        double IHorizontalScrollingData.VisibleRowWidth =>
            base.get_Width();

        bool IHorizontalScrollingData.ColumnsAutoWidth =>
            this.ColumnsAutoWidth;

        bool IHorizontalScrollingData.AllowHorizontalScrollingVirtualization =>
            this.AllowHorizontalScrollingVirtualization;

        public bool IsLoadedInVisualTree =>
            !this.isDisposed;

        bool IAndroidThemeChanger.IsThemeChanged =>
            this.isThemeChanged;

        private RowDefinition SaveCancelRowDefinition =>
            base.get_RowDefinitions().get_Item(1);

        internal bool IsEditingRowOpened { get; set; }

        [XtraSerializableProperty]
        public bool NewItemRowVisibility
        {
            get =>
                ((bool)((bool)base.GetValue(NewItemRowVisibilityProperty)));
            set =>
                base.SetValue(NewItemRowVisibilityProperty, (bool)value);
        }

        private RowDefinition NewItemRowDefinition =>
            base.get_RowDefinitions().get_Item(3);

        private SwipeButtonsStategy SwipeButtonsStategy { get; set; }

        public ICommand SwipeButtonCommand
        {
            get =>
                ((ICommand)base.GetValue(SwipeButtonCommandProperty));
            set =>
                base.SetValue(SwipeButtonCommandProperty, value);
        }

        private RowDragDirection DragDirection
        {
            get =>
                this.dragDirection;
            set
            {
                if (this.dragDirection != value)
                {
                    this.dragDirection = value;
                    this.OnDragDirectionChanged();
                }
            }
        }

        public ObservableCollection<SwipeButtonInfo> RightSwipeButtons { get; private set; }

        public ObservableCollection<SwipeButtonInfo> LeftSwipeButtons { get; private set; }

        private ServiceContainer ServiceContainer =>
            this.serviceContainer;

        internal static double DefaultTotalSummaryHeight
        {
            get
            {
                OnIdiom<double> idiom1 = new OnIdiom<double>();
                idiom1.set_Phone(50.0);
                idiom1.set_Tablet(70.0);
                return idiom1;
            }
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public ObservableCollection<GridColumnSummary> TotalSummaries { get; private set; }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, true)]
        public ObservableCollection<GridColumnSummary> GroupSummaries { get; private set; }

        private RowDefinition TotalSummaryRowDefinition =>
            base.get_RowDefinitions().get_Item(5);

        [XtraSerializableProperty]
        public double TotalSummaryHeight
        {
            get =>
                ((double)((double)base.GetValue(TotalSummaryHeightProperty)));
            set =>
                base.SetValue(TotalSummaryHeightProperty, (double)value);
        }

        [XtraSerializableProperty]
        public VisibilityState TotalSummaryVisibility
        {
            get =>
                ((VisibilityState)base.GetValue(TotalSummaryVisibilityProperty));
            set =>
                base.SetValue(TotalSummaryVisibilityProperty, value);
        }

        protected double ActualTotalSummaryHeight
        {
            get
            {
                if ((this.TotalSummaryVisibility != VisibilityState.Never) && ((this.TotalSummaryVisibility != VisibilityState.Default) || (this.TotalSummaries.Count != 0)))
                {
                    return this.TotalSummaryHeight;
                }
                return 0.0;
            }
        }

        private class EditorRowData : IRowData
        {
            // Fields
            private readonly IRowData sourceRowData;
            private readonly ObservableDictionary<string, object> values;

            // Methods
            public EditorRowData(IRowData sourceRowData, ObservableDictionary<string, object> values)
            {
                this.sourceRowData = sourceRowData;
                this.values = values;
            }

            public object GetFieldValue(string fieldName)
            {
                object obj2;
                return (!this.values.TryGetValue(fieldName, out obj2) ? this.sourceRowData.GetFieldValue(fieldName) : obj2);
            }

            public T GetFieldValueGeneric<T>(string fieldName)
            {
                object obj2;
                return (!this.values.TryGetValue(fieldName, out obj2) ? this.sourceRowData.GetFieldValueGeneric<T>(fieldName) : ((T)obj2));
            }

            // Properties
            public int RowHandle
            {
                get =>
                    this.sourceRowData.RowHandle;
                set
                {
                }
            }

            public object DataObject =>
                this.sourceRowData.DataObject;
        }

        private class OptionsEventRouter
        {
            // Fields
            private readonly IDataAwareExportOptions options;
            private readonly IDataAwareExportOptions sourceOptions;

            // Methods
            public OptionsEventRouter(IDataAwareExportOptions options, IDataAwareExportOptions sourceOptions)
            {
                this.options = options;
                this.sourceOptions = sourceOptions;
            }

            public void Attach()
            {
                if (this.sourceOptions != null)
                {
                    if (this.sourceOptions.CanRaiseCustomizeCellEvent)
                    {
                        this.options.CustomizeCell += new CustomizeCellEventHandler(this.OnCustomizeCell);
                    }
                    if (this.sourceOptions.CanRaiseCustomizeHeaderEvent)
                    {
                        this.options.CustomizeHeader += new CustomizeHeaderEventHandler(this.OnCustomizeHeader);
                    }
                    if (this.sourceOptions.CanRaiseCustomizeFooterCellEvent)
                    {
                        this.options.CustomizeFooter += new CustomizeFooterEventHandler(this.OnCustomizeFooter);
                    }
                    if (this.sourceOptions.CanRaiseCustomizeDataArea)
                    {
                        this.options.CustomizeDataArea += new CustomizeDataAreaEventHandler(this.OnCustomizeDataArea);
                    }
                    if (this.sourceOptions.CanRaiseCustomizeSheetEvent)
                    {
                        this.options.CustomizeSheet += new CustomizeSheetEventHandler(this.OnCustomizeSheet);
                    }
                    this.options.ExportProgress += new ExportProgressCallback(this.OnExportProgress);
                }
            }

            public void Detach()
            {
                if (this.sourceOptions != null)
                {
                    if (this.sourceOptions.CanRaiseCustomizeCellEvent)
                    {
                        this.options.CustomizeCell -= new CustomizeCellEventHandler(this.OnCustomizeCell);
                    }
                    if (this.sourceOptions.CanRaiseCustomizeHeaderEvent)
                    {
                        this.options.CustomizeHeader -= new CustomizeHeaderEventHandler(this.OnCustomizeHeader);
                    }
                    if (this.sourceOptions.CanRaiseCustomizeFooterCellEvent)
                    {
                        this.options.CustomizeFooter -= new CustomizeFooterEventHandler(this.OnCustomizeFooter);
                    }
                    if (this.sourceOptions.CanRaiseCustomizeDataArea)
                    {
                        this.options.CustomizeDataArea -= new CustomizeDataAreaEventHandler(this.OnCustomizeDataArea);
                    }
                    if (this.sourceOptions.CanRaiseCustomizeSheetEvent)
                    {
                        this.options.CustomizeSheet -= new CustomizeSheetEventHandler(this.OnCustomizeSheet);
                    }
                    this.options.ExportProgress -= new ExportProgressCallback(this.OnExportProgress);
                }
            }

            private void OnCustomizeCell(CustomizeCellEventArgs e)
            {
                this.sourceOptions.RaiseCustomizeCellEvent(e);
            }

            private void OnCustomizeDataArea(CustomizeDataAreaEventArgs e)
            {
                this.sourceOptions.RaiseCustomizeDataAreaEvent(e);
            }

            private void OnCustomizeFooter(CustomizeFooterEventArgs e)
            {
                this.sourceOptions.RaiseCustomizeFooterCellEvent(e);
            }

            private void OnCustomizeHeader(CustomizeHeaderEventArgs e)
            {
                this.sourceOptions.RaiseCustomizeHeaderEvent(e);
            }

            private void OnCustomizeSheet(CustomizeSheetEventArgs e)
            {
                this.sourceOptions.RaiseCustomizeSheetEvent(e);
            }

            private void OnExportProgress(ProgressChangedEventArgs e)
            {
                this.sourceOptions.ReportProgress(e);
            }
        }

        private enum RowDragDirection
        {
            None,
            Right,
            Left
        }
    }
}
