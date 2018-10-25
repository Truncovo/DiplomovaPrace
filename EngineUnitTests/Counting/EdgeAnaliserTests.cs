using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Counts;
using Engine.XyObjects;
using NUnit.Framework;

namespace EngineUnitTests.Counting
{
    [TestFixture]
    class EdgeAnaliserTests
    {
        [TestCase(1, 1, 2, 2, 0.7853981d)]
        [TestCase(2, 2, 1, 1, 0.7853981d)]
        [TestCase(2, 2, 4, 3, 0.463647d)]
        [TestCase(4, 3, 2, 2, 0.463647d)]
        [TestCase(-1, -1, 1, 1, 0.7853981d)]
        [TestCase(1, 1, -1, -1, 0.7853981d)]
        [TestCase(2, 2, 4, 3,0.463647d )]

        public void CountAngle_WhenCalled_GivesResult(double x1, double y1, double x2, double y2, double expectedRes)
        {
            var res = EdgeAnaliser.CountAngle(new PointMy(x1, y1), new PointMy(x2, y2));

            Assert.That(res, Is.EqualTo(expectedRes).Within(0.001d));
        }

        [TestCase(1, 1, 2, 2, 0)]
        [TestCase(2, 2, 1, 1, 0)]
        [TestCase(-2, -2, -1, -1, 0)]
        [TestCase(-2, -2, -1, -1, 0)]
        [TestCase(-3, -3, -1, -2, 3)]
        [TestCase(-3, -3, -1, -2, 3)]
        public void CountXCrossing_WhenCalled_GivesResult(double x1, double y1, double x2, double y2,
            double expectedRes)
        {
            var res = EdgeAnaliser.CountXCrossing(new PointMy(x1, y1), new PointMy(x2, y2));

            Assert.That(res, Is.EqualTo(expectedRes).Within(0.001d));
        }
    }
}