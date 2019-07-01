using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.DataGrid
{
    public class TextColumn : GridColumn
    {
        protected override Type GetComparerPropertyType()
        {
            return typeof(string);
        }
    }
}
