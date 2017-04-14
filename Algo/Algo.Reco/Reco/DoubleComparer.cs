using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public class DoubleComparer : IComparer<double>
    {
        public int Compare(double x, double y) => x > y ? 1 : (x < y ? -1 : 0);
    }
}
