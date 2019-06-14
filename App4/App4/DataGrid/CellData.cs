using System;
using System.Collections.Generic;
using System.Text;

namespace HitoAppCore.DataGrid
{
    public class CellData : NotificationObject
    {
        private object _value;
        private string _displayText;
        private bool _isSelected;
        private object _source;
        public object Value
        {
            get => _value;
            set
            {
                if(this._value != value)
                {
                    this._value = value;
                    base.OnPropertyChanged("Value");
                }
            }
        }
        public string DisplayText
        {
            get => _displayText;
            set
            {
                if (this._displayText != value)
                {
                    this._displayText = value;
                    base.OnPropertyChanged("DisplayText");
                }
            }
        }
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (this._isSelected != value)
                {
                    this._isSelected = value;
                    base.OnPropertyChanged("IsSelected");
                }
            }
        }
        public object Source
        {
            get => _source;
            set
            {
                if (this._source != value)
                {
                    this._source = value;
                    base.OnPropertyChanged("Source");
                }
            }
        }
        public CellIndex Index { get; set; }
    }
}
