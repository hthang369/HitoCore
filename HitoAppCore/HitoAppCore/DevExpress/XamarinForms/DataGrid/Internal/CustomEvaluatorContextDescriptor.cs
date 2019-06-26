// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.XamarinForms.DataGrid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    
    internal class CustomEvaluatorContextDescriptor : EvaluatorContextDescriptor
    {
        private Dictionary<string, string> displayFormatCollection;
        private Dictionary<string, ColumnFilterMode> filterModeCollection;
        private ICustomCellTextProvider customTextProvider;
        
        public CustomEvaluatorContextDescriptor(GridColumn column)
        {
            this.TryAddDisplayFormat(column);
            this.AddFilterMode(column);
        }
        
        public CustomEvaluatorContextDescriptor(IEnumerable<GridColumn> columns, ICustomCellTextProvider customProvider)
        {
            this.customTextProvider = customProvider;
            foreach (GridColumn column in columns)
            {
                this.TryAddDisplayFormat(column);
                this.AddFilterMode(column);
            }
        }
        
        private void AddFilterMode(GridColumn column)
        {
            if (this.filterModeCollection == null)
            {
                this.filterModeCollection = new Dictionary<string, ColumnFilterMode>();
            }
            this.filterModeCollection[column.FieldName] = column.FilterMode;
        }
        
        public override IEnumerable GetCollectionContexts(object source, string collectionName) => 
            null;
        
        private string GetDisplayFormat(string fieldName)
        {
            if ((this.displayFormatCollection == null) || !this.displayFormatCollection.ContainsKey(fieldName))
            {
                return null;
            }
            return this.displayFormatCollection[fieldName];
        }
        
        public override EvaluatorContext GetNestedContext(object source, string propertyPath) => 
            null;
        
        public override object GetPropertyValue(object source, EvaluatorProperty propertyPath)
        {
            string fieldName = propertyPath.PropertyPath;
            IRowData data = source as IRowData;
            if (data == null)
            {
                return null;
            }
            object fieldValue = data.GetFieldValue(fieldName);
            return (!this.IsSortByDisplayText(fieldName) ? fieldValue : CustomizeCellTextHelper.Format(fieldValue, this.GetDisplayFormat(fieldName), data.RowHandle, fieldName, this.customTextProvider));
        }
        
        private bool IsSortByDisplayText(string fieldName) => 
            ((this.filterModeCollection != null) && (this.filterModeCollection.ContainsKey(fieldName) && (((ColumnFilterMode) this.filterModeCollection[fieldName]) == ColumnFilterMode.DisplayText)));
        
        private void TryAddDisplayFormat(GridColumn column)
        {
            if (column.FilterMode == ColumnFilterMode.DisplayText)
            {
                if (this.displayFormatCollection == null)
                {
                    this.displayFormatCollection = new Dictionary<string, string>();
                }
                this.displayFormatCollection[column.FieldName] = column.DisplayFormat;
            }
        }
    }
}
