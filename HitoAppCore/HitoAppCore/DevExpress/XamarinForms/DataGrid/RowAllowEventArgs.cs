// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid
{
    using System;
    using System.Runtime.CompilerServices;
    
    public class RowAllowEventArgs : RowEventArgs
    {
        public RowAllowEventArgs(int rowHandle) : base(rowHandle)
        {
            this.Allow = true;
        }
        
        public bool Allow { get; set; }
    }
}
