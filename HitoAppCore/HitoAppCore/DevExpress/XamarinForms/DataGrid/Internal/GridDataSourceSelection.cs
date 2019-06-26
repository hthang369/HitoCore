// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using System;
    
    public class GridDataSourceSelection
    {
        private GridSelectedRowIndex index = new GridSelectedRowIndex();
        private GridSelectedRowIndex sourceIndex = new GridSelectedRowIndex();
        
        public void Link(GridDataSourceSelection selection)
        {
            this.sourceIndex = selection.Index;
        }
        
        public void Reset()
        {
            this.Index.Reset();
            this.SourceIndex.Reset();
        }
        
        public void Unlink()
        {
            this.sourceIndex = new GridSelectedRowIndex();
        }
        
        public GridSelectedRowIndex Index =>
            this.index;
        
        public GridSelectedRowIndex SourceIndex =>
            this.sourceIndex;
    }
}
