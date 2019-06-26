// Generated by .NET Reflector from D:\workspace\mobile\hito-mobile\HitoApp\lampart_lib_dll\DevExpress.XamarinForms.Grid.dll
namespace DevExpress.XamarinForms.DataGrid.Internal
{
    using DevExpress.Compatibility.System;
    using DevExpress.XamarinForms.Core;
    using System;
    using System.Collections.Generic;
    
    public static class MergeSort
    {
        private const int MaxSequentialElementCount = 0x400;
        
        private static int GetMaxDepth() => 
            Math.Max(0, (int) Math.Round(Math.Log((double) Environment.ProcessorCount, (double) 2.0)));
        
        private static void ParallelSort<T>(IList<T> list, int from, int to, IComparer<T> comparer, int depth, T[] buffer, IParallelService service)
        {
            if ((((to - from) + 1) <= 0x400) || (depth <= 0))
            {
                SequentialSort<T>(list, from, to, comparer, buffer);
            }
            else if (to > from)
            {
                int mid = (from + to) / 2;
                depth--;
                IComparer<T> comparer2 = comparer;
                System.ICloneable cloneable = comparer2 as System.ICloneable;
                if (cloneable != null)
                {
                    comparer2 = (IComparer<T>) cloneable.Clone();
                }
                Action[] actions = new Action[] { delegate {
                    ParallelSort<T>(list, from, mid, comparer, depth, buffer, service);
                }, delegate {
                    ParallelSort<T>(list, mid + 1, to, comparer2, depth, buffer, service);
                } };
                service.Invoke(actions);
                SequentialMerge<T>(list, from, mid, to, comparer, buffer);
            }
        }
        
        private static void SequentialMerge<T>(IList<T> array, int left, int mid, int right, IComparer<T> comparer, T[] buffer)
        {
            T[] localArray = buffer;
            int num = (array.Count == buffer.Length) ? left : 0;
            int index = num;
            int num3 = left;
            int num4 = mid + 1;
            while ((num3 <= mid) && (num4 <= right))
            {
                if (comparer.Compare(array.GetItem(num3), array.GetItem(num4)) <= 0)
                {
                    index++;
                    num3++;
                    localArray[index] = array.GetItem(num3);
                    continue;
                }
                index++;
                num4++;
                localArray[index] = array.GetItem(num4);
            }
            while (num3 <= mid)
            {
                index++;
                num3++;
                localArray[index] = array.GetItem(num3);
            }
            while (num4 <= right)
            {
                index++;
                num4++;
                localArray[index] = array.GetItem(num4);
            }
            if (num == 0)
            {
                for (int i = 0; i < index; i++)
                {
                    array.SetItem((int) (i + left), localArray[i]);
                }
            }
            else
            {
                for (int i = left; i < index; i++)
                {
                    array.SetItem(i, localArray[i]);
                }
            }
        }
        
        private static void SequentialSort<T>(IList<T> list, int from, int to, IComparer<T> comparer, T[] buffer)
        {
            if (to > from)
            {
                int num = (from + to) / 2;
                SequentialSort<T>(list, from, num, comparer, buffer);
                SequentialSort<T>(list, num + 1, to, comparer, buffer);
                SequentialMerge<T>(list, from, num, to, comparer, buffer);
            }
        }
        
        public static void Sort<T>(IList<T> list, int from, int to, IComparer<T> comparer)
        {
            IParallelService service = GlobalServices.Instance.GetService<IParallelService>();
            T[] buffer = new T[(to - from) + 1];
            if ((((to - from) + 1) <= 1024) || (service == null))
            {
                SequentialSort<T>(list, from, to, comparer, buffer);
            }
            else
            {
                ParallelSort<T>(list, from, to, comparer, GetMaxDepth(), buffer, service);
            }
        }
    }
}
