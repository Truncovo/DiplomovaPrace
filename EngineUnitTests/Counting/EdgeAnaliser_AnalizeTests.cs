using Engine.Counts;
using Engine.XyObjects;
using NUnit.Framework;

namespace EngineUnitTests.Counting
{
    [TestFixture]
    class EdgeAnaliser_AnalizeTests
    {
        private EdgeAnaliser _edgeAnaliser;

        [SetUp]
        public void SetUp()
        {
            _edgeAnaliser = new EdgeAnaliser();

        }

        //diferent angles - returns status 7
        [TestCase(0,0,1,1,2,2,4,3,7)]

        //same angles, different X crossing - returns status 6
        [TestCase(0, 0, 1, 1, 1, 2, 2, 3, 6)]


        //same angle (0 or 90) different axle crossing  - returns status 6 or -6
        [TestCase(0, 0,  1, 0,      1, 1,   2, 1,    -6)]
        [TestCase(0, 0,  0, 1,      1, 1,   1, 2,    6)]

        //same angle, same axle crossing - but NOT coliding with each other
        [TestCase(0, 0, 1, 1, 2, 2, 3, 3, 5)]
        [TestCase(0, 0, 1, 1, 1, 1, 3, 3, 5)]

        //same angle, same axle crossing - but coliding with each other
        [TestCase(0, 0, 2, 2, 1, 1, 3, 3, -2)]

        [TestCase(0, 0, 0, 2, 0, 1, 0, 3, -2)]
        [TestCase(0, 0, 2, 0, 1, 0, 3, 0, -2)]

        [TestCase(0, 0, 2, 0, 1, 0, 3, 0, -2)]

        public void WhenCalled_ReturnsRightStatus(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4, int status)
        {

            var res = _edgeAnaliser.Analize(new PointMy(x1, y1), new PointMy(x2, y2), new PointMy(x3, y3), new PointMy(x4, y4));

            Assert.AreEqual(status,res.Status);
        }

        [TestCase(1, 1, 5, 3, 3, 2, 7, 4, -2)]

        //[TestCase(5, 3, 1, 1, 3, 2, 7, 4, -2)]
        //[TestCase(5, 3, 1, 1, 7, 4, 3, 2, -2)]


        public void WhenCalled_ReturnsRightPoints(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4, int status)
        {
            var res = _edgeAnaliser.Analize(new PointMy(x1, y1), new PointMy(x2, y2), new PointMy(x3, y3), new PointMy(x4, y4));

            var expectedRes = new EdgeAnaliserResult();

            expectedRes.InContactFirst.Add(false);
            expectedRes.InContactFirst.Add(true);

            expectedRes.InContactSecond.Add(true);
            expectedRes.InContactSecond.Add(false);

            expectedRes.AddFirst.Add(new PointMy(3,2));
            expectedRes.AddSecond.Add(new PointMy(5,3));

            expectedRes.Status = status;

            Assert.AreEqual(expectedRes, res);
        }
        [TestCase(1, 1, 5, 3, 7, 4, 3, 2, -2)]

        public void WhenCalled_ReturnsRightPoints2(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4, int status)
        {
            var res = _edgeAnaliser.Analize(new PointMy(x1, y1), new PointMy(x2, y2), new PointMy(x3, y3), new PointMy(x4, y4));

            var expectedRes = new EdgeAnaliserResult();

            expectedRes.InContactFirst.Add(false);
            expectedRes.InContactFirst.Add(true);

            expectedRes.InContactSecond.Add(false);
            expectedRes.InContactSecond.Add(true);

            expectedRes.AddFirst.Add(new PointMy(3, 2));
            expectedRes.AddSecond.Add(new PointMy(5, 3));

            expectedRes.Status = status;

            Assert.AreEqual(expectedRes, res);
        }

        [TestCase(200, 200, 200, 0, 200, 100, 200, 400, -2)]

        public void WhenCalled_ReturnsRightPoints3(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4, int status)
        {

            var res = _edgeAnaliser.Analize(new PointMy(x1, y1), new PointMy(x2, y2), new PointMy(x3, y3), new PointMy(x4, y4));

            var expectedRes = new EdgeAnaliserResult();

            expectedRes.InContactFirst.Add(true);
            expectedRes.InContactFirst.Add(false);

            expectedRes.InContactSecond.Add(true);
            expectedRes.InContactSecond.Add(false);

            expectedRes.AddFirst.Add(new PointMy(200, 100));
            expectedRes.AddSecond.Add(new PointMy(200, 200));

            expectedRes.Status = status;

            Assert.AreEqual(expectedRes, res);
        }

        [TestCase(1, 1, 2, 2, 1, 1, 2, 2, 7)]

        public void WhenCalled_ReturnsRightPoints4(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4, int status)
        {

            var res = _edgeAnaliser.Analize(new PointMy(x1, y1), new PointMy(x2, y2), new PointMy(x3, y3), new PointMy(x4, y4));

            var expectedRes = new EdgeAnaliserResult();

            expectedRes.InContactFirst.Add(true);

            expectedRes.InContactSecond.Add(true);


            expectedRes.Status = status;

            Assert.AreEqual(expectedRes, res);
        }
    }
}