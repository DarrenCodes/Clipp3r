using System;
using System.Collections.Generic;
using System.Linq;

namespace Clipp3r.Core.Common
{
    public static class SortedListHelper
    {
        public static int GetNewElementIndex<T, TProperty>(ICollection<T> list, T item,
            Func<T, TProperty> selector) where TProperty : IComparable
        {
            int listSize = list.Count;

            if (listSize == 0)
                return 0;

            TProperty selectedItemComparisonValue = selector(item);

            if (selector(list.First()).CompareTo(selectedItemComparisonValue) > 0)
                return 0;
            else if (selector(list.Last()).CompareTo(selectedItemComparisonValue) < 0)
                return listSize;

            int upperLim = listSize;
            int lowerLim = 0;

            Func<int, int, int> halfPointerValue = (uLim, lLim) =>
                (int)Math.Round((uLim - lLim) / 2.0) + lLim;

            int halfPointer = halfPointerValue(upperLim, lowerLim);

            while (upperLim - lowerLim > 1)
            {
                var elementAtHalf = list.ElementAt(halfPointer - 1);
                var comparison = selector(elementAtHalf).CompareTo(selectedItemComparisonValue);

                if (comparison < 0)
                    lowerLim = halfPointer;
                else if (comparison > 0)
                    upperLim = halfPointer;
                else
                    return halfPointer;

                halfPointer = halfPointerValue(upperLim, lowerLim);
            }

            if (selector(list.ElementAt(lowerLim - 1)).CompareTo(selectedItemComparisonValue) > 0)
                return lowerLim - 1;
            else
                return upperLim - 1;
        }
    }
}
