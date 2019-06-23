using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class CellView : BaseCellView
    {
        #region Fields
        private const int imageSize = 48;
        private IBaseEdit editor;
        internal CellData CellData { get; set; }
        private Grid CellConditional;
        private TextAlignment contentAlignment;
        private readonly Image sortOrderView;
        private readonly Label labelCaption;
        private GridColumn column;
        #endregion

        #region Contructor
        public CellView(GridColumn col)
        {
            column = col;
            this.sortOrderView = new Image();
            this.labelCaption = new Label();
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
            this.labelCaption.Text = caption;
        }
        public void SetContentAlignment(TextAlignment textAlignment)
        {
            this.contentAlignment = textAlignment;
        }
        private void InitializeContent()
        {
            this.sortOrderView.HorizontalOptions = this.contentAlignment == TextAlignment.End ? LayoutOptions.Start : LayoutOptions.End;
            this.sortOrderView.VerticalOptions = LayoutOptions.Center;
            this.sortOrderView.HorizontalOptions = LayoutOptions.Center;
            this.labelCaption.VerticalOptions = LayoutOptions.Center;
            this.labelCaption.HorizontalOptions = LayoutOptions.FillAndExpand;
            this.labelCaption.HorizontalTextAlignment = this.contentAlignment;
            this.labelCaption.LineBreakMode = LineBreakMode.NoWrap;
            this.CellConditional = new Grid();
            CellConditional.ColumnSpacing = 0;
            CellConditional.RowSpacing = 0;
            CellConditional.HorizontalOptions = LayoutOptions.FillAndExpand;
            CellConditional.VerticalOptions = LayoutOptions.FillAndExpand;
            CellConditional.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) });
            if(contentAlignment == TextAlignment.End)
            {
                CellConditional.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(imageSize) });
                CellConditional.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Auto) });
                CellConditional.Children.Add(sortOrderView, 0, 0);
                CellConditional.Children.Add(labelCaption, 1, 0);
            }
            else
            {
                CellConditional.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Auto) });
                CellConditional.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(imageSize) });
                CellConditional.Children.Add(labelCaption, 0, 0);
                CellConditional.Children.Add(sortOrderView, 1, 0);
            }
            if(column.AllowSort == DefaultBoolean.True)
            {
                TapGestureRecognizer tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += TapGesture_Tapped;
                CellConditional.GestureRecognizers.Add(tapGesture);
            }
            base.Content = CellConditional;
        }

        private void TapGesture_Tapped(object sender, EventArgs e)
        {
            
        }

        public void ResetContent()
        {
            if (this.editor == null)
            {
                base.Content = null;
            }
            else if (base.Content != this.editor.Editor)
            {
                base.Content = this.editor.Editor;
            }
        }

        internal void SetFontColorToInternalText(Color color)
        {
            this.Editor.FontColor = color;
        }

        public override void UnsubscribeEvents()
        {
            if (this.Editor != null)
            {
                this.Editor.Column = null;
            }
        }

        protected virtual void UpdateInternalControlContent()
        {
            if ((this.CellData != null) && (this.Editor != null))
            {
                if (this.CellData.DisplayText == null)
                {
                    this.Editor.EditValue = this.CellData.Value;
                }
                else
                {
                    this.Editor.EditValue = this.CellData.DisplayText;
                }
            }
        }
        #endregion

        #region Properties
        public IBaseEdit Editor
        {
            get => this.editor;
            set
            {
                if (!object.ReferenceEquals(this.editor, value))
                {
                    this.editor = value;
                    this.ResetContent();
                }
            }
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.CellData = base.BindingContext as CellData;
            if (this.CellData != null)
            {
                this.CellData.PropertyChanged += new PropertyChangedEventHandler(this.OnCellDataPropertyChanged);
                this.UpdateInternalControlContent();
            }
        }

        private void OnCellDataPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ((e.PropertyName == "Value") || (e.PropertyName == "DisplayText"))
            {
                this.UpdateInternalControlContent();
            }
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Content")
            {
                this.UpdateInternalControlContent();
            }
        }
        #endregion
    }
}
