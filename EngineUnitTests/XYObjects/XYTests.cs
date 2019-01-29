using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Engine.XyObjects;
using NUnit.Framework;

namespace EngineUnitTests.XYObjects
{
    [TestFixture]
    class XYTests
    {
        [Test]
        public void Clone_WhenCalled_ClonedObjectIsEqualToSourceOne()
        {
            //arange
            var source = new XY(1, 2);

            //act
            var res = (XY)source.Clone();

            //assert
            Assert.AreEqual(source.X,res.X);
            Assert.AreEqual(source.Y, res.Y);
        }

        [Test]
        public void Equals_CalledWithSameObjectAsSecond_ReturnsTrue()
        {
            var source = new XY(1, 2);

            var res = source.Equals(source);

            Assert.IsTrue(res);
        }

        [Test]
        public void Equals_CalledWithEqualXY_ReturnsTrue()
        {
            var source = new XY(1, 2);
            var second = new XY(1, 2);
            var res = source.Equals(second);

            Assert.IsTrue(res);
        }

        [Test]
        [TestCase(2, 2)]
        [TestCase(1, 1)]
        [TestCase(0, 0)]
        public void Equals_CalledWithDiferentXY_ReturnsFalse(int x, int y)
        {
            var source = new XY(1, 2);
            var second = new XY(x, y);

            var res = source.Equals(second);

            Assert.IsFalse(res);
        }
        [Test]
        public void Equals_CalledWithDiferentObject_ReturnsFalse()
        {
            var source = new XY(1, 2);
            var second = String.Empty;

            var res = source.Equals(second);

            Assert.IsFalse(res);
        }

        [Test]
        public void Equals_CalledWithSameXYasObject_ReturnsTrue()
        {
            var source = new XY(1, 2);
            var second = (object)source;

            var res = source.Equals(second);

            Assert.IsTrue(res);
        }


        [TestCase(0, 0, 0, 2, 2)]
        [TestCase(1, 1, 2, 2, 1.41421d)]
        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(5, 5, 5, 5, 0)]
        [TestCase(1, 2, 4, 4, 3.60555d)]

        public void ELengthOfLine_WhenCalled_CalculateRightDouble(double x1, double y1, double x2, double y2, double expectedResult)
        {
            var first = new PointMy(x1, y1);
            var second =new PointMy(x2, y2);

            var res = PointMy.LengthOfLine(first,second);

            Assert.That(res,Is.EqualTo(expectedResult).Within(0.001d));
        }
    }
}
