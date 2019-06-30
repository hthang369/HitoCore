using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Xamarin.Forms.DataGrid
{
    public class CellView : GridLayout
    {
        #region Fields
        private const int imageSize = 48;
        //internal CellData CellData { get; set; }
        private TextAlignment contentAlignment;
        private readonly Image SortingIcon;
        private readonly Label HeaderLabel;
        private GridColumn column;
        private DataGrid gridControl;
        #endregion

        #region Contructor
        public CellView(GridColumn col, DataGrid grid)
        {
            column = col;
            gridControl = grid;
            this.SortingIcon = new Image();
            this.HeaderLabel = new Label();
            this.InitCellView(column.Caption, column.ContentAlignment);
        }
        public void InitCellView(string caption, TextAlignment textAlignment)
        {
            SetCaption(caption);
            SetContentAlignment(textAlignment);
            this.InitializeContent();
        }
        #endregion

        #region Methods
        public void SetCaption(string caption)
        {
            this.HeaderLabel.Text = caption;
        }
        public void SetContentAlignment(TextAlignment textAlignment)
        {
            this.contentAlignment = textAlignment;
        }
        private void InitializeContent()
        {
            this.SortingIcon.HorizontalOptions = this.contentAlignment == TextAlignment.End ? LayoutOptions.Start : LayoutOptions.End;
            this.SortingIcon.VerticalOptions = LayoutOptions.Center;
            this.SortingIcon.HorizontalOptions = LayoutOptions.Center;
            this.SortingIcon.Style = column.HeaderLabelStyle ?? gridControl.HeaderLabelStyle ?? gridControl.Resources[typeof(Image).FullName] as Style;
            this.HeaderLabel.VerticalOptions = LayoutOptions.Center;
            this.HeaderLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
            this.HeaderLabel.HorizontalTextAlignment = this.contentAlignment;
            this.HeaderLabel.LineBreakMode = LineBreakMode.NoWrap;
            this.HeaderLabel.Style = gridControl.Resources[typeof(Label).FullName] as Style;
            this.HorizontalOptions = LayoutOptions.FillAndExpand;
            this.VerticalOptions = LayoutOptions.FillAndExpand;
            this.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) });
            if(contentAlignment == TextAlignment.End)
            {
                this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(imageSize) });
                this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Auto) });
                this.Children.Add(SortingIcon, 0, 0);
                this.Children.Add(HeaderLabel, 1, 0);
            }
            else
            {
                this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Auto) });
                this.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(imageSize) });
                this.Children.Add(HeaderLabel, 0, 0);
                this.Children.Add(SortingIcon, 1, 0);
            }
            if(column.AllowSort == DefaultBoolean.True)
            {
                TapGestureRecognizer tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += TapGesture_Tapped;
                this.GestureRecognizers.Add(tapGesture);
            }
        }

        private void TapGesture_Tapped(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Properties
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            //this.CellData = base.BindingContext as CellData;
            //if (this.CellData != null)
            //{
            //    this.CellData.PropertyChanged += new PropertyChangedEventHandler(this.OnCellDataPropertyChanged);
            //    this.UpdateInternalControlContent();
            //}
        }

        private void OnCellDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ((e.PropertyName == "Value") || (e.PropertyName == "DisplayText"))
            {
                //this.UpdateInternalControlContent();
            }
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Content")
            {
                //this.UpdateInternalControlContent();
            }
        }
        #endregion
    }
}
