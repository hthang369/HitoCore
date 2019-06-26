using System;
using System.Collections.Generic;
using System.Text;

namespace HitoAppCore.DataGrid
{
    public interface IHorizontalScrollingData
    {
        double HorizontalScrollOffset { get; }
        double VisibleRowWidth { get; }
        bool ColumnsAutoWidth { get; }
        bool AllowHorizontalScrollingVirtualization { get; }
    }
}
