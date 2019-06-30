using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class GridControl : GridLayout, IDisposable, IServiceProvider
    {
        #region Fields
        private HeaderView headerView;
        private HeaderFilterView headerFilter;
        private GridColumnCollection columns;
        private List<GridColumn> visibleColumns;
        private List<object> dataSource;
        public ListView listViewSource;
        public ResourceDictionary resourceDic;
        private Dictionary<int, SortingOrder> _sortingOrders;
        public const double DefaultColumnHeaderHeight = 40;
        public const int DefaultRowHeight = 40;
        public const double DefaultColumnWidth = 120.0;
        public const double DefaultFontSize = 13.0;
        public static readonly BindableProperty ItemsSourceProperty;
        public static readonly BindableProperty IsReadOnlyProperty;
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
        public static readonly BindableProperty ActiveRowColorProperty;
        public static readonly BindableProperty IsSortableProperty;
        public static readonly BindableProperty SortedColumnIndexProperty;
        #endregion

        #region Events
        private void SubscribeColumnEvents()
        {
            if (this.columns != null)
            {
                this.columns.CollectionChanged += Columns_CollectionChanged;
                this.columns.ColumnPropertyChanged += Columns_ColumnPropertyChanged;
            }
        }
        #endregion

        #region Contructor
        static GridControl()
        {
            ItemsSourceProperty = BindingUtils.CreateProperty<GridControl, object>(nameof(ItemsSource), null, OnItemsSourceChanged);
            IsReadOnlyProperty = BindingUtils.CreateProperty<GridControl, bool>(nameof(IsReadOnly), false, OnReadOnlyChanged);
            ActiveRowColorProperty = BindingUtils.CreateProperty<GridControl, Color>(nameof(ActiveRowColor), Color.FromRgb(128, 144, 160));
            HeaderBackgroundProperty = BindingUtils.CreateProperty<GridControl, Color>(nameof(HeaderBackground), Color.White, OnHeaderBackgroundChanged);
            BackgroundDataColorProperty = BindingUtils.CreateProperty<GridControl, Color>(nameof(BackgroundDataColor), Color.Transparent, OnBackgroundDataColorChanged);
            HeaderBordersVisibleProperty = BindingUtils.CreateProperty<GridControl, bool>(nameof(HeaderBordersVisible), true, OnHeaderBordersVisibleChanged);
            BorderThicknessProperty = BindingUtils.CreateProperty<GridControl, Thickness>(nameof(BorderThickness), new Thickness(1), OnBorderThicknessChanged);
            AllowFilterProperty = BindingUtils.CreateProperty<GridControl, bool>(nameof(AllowFilter), true);
            RowHeightProperty = BindingUtils.CreateProperty<GridControl, int>(nameof(RowHeight), DefaultRowHeight, OnRowHeightChanged);
            IsSortableProperty = BindingUtils.CreateProperty<GridControl, bool>(nameof(IsSortable), true);
            Assembly assembly = typeof(GridControl).GetTypeInfo().Assembly;
            string[] ResoureNames = assembly.GetManifestResourceNames();
            AscendingIconProperty = BindingUtils.CreateProperty<GridControl, ImageSource>(nameof(AscendingIcon), ImageSource.FromResource(ResoureNames.Where(x => x.EndsWith("up.png")).FirstOrDefault(), assembly));
            DescendingIconProperty = BindingUtils.CreateProperty<GridControl, ImageSource>(nameof(DescendingIcon), ImageSource.FromResource(ResoureNames.Where(x => x.EndsWith("down.png")).FirstOrDefault(), assembly));
            AscendingIconStyleProperty = BindingUtils.CreateProperty<GridControl, Style>(nameof(AscendingIconStyle), null, OnAscendingIconStyleChanged);
            DescendingIconStyleProperty = BindingUtils.CreateProperty<GridControl, Style>(nameof(DescendingIconStyle), null, OnDescendingIconStyleChanged);
            FontFamilyProperty = BindingUtils.CreateProperty<GridControl, string>(nameof(FontFamily), Font.Default.FontFamily);
            FontSizeProperty = BindingUtils.CreateProperty<GridControl, double>(nameof(FontSize), DefaultFontSize);
            SortedColumnIndexProperty = BindingUtils.CreateProperty<GridControl, SortData>(nameof(SortedColumnIndex), null, OnSortedColumnIndexChanged);
        }
        public GridControl()
        {
            this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(DefaultColumnHeaderHeight) });
            if (AllowFilter)
            {
                this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(DefaultColumnHeaderHeight) });
            }
            this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            _sortingOrders = new Dictionary<int, SortingOrder>();
            visibleColumns = new List<GridColumn>();
            dataSource = new List<object>();
            headerView = new HeaderView(this);
            headerFilter = new HeaderFilterView();
            headerFilter.DataGrid = this;
            InitResourceDictionarys();
            listViewSource = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                BackgroundColor = Color.BlueViolet,
                ItemTemplate = new GridColumnData(),
                SeparatorVisibility = SeparatorVisibility.Default,
                ItemsSource = this.dataSource
            };
            listViewSource.ItemSelected += ListViewSource_ItemSelected;
            listViewSource.Refreshing += ListViewSource_Refreshing;
            listViewSource.SetBinding(ListView.RowHeightProperty, new Binding("RowHeight", source: this));

        }
        #endregion

        #region Methods
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
        private void InitResourceDictionarys()
        {
            if (resourceDic == null) resourceDic = new ResourceDictionary();
            Style headerStyle = new Style(typeof(Label));
            headerStyle.Setters.Add(Label.FontSizeProperty, FontSize);
            headerStyle.Setters.Add(Label.FontAttributesProperty, FontAttributes.Bold);
            headerStyle.Setters.Add(Label.LineBreakModeProperty, LineBreakMode.WordWrap);
            Style imageStyle = new Style(typeof(Image));
            imageStyle.Setters.Add(Image.AspectProperty, Aspect.AspectFill);
            resourceDic.Add(headerStyle);
            resourceDic.Add(imageStyle);
        }
        protected override void OnParentSet()
        {
            if (base.Parent != null)
            {
                UpdateVisibleColumns();
            }
            base.OnParentSet();
        }
        protected virtual GridColumnCollection CreateColumns() => new GridColumnCollection();
        private void UpdateVisibleColumns()
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
                headerView.InitColumns(visibleColumns);
                listViewSource.ItemsSource = this.dataSource;
                this.Children.Add(headerView, 0, 0);
                Grid.SetRow(headerView, 0);
                if (AllowFilter)
                {
                    headerFilter.InitColumns(VisibleColumns);
                    this.Children.Add(headerFilter, 0, 1);
                    Grid.SetRow(headerFilter, 1);
                    Children.Add(listViewSource, 0, 2);
                    Grid.SetRow(listViewSource, 2);
                }
                else
                {
                    Children.Add(listViewSource, 0, 1);
                    Grid.SetRow(listViewSource, 1);
                }
            }
        }
        #endregion

        #region Method Event
        private void Columns_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (GridColumn item in e.NewItems)
                {
                    item.BindingContext = base.BindingContext;
                    item.IsParentReadOnly = this.IsReadOnly;
                }
            }
        }
        private void Columns_ColumnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsVisible")
            {

            }
            if (e.PropertyName == "IsReadOnly")
            {

            }
            if (e.PropertyName == GridColumn.WidthProperty.PropertyName)
            {

            }
        }
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                ((GridControl)bindable).dataSource = new List<object>(((IEnumerable<object>)newValue).Cast<object>());
            }
            catch
            {

            }
        }
        private static void OnReadOnlyChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            ((GridControl)bindable).Columns.ToList().ForEach(x => x.IsParentReadOnly = newValue);
        }
        private static void OnBackgroundDataColorChanged(BindableObject bindable, Color oldValue, Color newValue)
        {
            GridControl safe = bindable as GridControl;
        }
        private static void OnRowHeightChanged(BindableObject bindable, int oldValue, int newValue)
        {
            GridControl safe = bindable as GridControl;
            safe.listViewSource.RowHeight = newValue;
        }
        private static void OnHeaderBackgroundChanged(BindableObject bindable, Color oldValue, Color newValue)
        {
            GridControl safe = bindable as GridControl;
            safe.headerView.BackgroundColor = newValue;
        }
        private static void OnHeaderBordersVisibleChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            if (newValue)
            {
                GridControl safe = bindable as GridControl;
                safe.headerView.Padding = safe.BorderThickness.HorizontalThickness / 2;
                safe.headerView.ColumnSpacing = safe.BorderThickness.HorizontalThickness / 2;
            }
        }
        private static void OnBorderThicknessChanged(BindableObject bindable, Thickness oldValue, Thickness newValue)
        {
            GridControl safe = bindable as GridControl;
            if (safe.HeaderBordersVisible)
            {
                safe.headerView.Padding = newValue.HorizontalThickness / 2;
                safe.headerView.ColumnSpacing = newValue.HorizontalThickness / 2;
            }
        }
        private static void OnDescendingIconStyleChanged(BindableObject bindable, Style oldValue, Style newValue)
        {
            GridControl self = bindable as GridControl;
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
            GridControl self = bindable as GridControl;
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
            GridControl self = bindable as GridControl;
            if(oldValue != newValue)
            {
                self.SortItems(newValue);
            }
        }
        private void ListViewSource_Refreshing(object sender, EventArgs e)
        {

        }
        private void ListViewSource_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
        private void HeaderFilter_textChanged(object sender, TextChangedEventArgs e)
        {
            HeaderFilterView view = sender as HeaderFilterView;
            if (this.dataSource != null)
            {
                List<object> lstSources = new List<object>();
                listViewSource.ItemsSource = GetDataSource(view.FieldNameSelected, this.dataSource, 0, ref lstSources);
            }
        }
        private List<object> GetDataSource(Dictionary<string, object> lstColumns, List<object> lstDatas, int idx, ref List<object> lstSources)
        {
            string col = lstColumns.Keys.ElementAtOrDefault(idx);
            if (!string.IsNullOrEmpty(col))
            {
                object val = lstColumns[col];
                lstSources.AddRange(lstDatas.Where(x => Convert.ToString(ObjectUtils.GetPropertyValue(x, col)).Contains(Convert.ToString(val))));
                GetDataSource(lstColumns, lstSources, idx + 1, ref lstSources);
            }
            return lstSources;
        }
        internal void SortItems(SortData sData)
        {
            if (dataSource == null || sData.Index >= Columns.Count || Columns[sData.Index].AllowSort == DefaultBoolean.False)
                return;

            var items = dataSource;
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
        public IReadOnlyList<GridColumn> VisibleColumns => (IReadOnlyList<GridColumn>)this.visibleColumns;
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
        public List<object> DataSource
        {
            get => this.dataSource;
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
        #endregion
        private enum RowDragDirection
        {
            None,
            Right,
            Left
        }
    }
}
