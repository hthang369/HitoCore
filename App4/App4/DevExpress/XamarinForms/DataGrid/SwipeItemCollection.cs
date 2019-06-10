// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid
{
    using System;
    using System.Collections.ObjectModel;
    
    public class SwipeItemCollection : ObservableCollection<SwipeItem>
    {
        private DataGridView owner;
        
        public SwipeItemCollection(DataGridView owner)
        {
            this.owner = owner;
        }
        
        protected override void InsertItem(int index, SwipeItem item)
        {
            if (item != null)
            {
                item.Owner = this.owner;
            }
            base.InsertItem(index, item);
        }
    }
}