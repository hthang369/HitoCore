using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class HeaderView : BaseCellView
    {
        // Fields
        private const int imageSize = 0x30;
        private readonly ContentView contentView = new ContentView();
        private readonly Image sortOrderView = new Image();
        private readonly Label labelCaption = new Label();
        private Grid grid;
        private int nativeImageSize;
        private TextAlignment contentAlignment;
        private DataTemplate contentTemplate;

        // Methods
        public HeaderView()
        {
            this.InitDefaultHeaderContent();
            this.InitializeContent();
        }

        public void ChangeSortOrderImage(ColumnSortOrder columnSortOrder)
        {
            switch (columnSortOrder)
            {
                case ColumnSortOrder.None:
                    //this.sortOrderView.set_Source(this.HeaderCustomizer.CreateArrowNoneImageSource());
                    this.ImageColumnDefinition.Width = (new GridLength(0.0));
                    return;

                case ColumnSortOrder.Ascending:
                    //this.sortOrderView.set_Source(this.HeaderCustomizer.CreateArrowUpImageSource());
                    this.ImageColumnDefinition.Width = (new GridLength((double)this.nativeImageSize));
                    return;

                case ColumnSortOrder.Descending:
                    //this.sortOrderView.set_Source(this.HeaderCustomizer.CreateArrowDownImageSource());
                    this.ImageColumnDefinition.Width = (new GridLength((double)this.nativeImageSize));
                    return;
            }
        }

        private void InitDefaultHeaderContent()
        {
            this.contentView.Content = (this.labelCaption);
        }

        private void InitializeContent()
        {
            this.sortOrderView.HorizontalOptions = (this.contentAlignment == TextAlignment.End) ? LayoutOptions.Start : LayoutOptions.End;
            this.sortOrderView.VerticalOptions = LayoutOptions.Center;
            //this.HeaderCustomizer.Font.ApplyToLabel(this.labelCaption);
            this.labelCaption.VerticalOptions = LayoutOptions.Center;
            this.labelCaption.HorizontalOptions = LayoutOptions.FillAndExpand;
            this.labelCaption.HorizontalTextAlignment = this.ContentAlignment;
            this.labelCaption.LineBreakMode = LineBreakMode.NoWrap;
            Grid grid1 = new Grid();
            grid1.ColumnSpacing = (0.0);
            grid1.RowSpacing = (0.0);
            this.grid = grid1;
            this.grid.HorizontalOptions = (LayoutOptions.FillAndExpand);
            this.grid.VerticalOptions = (LayoutOptions.FillAndExpand);
            //IDisplayService service = GlobalServices.Instance.GetService<IDisplayService>();
            //this.nativeImageSize = (service != null) ? service.ConvertToNativeSize(0x30) : 0x30;
            if (this.contentAlignment == TextAlignment.End)
            {
                RowDefinition definition1 = new RowDefinition();
                definition1.Height = new GridLength(1.0, GridUnitType.Star);
                this.grid.RowDefinitions.Add(definition1);
                ColumnDefinition definition2 = new ColumnDefinition();
                definition2.Width = new GridLength((double)this.nativeImageSize);
                this.grid.ColumnDefinitions.Add(definition2);
                ColumnDefinition definition3 = new ColumnDefinition();
                definition3.Width = new GridLength(1.0, GridUnitType.Star);
                this.grid.ColumnDefinitions.Add(definition3);
                this.grid.Children.Add(this.sortOrderView, 0, 0);
                this.grid.Children.Add(this.contentView, 1, 0);
            }
            else
            {
                RowDefinition definition4 = new RowDefinition();
                definition4.Height = new GridLength(1.0, GridUnitType.Star);
                this.grid.RowDefinitions.Add(definition4);
                ColumnDefinition definition5 = new ColumnDefinition();
                definition5.Width = (new GridLength(1.0, GridUnitType.Star));
                this.grid.ColumnDefinitions.Add(definition5);
                ColumnDefinition definition6 = new ColumnDefinition();
                definition6.Width = new GridLength((double)this.nativeImageSize);
                this.grid.ColumnDefinitions.Add(definition6);
                this.grid.Children.Add(this.contentView, 0, 0);
                this.grid.Children.Add(this.sortOrderView, 1, 0);
            }
            base.Content = this.grid;
        }

        public void SetCaption(string caption)
        {
            this.labelCaption.Text = caption;
        }

        private void SetContentAlignment(TextAlignment contentAligment)
        {
            if (this.contentAlignment != contentAligment)
            {
                this.contentAlignment = contentAligment;
                this.InitializeContent();
            }
        }

        public void SetTemplate(DataTemplate template)
        {
            if (this.contentTemplate != template)
            {
                this.contentTemplate = template;
                if (this.contentTemplate != null)
                {
                    this.contentView.Content = this.contentTemplate.CreateContent() as View;
                }
                else
                {
                    this.InitDefaultHeaderContent();
                }
            }
        }

        public override void UnsubscribeEvents()
        {
            throw new NotImplementedException();
        }

        // Properties
        //private IHeaderCustomizer HeaderCustomizer =>
        //    ThemeManager.Theme.HeaderCustomizer;

        private ColumnDefinition ImageColumnDefinition =>
            ((this.contentAlignment == TextAlignment.End) ? this.grid.ColumnDefinitions[0] : this.grid.ColumnDefinitions[1]);

        public TextAlignment ContentAlignment
        {
            get =>
                this.contentAlignment;
            set =>
                this.SetContentAlignment(value);
        }
    }
}
