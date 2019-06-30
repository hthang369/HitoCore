using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Xamarin.Forms.DataGrid
{
    public abstract class GridColumn : BindableObject
    {
        #region Fields
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
        public static readonly BindableProperty HeaderLabelStyleProperty;
        public static readonly BindableProperty SortingIconProperty;
        #endregion

        #region Events
        [CompilerGenerated]
        internal event PropertyChangedEventHandler AfterPropertyChanged;
        #endregion

        #region Contructor
        static GridColumn()
        {
            FieldNameProperty = BindingUtils.CreateProperty<GridColumn, string>(nameof(FieldName), string.Empty, OnFieldNameChanged);
            CaptionProperty = BindingUtils.CreateProperty<GridColumn, string>(nameof(Caption), string.Empty, OnCaptionChanged);
            WidthProperty = BindingUtils.CreateProperty<GridColumn, double>(nameof(Width), double.NaN, OnWidthChanged);
            IsReadOnlyProperty = BindingUtils.CreateProperty<GridColumn, bool>(nameof(IsReadOnly), false, OnIsReadOnlyChanged);
            UnboundTypeProperty = BindingUtils.CreateProperty<GridColumn, UnboundColumnType>(nameof(UnboundType), UnboundColumnType.Bound, OnUnboundTypeChanged);
            UnboundExpressionProperty = BindingUtils.CreateProperty<GridColumn, string>(nameof(UnboundExpression), string.Empty, OnUnboundExpressionChanged);
            ContentAlignmentProperty = BindingUtils.CreateProperty<GridColumn, TextAlignment>(nameof(ContentAlignment), TextAlignment.Start, OnContentAlignmentChanged);
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
            ColumnFilterModeProperty = BindingUtils.CreateProperty<GridColumn, ColumnFilterMode>(nameof(ColumnFilterMode), ColumnFilterMode.Value, OnColumnFilterModeChanged);
            ImmediateUpdateAutoFilterProperty = BindingUtils.CreateProperty<GridColumn, bool>(nameof(ImmediateUpdateAutoFilter), true, OnImmediateUpdateAutoFilterChanged);
            GroupIntervalProperty = BindingUtils.CreateProperty<GridColumn, ColumnGroupInterval>(nameof(GroupInterval), ColumnGroupInterval.Default, OnGroupIntervalChanged);
            HeaderTemplateProperty = BindingUtils.CreateProperty<GridColumn, DataTemplate>(nameof(HeaderTemplate), null, OnHeaderTemplateChanged);
            FixedStyleProperty = BindingUtils.CreateProperty<GridColumn, FixedStyle>(nameof(FixedStyle), FixedStyle.None, OnFixedStyleChanged);
            HeaderLabelStyleProperty = BindingUtils.CreateProperty<GridColumn, Style>(nameof(HeaderLabelStyle));
            SortingIconProperty = BindingUtils.CreateProperty<GridColumn, Image>(nameof(SortingIcon));
        }

        public GridColumn()
        {
            this.SortMode = ColumnSortMode.Value;
        }
        #endregion

        #region Methods
        private static void OnFieldNameChanged(BindableObject bindable, string oldValue, string newValue)
        {
            GridColumn col = bindable as GridColumn;
            col.RaiseAfterPropertyChanged(nameof(FieldName));
            col.OnActualCaptionChanged();
        }
        private void OnActualCaptionChanged()
        {
            this.OnPropertyChanged(nameof(ActualCaption));
        }
        private static void OnCaptionChanged(BindableObject bindable, string oldValue, string newValue)
        {
            GridColumn col = bindable as GridColumn;
            col.RaiseAfterPropertyChanged(nameof(Caption));
            col.OnActualCaptionChanged();
        }
        private static void OnWidthChanged(BindableObject bindable, double oldValue, double newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(Width));
        }
        private static void OnIsReadOnlyChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(IsReadOnly));
        }
        private static void OnUnboundTypeChanged(BindableObject bindable, UnboundColumnType oldValue, UnboundColumnType newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(UnboundType));
        }
        private static void OnUnboundExpressionChanged(BindableObject bindable, string oldValue, string newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(UnboundExpression));
        }
        private static void OnContentAlignmentChanged(BindableObject bindable, TextAlignment oldValue, TextAlignment newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(ContentAlignment));
        }
        private static void OnMinWidthChanged(BindableObject bindable, double oldValue, double newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(MinWidth));
        }
        private static void OnIsGroupedChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(IsGrouped));
        }
        private static void OnIsVisibleChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(IsVisible));
        }
        private static void OnSortOrderChanged(BindableObject bindable, ColumnSortOrder oldValue, ColumnSortOrder newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(SortOrder));
        }
        private static void OnSortModeChanged(BindableObject bindable, ColumnSortMode oldValue, ColumnSortMode newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(SortMode));
        }
        private static void OnAllowGroupChanged(BindableObject bindable, DefaultBoolean oldValue, DefaultBoolean newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(AllowGroup));
        }
        private static void OnAllowSortChanged(BindableObject bindable, DefaultBoolean oldValue, DefaultBoolean newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(AllowSort));
        }
        private static void OnSortIndexChanged(BindableObject bindable, int oldValue, int newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(SortIndex));
        }
        private static void OnDisplayFormatChanged(BindableObject bindable, string oldValue, string newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(DisplayFormat));
        }
        private static void OnAllowAutoFilterChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(AllowAutoFilter));
        }
        private static void OnColumnFilterModeChanged(BindableObject bindable, ColumnFilterMode oldValue, ColumnFilterMode newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(ColumnFilterMode));
        }
        private static void OnAutoFilterValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(AutoFilterValue));
        }
        private static void OnImmediateUpdateAutoFilterChanged(BindableObject bindable, bool oldValue, bool newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(ImmediateUpdateAutoFilter));
        }
        private static void OnGroupIntervalChanged(BindableObject bindable, ColumnGroupInterval oldValue, ColumnGroupInterval newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(GroupInterval));
        }
        private static void OnHeaderTemplateChanged(BindableObject bindable, DataTemplate oldValue, DataTemplate newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(HeaderTemplate));
        }
        private static void OnFixedStyleChanged(BindableObject bindable, FixedStyle oldValue, FixedStyle newValue)
        {
            ((GridColumn)bindable).RaiseAfterPropertyChanged(nameof(FixedStyle));
        }
        protected void RaiseAfterPropertyChanged(string propertyName)
        {
            if (this.AfterPropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                this.AfterPropertyChanged(this, args);
            }
        }
        protected abstract Type GetComparerPropertyType();
        internal LayoutOptions GetControlContentAlignment(bool isExpand = false)
        {
            return new LayoutOptions((LayoutAlignment)Enum.Parse(typeof(LayoutAlignment), ContentAlignment.ToString()), isExpand);
        }
        public Type GetPreferredDataType()
        {
            if (this.IsUnbound)
            {
                return GetComparerPropertyType();
            }
            switch (UnboundType)
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
        #endregion

        #region Properties
        protected internal bool IsAutoGenerated { get; set; }

        internal bool IsParentReadOnly { get; set; }

        public string FieldName
        {
            get => (string)base.GetValue(FieldNameProperty);
            set => base.SetValue(FieldNameProperty, value);
        }

        public string Caption
        {
            get => (string)base.GetValue(CaptionProperty);
            set => base.SetValue(CaptionProperty, value);
        }

        public string ActualCaption =>
            !string.IsNullOrEmpty(this.Caption) ? this.Caption : SplitStringHelper.SplitPascalCaseString(this.FieldName);

        public double Width
        {
            get => (double)base.GetValue(WidthProperty);
            set => base.SetValue(WidthProperty, value);
        }

        public bool IsReadOnly
        {
            get => (bool)base.GetValue(IsReadOnlyProperty);
            set => base.SetValue(IsReadOnlyProperty, value);
        }

        public UnboundColumnType UnboundType
        {
            get => (UnboundColumnType)base.GetValue(UnboundTypeProperty);
            set => base.SetValue(UnboundTypeProperty, value);
        }

        public string UnboundExpression
        {
            get => (string)base.GetValue(UnboundExpressionProperty);
            set => base.SetValue(UnboundExpressionProperty, value);
        }

        public TextAlignment ContentAlignment
        {
            get => (TextAlignment)base.GetValue(ContentAlignmentProperty);
            set => base.SetValue(ContentAlignmentProperty, value);
        }

        public double MinWidth
        {
            get => (double)base.GetValue(MinWidthProperty);
            set => base.SetValue(MinWidthProperty, value);
        }

        public double ActualWidth
        {
            get => (double)base.GetValue(ActualWidthProperty);
            protected set => base.SetValue(ActualWidthPropertyKey, value);
        }

        public bool IsGrouped
        {
            get => (bool)base.GetValue(IsGroupedProperty);
            set => base.SetValue(IsGroupedProperty, value);
        }

        public bool IsVisible
        {
            get => (bool)base.GetValue(IsVisibleProperty);
            set => base.SetValue(IsVisibleProperty, value);
        }

        public ColumnSortOrder SortOrder
        {
            get => (ColumnSortOrder)base.GetValue(SortOrderProperty);
            set => base.SetValue(SortOrderProperty, value);
        }

        public ColumnSortMode SortMode
        {
            get => (ColumnSortMode)base.GetValue(SortModeProperty);
            set => base.SetValue(SortModeProperty, value);
        }

        public DefaultBoolean AllowSort
        {
            get => (DefaultBoolean)base.GetValue(AllowSortProperty);
            set => base.SetValue(AllowSortProperty, value);
        }

        public DefaultBoolean AllowGroup
        {
            get => (DefaultBoolean)base.GetValue(AllowGroupProperty);
            set => base.SetValue(AllowGroupProperty, value);
        }

        public string DisplayFormat
        {
            get => (string)base.GetValue(DisplayFormatProperty);
            set => base.SetValue(DisplayFormatProperty, value);
        }

        public int SortIndex
        {
            get => (int)base.GetValue(SortIndexProperty);
            set => base.SetValue(SortIndexProperty, value);
        }

        public object AutoFilterValue
        {
            get => base.GetValue(AutoFilterValueProperty);
            set => base.SetValue(AutoFilterValueProperty, value);
        }

        public bool AllowAutoFilter
        {
            get => (bool)base.GetValue(AllowAutoFilterProperty);
            set => base.SetValue(AllowAutoFilterProperty, value);
        }

        public bool ImmediateUpdateAutoFilter
        {
            get => (bool)base.GetValue(ImmediateUpdateAutoFilterProperty);
            set => base.SetValue(ImmediateUpdateAutoFilterProperty, value);
        }

        public ColumnFilterMode ColumnFilterMode
        {
            get => (ColumnFilterMode)base.GetValue(ColumnFilterModeProperty);
            set => base.SetValue(ColumnFilterModeProperty, value);
        }

        public ColumnGroupInterval GroupInterval
        {
            get => (ColumnGroupInterval)base.GetValue(GroupIntervalProperty);
            set => base.SetValue(GroupIntervalProperty, value);
        }

        public DataTemplate HeaderTemplate
        {
            get => (DataTemplate)base.GetValue(HeaderTemplateProperty);
            set => base.SetValue(HeaderTemplateProperty, value);
        }

        public FixedStyle FixedStyle
        {
            get => (FixedStyle)base.GetValue(FixedStyleProperty);
            set => base.SetValue(FixedStyleProperty, value);
        }
        public bool IsUnbound => ((UnboundType != UnboundColumnType.Bound) || !string.IsNullOrEmpty(UnboundExpression));
        public Style HeaderLabelStyle
        {
            get { return (Style)GetValue(HeaderLabelStyleProperty); }
            set { SetValue(HeaderLabelStyleProperty, value); }
        }
        public Image SortingIcon
        {
            get => (Image)base.GetValue(SortingIconProperty);
            set => base.SetValue(SortingIconProperty, value);
        }
        #endregion
    }
}
