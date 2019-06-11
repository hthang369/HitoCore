using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public class AppCore
    {
    }
    public static class ListExtra
    {
        public static T GetItem<T>(this IList<T> lst, int key)
        {
            return lst[key];
        }
        public static void SetItem<T>(this IList<T> lst, int key, T val)
        {
            lst[key] = val;
        }

        public static void SetItem<TKey, TVal>(this IDictionary<TKey, TVal> lst, TKey key, TVal val)
        {
            lst[key] = val;
        }
    }
}
