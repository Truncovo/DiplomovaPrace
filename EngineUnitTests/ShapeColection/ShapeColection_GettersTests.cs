using System.Linq;
using Engine.ShapeColections;
using Engine.Shapes;
using NUnit.Framework;

namespace EngineUnitTests.Shapes
{
    /*
    [TestFixture]
    public class ShapeColection_GettersTests
    {
        private ShapeColection _shapeColection;

        private Square _selectedSquare;
        private Square _basicSquare;
        private Square _temprarySquare;

        [SetUp]
        public void SetUp()
        {

            _selectedSquare = new Square();
            _selectedSquare.State = ShapeStates.Selected;

            _basicSquare = new Square();
            _basicSquare.State = ShapeStates.Basic;

            _temprarySquare = new Square();
            _temprarySquare.State = ShapeStates.Temporary;

            _shapeColection = new ShapeColection();
            _shapeColection.Add(_basicSquare);
            _shapeColection.Add(_selectedSquare);
            _shapeColection.Add(_temprarySquare);
        }

        [Test]
        public void GetCollection_CalledWithNoAsArgument_ReturnsAllShapes()
        {
            //act
            var res = _shapeColection.GetColection();

            //assert
            Assert.AreEqual(3, res.Count());
            CollectionAssert.Contains(res,_basicSquare);
            CollectionAssert.Contains(res, _selectedSquare);
            CollectionAssert.Contains(res, _temprarySquare);

        }

        [Test]
        [TestCase(ShapeStates.Basic)]
        [TestCase(ShapeStates.Temporary)]
        [TestCase(ShapeStates.Selected)]
        public void GetCollection_CalledWithSelectedAsArgument_ReturnsOnlyShapesInSelectedState
            (ShapeStates shapeStates)
        {
            //act
            var res = _shapeColection.GetColection(shapeStates);

            //assert
            Assert.AreEqual(1,res.Count());
        }

    
    }
    */
}