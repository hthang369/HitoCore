// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    
    public class CustomizeCellEventArgs : EventArgs
    {
        private readonly CellData cellData;
        
        internal CustomizeCellEventArgs(CellData cellData)
        {
            this.cellData = cellData;
        }
        
        public bool IsSelected { get; internal set; }
        
        public Color BackgroundColor { get; set; }
        
        public Color ForeColor { get; set; }
        
        public int RowHandle =>
            this.cellData.Index.RowHandle;
        
        public string FieldName =>
            this.cellData.Index.FieldName;
        
        public object Value =>
            this.cellData?.Value;
        
        public bool Handled { get; set; }
        
        public string DisplayText =>
            ((this.cellData == null) ? string.Empty : ((this.cellData.DisplayText != null) ? this.cellData.DisplayText : ((this.cellData.Value != null) ? this.cellData.Value.ToString() : string.Empty)));
    }
}
