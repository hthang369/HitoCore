using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class GridLayout : Grid
    {
        // Fields
        private int updateCount;
        private SizeRequest previousSizeRequest = new SizeRequest(new Size(0.0, 0.0));

        // Methods
        protected void BeginUpdate()
        {
            this.updateCount++;
        }

        protected void CancelUpdate()
        {
            this.updateCount--;
        }

        protected void EndUpdate()
        {
            this.updateCount--;
            if (this.updateCount <= 0)
            {
                base.ForceLayout();
            }
        }

        //protected override void LayoutChildren(double x, double y, double width, double height)
        //{
        //    if (this.updateCount <= 0)
        //    {
        //        base.LayoutChildren(x, y, width, height);
        //    }
        //}

        //protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        //{
        //    if (this.updateCount <= 0)
        //    {
        //        this.previousSizeRequest = base.Measure(widthConstraint, heightConstraint, 0);
        //    }
        //    return this.previousSizeRequest;
        //}
    }
}
