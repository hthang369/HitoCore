using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class CellView : BaseCellView
    {
        private IBaseEdit editor;
        internal CellData CellData { get; set; }
        public IBaseEdit Editor
        {
            get =>
                this.editor;
            set
            {
                if (!object.ReferenceEquals(this.editor, value))
                {
                    this.editor = value;
                    this.ResetContent();
                }
            }
        }

        private CellConditionalFormattingLayer AddConditionalFormattingLayer()
        {
            if (this.HasConditionalFormattingLayer())
            {
                return (base.Content as CellConditionalFormattingLayer);
            }
            View item = base.Content;
            CellConditionalFormattingLayer layer = new CellConditionalFormattingLayer();
            base.Content = layer;
            layer.Children.Add(item);
            return layer;
        }
        public T AddToConditionalFormattingLayer<T>() where T : View, new()
        {
            CellConditionalFormattingLayer layer = this.AddConditionalFormattingLayer();
            if (layer.Children.Count <= 0)
            {
                layer.Children.Add(Activator.CreateInstance<T>());
            }
            T item = layer.Children[0] as T;
            if (item == null)
            {
                if (layer.Children.Count > 1)
                {
                    layer.Children.RemoveAt(0);
                }
                item = Activator.CreateInstance<T>();
                layer.Children.Insert(0, item);
            }
            return item;
        }
        public void ClearConditionalFormatting()
        {
            this.RemoveConditionalFormattingLayer();
        }

        private bool HasConditionalFormattingLayer() =>
            base.Content is CellConditionalFormattingLayer;

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

        private void RemoveConditionalFormattingLayer()
        {
            if (this.HasConditionalFormattingLayer())
            {
                CellConditionalFormattingLayer layer = base.get_Content() as CellConditionalFormattingLayer;
                if ((layer != null) && (layer.get_Children().Count > 0))
                {
                    View view = layer.get_Children()[layer.get_Children().Count - 1];
                    layer.Children.Clear();
                    base.Content = view;
                }
            }
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

        internal void SetDefaultFontAttributes()
        {
            LabelBaseEdit editor = this.Editor as LabelBaseEdit;
            if (editor != null)
            {
                editor.SetDefaultFontAttributes();
            }
            LookupLabelBaseEdit edit2 = this.Editor as LookupLabelBaseEdit;
            if (edit2 != null)
            {
                edit2.SetDefaultFontAttributes();
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


    }
}
