using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public static class Extensions
    {
        public static IEnumerable<T> BestKeep<T>(this IEnumerable<T> @this, int count, IComparer<T> comparer)
        {
            var keeper = new BestKeeper<T>(count, comparer);

            foreach (T item in @this)
            {
                keeper.Add(item);
            }

            return keeper.GetTop();
        }
    }
}
