using Algo;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Tests
{
    public class BestKeeperTests
    {
        [Test]
        public void BestKeeper_GetTop_WithoutItems_ReturnsEmptyArray()
        {
            var keeper = new BestKeeper<double>(10, new DoubleComparer());

            double[] top = keeper.GetTop();

            Assert.That(top, Is.Empty);
        }

        [Test]
        public void BestKeeper_GetTop_WithoutLessItemsThanCapacity_ReturnsAllItems()
        {
            var keeper = new BestKeeper<double>(5, new DoubleComparer());
            keeper.Add(1.0);
            keeper.Add(1.2);
            keeper.Add(1.1);
            keeper.Add(1.3);

            double[] top = keeper.GetTop();

            Assert.That(top, Is.EquivalentTo(new[] { 1.0, 1.1, 1.2, 1.3 }));
        }

        [Test]
        public void BestKeeper_GetTop_WithoutMoreItemsThanCapacity_ReturnsTopItems()
        {
            var keeper = new BestKeeper<double>(5, new DoubleComparer());
            keeper.Add(1.8);
            keeper.Add(1.1);
            keeper.Add(100);
            keeper.Add(1.2);
            keeper.Add(1.3);
            keeper.Add(1.5);
            keeper.Add(1.4);
            keeper.Add(1.6);
            keeper.Add(1.7);
            keeper.Add(1.0);

            double[] top = keeper.GetTop();

            GlobalSettings.DefaultFloatingPointTolerance = 0.001;
            Assert.That(top, Is.EquivalentTo(new[] { 100, 1.8, 1.7, 1.6, 1.5 }));
        }
    }
}
