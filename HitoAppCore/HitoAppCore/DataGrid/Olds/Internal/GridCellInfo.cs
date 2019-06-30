using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HitoAppCore.DataGrid
{
    public class GridCellInfo
    {
        // Fields
        private int cellHandle;
        private View cellView;
        private GridColumn column;
        private Rectangle realCellPosition;

        // Methods
        public GridCellInfo(int cellHandle, View cellView, GridColumn column, Rectangle realCellPosition)
        {
            this.cellHandle = cellHandle;
            this.cellView = cellView;
            this.column = column;
            this.realCellPosition = realCellPosition;
        }

        // Properties
        public int CellHandle
        {
            get =>
                this.cellHandle;
            set =>
                this.cellHandle = value;
        }

        public View CellView
        {
            get =>
                this.cellView;
            set =>
                this.cellView = value;
        }

        public GridColumn Column =>
            this.column;

        public Rectangle RealCellPosition =>
            this.realCellPosition;
    }
}
