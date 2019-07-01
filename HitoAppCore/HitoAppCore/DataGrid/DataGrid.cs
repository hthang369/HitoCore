using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms.DataGrid.Utils;

namespace Xamarin.Forms.DataGrid
{
    public class DataGrid : GridLayout, IDisposable
    {
        #region Fields
        private GridColumnCollection columns;
        public const int DefaultRowHeight = 40;
        public const double DefaultFontSize = 13.0;
        public const double DefaultColumnHeaderHeight = 40;
        public const double DefaultColumnWidth = 120.0;
        IList<object> _internalItems;
        Dictionary<int, SortingOrder> _sortingOrders;
        ListView _listView;
        private HeaderView _headerView;
        private HeaderFilterView _headerFilterView;
        private List<GridColumn> visibleColumns;
        private ContentView _noDataView;
        public static readonly BindableProperty ActiveRowColorProperty;
        public static readonly BindableProperty HeaderBackgroundProperty;
        public static readonly BindableProperty BackgroundDataColorProperty;
        public static readonly BindableProperty AllowFilterProperty;
        public static readonly BindableProperty HeaderBordersVisibleProperty;
        public static readonly BindableProperty BorderThicknessProperty;
        public static readonly BindableProperty AscendingIconProperty;
        public static readonly BindableProperty DescendingIconProperty;
        public static readonly BindableProperty AscendingIconStyleProperty;
        public static readonly BindableProperty DescendingIconStyleProperty;
        public static readonly BindableProperty RowHeightProperty;
        public static readonly BindableProperty FontFamilyProperty;
        public static readonly BindableProperty FontSizeProperty;
        public static readonly BindableProperty IsSortableProperty;
        public static readonly BindableProperty SortedColumnIndexProperty;
        public static readonly BindableProperty ItemsSourceProperty;
        public static readonly BindableProperty IsReadOnlyProperty;
        public static readonly BindableProperty SelectedItemProperty;
        public static readonly BindableProperty SelectionEnabledProperty;
        public static readonly BindableProperty PullToRefreshCommandProperty;
        public static readonly BindableProperty IsRefreshingProperty;
        public static readonly BindableProperty HeaderLabelStyleProperty;
        public static readonly BindableProperty RowsBackgroundColorPaletteProperty;
        public static readonly BindableProperty RowsTextColorPaletteProperty;
        public static readonly BindableProperty NoDataViewProperty;
        #endregion

        #region Events
        public event EventHandler Refreshing;
        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;
        #endregion

