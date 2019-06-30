using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public abstract class BaseCellView : ContentView
    {
        public BaseCellView()
        {
            base.InputTransparent = true;
            base.IsClippedToBounds = true;
        }
        public abstract void UnsubscribeEvents();
    }
}
