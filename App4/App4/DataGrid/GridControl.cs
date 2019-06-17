using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class GridControl : GridLayout, IDisposable, IServiceProvider
    {
        static GridControl()
        {

        }
        public GridControl()
        {
            this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Star) });
            this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Auto) });

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        protected override void OnParentSet()
        {
            if (base.Parent != null)
            {

            }
            base.OnParentSet();
        }

        private enum RowDragDirection
        {
            None,
            Right,
            Left
        }
    }
}
