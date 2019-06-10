// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Localization
{
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Localization.Internal;
    using System;
    using System.Reflection;
    using System.Resources;
    
    public class GridLocalizer : XtraLocalizer<GridStringId>
    {
        static GridLocalizer()
        {
            SetActiveLocalizerProvider(new GlobalActiveLocalizerProvider<GridStringId>(CreateDefaultLocalizer()));
        }
        
        public static XtraLocalizer<GridStringId> CreateDefaultLocalizer() => 
            new GridResLocalizer();
        
        public override XtraLocalizer<GridStringId> CreateResXLocalizer() => 
            new GridResLocalizer();
        
        public static string GetString(GridStringId id) => 
            XtraLocalizer<GridStringId>.Active.GetLocalizedString(id);
        
        protected override void PopulateStringTable()
        {
            this.AddString(GridStringId.Msg_ErrorInternalError, "Internal error is occured!");
            this.AddString(GridStringId.TotalSummaryDisplayFormat, "{0}={2}");
            this.AddString(GridStringId.GroupSummaryCountDisplayFormat, "{0}={2}");
            this.AddString(GridStringId.GroupCaptionDisplayFormat, "{0}: {1}");
            this.AddString(GridStringId.GroupSummaryShortDisplayFormat, "{0}: {1}");
            this.AddString(GridStringId.GroupIntervalYearDisplayFormat, "{0}");
            this.AddString(GridStringId.GroupIntervalMonthDisplayFormat, "{0}, {1}");
            this.AddString(GridStringId.GroupIntervalQuarterDisplayFormat, "Q{0}, {1}");
            this.AddString(GridStringId.GroupNameNull, "(null)");
            this.AddString(GridStringId.GridOutlookIntervals, "Older;Last Month;Earlier this Month;Three Weeks Ago;Two Weeks Ago;Last Week;;;;;;;;Yesterday;Today;Tomorrow;;;;;;;;Next Week;Two Weeks Away;Three Weeks Away;Later this Month;Next Month;Beyond Next Month;");
            this.AddString(GridStringId.Caption_On, " ON ");
            this.AddString(GridStringId.Caption_Off, "OFF");
            this.AddString(GridStringId.Caption_Undefined, "  0  ");
            this.AddString(GridStringId.Caption_All, "(All)");
            this.AddString(GridStringId.NewItemRowCaption, "Tap here to add a new row");
            this.AddString(GridStringId.MenuCmd_None, " ");
            this.AddString(GridStringId.MenuCmd_ColumnSortAscending, "Sort Ascending");
            this.AddString(GridStringId.MenuCmd_ColumnSortAscendingDescription, "Sort Ascending.");
            this.AddString(GridStringId.MenuCmd_ColumnSortDescending, "Sort Descending");
            this.AddString(GridStringId.MenuCmd_ColumnSortDescendingDescription, "Sort Descending.");
            this.AddString(GridStringId.MenuCmd_ColumnSortNone, "Clear Sort Order");
            this.AddString(GridStringId.MenuCmd_ColumnSortNoneDescription, "Clear Sort Order.");
            this.AddString(GridStringId.MenuCmd_ColumnTotalSummaryNone, "None");
            this.AddString(GridStringId.MenuCmd_ColumnTotalSummaryNoneDescription, "None.");
            this.AddString(GridStringId.MenuCmd_ColumnTotalSummarySum, "Sum");
            this.AddString(GridStringId.MenuCmd_ColumnTotalSummarySumDescription, "Sum.");
            this.AddString(GridStringId.MenuCmd_ColumnTotalSummaryAverage, "Average");
            this.AddString(GridStringId.MenuCmd_ColumnTotalSummaryAverageDescription, "Average.");
            this.AddString(GridStringId.MenuCmd_ColumnTotalSummaryMin, "Min");
            this.AddString(GridStringId.MenuCmd_ColumnTotalSummaryMinDescription, "Min.");
            this.AddString(GridStringId.MenuCmd_ColumnTotalSummaryMax, "Max");
            this.AddString(GridStringId.MenuCmd_ColumnTotalSummaryMaxDescription, "Max.");
            this.AddString(GridStringId.MenuCmd_ColumnTotalSummaryCount, "Count");
            this.AddString(GridStringId.MenuCmd_ColumnTotalSummaryCountDescription, "Count.");
            this.AddString(GridStringId.MenuCmd_ColumnGroupSummaryNone, "None");
            this.AddString(GridStringId.MenuCmd_ColumnGroupSummaryNoneDescription, "None.");
            this.AddString(GridStringId.MenuCmd_ColumnGroupSummarySum, "Sum");
            this.AddString(GridStringId.MenuCmd_ColumnGroupSummarySumDescription, "Sum.");
            this.AddString(GridStringId.MenuCmd_ColumnGroupSummaryAverage, "Average");
            this.AddString(GridStringId.MenuCmd_ColumnGroupSummaryAverageDescription, "Average.");
            this.AddString(GridStringId.MenuCmd_ColumnGroupSummaryMin, "Min");
            this.AddString(GridStringId.MenuCmd_ColumnGroupSummaryMinDescription, "Min.");
            this.AddString(GridStringId.MenuCmd_ColumnGroupSummaryMax, "Max");
            this.AddString(GridStringId.MenuCmd_ColumnGroupSummaryMaxDescription, "Max.");
            this.AddString(GridStringId.MenuCmd_ColumnGroupSummaryCount, "Count");
            this.AddString(GridStringId.MenuCmd_ColumnGroupSummaryCountDescription, "Count.");
            this.AddString(GridStringId.Caption_SummaryNone, " ");
            this.AddString(GridStringId.Caption_SummarySum, "SUM");
            this.AddString(GridStringId.Caption_SummaryMin, "MIN");
            this.AddString(GridStringId.Caption_SummaryMax, "MAX");
            this.AddString(GridStringId.Caption_SummaryCount, "COUNT");
            this.AddString(GridStringId.Caption_SummaryAverage, "AVG");
            this.AddString(GridStringId.Caption_SummaryCustom, "VAL");
            this.AddString(GridStringId.MenuCmd_ColumnGroup, "Group By '{0}'");
            this.AddString(GridStringId.MenuCmd_ColumnGroupDescription, "Group By '{0}'.");
            this.AddString(GridStringId.MenuCmd_ColumnGroupNone, "Remove Grouping");
            this.AddString(GridStringId.MenuCmd_ColumnGroupNoneDescription, "Remove Grouping.");
            this.AddString(GridStringId.MenuCmd_DataRowEdit, "Edit Cell");
            this.AddString(GridStringId.MenuCmd_DataRowEditDescription, "Edit Cell.");
            this.AddString(GridStringId.MenuCmd_DataRowDelete, "Delete Row");
            this.AddString(GridStringId.MenuCmd_DataRowDeleteDescription, "Delete Row.");
            this.AddString(GridStringId.MenuCmd_GroupsExpandAll, "Expand All Groups");
            this.AddString(GridStringId.MenuCmd_GroupsExpandAllDescription, "Expand All Groups.");
            this.AddString(GridStringId.MenuCmd_GroupsCollapseAll, "Collapse All Groups");
            this.AddString(GridStringId.MenuCmd_GroupsCollapseAllDescription, "Collapse All Groups.");
            this.AddString(GridStringId.MenuCmd_ShowColumnChooser, "Show Column Chooser");
            this.AddString(GridStringId.MenuCmd_ShowColumnChooserDescription, "Show Column Chooser.");
            this.AddString(GridStringId.MenuCmd_CloseMenu, "Close Menu");
            this.AddString(GridStringId.DialogForm_ButtonOk, "Done");
            this.AddString(GridStringId.MenuCmd_CloseMenuDescription, "Close Menu");
            this.AddString(GridStringId.DialogForm_ButtonCancel, "Cancel");
            this.AddString(GridStringId.ColumnChooserDlg_LabelCaption, "Choose Columns");
            this.AddString(GridStringId.EditingForm_LabelCaption, "Edit Values");
            this.AddString(GridStringId.EditingRow_ButtonCancel, "Cancel");
            this.AddString(GridStringId.EditingRow_ButtonApply, "Apply");
            this.AddString(GridStringId.FilterExpression_Between, "Between");
            this.AddString(GridStringId.FilterExpression_In, "In");
            this.AddString(GridStringId.FilterExpression_IsNotNull, "Is Not Null");
            this.AddString(GridStringId.FilterExpression_IsNull, "Is Null");
            this.AddString(GridStringId.FilterExpression_NotLike, "Not Like");
            this.AddString(GridStringId.FilterExpression_Aggregate_Avg, "Avg");
            this.AddString(GridStringId.FilterExpression_Aggregate_Count, "Count");
            this.AddString(GridStringId.FilterExpression_Aggregate_Exists, "Exists");
            this.AddString(GridStringId.FilterExpression_Aggregate_Max, "Max");
            this.AddString(GridStringId.FilterExpression_Aggregate_Min, "Min");
            this.AddString(GridStringId.FilterExpression_Aggregate_Single, "Single");
            this.AddString(GridStringId.FilterExpression_Aggregate_Sum, "Sum");
            this.AddString(GridStringId.FilterExpression_GroupOperator_And, "And");
            this.AddString(GridStringId.FilterExpression_GroupOperator_Or, "Or");
            this.AddString(GridStringId.FilterExpression_UnaryOperator_BitwiseNot, "~");
            this.AddString(GridStringId.FilterExpression_UnaryOperator_IsNull, "Is Null");
            this.AddString(GridStringId.FilterExpression_UnaryOperator_Minus, "-");
            this.AddString(GridStringId.FilterExpression_UnaryOperator_Not, "Not");
            this.AddString(GridStringId.FilterExpression_UnaryOperator_Plus, "+");
            this.AddString(GridStringId.FilterExpression_BinaryOperator_BitwiseAnd, "&");
            this.AddString(GridStringId.FilterExpression_BinaryOperator_BitwiseOr, "|");
            this.AddString(GridStringId.FilterExpression_BinaryOperator_BitwiseXor, "^");
            this.AddString(GridStringId.FilterExpression_BinaryOperator_Divide, "/");
            this.AddString(GridStringId.FilterExpression_BinaryOperator_Equal, "=");
            this.AddString(GridStringId.FilterExpression_BinaryOperator_Greater, ">");
            this.AddString(GridStringId.FilterExpression_BinaryOperator_GreaterOrEqual, ">=");
            this.AddString(GridStringId.FilterExpression_BinaryOperator_Less, "<");
            this.AddString(GridStringId.FilterExpression_BinaryOperator_LessOrEqual, "<=");
            this.AddString(GridStringId.FilterExpression_BinaryOperator_Like, "Like");
            this.AddString(GridStringId.FilterExpression_BinaryOperator_Minus, "-");
            this.AddString(GridStringId.FilterExpression_BinaryOperator_Modulo, "%");
            this.AddString(GridStringId.FilterExpression_BinaryOperator_Multiply, "*");
            this.AddString(GridStringId.FilterExpression_BinaryOperator_NotEqual, "<>");
            this.AddString(GridStringId.FilterExpression_BinaryOperator_Plus, "+");
            this.AddString(GridStringId.FilterExpression_Function_Iif, "Iif");
            this.AddString(GridStringId.FilterExpression_Function_IsNull, "IsNull");
            this.AddString(GridStringId.FilterExpression_Function_Len, "Len");
            this.AddString(GridStringId.FilterExpression_Function_Lower, "Lower");
            this.AddString(GridStringId.FilterExpression_Function_None, "None");
            this.AddString(GridStringId.FilterExpression_Function_Substring, "Substring");
            this.AddString(GridStringId.FilterExpression_Function_Trim, "Trim");
            this.AddString(GridStringId.FilterExpression_Function_Upper, "Upper");
            this.AddString(GridStringId.FilterExpression_Function_Custom, "Custom");
            this.AddString(GridStringId.FilterExpression_Function_LocalDateTimeThisYear, "This year");
            this.AddString(GridStringId.FilterExpression_Function_LocalDateTimeThisMonth, "This month");
            this.AddString(GridStringId.FilterExpression_Function_LocalDateTimeLastWeek, "Last week");
            this.AddString(GridStringId.FilterExpression_Function_LocalDateTimeThisWeek, "This week");
            this.AddString(GridStringId.FilterExpression_Function_LocalDateTimeYesterday, "Yesterday");
            this.AddString(GridStringId.FilterExpression_Function_LocalDateTimeToday, "Today");
            this.AddString(GridStringId.FilterExpression_Function_LocalDateTimeNow, "Now");
            this.AddString(GridStringId.FilterExpression_Function_LocalDateTimeTomorrow, "Tomorrow");
            this.AddString(GridStringId.FilterExpression_Function_LocalDateTimeDayAfterTomorrow, "Day after tomorrow");
            this.AddString(GridStringId.FilterExpression_Function_LocalDateTimeNextWeek, "Next week");
            this.AddString(GridStringId.FilterExpression_Function_LocalDateTimeTwoWeeksAway, "Two weeks away");
            this.AddString(GridStringId.FilterExpression_Function_LocalDateTimeNextMonth, "Next month");
            this.AddString(GridStringId.FilterExpression_Function_LocalDateTimeNextYear, "Next year");
            this.AddString(GridStringId.FilterExpression_Function_IsOutlookIntervalBeyondThisYear, "Is beyond this year");
            this.AddString(GridStringId.FilterExpression_Function_IsOutlookIntervalLaterThisYear, "Is later this year");
            this.AddString(GridStringId.FilterExpression_Function_IsOutlookIntervalLaterThisMonth, "Is later this month");
            this.AddString(GridStringId.FilterExpression_Function_IsOutlookIntervalNextWeek, "Is next week");
            this.AddString(GridStringId.FilterExpression_Function_IsOutlookIntervalLaterThisWeek, "Is later this week");
            this.AddString(GridStringId.FilterExpression_Function_IsOutlookIntervalTomorrow, "Is tomorrow");
            this.AddString(GridStringId.FilterExpression_Function_IsOutlookIntervalToday, "Is today");
            this.AddString(GridStringId.FilterExpression_Function_IsOutlookIntervalYesterday, "Is yesterday");
            this.AddString(GridStringId.FilterExpression_Function_IsOutlookIntervalEarlierThisWeek, "Is earlier this week");
            this.AddString(GridStringId.FilterExpression_Function_IsOutlookIntervalLastWeek, "Is last week");
            this.AddString(GridStringId.FilterExpression_Function_IsOutlookIntervalEarlierThisMonth, "Is earlier this month");
            this.AddString(GridStringId.FilterExpression_Function_IsOutlookIntervalEarlierThisYear, "Is earlier this year");
            this.AddString(GridStringId.FilterExpression_Function_IsOutlookIntervalPriorThisYear, "Is prior to this year");
            this.AddString(GridStringId.FilterExpression_Function_IsNullOrEmpty, "Is null or empty");
            this.AddString(GridStringId.FilterExpression_Function_Concat, "Concat");
            this.AddString(GridStringId.FilterExpression_Function_Ascii, "Ascii");
            this.AddString(GridStringId.FilterExpression_Function_Char, "Char");
            this.AddString(GridStringId.FilterExpression_Function_ToInt, "To int");
            this.AddString(GridStringId.FilterExpression_Function_ToLong, "To long");
            this.AddString(GridStringId.FilterExpression_Function_ToFloat, "To float");
            this.AddString(GridStringId.FilterExpression_Function_ToDouble, "To double");
            this.AddString(GridStringId.FilterExpression_Function_ToDecimal, "To decimal");
            this.AddString(GridStringId.FilterExpression_Function_ToStr, "To str");
            this.AddString(GridStringId.FilterExpression_Function_Replace, "Replace");
            this.AddString(GridStringId.FilterExpression_Function_Reverse, "Reverse");
            this.AddString(GridStringId.FilterExpression_Function_Insert, "Insert");
            this.AddString(GridStringId.FilterExpression_Function_CharIndex, "Char index");
            this.AddString(GridStringId.FilterExpression_Function_Remove, "Remove");
            this.AddString(GridStringId.FilterExpression_Function_Abs, "Abs");
            this.AddString(GridStringId.FilterExpression_Function_Sqr, "Sqr");
            this.AddString(GridStringId.FilterExpression_Function_Cos, "Cos");
            this.AddString(GridStringId.FilterExpression_Function_Sin, "Sin");
            this.AddString(GridStringId.FilterExpression_Function_Atn, "Atn");
            this.AddString(GridStringId.FilterExpression_Function_Exp, "Exp");
            this.AddString(GridStringId.FilterExpression_Function_Log, "Log");
            this.AddString(GridStringId.FilterExpression_Function_Rnd, "Rnd");
            this.AddString(GridStringId.FilterExpression_Function_Tan, "Tan");
            this.AddString(GridStringId.FilterExpression_Function_Power, "Power");
            this.AddString(GridStringId.FilterExpression_Function_Sign, "Sign");
            this.AddString(GridStringId.FilterExpression_Function_Round, "Round");
            this.AddString(GridStringId.FilterExpression_Function_Ceiling, "Ceiling");
            this.AddString(GridStringId.FilterExpression_Function_Floor, "Floor");
            this.AddString(GridStringId.FilterExpression_Function_Max, "Max");
            this.AddString(GridStringId.FilterExpression_Function_Min, "Min");
            this.AddString(GridStringId.FilterExpression_Function_Acos, "Acos");
            this.AddString(GridStringId.FilterExpression_Function_Asin, "Asin");
            this.AddString(GridStringId.FilterExpression_Function_Atn2, "Atn2");
            this.AddString(GridStringId.FilterExpression_Function_BigMul, "Big mul");
            this.AddString(GridStringId.FilterExpression_Function_Cosh, "Cosh");
            this.AddString(GridStringId.FilterExpression_Function_Log10, "Log10");
            this.AddString(GridStringId.FilterExpression_Function_Sinh, "Sinh");
            this.AddString(GridStringId.FilterExpression_Function_Tanh, "Tanh");
            this.AddString(GridStringId.FilterExpression_Function_PadLeft, "Pad left");
            this.AddString(GridStringId.FilterExpression_Function_PadRight, "Pad right");
            this.AddString(GridStringId.FilterExpression_Function_DateDiffTick, "Date diff tick");
            this.AddString(GridStringId.FilterExpression_Function_DateDiffSecond, "Date diff second");
            this.AddString(GridStringId.FilterExpression_Function_DateDiffMilliSecond, "Date diff millisecond");
            this.AddString(GridStringId.FilterExpression_Function_DateDiffMinute, "Date diff minute");
            this.AddString(GridStringId.FilterExpression_Function_DateDiffHour, "Date diff hour");
            this.AddString(GridStringId.FilterExpression_Function_DateDiffDay, "Date diff day");
            this.AddString(GridStringId.FilterExpression_Function_DateDiffMonth, "Date diff month");
            this.AddString(GridStringId.FilterExpression_Function_DateDiffYear, "Date diff year");
            this.AddString(GridStringId.FilterExpression_Function_GetDate, "Get date");
            this.AddString(GridStringId.FilterExpression_Function_GetMilliSecond, "Get millisecond");
            this.AddString(GridStringId.FilterExpression_Function_GetSecond, "Get second");
            this.AddString(GridStringId.FilterExpression_Function_GetMinute, "Get minute");
            this.AddString(GridStringId.FilterExpression_Function_GetHour, "Get hour");
            this.AddString(GridStringId.FilterExpression_Function_GetDay, "Get day");
            this.AddString(GridStringId.FilterExpression_Function_GetMonth, "Get month");
            this.AddString(GridStringId.FilterExpression_Function_GetYear, "Get year");
            this.AddString(GridStringId.FilterExpression_Function_GetDayOfWeek, "Get day of week");
            this.AddString(GridStringId.FilterExpression_Function_GetDayOfYear, "Get day of year");
            this.AddString(GridStringId.FilterExpression_Function_GetTimeOfDay, "Get time of day");
            this.AddString(GridStringId.FilterExpression_Function_Now, "Now");
            this.AddString(GridStringId.FilterExpression_Function_UtcNow, "Utc now");
            this.AddString(GridStringId.FilterExpression_Function_Today, "Today");
            this.AddString(GridStringId.FilterExpression_Function_AddTimeSpan, "Add time span");
            this.AddString(GridStringId.FilterExpression_Function_AddTicks, "Add ticks");
            this.AddString(GridStringId.FilterExpression_Function_AddMilliSeconds, "Add milliseconds");
            this.AddString(GridStringId.FilterExpression_Function_AddSeconds, "Add seconds");
            this.AddString(GridStringId.FilterExpression_Function_AddMinutes, "Add minutes");
            this.AddString(GridStringId.FilterExpression_Function_AddHours, "Add hours");
            this.AddString(GridStringId.FilterExpression_Function_AddDays, "Add days");
            this.AddString(GridStringId.FilterExpression_Function_AddMonths, "Add months");
            this.AddString(GridStringId.FilterExpression_Function_AddYears, "Add years");
            this.AddString(GridStringId.FilterExpression_Function_StartsWith, "Starts with");
            this.AddString(GridStringId.FilterExpression_Function_EndsWith, "Ends with");
            this.AddString(GridStringId.FilterExpression_Function_Contains, "Contains");
            this.AddString(GridStringId.IOS_PullToRefresh_Updating, "Updating");
            this.AddString(GridStringId.LoadMore_Row_Label, "Loading More");
        }
        
        public static void ResetCache()
        {
            XtraLocalizer<GridStringId>.Active.Reset();
        }
        
        public static void SetResource(string resourceName, Assembly assembly)
        {
            ResourceManager resourceManager = new ResourceManager(resourceName, assembly);
            GridResLocalizer localizer1 = new GridResLocalizer();
            localizer1.SetResourceManager(resourceManager);
            XtraLocalizer<GridStringId>.Active = localizer1;
        }
    }
}
