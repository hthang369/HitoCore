// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using DevExpress.XamarinForms.DataGrid;
    using DevExpress.XamarinForms.DataGrid.Localization;
    using System;
    
    public class DisplayTextHelper
    {
        private static DisplayTextHelper instance;
        
        private DisplayTextHelper()
        {
        }
        
        private string CreateFormatString(string displayFormat)
        {
            if (string.IsNullOrEmpty(displayFormat))
            {
                return "{0}";
            }
            int index = displayFormat.IndexOf("{0");
            if ((index < 0) || (displayFormat.IndexOf("}") <= index))
            {
                return ("{0:" + displayFormat + "}");
            }
            return displayFormat;
        }
        
        private string GetActualDisplayFormat(string columnDisplayFormat, GridColumnSummary summary) => 
            ((summary.Type != SummaryType.None) ? ((summary.Type == SummaryType.Count) ? (string.IsNullOrEmpty(summary.DisplayFormat) ? string.Empty : summary.DisplayFormat) : (string.IsNullOrEmpty(summary.DisplayFormat) ? columnDisplayFormat : summary.DisplayFormat)) : string.Empty);
        
        private string GetActualDisplayFormat2(string columnDisplayFormat, GridColumnSummary summary) => 
            ((summary.Type != SummaryType.None) ? ((summary.Type == SummaryType.Count) ? (string.IsNullOrEmpty(summary.DisplayFormat) ? string.Empty : summary.DisplayFormat) : summary.DisplayFormat) : string.Empty);
        
        public string GetDisplayText(string displayFormat, object value)
        {
            string str2;
            if (value == null)
            {
                return string.Empty;
            }
            string str = this.CreateFormatString(displayFormat);
            try
            {
                str2 = string.Format(str, value);
            }
            catch
            {
                try
                {
                    str2 = value.ToString();
                }
                catch
                {
                    str2 = string.Empty;
                }
            }
            return str2;
        }
        
        public string GetGroupSummaryString(string columnDisplayFormat, GridColumnSummary summary, object value, string caption)
        {
            if (summary.Type == SummaryType.None)
            {
                return string.Empty;
            }
            if (!string.IsNullOrEmpty(summary.DisplayFormat))
            {
                return string.Format(GridLocalizer.GetString(GridStringId.GroupSummaryShortDisplayFormat), string.IsNullOrEmpty(caption) ? summary.FieldName : caption, this.GetDisplayText(summary.DisplayFormat, value));
            }
            GridStringId groupSummaryDisplayFormat = GridStringId.GroupSummaryDisplayFormat;
            if (summary.Type == SummaryType.Count)
            {
                groupSummaryDisplayFormat = GridStringId.GroupSummaryCountDisplayFormat;
            }
            string str = GridLocalizer.GetString((GridStringId)Enum.ToObject(typeof(GridStringId), 35 + ((int)summary.Type) - 0));
            return string.Format(GridLocalizer.GetString(groupSummaryDisplayFormat), str, string.IsNullOrEmpty(caption) ? summary.FieldName : caption, this.GetDisplayText(columnDisplayFormat, value));
        }
        
        private string GetSummaryStringCore(string columnDisplayFormat, GridColumnSummary summary, string joinDisplayFormat, object value)
        {
            if (summary.Type == SummaryType.None)
            {
                return string.Empty;
            }
            string actualDisplayFormat = this.GetActualDisplayFormat(columnDisplayFormat, summary);
            if (!string.IsNullOrEmpty(actualDisplayFormat))
            {
                return this.GetDisplayText(actualDisplayFormat, value);
            }
            GridStringId id = (GridStringId)Enum.ToObject(typeof(GridStringId), (23 + ((int)SummaryType.Min * ((int)summary.Type - 0))));
            return string.Format(joinDisplayFormat, GridLocalizer.GetString(id), summary.FieldName, this.GetSummaryValueText(columnDisplayFormat, summary, value));
        }
        
        private string GetSummaryValueText(string columnDisplayFormat, GridColumnSummary summary, object value) => 
            this.GetDisplayText(this.GetActualDisplayFormat(columnDisplayFormat, summary), value);
        
        public string GetTotalSummaryString(string columnDisplayFormat, GridColumnSummary summary, object value)
        {
            if (summary.Type == SummaryType.None)
            {
                return string.Empty;
            }
            if (!string.IsNullOrEmpty(summary.DisplayFormat))
            {
                return this.GetDisplayText(summary.DisplayFormat, value);
            }
            string str = GridLocalizer.GetString((GridStringId)Enum.ToObject(typeof(GridStringId), 35 + ((int)summary.Type) - 0));
            return string.Format(GridLocalizer.GetString(GridStringId.TotalSummaryDisplayFormat), str, summary.FieldName, this.GetDisplayText(columnDisplayFormat, value));
        }
        
        public static DisplayTextHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DisplayTextHelper();
                }
                return instance;
            }
        }
    }
}
