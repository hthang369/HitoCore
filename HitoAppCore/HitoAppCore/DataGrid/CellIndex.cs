using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Xamarin.Forms.DataGrid
{
    [StructLayout(LayoutKind.Sequential)]
    public class CellIndex
    {
        private readonly int rowHandle;
        private readonly string fieldName;
        public static CellIndex InvalidIndex =>
            new CellIndex(-2_147_483_648, string.Empty);
        public static bool operator ==(CellIndex a, CellIndex b) =>
            ((a.rowHandle == b.rowHandle) && (a.fieldName == b.fieldName));

        public static bool operator !=(CellIndex a, CellIndex b) =>
            !(a == b);

        public int RowHandle =>
            this.rowHandle;
        public string FieldName =>
            this.fieldName;
        public bool IsValid =>
            (this != InvalidIndex);
        public CellIndex(int rowHandle, string fieldName)
        {
            this.fieldName = fieldName;
            this.rowHandle = rowHandle;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || (obj.GetType() != this.GetType()))
            {
                return false;
            }
            CellIndex index = (CellIndex)obj;
            return ((index.rowHandle == this.rowHandle) && (index.fieldName == this.fieldName));
        }

        public override int GetHashCode() =>
            (this.rowHandle ^ ((this.fieldName == null) ? 0 : this.fieldName.GetHashCode()));
    }
}
