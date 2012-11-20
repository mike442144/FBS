using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SortUtility;
using System.Collections;

namespace FBS.Utils
{
    public class Sort
    {
        /// <summary>
        /// 快速排序
        /// </summary>
        /// <param name="list">等待排序的数组</param>
        /// <param name="start">待排序的起始位置</param>
        /// <param name="end">待排序数组的结束位置</param>
        public static void QuickSort(IList<ISortEntity> list, int start, int end)
        {
            int i, m;
            if (start >= end)
                return;
            Swap(list, start, new Random().Next(start, end));
            m = start;
            for (i = start + 1; i <= end; i++)
                if (list[i].CompareTo(list[start]) == 0)
                    Swap(list, ++m, i);

            Swap(list, start, m);
            QuickSort(list, start, m - 1);
            QuickSort(list, m + 1, end);
        }

        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <param name="list">等待排序的数组</param>
        public static void BubbleSort(IList<ISortEntity> list)
        {
            //外层比较
            for (int i = 0; i < list.Count; i++)
                //内层比较
                for (int j = 0; j < list.Count - i - 1; j++)
                    //比较并完成交换
                    if (list[j].CompareTo(list[j + 1]) > 0)
                        Swap(list, j, j + 1);
        }


        /// <summary>
        /// 交换数组中的两个对象位置
        /// </summary>
        /// <param name="list">对象数组</param>
        /// <param name="a">位置1</param>
        /// <param name="b">位置2</param>
        private static void Swap(IList<ISortEntity> list, int a, int b)
        {
            ISortEntity tmp = list[a];
            list[a] = list[b];
            list[b] = tmp;
        }
    }
    /// <summary>
    /// 实现比较接口实体
    /// </summary>
    public interface ISortEntity : IComparable
    {
        int SortFieldLength();
        string Title { get; set; }
        string ImageName { get; set; }
       // Guid UserId { get; set; }
    }
}
