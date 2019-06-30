using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public interface IBaseEdit
    {
        event EventHandler IsFocusedChanged;

        Color FontColor { get; set; }
        Color BackgroundColor { get; set; }
        object EditValue { get; set; }
        bool IsFocused { get; set; }
        View Editor { get; }
        bool UseFormEditorsFont { get; set; }
        ICommand EditValueChangedCommand { get; set; }
        GridColumn Column { get; set; }
    }
}