        #region Contructor
        static DataGrid()
        {
            ActiveRowColorProperty = BindingUtils.CreateProperty<DataGrid, Color>(nameof(ActiveRowColor), Color.FromRgb(128, 144, 160));
            HeaderBackgroundProperty = BindingUtils.CreateProperty<DataGrid, Color>(nameof(HeaderBackground), Color.White, OnHeaderBackgroundChanged);
            BackgroundDataColorProperty = BindingUtils.CreateProperty<DataGrid, Color>(nameof(BackgroundDataColor), Color.Transparent, OnBackgroundDataColorChanged);
            HeaderBordersVisibleProperty = BindingUtils.CreateProperty<DataGrid, bool>(nameof(HeaderBordersVisible), true, OnHeaderBordersVisibleChanged);
            BorderThicknessProperty = BindingUtils.CreateProperty<DataGrid, Thickness>(nameof(BorderThickness), new Thickness(1), OnBorderThicknessChanged);
            AllowFilterProperty = BindingUtils.CreateProperty<DataGrid, bool>(nameof(AllowFilter), false);
            RowHeightProperty = BindingUtils.CreateProperty<DataGrid, int>(nameof(RowHeight), DefaultRowHeight, OnRowHeightChanged);
            IsSortableProperty = BindingUtils.CreateProperty<DataGrid, bool>(nameof(IsSortable), true);
            Assembly assembly = typeof(DataGrid).GetTypeInfo().Assembly;
            string[] ResoureNames = assembly.GetManifestResourceNames();
            AscendingIconProperty = BindingUtils.CreateProperty<DataGrid, ImageSource>(nameof(AscendingIcon), ImageSource.FromResource(ResoureNames.Where(x => x.EndsWith("up.png")).FirstOrDefault(), assembly));
            DescendingIconProperty = BindingUtils.CreateProperty<DataGrid, ImageSource>(nameof(DescendingIcon), ImageSource.FromResource(ResoureNames.Where(x => x.EndsWith("down.png")).FirstOrDefault(), assembly));
            AscendingIconStyleProperty = BindingUtils.CreateProperty<DataGrid, Style>(nameof(AscendingIconStyle), null, OnAscendingIconStyleChanged);
            DescendingIconStyleProperty = BindingUtils.CreateProperty<DataGrid, Style>(nameof(DescendingIconStyle), null, OnDescendingIconStyleChanged);
            FontFamilyProperty = BindingUtils.CreateProperty<DataGrid, string>(nameof(FontFamily), Font.Default.FontFamily);
            FontSizeProperty = BindingUtils.CreateProperty<DataGrid, double>(nameof(FontSize), DefaultFontSize);
            SortedColumnIndexProperty = BindingUtils.CreateProperty<DataGrid, SortData>(nameof(SortedColumnIndex), null, OnSortedColumnIndexChanged, null, OnSortedColumnIndexValidate);
            ItemsSourceProperty = BindingUtils.CreateProperty<DataGrid, object>(nameof(ItemsSource), null, OnItemsSourceChanged);
            IsReadOnlyProperty = BindingUtils.CreateProperty<DataGrid, bool>(nameof(IsReadOnly), false, OnReadOnlyChanged);
            HeaderLabelStyleProperty = BindingUtils.CreateProperty<DataGrid, Style>(nameof(HeaderLabelStyle));
            SelectedItemProperty = BindingUtils.CreateProperty<DataGrid, object>(nameof(SelectedItem),null, OnSelectedItemChanged, OnSelectedItemCoerceValue);
            SelectionEnabledProperty = BindingUtils.CreateProperty<DataGrid, bool>(nameof(SelectionEnabled), true, OnSelectionEnabledChanged);
            PullToRefreshCommandProperty = BindingUtils.CreateProperty<DataGrid, ICommand>(nameof(PullToRefreshCommand), null, OnPullToRefreshCommandChanged);
            IsRefreshingProperty = BindingUtils.CreateProperty<DataGrid, bool>(nameof(IsRefreshing), false, OnIsRefreshingChanged);
            RowsBackgroundColorPaletteProperty = BindingUtils.CreateProperty<DataGrid, IColorProvider>(nameof(RowsBackgroundColorPalette), new PaletteCollection { default(Color) }, OnRowsBackgroundColorPaletteChanged);
            RowsTextColorPaletteProperty = BindingUtils.CreateProperty<DataGrid, IColorProvider>(nameof(RowsTextColorPalette), new PaletteCollection { Color.Black }, OnRowsTextColorPaletteChanged);
        }

