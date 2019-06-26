using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HitoAppCore
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadControl();
        }
        public void LoadControl()
        {
            List<Employee> emp = new List<Employee>()
            {
                new Employee{no="LP01",name="LP 123",sex="nam"},
                new Employee{no="LP02",name="LP 124",sex="nu"},
                new Employee{no="LP03",name="LP 125",sex="nam"},
            };
            DataGrid.GridControl grid = new DataGrid.GridControl();
            grid.Columns.Add(new DataGrid.TextColumn { FieldName = "no", Caption = "Mã NV" });
            grid.Columns.Add(new DataGrid.TextColumn { FieldName = "name", Caption = "Tên NV" });
            grid.Columns.Add(new DataGrid.TextColumn { FieldName = "sex", Caption = "Giới tính" });
            grid.BackgroundHeaderColor = Color.Red;
            grid.ItemsSource = emp;
            (Content as StackLayout).Children.Add(grid);
            Xamarin.Forms.DataGrid.DataGrid g = new Xamarin.Forms.DataGrid.DataGrid();
            g.Columns.Add(new Xamarin.Forms.DataGrid.DataGridColumn { FieldName = "no", Caption = "Mã NV" });
            g.Columns.Add(new Xamarin.Forms.DataGrid.DataGridColumn { FieldName = "name", Caption = "Tên NV" });
            g.NoDataView = new Label { Text = "Không có dữ liệu", BackgroundColor=Color.BlueViolet };
            g.ItemsSource = emp;
            (Content as StackLayout).Children.Add(g);
        }
    }
    public class Employee
    {
        public string no { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
    }
}
