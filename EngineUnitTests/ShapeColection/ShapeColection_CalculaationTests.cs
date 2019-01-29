using System;
using System.Linq;
using Engine;
using Engine.Logger;
using Engine.ShapeColections;
using Engine.Shapes;
using Engine.XyObjects;
using NUnit.Framework;

namespace EngineUnitTests.Shapes
{
    [TestFixture]

    public class ShapeColection_CalculaationTests
    {
        [Test]
        public void BasicSquare()
        {
            var shapeColection = new ShapeColection();
            shapeColection.ColectionValues.LamdaGround = 1.5;
            var square = new Square(shapeColection);
            square.Point = new PointMy(1,1);
            square.Size = new SizeMy(2, 3);

            square.Skladba.Value = 2;
            foreach (var edge in square.EdgeShells)
            {
                edge.EdgeValues.Psi = 0.2;
                edge.EdgeValues.WallThickness = 0.3;

            }
            var res = shapeColection.TepelnyOdporSkladby;

            CalculationLogger.ConsoleWrite(shapeColection,square);

            AssertDouble(2,res);
        }
        private void AssertDouble(double expected, double result)
        {
            Assert.That(result, Is.EqualTo(expected).Within(0.01d));
        }
    }


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