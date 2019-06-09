using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace HitoAppCore
{
	public class TestPage : ContentPage
	{
		public TestPage ()
		{
			Content = new StackLayout {
				Children = {
					new Label { Text = "Welcome to Xamarin.Forms!" }
				}
			};
            LoadControl();

        }
        public void LoadControl()
        {
            try
            {
                //Xamarin.Forms.DataGrid.DataGrid grid = new Xamarin.Forms.DataGrid.DataGrid();
                //grid.Columns.Add(new Xamarin.Forms.DataGrid.DataGridColumn { FieldName = "no", Caption = "no" });
                //grid.Columns.Add(new Xamarin.Forms.DataGrid.DataGridColumn { FieldName = "name", Caption = "name" });
                //(Content as StackLayout).Children.Add(grid);

                Xamarin.Forms.Calendar.Calendar calendar = new Xamarin.Forms.Calendar.Calendar();
                calendar.ShowInBetweenMonthLabels = true;
                calendar.ShowNumberOfWeek = true;
                calendar.EnableTitleMonthYearView = true;
                calendar.WeekdaysShow = true;
                (Content as StackLayout).Children.Add(calendar);

                DatePicker picker = new DatePicker();
                (Content as StackLayout).Children.Add(picker);

            }
            catch (Exception ex)
            {

            }
        }
	}
}