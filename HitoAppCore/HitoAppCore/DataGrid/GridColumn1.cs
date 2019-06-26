using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public abstract class GridColumn1 : BindableObject//, ILayoutCalculatorItem, IFieldValueVisitor
    {
        // Fields
        internal const int defaultSortIndex = -1;
        internal const int singleModeSortIndex = 1;
        public static readonly BindableProperty FieldNameProperty;
        public static readonly BindableProperty CaptionProperty;
        public static readonly BindableProperty WidthProperty;
        public static readonly BindableProperty IsReadOnlyProperty;
        public static readonly BindableProperty UnboundTypeProperty;
        public static readonly BindableProperty UnboundExpressionProperty;
        public static readonly BindableProperty ContentAlignmentProperty;
        public static readonly BindableProperty MinWidthProperty;
        private static readonly BindablePropertyKey ActualWidthPropertyKey;
        public static readonly BindableProperty ActualWidthProperty;
        public static readonly BindableProperty IsGroupedProperty;
        public static readonly BindableProperty IsVisibleProperty;
        public static readonly BindableProperty SortOrderProperty;
        public static readonly BindableProperty SortModeProperty;
        public static readonly BindableProperty AllowSortProperty;
        public static readonly BindableProperty AllowGroupProperty;
        public static readonly BindableProperty DisplayFormatProperty;
        public static readonly BindableProperty SortIndexProperty;
        public static readonly BindableProperty AutoFilterConditionProperty;
        public static readonly BindableProperty AutoFilterValueProperty;
        public static readonly BindableProperty AllowAutoFilterProperty;
        public static readonly BindableProperty ColumnFilterModeProperty;
        public static readonly BindableProperty ImmediateUpdateAutoFilterProperty;
        public static readonly BindableProperty GroupIntervalProperty;
        public static readonly BindableProperty HeaderTemplateProperty;
        public static readonly BindableProperty FixedStyleProperty;
        //private PropertyAccessor propertyAccessor;
        //private SortDescriptor<IRowData> comparer;
        //private IComparer<IRowData> ascendingComparer;
        //private GroupDescriptor<IRowData> groupByComparer;
        // Events
        [CompilerGenerated]
        internal event PropertyChangedEventHandler AfterPropertyChanged;

        // Methods
        static GridColumn()
        {
            FieldNameProperty = BindingUtils.CreateProperty<GridColumn, string>(nameof(FieldName), string.Empty, OnFieldNameChanged);
            CaptionProperty = BindingUtils.CreateProperty<GridColumn, string>(nameof(Caption), string.Empty, OnCaptionChanged);
            WidthProperty = BindingUtils.CreateProperty<GridColumn, double>(nameof(Width), double.NaN, OnWidthChanged);
            IsReadOnlyProperty = BindingUtils.CreateProperty<GridColumn, bool>(nameof(IsReadOnly), false, OnIsReadOnlyChanged);
            UnboundTypeProperty = BindingUtils.CreateProperty<GridColumn, UnboundColumnType>(nameof(UnboundType), UnboundColumnType.Bound, OnUnboundTypeChanged);
            UnboundExpressionProperty = BindingUtils.CreateProperty<GridColumn, string>(nameof(UnboundExpression), string.Empty, OnUnboundExpressionChanged);
            //ContentAlignmentProperty = BindingUtils.CreateProperty<GridColumn, ColumnContentAlignment>(nameof(ContentAlignment), ColumnContentAlignment.Start, OnContentAlignmentChanged);
            MinWidthProperty = BindingUtils.CreateProperty<GridColumn, double>(nameof(MinWidth), 50.0, OnMinWidthChanged);
            ActualWidthPropertyKey = BindingUtils.CreateReadOnlyProperty<GridColumn, double>(nameof(ActualWidth), double.NaN);
            ActualWidthProperty = ActualWidthPropertyKey.BindableProperty;
            IsGroupedProperty = BindingUtils.CreateProperty<GridColumn, bool>(nameof(IsGrouped), false, OnIsGroupedChanged);
            IsVisibleProperty = BindingUtils.CreateProperty<GridColumn, bool>(nameof(IsVisible), true, OnIsVisibleChanged);
            SortOrderProperty = BindingUtils.CreateProperty<GridColumn, ColumnSortOrder>(nameof(SortOrder), ColumnSortOrder.None, OnSortOrderChanged);
            SortModeProperty = BindingUtils.CreateProperty<GridColumn, ColumnSortMode>(nameof(SortMode), ColumnSortMode.DisplayText, OnSortModeChanged);
            AllowSortProperty = BindingUtils.CreateProperty<GridColumn, DefaultBoolean>(nameof(AllowSort), DefaultBoolean.Default, OnAllowSortChanged);
            AllowGroupProperty = BindingUtils.CreateProperty<GridColumn, DefaultBoolean>(nameof(AllowGroup), DefaultBoolean.Default, OnAllowGroupChanged);
            DisplayFormatProperty = BindingUtils.CreateProperty<GridColumn, string>(nameof(DisplayFormat), null, OnDisplayFormatChanged);
            SortIndexProperty = BindingUtils.CreateProperty<GridColumn, int>(nameof(SortIndex), -1, OnSortIndexChanged);
            //AutoFilterConditionProperty = BindingUtils.CreateProperty<GridColumn, AutoFilterCondition>(nameof(AutoFilterCondition), AutoFilterCondition.Default, OnAutoFilterConditionChanged);
            AutoFilterValueProperty = BindingUtils.CreateProperty<GridColumn, object>(nameof(AutoFilterValue), null, OnAutoFilterValueChanged);
            AllowAutoFilterProperty = BindingUtils.CreateProperty<GridColumn, bool>(nameof(AllowAutoFilter), true, OnAllowAutoFilterChanged);
            //ColumnFilterModeProperty = BindingUtils.CreateProperty<GridColumn, ColumnFilterMode>(nameof(ColumnFilterMode), ColumnFilterMode.Value, OnColumnFilterModeChanged);
            ImmediateUpdateAutoFilterProperty = BindingUtils.CreateProperty<GridColumn, bool>(nameof(ImmediateUpdateAutoFilter), true, OnImmediateUpdateAutoFilterChanged);
            //GroupIntervalProperty = BindingUtils.CreateProperty<GridColumn, ColumnGroupInterval>(nameof(GroupInterval), ColumnGroupInterval.Default, OnGroupIntervalChanged);
            HeaderTemplateProperty = BindingUtils.CreateProperty<GridColumn, DataTemplate>(nameof(HeaderTemplate), null, OnHeaderTemplateChanged);
            FixedStyleProperty = BindingUtils.CreateProperty<GridColumn, FixedStyle>(nameof(FixedStyle), FixedStyle.None, OnFixedStyleChanged);
        }

        public GridColumn()
        {
            this.SortMode = ColumnSortMode.Value;
        }

        internal bool CalculateActualAllowGroup(bool controlAllowGroup, bool controlAllowSort) =>
            (this.CalculateActualAllowSort(controlAllowSort) && this.CalculateActualPropertyCore(controlAllowGroup, this.AllowGroup));

        internal bool CalculateActualAllowSort(bool controlAllowSort) =>
            this.CalculateActualPropertyCore(controlAllowSort, this.AllowSort);

        //protected virtual ColumnGroupInterval CalculateActualColumnGroupInterval()
        //{
        //    if ((this.GroupInterval != ColumnGroupInterval.Default) && ((this.GroupInterval == ColumnGroupInterval.DisplayText) || (this.GroupInterval == ColumnGroupInterval.Alphabetical)))
        //    {
        //        return this.GroupInterval;
        //    }
        //    return ColumnGroupInterval.Value;
        //}

        private bool CalculateActualPropertyCore(bool controlPropertyValue, DefaultBoolean columnPropertyValue) =>
            ((columnPropertyValue != DefaultBoolean.Default) ? (columnPropertyValue == DefaultBoolean.True) : controlPropertyValue);

        //internal bool CellTextCanBeCustomized() =>
        //    (string.IsNullOrEmpty(this.DisplayFormat) ? ((this.CustomCellTextProvider != null) && this.CustomCellTextProvider.CanCustomize) : true);

        internal void ChangeSortOrder()
        {
            switch (this.SortOrder)
            {
                case ColumnSortOrder.None:
                    this.SortOrder = ColumnSortOrder.Ascending;
                    return;

                case ColumnSortOrder.Ascending:
                    this.SortOrder = ColumnSortOrder.Descending;
                    return;

                case ColumnSortOrder.Descending:
                    this.SortOrder = ColumnSortOrder.None;
                    return;
            }
        }

        //protected SortDescriptor<IRowData> CreateAscendingComparer() =>
        //    this.CreateSortDescriptorCore(ColumnSortOrder.Ascending);

        //protected SortDescriptor<IRowData> CreateComparer() =>
        //    this.CreateSortDescriptorCore(this.SortOrder);

        //private IComparer<IRowData> CreateComparer(Type propertyType, bool isAscending)
        //{
        //    if ((propertyType == null) || (propertyType == typeof(object)))
        //    {
        //        return new NumbersComparer(this.FieldName, isAscending, this.SortMode, this);
        //    }
        //    return ((propertyType != typeof(string)) ? ((propertyType != typeof(int)) ? ((propertyType != typeof(DateTime)) ? ((propertyType != typeof(DateTime?)) ? ((propertyType != typeof(bool)) ? ((propertyType != typeof(long)) ? ((propertyType != typeof(float)) ? ((propertyType != typeof(double)) ? ((propertyType != typeof(decimal)) ? ((propertyType != typeof(byte)) ? ((propertyType != typeof(short)) ? ((propertyType != typeof(uint)) ? ((propertyType != typeof(sbyte)) ? ((propertyType != typeof(ulong)) ? ((IComparer<IRowData>)new NumbersComparer(this.FieldName, isAscending, this.SortMode, this)) : ((IComparer<IRowData>)new ColumnComparer<ulong>(this.FieldName, isAscending, this.SortMode, this))) : ((IComparer<IRowData>)new ColumnComparer<sbyte>(this.FieldName, isAscending, this.SortMode, this))) : ((IComparer<IRowData>)new ColumnComparer<uint>(this.FieldName, isAscending, this.SortMode, this))) : ((IComparer<IRowData>)new ColumnComparer<short>(this.FieldName, isAscending, this.SortMode, this))) : ((IComparer<IRowData>)new ColumnComparer<byte>(this.FieldName, isAscending, this.SortMode, this))) : ((IComparer<IRowData>)new ColumnComparer<decimal>(this.FieldName, isAscending, this.SortMode, this))) : ((IComparer<IRowData>)new ColumnComparer<double>(this.FieldName, isAscending, this.SortMode, this))) : ((IComparer<IRowData>)new ColumnComparer<float>(this.FieldName, isAscending, this.SortMode, this))) : ((IComparer<IRowData>)new ColumnComparer<long>(this.FieldName, isAscending, this.SortMode, this))) : ((IComparer<IRowData>)new ColumnComparer<bool>(this.FieldName, isAscending, this.SortMode, this))) : ((IComparer<IRowData>)new ColumnComparer<DateTime?>(this.FieldName, isAscending, this.SortMode, this))) : ((IComparer<IRowData>)new ColumnComparer<DateTime>(this.FieldName, isAscending, this.SortMode, this))) : ((IComparer<IRowData>)new ColumnComparer<int>(this.FieldName, isAscending, this.SortMode, this))) : ((IComparer<IRowData>)new ColumnComparer<string>(this.FieldName, isAscending, this.SortMode, this)));
        //}

        //protected GroupDescriptor<IRowData> CreateGroupByComparer()
        //{
        //    RowDataGroupDescriptor descriptor = new RowDataGroupDescriptor();
        //    this.SetupSortDescriptorCore(descriptor, (this.SortOrder == ColumnSortOrder.Descending) ? ColumnSortOrder.Descending : ColumnSortOrder.Ascending);
        //    descriptor.DisplayFormat = this.DisplayFormat;
        //    descriptor.GroupInterval = this.CalculateActualColumnGroupInterval();
        //    return descriptor;
        //}

        //private SortDescriptor<IRowData> CreateSortDescriptorCore(ColumnSortOrder sortOrder)
        //{
        //    SortDescriptor<IRowData> descriptor = new SortDescriptor<IRowData>();
        //    this.SetupSortDescriptorCore(descriptor, sortOrder);
        //    return descriptor;
        //}

        //protected internal virtual LabelControl CreateTotalSummaryLabel()
        //{
        //    LabelControl label = new LabelControl();
        //    label.set_Text((string)new string('W', 10));
        //    label.set_LineBreakMode(4);
        //    label.set_VerticalOptions(LayoutOptions.FillAndExpand);
        //    label.set_HorizontalOptions(LayoutOptions.FillAndExpand);
        //    label.set_HorizontalTextAlignment(this.GetTextContentAlignment());
        //    label.set_VerticalTextAlignment(1);
        //    //ThemeManager.Theme.TotalSummaryCustomizer.Font.ApplyToLabel(label);
        //    return label;
        //}

        //internal Func<IRowData, object> CreateUnboundFieldFunction()
        //{
        //    if (string.IsNullOrEmpty(this.UnboundExpression))
        //    {
        //        return null;
        //    }
        //    CriteriaOperator criteria = CriteriaOperator.Parse(this.UnboundExpression, Array.Empty<object>());
        //    ExpressionEvaluator evaluator = new ExpressionEvaluator(new CustomEvaluatorContextDescriptor(this), criteria, false);
        //    return delegate (IRowData row) {
        //        return evaluator.Evaluate(row);
        //    };
        //}

        //object IFieldValueVisitor.GetFieldValue(IRowData rowData) =>
        //    this.GetFieldValueForVisitor(rowData, false);

        //object IFieldValueVisitor.GetFormattedFieldValue(IRowData rowData) =>
        //    this.GetFieldValueForVisitor(rowData, true);

        internal virtual ColumnSortMode GetActualSortModeForVisitor()
        {
            //if ((this.SortMode != ColumnSortMode.DisplayText) || !this.CellTextCanBeCustomized())
            //{
            //    return ColumnSortMode.Value;
            //}
            return ColumnSortMode.DisplayText;
        }

        protected abstract Type GetComparerPropertyType();
        internal LayoutOptions GetControlContentAlignment(bool isExpand = false)
        {
            if (isExpand)
            {
                switch (this.ContentAlignment)
                {
                    case ColumnContentAlignment.Start:
                        return LayoutOptions.StartAndExpand;

                    case ColumnContentAlignment.Center:
                        return LayoutOptions.CenterAndExpand;

                    case ColumnContentAlignment.End:
                        return LayoutOptions.EndAndExpand;
                }
                return LayoutOptions.StartAndExpand;
            }
            switch (this.ContentAlignment)
            {
                case ColumnContentAlignment.Start:
                    return LayoutOptions.Start;

                case ColumnContentAlignment.Center:
                    return LayoutOptions.Center;

                case ColumnContentAlignment.End:
                    return LayoutOptions.End;
            }
            return LayoutOptions.Start;
        }

        internal virtual Type GetFieldTypeForVisitor()
        {
            //if ((this.SortMode != ColumnSortMode.DisplayText) || !this.CellTextCanBeCustomized())
            //{
            //    return this.GetPreferredDataType();
            //}
            return typeof(string);
        }

        //internal virtual object GetFieldValueForVisitor(IRowData rowData, bool format)
        //{
        //    if (((IFieldValueVisitor)this).ActualSortMode != ColumnSortMode.DisplayText)
        //    {
        //        return null;
        //    }
        //    object fieldValue = rowData.GetFieldValue(this.FieldName);
        //    if (format && (fieldValue != null))
        //    {
        //        fieldValue = CustomizeCellTextHelper.Format(fieldValue, this.DisplayFormat, rowData.RowHandle, this.FieldName, this.CustomCellTextProvider);
        //    }
        //    return fieldValue;
        //}

        internal static LayoutOptions GetIconContentAlignment(LayoutOptions align)
        {
            if (align.Equals(LayoutOptions.StartAndExpand))
            {
                return LayoutOptions.EndAndExpand;
            }
            if (align.Equals(LayoutOptions.EndAndExpand) || align.Equals(LayoutOptions.CenterAndExpand))
            {
                return LayoutOptions.StartAndExpand;
            }
            if (align.Equals(LayoutOptions.Start))
            {
                return LayoutOptions.EndAndExpand;
            }
            if (align.Equals(LayoutOptions.End) || align.Equals(LayoutOptions.Center))
            {
                return LayoutOptions.StartAndExpand;
            }
            return align;
        }

        internal Type GetPreferredDataType()
        {
            if (!this.IsUnbound)
            {
                return this.GetComparerPropertyType();
            }
            switch (this.UnboundType)
            {
                case UnboundColumnType.Integer:
                    return typeof(int);

                case UnboundColumnType.Decimal:
                    return typeof(decimal);

                case UnboundColumnType.DateTime:
                    return typeof(DateTime);

                case UnboundColumnType.String:
                    return typeof(string);

                case UnboundColumnType.Boolean:
                    return typeof(bool);
            }
            return typeof(object);
        }

        internal TextAlignment GetTextContentAlignment() =>
            ((TextAlignment)this.ContentAlignment);

        internal static bool IsPropertyAffectsFilter(string propertyName) =>
            ((propertyName == "DisplayFormat") || ((propertyName == "FieldName") || ((propertyName == "AutoFilterCondition") || ((propertyName == "AutoFilterValue") || ((propertyName == "AllowAutoFilter") || ((propertyName == "ColumnFilterMode") || ((propertyName == "ImmediateUpdateAutoFilter") || ((propertyName == "DisplayMember") || (propertyName == "ValueMember")))))))));

        internal static bool IsPropertyAffectsUnboundFields(string propertyName) =>
            (propertyName == "UnboundExpression");

        private static void OnAllowAutoFilterChanged(BindableObject obj, bool oldValue, bool newValue)
        {
            ((GridColumn1)obj).RaiseAfterPropertyChanged("AllowAutoFilter");
        }

        private static void OnAllowGroupChanged(BindableObject obj, DefaultBoolean oldValue, DefaultBoolean newValue)
        {
            ((GridColumn)obj).RaiseAfterPropertyChanged("AllowGroup");
        }

        private static void OnAllowSortChanged(BindableObject obj, DefaultBoolean oldValue, DefaultBoolean newValue)
        {
            ((GridColumn)obj).RaiseAfterPropertyChanged("AllowSort");
        }

        //private static void OnAutoFilterConditionChanged(BindableObject obj, AutoFilterCondition oldValue, AutoFilterCondition newValue)
        //{
        //    ((GridColumn)obj).RaiseAfterPropertyChanged("AutoFilterCondition");
        //}

        private static void OnAutoFilterValueChanged(BindableObject obj, object oldValue, object newValue)
        {
            ((GridColumn)obj).RaiseAfterPropertyChanged("AutoFilterValue");
        }

        protected void OnCaptionChanged()
        {
            this.OnPropertyChanged("ActualCaption");
        }

        private static void OnCaptionChanged(BindableObject obj, string oldValue, string newValue)
        {
            GridColumn column1 = obj as GridColumn;
            column1.OnCaptionChanged();
            column1.RaiseAfterPropertyChanged("Caption");
        }

        //private static void OnColumnFilterModeChanged(BindableObject obj, ColumnFilterMode oldValue, ColumnFilterMode newValue)
        //{
        //    ((GridColumn)obj).RaiseAfterPropertyChanged("ColumnFilterMode");
        //}

        private static void OnContentAlignmentChanged(BindableObject obj, ColumnContentAlignment oldValue, ColumnContentAlignment newValue)
        {
            GridColumn column1 = obj as GridColumn;
            column1.OnLayoutPropertyChanged();
            column1.RaiseAfterPropertyChanged("ContentAlignment");
        }

        protected internal void OnDisplayFormatChanged()
        {
            this.ResetGroupByComparer();
        }

        private static void OnDisplayFormatChanged(BindableObject obj, string oldValue, string newValue)
        {
            GridColumn column1 = obj as GridColumn;
            column1.OnDisplayFormatChanged();
            column1.RaiseAfterPropertyChanged("DisplayFormat");
        }

        protected void OnFieldNameChanged()
        {
            //this.ResetComparer();
            //this.ResetAscendingComparer();
            this.ResetGroupByComparer();
            this.OnPropertyChanged("ActualCaption");
        }

        private static void OnFieldNameChanged(BindableObject obj, string oldValue, string newValue)
        {
            GridColumn column1 = obj as GridColumn;
            column1.OnFieldNameChanged();
            column1.RaiseAfterPropertyChanged("FieldName");
        }

        private static void OnFixedStyleChanged(BindableObject obj, FixedStyle oldValue, FixedStyle newValue)
        {
            ((GridColumn)obj).RaiseAfterPropertyChanged("FixedStyle");
        }

        private void OnGroupIntervalChanged()
        {
            this.ResetGroupByComparer();
        }

        //private static void OnGroupIntervalChanged(BindableObject obj, ColumnGroupInterval oldValue, ColumnGroupInterval newValue)
        //{
        //    GridColumn column1 = obj as GridColumn;
        //    column1.OnGroupIntervalChanged();
        //    column1.RaiseAfterPropertyChanged("GroupInterval");
        //}

        private static void OnHeaderTemplateChanged(BindableObject obj, DataTemplate oldValue, DataTemplate newValue)
        {
            ((GridColumn)obj).RaiseAfterPropertyChanged("HeaderTemplate");
        }

        private static void OnImmediateUpdateAutoFilterChanged(BindableObject obj, bool oldValue, bool newValue)
        {
            ((GridColumn)obj).RaiseAfterPropertyChanged("ImmediateUpdateAutoFilter");
        }

        private void OnIsGroupedChanged(bool oldValue, bool newValue)
        {
        }

        private static void OnIsGroupedChanged(BindableObject obj, bool oldValue, bool newValue)
        {
            GridColumn column1 = obj as GridColumn;
            column1.OnIsGroupedChanged(oldValue, newValue);
            column1.RaiseAfterPropertyChanged("IsGrouped");
        }

        private static void OnIsReadOnlyChanged(BindableObject obj, bool oldValue, bool newValue)
        {
            ((GridColumn)obj).RaiseAfterPropertyChanged("IsReadOnly");
        }

        private static void OnIsVisibleChanged(BindableObject obj, bool oldValue, bool newValue)
        {
            ((GridColumn)obj).RaiseAfterPropertyChanged("IsVisible");
        }

        private void OnLayoutPropertyChanged()
        {
        }

        private static void OnMinWidthChanged(BindableObject obj, double oldValue, double newValue)
        {
            GridColumn column1 = obj as GridColumn;
            column1.OnLayoutPropertyChanged();
            column1.RaiseAfterPropertyChanged("MinWidth");
        }

        private static void OnSortIndexChanged(BindableObject obj, int oldValue, int newValue)
        {
            ((GridColumn)obj).RaiseAfterPropertyChanged("SortIndex");
        }

        private void OnSortModeChanged(ColumnSortMode oldValue, ColumnSortMode newValue)
        {
            //if (this.SortComparer != null)
            //{
            //    this.SortComparer.SortMode = newValue;
            //}
            //if (this.GroupComparer != null)
            //{
            //    this.GroupComparer.SortMode = newValue;
            //}
            //this.ResetComparer();
            //this.ResetAscendingComparer();
            this.ResetGroupByComparer();
        }

        private static void OnSortModeChanged(BindableObject obj, ColumnSortMode oldValue, ColumnSortMode newValue)
        {
            GridColumn column1 = obj as GridColumn;
            column1.OnSortModeChanged(oldValue, newValue);
            column1.RaiseAfterPropertyChanged("SortMode");
        }

        private void OnSortOrderChanged(ColumnSortOrder oldValue, ColumnSortOrder newValue)
        {
            //this.ResetComparer();
            this.ResetGroupByComparer();
        }

        private static void OnSortOrderChanged(BindableObject obj, ColumnSortOrder oldValue, ColumnSortOrder newValue)
        {
            GridColumn column1 = obj as GridColumn;
            column1.OnSortOrderChanged(oldValue, newValue);
            column1.RaiseAfterPropertyChanged("SortOrder");
        }

        private void OnUnboundChanged()
        {
        }

        private static void OnUnboundExpressionChanged(BindableObject obj, string oldValue, string newValue)
        {
            GridColumn column1 = obj as GridColumn;
            column1.OnUnboundChanged();
            column1.RaiseAfterPropertyChanged("UnboundExpression");
        }

        protected virtual void OnUnboundTypeChanged()
        {
            this.OnUnboundChanged();
        }

        private static void OnUnboundTypeChanged(BindableObject obj, UnboundColumnType oldValue, UnboundColumnType newValue)
        {
            GridColumn column1 = obj as GridColumn;
            column1.OnUnboundTypeChanged();
            column1.RaiseAfterPropertyChanged("UnboundType");
        }

        private static void OnWidthChanged(BindableObject obj, double oldValue, double newValue)
        {
            GridColumn column1 = obj as GridColumn;
            column1.OnLayoutPropertyChanged();
            column1.RaiseAfterPropertyChanged("Width");
        }

        protected void RaiseAfterPropertyChanged(string propertyName)
        {
            if (this.AfterPropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                this.AfterPropertyChanged(this, args);
            }
        }

        //private void ResetAscendingComparer()
        //{
        //    this.ascendingComparer = null;
        //}

        //private void ResetComparer()
        //{
        //    this.comparer = null;
        //}

        internal void ResetComparers()
        {
            //this.ResetComparer();
            //this.ResetAscendingComparer();
            this.ResetGroupByComparer();
        }

        protected void ResetGroupByComparer()
        {
            //this.groupByComparer = null;
        }

        //protected internal virtual void SetPropertyAccessor(PropertyAccessor accessor)
        //{
        //    this.propertyAccessor = accessor;
        //}

        //private void SetupSortDescriptorCore(SortDescriptor<IRowData> descriptor, ColumnSortOrder sortOrder)
        //{
        //    descriptor.FieldName = this.FieldName;
        //    descriptor.FieldType = this.GetComparerPropertyType();
        //    descriptor.AscendingComparer = (this.ascendingComparer == null) ? this.CreateComparer(descriptor.FieldType, true) : this.ascendingComparer;
        //    this.ascendingComparer = descriptor.AscendingComparer;
        //    descriptor.FieldValueVisitor = this;
        //    descriptor.SortOrder = sortOrder;
        //    descriptor.SortMode = this.SortMode;
        //}

        //protected internal abstract void Visit(IGridColumnVisitor visitor);

        // Properties
        protected internal bool IsAutoGenerated { get; set; }

        //protected internal PropertyAccessor PropertyAccessor =>
        //    this.propertyAccessor;

        protected internal virtual bool ActualAllowGroupCore =>
            false;

        //protected internal virtual bool ActualIsReadOnly =>
        //    (!this.IsParentReadOnly ? ((this.propertyAccessor == null) ? this.IsReadOnly : (this.propertyAccessor.IsReadOnly || this.IsReadOnly)) : true);

        internal bool IsParentReadOnly { get; set; }

        //internal ICustomCellTextProvider CustomCellTextProvider { get; set; }

        //protected virtual AutoFilterCondition DefaultAutoFilterCondition =>
        //    AutoFilterCondition.Like;

        //internal AutoFilterCondition ActualAutoFilterCondition =>
        //    ((this.AutoFilterCondition != AutoFilterCondition.Default) ? this.AutoFilterCondition : this.DefaultAutoFilterCondition);

        public bool IsUnbound =>
            ((this.UnboundType != UnboundColumnType.Bound) || !string.IsNullOrEmpty(this.UnboundExpression));

        ////[XtraSerializableProperty]
        public ColumnSortMode SortMode
        {
            get =>
                ((ColumnSortMode)base.GetValue(SortModeProperty));
            set =>
                base.SetValue(SortModeProperty, value);
        }

        ////[XtraSerializableProperty]
        public string FieldName
        {
            get =>
                ((string)((string)base.GetValue(FieldNameProperty)));
            set =>
                base.SetValue(FieldNameProperty, value);
        }

        ////[XtraSerializableProperty]
        public string Caption
        {
            get =>
                ((string)((string)base.GetValue(CaptionProperty)));
            set =>
                base.SetValue(CaptionProperty, value);
        }

        //public string ActualCaption =>
        //    (!string.IsNullOrEmpty(this.Caption) ? this.Caption : SplitStringHelper.SplitPascalCaseString(this.FieldName));

        ////[XtraSerializableProperty]
        public double Width
        {
            get =>
                ((double)((double)base.GetValue(WidthProperty)));
            set =>
                base.SetValue(WidthProperty, (double)value);
        }

        //[XtraSerializableProperty]
        public bool IsReadOnly
        {
            get =>
                ((bool)((bool)base.GetValue(IsReadOnlyProperty)));
            set =>
                base.SetValue(IsReadOnlyProperty, (bool)value);
        }

        //[XtraSerializableProperty]
        public ColumnContentAlignment ContentAlignment
        {
            get =>
                ((ColumnContentAlignment)base.GetValue(ContentAlignmentProperty));
            set =>
                base.SetValue(ContentAlignmentProperty, value);
        }

        //[XtraSerializableProperty]
        public UnboundColumnType UnboundType
        {
            get =>
                ((UnboundColumnType)base.GetValue(UnboundTypeProperty));
            set =>
                base.SetValue(UnboundTypeProperty, value);
        }

        //[XtraSerializableProperty]
        public string UnboundExpression
        {
            get =>
                ((string)((string)base.GetValue(UnboundExpressionProperty)));
            set =>
                base.SetValue(UnboundExpressionProperty, value);
        }

        //[XtraSerializableProperty]
        public double MinWidth
        {
            get =>
                ((double)((double)base.GetValue(MinWidthProperty)));
            set =>
                base.SetValue(MinWidthProperty, (double)value);
        }

        public double ActualWidth
        {
            get =>
                ((double)((double)base.GetValue(ActualWidthProperty)));
            protected set =>
                base.SetValue(ActualWidthPropertyKey, (double)value);
        }

        //[XtraSerializableProperty]
        public bool IsGrouped
        {
            get =>
                ((bool)((bool)base.GetValue(IsGroupedProperty)));
            set =>
                base.SetValue(IsGroupedProperty, (bool)value);
        }

        //[XtraSerializableProperty]
        public bool IsVisible
        {
            get =>
                ((bool)((bool)base.GetValue(IsVisibleProperty)));
            set =>
                base.SetValue(IsVisibleProperty, (bool)value);
        }

        //[XtraSerializableProperty]
        public ColumnSortOrder SortOrder
        {
            get =>
                ((ColumnSortOrder)base.GetValue(SortOrderProperty));
            set =>
                base.SetValue(SortOrderProperty, value);
        }

        //[XtraSerializableProperty]
        public DefaultBoolean AllowSort
        {
            get =>
                ((DefaultBoolean)base.GetValue(AllowSortProperty));
            set =>
                base.SetValue(AllowSortProperty, value);
        }

        //[XtraSerializableProperty]
        public DefaultBoolean AllowGroup
        {
            get =>
                ((DefaultBoolean)base.GetValue(AllowGroupProperty));
            set =>
                base.SetValue(AllowGroupProperty, value);
        }

        //[XtraSerializableProperty]
        public string DisplayFormat
        {
            get =>
                ((string)((string)base.GetValue(DisplayFormatProperty)));
            set =>
                base.SetValue(DisplayFormatProperty, value);
        }

        //[XtraSerializableProperty]
        public int SortIndex
        {
            get =>
                ((int)((int)base.GetValue(SortIndexProperty)));
            set =>
                base.SetValue(SortIndexProperty, (int)value);
        }

        //[XtraSerializableProperty]
        //public AutoFilterCondition AutoFilterCondition
        //{
        //    get =>
        //        ((AutoFilterCondition)base.GetValue(AutoFilterConditionProperty));
        //    set =>
        //        base.SetValue(AutoFilterConditionProperty, value);
        //}

        //[XtraSerializableProperty]
        public object AutoFilterValue
        {
            get =>
                base.GetValue(AutoFilterValueProperty);
            set =>
                base.SetValue(AutoFilterValueProperty, value);
        }

        //[XtraSerializableProperty]
        public bool AllowAutoFilter
        {
            get =>
                ((bool)((bool)base.GetValue(AllowAutoFilterProperty)));
            set =>
                base.SetValue(AllowAutoFilterProperty, (bool)value);
        }

        //[XtraSerializableProperty]
        public bool ImmediateUpdateAutoFilter
        {
            get =>
                ((bool)((bool)base.GetValue(ImmediateUpdateAutoFilterProperty)));
            set =>
                base.SetValue(ImmediateUpdateAutoFilterProperty, (bool)value);
        }

        //[XtraSerializableProperty]
        //public ColumnFilterMode ColumnFilterMode
        //{
        //    get =>
        //        ((ColumnFilterMode)base.GetValue(ColumnFilterModeProperty));
        //    set =>
        //        base.SetValue(ColumnFilterModeProperty, value);
        //}

        //[XtraSerializableProperty]
        //public ColumnGroupInterval GroupInterval
        //{
        //    get =>
        //        ((ColumnGroupInterval)base.GetValue(GroupIntervalProperty));
        //    set =>
        //        base.SetValue(GroupIntervalProperty, value);
        //}

        public DataTemplate HeaderTemplate
        {
            get =>
                ((DataTemplate)base.GetValue(HeaderTemplateProperty));
            set =>
                base.SetValue(HeaderTemplateProperty, value);
        }

        //[XtraSerializableProperty]
        public FixedStyle FixedStyle
        {
            get =>
                ((FixedStyle)base.GetValue(FixedStyleProperty));
            set =>
                base.SetValue(FixedStyleProperty, value);
        }

        //[Browsable(false), EditorBrowsable((EditorBrowsableState)EditorBrowsableState.Never), XtraSerializableProperty(XtraSerializationFlags.None)]
        public string SerializationTypeName =>
            base.GetType().FullName;

        //internal GroupDescriptor<IRowData> GroupComparer
        //{
        //    get
        //    {
        //        if (!this.IsGrouped)
        //        {
        //            return null;
        //        }
        //        if (this.groupByComparer == null)
        //        {
        //            this.groupByComparer = this.CreateGroupByComparer();
        //        }
        //        return this.groupByComparer;
        //    }
        //}

        //internal SortDescriptor<IRowData> SortComparer
        //{
        //    get
        //    {
        //        if (this.SortOrder == ColumnSortOrder.None)
        //        {
        //            return null;
        //        }
        //        if (this.comparer == null)
        //        {
        //            this.comparer = this.CreateComparer();
        //        }
        //        return this.comparer;
        //    }
        //}

        //double ILayoutCalculatorItem.Width
        //{
        //    get =>
        //        this.Width;
        //    set
        //    {
        //        if (this.Width != value)
        //        {
        //            this.Width = value;
        //        }
        //    }
        //}

        //double ILayoutCalculatorItem.MinWidth =>
        //    this.MinWidth;

        //double ILayoutCalculatorItem.ActualWidth
        //{
        //    get =>
        //        this.ActualWidth;
        //    set
        //    {
        //        if (this.ActualWidth != value)
        //        {
        //            this.ActualWidth = value;
        //        }
        //    }
        //}

        //Type IFieldValueVisitor.FieldValueType =>
        //    this.GetFieldTypeForVisitor();

        //ColumnSortMode IFieldValueVisitor.ActualSortMode =>
        //    this.GetActualSortModeForVisitor();

        //internal class ColumnComparer<T> : IComparer<IRowData>, GridColumn.IColumnComparer
        //{
        //    // Methods
        //    //public ColumnComparer(string fieldName, bool ascending, ColumnSortMode sortMode, IFieldValueVisitor fieldValueVisitor)
        //    //{
        //    //    this.FieldName = fieldName;
        //    //    this.Ascending = ascending;
        //    //    this.UseGenericMethods = true;
        //    //    this.SortMode = sortMode;
        //    //    this.FieldValueVisitor = fieldValueVisitor;
        //    //}

        //    //public int Compare(IRowData x, IRowData y)
        //    //{
        //    //    int num = this.CompareCore(x, y);
        //    //    return (this.Ascending ? num : -num);
        //    //}

        //    //protected virtual int CompareCore(IRowData x, IRowData y)
        //    //{
        //    //    object a = null;
        //    //    object b = null;
        //    //    ColumnSortMode sortMode = this.SortMode;
        //    //    if ((sortMode != ColumnSortMode.DisplayText) || (this.FieldValueVisitor == null))
        //    //    {
        //    //        sortMode = ColumnSortMode.Value;
        //    //    }
        //    //    else
        //    //    {
        //    //        a = this.FieldValueVisitor.GetFormattedFieldValue(x);
        //    //        b = this.FieldValueVisitor.GetFormattedFieldValue(y);
        //    //        if ((a == null) || (b == null))
        //    //        {
        //    //            sortMode = ColumnSortMode.Value;
        //    //        }
        //    //    }
        //    //    if (sortMode == ColumnSortMode.Value)
        //    //    {
        //    //        if (!this.UseGenericMethods || !(a is T))
        //    //        {
        //    //            a = x.GetFieldValue(this.FieldName);
        //    //            b = y.GetFieldValue(this.FieldName);
        //    //        }
        //    //        else
        //    //        {
        //    //            a = x.GetFieldValueGeneric<T>(this.FieldName);
        //    //            b = y.GetFieldValueGeneric<T>(this.FieldName);
        //    //        }
        //    //    }
        //    //    if (!this.UseGenericMethods || !(a is T))
        //    //    {
        //    //        return Comparer.Default.Compare(a, b);
        //    //    }
        //    //    return Comparer<T>.get_Default().Compare((T)a, (T)b);
        //    //}

        //    // Properties
        //    protected bool Ascending { get; private set; }

        //    public string FieldName { get; private set; }

        //    public bool UseGenericMethods { get; set; }

        //    //public IFieldValueVisitor FieldValueVisitor { get; set; }

        //    public ColumnSortMode SortMode { get; set; }
        //}

        internal interface IColumnComparer
        {
            // Properties
            bool UseGenericMethods { get; set; }
        }

        //internal class NumbersComparer : GridColumn.ColumnComparer<object>
        //{
        //    // Methods
        //    public NumbersComparer(string fieldName, bool ascending, ColumnSortMode sortMode, IFieldValueVisitor fieldValueVisitor) : base(fieldName, ascending, sortMode, fieldValueVisitor)
        //    {
        //    }

        //    protected override int CompareCore(IRowData x, IRowData y)
        //    {
        //        if ((x == null) || (y == null))
        //        {
        //            return 0;
        //        }
        //        object formattedFieldValue = null;
        //        object fieldValue = null;
        //        ColumnSortMode sortMode = base.SortMode;
        //        if ((sortMode != ColumnSortMode.DisplayText) || (base.FieldValueVisitor == null))
        //        {
        //            sortMode = ColumnSortMode.Value;
        //        }
        //        else
        //        {
        //            formattedFieldValue = base.FieldValueVisitor.GetFormattedFieldValue(x);
        //            fieldValue = base.FieldValueVisitor.GetFormattedFieldValue(y);
        //            if ((formattedFieldValue == null) && (fieldValue == null))
        //            {
        //                sortMode = ColumnSortMode.Value;
        //            }
        //        }
        //        if (sortMode == ColumnSortMode.Value)
        //        {
        //            formattedFieldValue = x.GetFieldValue(base.FieldName);
        //            fieldValue = y.GetFieldValue(base.FieldName);
        //        }
        //        if ((formattedFieldValue == null) && (fieldValue == null))
        //        {
        //            return 0;
        //        }
        //        if (formattedFieldValue == null)
        //        {
        //            return -1;
        //        }
        //        if (fieldValue == null)
        //        {
        //            return 1;
        //        }
        //        Type type = fieldValue.GetType();
        //        if (!IsNumberType(formattedFieldValue.GetType()) || !IsNumberType(type))
        //        {
        //            return 0;
        //        }
        //        double num = Convert.ToDouble(formattedFieldValue);
        //        return ((double)num).CompareTo(Convert.ToDouble(fieldValue));
        //    }

        //    public static bool IsNumberType(Type type) =>
        //        ((type == typeof(int)) || ((type == typeof(long)) || ((type == typeof(float)) || ((type == typeof(double)) || ((type == typeof(decimal)) || ((type == typeof(byte)) || ((type == typeof(short)) || ((type == typeof(uint)) || ((type == typeof(sbyte)) || ((type == typeof(ulong)) || IntrospectionExtensions.GetTypeInfo(type).IsEnum))))))))));
        //}
    }
}
