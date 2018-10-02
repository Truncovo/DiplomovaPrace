using System.Linq;
using Engine;
using Engine.ShapeColection;
using Engine.Shapes;
using Engine.XyObjects;
using NUnit.Framework;

namespace EngineUnitTests.Shapes
{
    [TestFixture]
    public class ShapeColectionTests
    {
        private ShapeColection _shapeColection;
        private int _timesCalled;

        [SetUp]
        public void SetUp()
        {
            _shapeColection = new ShapeColection();

            _timesCalled = 0;
            _shapeColection.Changed += (obj) => { _timesCalled++; };
        }

        [Test]
        public void Add_WhenCalled_ShapeIsAddedAndOnChangedIsInvoked()
        {
            //arange
            var square = new Square();
            var polygon = new Polygon();
            //act
            _shapeColection.Add(square);
            _shapeColection.Add(polygon);


            //assert
            CollectionAssert.Contains(_shapeColection.GetColection(),square);
            CollectionAssert.Contains(_shapeColection.GetColection(), polygon);
            Assert.AreEqual(2,_shapeColection.GetColection().Count());

            Assert.AreEqual(2,_timesCalled);
        }

        [Test]
        public void Add_WhenAddedShapeIsChanged_ChangedIsInvoked()
        {
            //arange
            var square = new Square();
            _shapeColection.Add(square);
            _timesCalled = 0;
            
            //act
            square.Size = new SizeMy(5,5);

            //assert
            Assert.AreEqual(1,_timesCalled);
        }

        [Test]
        public void Remove_WhenCalled_ShapeIsRemovedAndChangedIsInvoked()
        {
            //arange
            var square = new Square();
            var polygon = new Polygon();
            _shapeColection.Add(square);
            _shapeColection.Add(polygon);
            _timesCalled = 0;
            
            //act
            _shapeColection.Remove(square);

            //Assert
            CollectionAssert.Contains(_shapeColection.GetColection(), polygon);
            Assert.AreEqual(1, _shapeColection.GetColection().Count());

            Assert.AreEqual(1, _timesCalled);
        }

        [Test]
        public void Remove_RemovedShapeIsChanged_nChangedIsNOTInvoked()
        {
            //arange
            var square = new Square();
            _shapeColection.Add(square);
            _shapeColection.Remove(square);
            _timesCalled = 0;

            //act
            square.Size = new SizeMy(2,2);

            //assert
            Assert.AreEqual(0,_timesCalled);
        }

        [Test]
        public void Clear_WhenCalled_AllShapesAreDelletedAndChangedIsCalled()
        {
            //arange
            var square = new Square();
            var polygon = new Polygon();
            _shapeColection.Add(square);
            _shapeColection.Add(polygon);
            _timesCalled = 0;

            //act
            _shapeColection.Clear();

            //Assert
            CollectionAssert.IsEmpty(_shapeColection.GetColection());
            Assert.AreEqual(1,_timesCalled);
        }

        [Test]
        public void Clear_WhenDelletedShapesAreChanged_ChangedIsNOTInvoked()
        {
            //arange
            var square = new Square();
            var polygon = new Polygon();
            _shapeColection.Add(square);
            _shapeColection.Add(polygon);
            _shapeColection.Clear();
            _timesCalled = 0;

            //act
            square.Point = new PointMy(5,5);
            polygon.Add(new PointMy(2,2));

            //Assert
            Assert.AreEqual(0,_timesCalled);
        }

    }
}