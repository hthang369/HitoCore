// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid
{
    using System;
    using System.Runtime.CompilerServices;
    
    public class SwipeItemTapEventArgs : EventArgs
    {
        public SwipeItemTapEventArgs(int rowHandle, int sourceIndex, object dataObject)
        {
            this.RowHandle = rowHandle;
            this.SourceIndex = sourceIndex;
            this.DataObject = dataObject;
        }
        
        public int RowHandle;
        
        public int SourceIndex;
        
        public object DataObject;
    }
}