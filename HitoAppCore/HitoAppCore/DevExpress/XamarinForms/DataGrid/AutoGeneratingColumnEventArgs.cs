// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    
    public class AutoGeneratingColumnEventArgs : CancelEventArgs
    {
        public AutoGeneratingColumnEventArgs(GridColumn column)
        {
            this.Column = column;
        }
        
        public GridColumn Column { get; private set; }
    }
}
