using Engine.Shapes;
using Engine.XyObjects;
using NUnit.Framework;
using System.Collections.Generic;

namespace EngineUnitTests.Shapes
{
    [TestFixture]
    public class PolygonTests
    {
        private Polygon _polygon;
        private int _timesCalled;
        private List<PointMy> _pointsInPolygon;
        [SetUp]
        public void SetUp()
        {
            _pointsInPolygon = new List<PointMy>
            {
                new PointMy(1, 1),
                new PointMy(2, 2),
                new PointMy(3, 3)
            };

            _polygon = new Polygon();
            _polygon.Add(new PointMy(1,1));
            _polygon.Add(new PointMy(2, 2));
            _polygon.Add(new PointMy(3, 3));

            _timesCalled = 0;
            _polygon.ShapeEdited += () => { _timesCalled++; };
        }

        [Test]
        public void GetPoints_WhenCalled_ReturnsAllAddedPoints()
        {
            //arange
            //act
            var res = _polygon.GetPoints();
            //assert
            CollectionAssert.AreEquivalent(_pointsInPolygon,res);
        }

        [Test]
        public void Add_WhenCalled_PointIsAdded()
        {
            //act
            _polygon.Add(new PointMy(4,4));
            //assert
            _pointsInPolygon.Add(new PointMy(4,4));
            CollectionAssert.AreEquivalent(_pointsInPolygon,_polygon.GetPoints());
        }

        [Test]
        public void Add_WhenCalled_OnShapeEditedIsInvoked()
        {
            //act
            _polygon.Add(new PointMy(4, 4));
            //assert
            Assert.AreEqual(_timesCalled,1);
        }

        [Test]
        public void Clear_WhenCalled_PointsAreEmpty()
        {
            //act
            _polygon.Clear();
            //assert
            CollectionAssert.IsEmpty(_polygon.GetPoints());
        }
        [Test]
        public void Clear_WhenCalled_OnShapeEditedIsInvoked()
        {
            //act
            _polygon.Clear();
            //assert
            Assert.AreEqual(_timesCalled,1);
        }
    }
}