using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public class RecoComparer : IComparer<Reco>
    {
        public int Compare(Reco x, Reco y) => new DoubleComparer().Compare(x.Weight, y.Weight);
    }
}
