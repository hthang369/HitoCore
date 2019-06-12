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
                DevExpress.XamarinForms.Core.Themes.ThemeManager.ThemeName = DevExpress.XamarinForms.Core.Themes.Theme.Light;
                DevExpress.XamarinForms.DataGrid.DataGridView view = new DevExpress.XamarinForms.DataGrid.DataGridView();
                view.Columns.Add(new DevExpress.XamarinForms.DataGrid.TextColumn { FieldName = "no" });
                view.Columns.Add(new DevExpress.XamarinForms.DataGrid.TextColumn { FieldName = "name" });
                view.HeaderStyle = new DevExpress.XamarinForms.DataGrid.HeaderStyle(view);
                view.HeaderStyle.Padding = new Thickness(5, 5, 5, 5);
                view.HeaderStyle.BackgroundColor = Color.FromHex("#333333");
                view.HeaderStyle.FontColor = Color.FromHex("#929292");
                view.HeaderStyle.FontSize = 12;
                //testView view = new testView();
                view.BorderColor = Color.Red;
                view.WidthRequest = (double)300;
                view.HeightRequest = 200;
                ContentView page = new ContentView();
                page.Content = view;
                (Content as StackLayout).Children.Add(page);

                //Xamarin.Forms.Calendar.Calendar calendar = new Xamarin.Forms.Calendar.Calendar();
                //calendar.ShowInBetweenMonthLabels = true;
                //calendar.ShowNumberOfWeek = true;
                //calendar.EnableTitleMonthYearView = true;
                //calendar.WeekdaysShow = true;
                //(Content as StackLayout).Children.Add(calendar);

                DatePicker picker = new DatePicker();
                (Content as StackLayout).Children.Add(picker);

            }
            catch (Exception ex)
            {

            }
        }
	}
    class testView : View
    {

    }
}