using System;
using System.Collections.Generic;
using System.Text;

namespace HitoAppCore.DataGrid
{
    public static class ListHelper
    {
        // Methods
        public static bool AreEqual<T>(IList<T> list1, IList<T> list2)
        {
            if ((list1 == null) || (list2 == null))
            {
                return object.ReferenceEquals(list1, list2);
            }
            if (list1.Count != list2.Count)
            {
                return false;
            }
            for (int i = 0; i < list1.Count; i++)
            {
                if (!list1[i].Equals(list2[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static T Find<T>(IList<T> list, Predicate<T> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (match(list[i]))
                {
                    return list[i];
                }
            }
            return default(T);
        }
    }
}
