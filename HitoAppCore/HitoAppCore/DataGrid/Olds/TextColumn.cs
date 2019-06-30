using System;
using System.Collections.Generic;
using System.Text;

namespace HitoAppCore.DataGrid
{
    public class TextColumn : GridColumn
    {
        protected override Type GetComparerPropertyType()
        {
            return typeof(string);
        }
    }
}