        public DataGrid()
        {
            this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(DefaultColumnHeaderHeight) });
            if (AllowFilter)
            {
                this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(DefaultColumnHeaderHeight) });
            }
            this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            _sortingOrders = new Dictionary<int, SortingOrder>();
            _headerView = new HeaderView(this);
            _headerFilterView = new HeaderFilterView();
            InitResourceDictionarys();
            _listView = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                BackgroundColor = Color.Transparent,
                ItemTemplate = new DataGridRowTemplateSelector(),
                SeparatorVisibility = SeparatorVisibility.None,
            };

            _listView.ItemSelected += ListViewSource_ItemSelected;
            _listView.Refreshing += ListViewSource_Refreshing;
            _listView.SetBinding(ListView.RowHeightProperty, new Binding("RowHeight", source: this));
        }
        #endregion

        #region Methods
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        protected virtual GridColumnCollection CreateColumns() => new GridColumnCollection();
        private void InitResourceDictionarys()
        {
            if (_headerView.Resources == null) _headerView.Resources = new ResourceDictionary();
            Style headerStyle = new Style(typeof(Label));
            headerStyle.Setters.Add(Label.FontSizeProperty, FontSize);
            headerStyle.Setters.Add(Label.FontAttributesProperty, FontAttributes.Bold);
            headerStyle.Setters.Add(Label.LineBreakModeProperty, LineBreakMode.WordWrap);
            Style imageStyle = new Style(typeof(Image));
            imageStyle.Setters.Add(Image.AspectProperty, Aspect.AspectFill);
            _headerView.Resources.Add(headerStyle);
            _headerView.Resources.Add(imageStyle);
        }
        protected override void OnParentSet()
        {
            if (base.Parent != null)
            {
                InitDataGridView();
            }
            base.OnParentSet();
        }
        private void InitDataGridView()
        {
            List<GridColumn> lst = new List<GridColumn>();
            List<GridColumn> lst1 = new List<GridColumn>();
            List<GridColumn> lst2 = new List<GridColumn>();
            this.Columns.ToList().ForEach(x =>
            {
                if (x.IsVisible)
                {
                    x.SortIndex = Columns.IndexOf(x);
                    if (x.FixedStyle == FixedStyle.Left) lst1.Add(x);
                    else if (x.FixedStyle == FixedStyle.Right) lst2.Add(x);
                    else lst.Add(x);
                }
            });
            lst.InsertRange(0, lst1);
            lst.AddRange(lst2);
            if (!ListHelper.AreEqual<GridColumn>(visibleColumns, lst))
            {
                visibleColumns = lst;
                _headerView.InitColumns(visibleColumns);
                this.Children.Add(_headerView, 0, 0);
                Grid.SetRow(_headerView, 0);
                if (AllowFilter)
                {
                    _headerFilterView.InitColumns(visibleColumns);
                    this.Children.Add(_headerFilterView, 0, 1);
                    Grid.SetRow(_headerFilterView, 1);
                    Children.Add(_listView, 0, 2);
                    Grid.SetRow(_listView, 2);
                }
                else
                {
                    Children.Add(_listView, 0, 1);
                    Grid.SetRow(_listView, 1);
                }
            }
        }
        private void Reload()
        {
            InternalItems = new List<object>(_internalItems);
        }
        internal void SortItems(SortData sData)
        {
            if (InternalItems == null || sData.Index >= Columns.Count || Columns[sData.Index].AllowSort != DefaultBoolean.True)
                return;

            var items = InternalItems;
            var column = Columns[sData.Index];
            SortingOrder order = sData.Order;

            if (!IsSortable)
                throw new InvalidOperationException("This DataGrid is not sortable");
            else if (column.FieldName == null)
                throw new InvalidOperationException("Please set the FieldName property of Column");

            //Sort
            if (order == SortingOrder.Descendant)
                items = items.OrderByDescending(x => ReflectionUtils.GetValueByPath(x, column.FieldName)).ToList();
            else
                items = items.OrderBy(x => ReflectionUtils.GetValueByPath(x, column.FieldName)).ToList();

            column.SortingIcon.Style = (order == SortingOrder.Descendant) ?
                AscendingIconStyle ?? (Style)_headerView.Resources["DescendingIconStyle"] :
                DescendingIconStyle ?? (Style)_headerView.Resources["AscendingIconStyle"];

            //Support DescendingIcon property (if setted)
            if (!column.SortingIcon.Style.Setters.Any(x => x.Property == Image.SourceProperty))
            {
                if (order == SortingOrder.Descendant && DescendingIconProperty.DefaultValue != DescendingIcon)
                    column.SortingIcon.Source = DescendingIcon;
                if (order == SortingOrder.Ascendant && AscendingIconProperty.DefaultValue != AscendingIcon)
                    column.SortingIcon.Source = AscendingIcon;
            }

            for (int i = 0; i < Columns.Count; i++)
            {
                if (i != sData.Index)
                {
                    if (Columns[i].SortingIcon.Style != null)
                        Columns[i].SortingIcon.Style = null;
                    if (Columns[i].SortingIcon.Source != null)
                        Columns[i].SortingIcon.Source = null;
                    _sortingOrders[i] = SortingOrder.None;
                }
            }

            _internalItems = items;

            _sortingOrders[sData.Index] = order;
            SortedColumnIndex = sData;

            _listView.ItemsSource = _internalItems;
        }
        void HandleItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            InternalItems = new List<object>(((IEnumerable<object>)sender).Cast<object>());
            if (SelectedItem != null && !InternalItems.Contains(SelectedItem))
                SelectedItem = null;
        }
        #endregion

        #region Method Events
        private void ListViewSource_Refreshing(object sender, EventArgs e)
        {
            Refreshing?.Invoke(this, e);
        }
        private void ListViewSource_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (SelectionEnabled)
                SelectedItem = _listView.SelectedItem;
            else
                _listView.SelectedItem = null;

            ItemSelected?.Invoke(this, e);
        }
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                DataGrid self = bindable as DataGrid;
                if (oldValue != null && oldValue is INotifyCollectionChanged)
                    (oldValue as INotifyCollectionChanged).CollectionChanged -= self.HandleItemsSourceCollectionChanged;
                if(newValue != null)
                {
                    if(newValue is INotifyCollectionChanged)
                        (newValue as INotifyCollectionChanged).CollectionChanged += self.HandleItemsSourceCollectionChanged;

                    self.InternalItems = new List<object>(((IEnumerable<object>)newValue).Cast<object>());
                }

                if (self.SelectedItem != null && !self.InternalItems.Contains(self.SelectedItem))
                    self.SelectedItem = null;

                if (self.NoDataView != null)
                {
                    if (self.ItemsSource == null || self.InternalItems.Count() == 0)
                        self._noDataView.IsVisible = true;
                    else if (self._noDataView.IsVisible)
                        self._noDataView.IsVisible = false;
                }
            }
            catch
            {
            }
        }
        private static void OnReadOnlyChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            ((DataGrid)bindable).Columns.ToList().ForEach(x => x.IsParentReadOnly = newValue);
        }
        private static void OnBackgroundDataColorChanged(BindableObject bindable, Color oldValue, Color newValue)
        {
            DataGrid safe = bindable as DataGrid;
        }
        private static void OnRowHeightChanged(BindableObject bindable, int oldValue, int newValue)
        {
            DataGrid safe = bindable as DataGrid;
            safe._listView.RowHeight = newValue;
        }
        private static void OnHeaderBackgroundChanged(BindableObject bindable, Color oldValue, Color newValue)
        {
            DataGrid safe = bindable as DataGrid;
            if(safe._headerView != null && !safe.HeaderBordersVisible)
                safe._headerView.BackgroundColor = newValue;
        }
        private static void OnHeaderBordersVisibleChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            DataGrid safe = bindable as DataGrid;
            safe.BackgroundColor = newValue ? safe.HeaderBackground : Color.White;
        }
        private static void OnBorderThicknessChanged(BindableObject bindable, Thickness oldValue, Thickness newValue)
        {
            DataGrid safe = bindable as DataGrid;
            if (safe.HeaderBordersVisible)
            {
                safe._headerView.Padding = newValue.HorizontalThickness / 2;
                safe._headerView.ColumnSpacing = newValue.HorizontalThickness / 2;
            }
        }
        private static void OnDescendingIconStyleChanged(BindableObject bindable, Style oldValue, Style newValue)
        {
            DataGrid self = bindable as DataGrid;
            var style = (newValue as Style).Setters.FirstOrDefault(x => x.Property == Image.SourceProperty);
            if (style != null)
            {
                if (style.Value is string vs)
                    self.DescendingIcon = ImageSource.FromFile(vs);
                else
                    self.DescendingIcon = (ImageSource)style.Value;
            }
        }
        private static void OnAscendingIconStyleChanged(BindableObject bindable, Style oldValue, Style newValue)
        {
            DataGrid self = bindable as DataGrid;
            if ((newValue as Style).Setters.Any(x => x.Property == Image.SourceProperty))
            {
                var style = (newValue as Style).Setters.FirstOrDefault(x => x.Property == Image.SourceProperty);
                if (style != null)
                {
                    if (style.Value is string vs)
                        self.AscendingIcon = ImageSource.FromFile(vs);
                    else
                        self.AscendingIcon = (ImageSource)style.Value;
                }
            }
        }
        private static void OnSortedColumnIndexChanged(BindableObject bindable, SortData oldValue, SortData newValue)
        {
            DataGrid self = bindable as DataGrid;
            if (oldValue != newValue)
            {
                self.SortItems(newValue);
            }
        }
        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            DataGrid self = bindable as DataGrid;
            if (self._listView.SelectedItem != newValue)
                self._listView.SelectedItem = newValue;
        }
        private static object OnSelectedItemCoerceValue(BindableObject bindable, object value)
        {
            DataGrid self = bindable as DataGrid;
            if (!self.SelectionEnabled && value != null)
                throw new InvalidOperationException("Datagrid must be SelectionEnabled=true to set SelectedItem");
            if (self.InternalItems != null && self.InternalItems.Contains(value))
                return value;
            else
                return null;
        }
        private static void OnSelectionEnabledChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            DataGrid self = bindable as DataGrid;
            if (!self.SelectionEnabled && self.SelectedItem != null)
                self.SelectedItem = null;
        }
        private static void OnIsRefreshingChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            (bindable as DataGrid)._listView.IsRefreshing = newValue;
        }
        private static void OnPullToRefreshCommandChanged(BindableObject bindable, ICommand oldValue, ICommand newValue)
        {
            DataGrid self = bindable as DataGrid;
            if (newValue == null)
            {
                self._listView.IsPullToRefreshEnabled = false;
                self._listView.RefreshCommand = null;
            }
            else
            {
                self._listView.IsPullToRefreshEnabled = true;
                self._listView.RefreshCommand = newValue;
            }
        }
        private static bool OnSortedColumnIndexValidate(BindableObject bindable, SortData value)
        {
            DataGrid self = bindable as DataGrid;
            return value == null ||
                self.Columns == null ||
                self.Columns.Count == 0 ||
                (value.Index < self.Columns.Count && self.Columns.ElementAt(value.Index).AllowSort == DefaultBoolean.True);
        }
        private static void OnRowsTextColorPaletteChanged(BindableObject bindable, IColorProvider oldValue, IColorProvider newValue)
        {
            DataGrid self = bindable as DataGrid;
            if (self.Columns != null && self.ItemsSource != null)
                self.Reload();
        }
        private static void OnRowsBackgroundColorPaletteChanged(BindableObject bindable, IColorProvider oldValue, IColorProvider newValue)
        {
            DataGrid self = bindable as DataGrid;
            if (self.Columns != null && self.ItemsSource != null)
                self.Reload();
        }
        #endregion

        #region Properties
        public GridColumnCollection Columns
        {
            get
            {
                if (this.columns == null)
                {
                    this.columns = this.CreateColumns();
                }
                return this.columns;
            }
        }
        public IReadOnlyList<GridColumn> VisibleColumns => (IReadOnlyList<GridColumn>)this.visibleColumns;
        public object ItemsSource
        {
            get => base.GetValue(ItemsSourceProperty);
            set
            {
                if (value == null) base.ClearValue(ItemsSourceProperty);
                else base.SetValue(ItemsSourceProperty, value);
            }
        }
        public bool IsReadOnly
        {
            get => (bool)base.GetValue(IsReadOnlyProperty);
            set => base.SetValue(IsReadOnlyProperty, value);
        }
        public Color HeaderBackground
        {
            get => (Color)base.GetValue(HeaderBackgroundProperty);
            set => base.SetValue(HeaderBackgroundProperty, value);
        }
        public Color BackgroundDataColor
        {
            get => (Color)base.GetValue(BackgroundDataColorProperty);
            set => base.SetValue(BackgroundDataColorProperty, value);
        }
        public bool AllowFilter
        {
            get => (bool)base.GetValue(AllowFilterProperty);
            set => base.SetValue(AllowFilterProperty, value);
        }
        public bool HeaderBordersVisible
        {
            get => (bool)base.GetValue(HeaderBordersVisibleProperty);
            set => base.SetValue(HeaderBordersVisibleProperty, value);
        }
        public Thickness BorderThickness
        {
            get => (Thickness)base.GetValue(BorderThicknessProperty);
            set => base.SetValue(BorderThicknessProperty, value);
        }
        public ImageSource AscendingIcon
        {
            get => (ImageSource)base.GetValue(AscendingIconProperty);
            set => base.SetValue(AscendingIconProperty, value);
        }
        public ImageSource DescendingIcon
        {
            get => (ImageSource)base.GetValue(DescendingIconProperty);
            set => base.SetValue(DescendingIconProperty, value);
        }
        public int RowHeight
        {
            get => (int)base.GetValue(RowHeightProperty);
            set => base.SetValue(RowHeightProperty, value);
        }
        public string FontFamily
        {
            get => (string)base.GetValue(FontFamilyProperty);
            set => base.SetValue(FontFamilyProperty, value);
        }
        public double FontSize
        {
            get => (double)base.GetValue(FontSizeProperty);
            set => base.SetValue(FontSizeProperty, value);
        }
        public Color ActiveRowColor
        {
            get => (Color)base.GetValue(ActiveRowColorProperty);
            set => base.SetValue(ActiveRowColorProperty, value);
        }
        public bool IsSortable
        {
            get => (bool)base.GetValue(IsSortableProperty);
            set => base.SetValue(IsSortableProperty, value);
        }
        public Style AscendingIconStyle
        {
            get => (Style)base.GetValue(AscendingIconStyleProperty);
            set => base.SetValue(AscendingIconStyleProperty, value);
        }
        public Style DescendingIconStyle
        {
            get => (Style)base.GetValue(DescendingIconStyleProperty);
            set => base.SetValue(DescendingIconStyleProperty, value);
        }
        public SortData SortedColumnIndex
        {
            get => (SortData)base.GetValue(SortedColumnIndexProperty);
            set => base.SetValue(SortedColumnIndexProperty, value);
        }
        public bool SelectionEnabled
        {
            get { return (bool)GetValue(SelectionEnabledProperty); }
            set { SetValue(SelectionEnabledProperty, value); }
        }
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        public ICommand PullToRefreshCommand
        {
            get { return (ICommand)GetValue(PullToRefreshCommandProperty); }
            set { SetValue(PullToRefreshCommandProperty, value); }
        }
        public bool IsRefreshing
        {
            get { return (bool)GetValue(IsRefreshingProperty); }
            set { SetValue(IsRefreshingProperty, value); }
        }
        public Style HeaderLabelStyle
        {
            get { return (Style)GetValue(HeaderLabelStyleProperty); }
            set { SetValue(HeaderLabelStyleProperty, value); }
        }
        internal IList<object> InternalItems
        {
            get { return _internalItems; }
            set
            {
                _internalItems = value;

                if (IsSortable && SortedColumnIndex != null)
                    SortItems(SortedColumnIndex);
                else
                    _listView.ItemsSource = _internalItems;
            }
        }
        public IColorProvider RowsBackgroundColorPalette
        {
            get { return (IColorProvider)GetValue(RowsBackgroundColorPaletteProperty); }
            set { SetValue(RowsBackgroundColorPaletteProperty, value); }
        }
        public IColorProvider RowsTextColorPalette
        {
            get { return (IColorProvider)GetValue(RowsTextColorPaletteProperty); }
            set { SetValue(RowsTextColorPaletteProperty, value); }
        }
        public View NoDataView
        {
            get { return (View)GetValue(NoDataViewProperty); }
            set { SetValue(NoDataViewProperty, value); }
        }
        public HeaderView headerView
        {
            get => _headerView;
        }
        #endregion
    }
}
