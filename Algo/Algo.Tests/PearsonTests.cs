using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static Algo.RecoContext;

namespace Algo.Tests
{
    public class PearsonTests
    {
        [Test]
        public void Dummy()
        {
            var ratings1 = new List<int> { 1, 2, 3, 4, 5 };
            var ratings2 = new List<int> { 1, 2, 3, 4, 5 };
            var ratings3 = new List<int> { 5, 4, 3, 2, 1 };
            var ratings4 = new List<int> { 5, 2, 4, 4, 3 };
            var ratings5 = new List<int> { 3, 3, 3, 3, 3 };

            Assert.That(Pearson(ratings1, ratings2), Is.EqualTo(1).Within(0.0001));
            Assert.That(Pearson(ratings1, ratings3), Is.EqualTo(-1).Within(0.0001));
            Assert.That(Pearson(ratings1, ratings4), Is.EqualTo(-0.277350098).Within(0.0001));
            Assert.That(Pearson(ratings1, ratings5), Is.NaN);
        }
    }
}
