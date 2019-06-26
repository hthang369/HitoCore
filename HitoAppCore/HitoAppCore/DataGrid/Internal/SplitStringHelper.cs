using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HitoAppCore.DataGrid
{
    public class SplitStringHelper
    {
        // Fields
        private static Regex reg1 = new Regex(@"(\p{Ll})(\p{Lu})");
        private static Regex reg2 = new Regex(@"(\p{Lu}{2})(\p{Lu}\p{Ll}{2})");

        // Methods
        public static string SplitPascalCaseString(string value)
        {
            if (value == null) return string.Empty;
            string text1 = reg1.Replace(value, "$1 $2");
            value = text1;
            string text2 = reg2.Replace(value, "$1 $2");
            value = text2;
            return value;
        }
    }
}
