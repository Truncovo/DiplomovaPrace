using System.Collections.Generic;
using Engine;
using Engine.Shapes;
using Engine.XyObjects;
using NUnit.Framework;

namespace EngineUnitTests.Shapes
{
    [TestFixture]
    public class SquareTests
    {
        private Square _square; 
        [SetUp]
        public void SetUp()
        {
            _square = new Square(new PointMy(1,1),new SizeMy(2,2) );
        }

        [Test]
        public void Size_IsChanged_OnShapeEditedInvoked()
        {
            //SetUp
            var timesCalled = 0;
            _square.ShapeEdited += () => { timesCalled++; };
            //act
            _square.Size = new SizeMy(3, 3);
            //Assert
            Assert.That(timesCalled, Is.EqualTo(1));

        }

        [Test]
        public void Point_IsChanged_OnShapeEditedInvoked()
        {
            //SetUp
            var timesCalled = 0;
            _square.ShapeEdited += () => { timesCalled++; };
            //act
            _square.Point = new PointMy(3, 3);
            //Assert
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        public void Square_ZeroSquare_ReturnsPoints()
        {
            //SetUp
            _square = new Square();       
            //Act
            var res = _square.GetPoints();
            //Assert
            var expectedRes = new List<PointMy>
            {
                new PointMy(0, 0),
                new PointMy(0, 0),
                new PointMy(0, 0),
                new PointMy(0, 0)
            };
            CollectionAssert.AreEquivalent(expectedRes, res);
        }

        [Test]
        public void Square_BasicSquare_ReturnsPoints()
        {
            //SetUp
            //Act
            var res = _square.GetPoints();
            //Assert
            var expectedRes = new List<PointMy>
            {
                new PointMy(1, 1),
                new PointMy(1, 3),
                new PointMy(3, 3),
                new PointMy(3, 1)
            };
            CollectionAssert.AreEquivalent(expectedRes, res);
        }

        [Test]
        public void Clear_WhenCalled_PointAndSizeResetedTo0()
        {
            //SetUp
            //Act
            _square.Clear();
            //Assert
            Assert.That(_square.Size,Is.EqualTo(new SizeMy(0,0)));
            Assert.That(_square.Point, Is.EqualTo(new PointMy(0, 0)));
        }

        [Test]
        public void Clear_IsCalled_OnShapeEditedInvoked()
        {
            //SetUp
            var timesCalled = 0;
            _square.ShapeEdited += () => { timesCalled++; };
            //act
            _square.Clear();
            //Assert
            Assert.That(timesCalled, Is.EqualTo(1));
        }



        
    }
}
