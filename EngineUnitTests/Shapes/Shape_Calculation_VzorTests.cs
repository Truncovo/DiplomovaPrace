using System;
using Engine;
using Engine.Logger;
using Engine.ShapeColections;
using Engine.Shapes;
using Engine.XyObjects;
using NUnit.Framework;

namespace EngineUnitTests.Shapes
{
    [TestFixture]
    class Shape_Calculation_VzorTests
    {
        private Polygon polygon;
        private Square square;
        private ShapeColection shapeColection;

        [SetUp]
        public void SetUp()
        {
            shapeColection = new ShapeColection();
            shapeColection.ColectionValues.LamdaGround = 1.5;

            polygon = new Polygon(shapeColection);
            polygon.Skladba.Value = 2;

            polygon.Add(0, 0);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.WallThickness = 0.3;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.Psi = 0.1;

            polygon.Add(3, 0);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = true;

            polygon.Add(3, 4);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.WallThickness = 0.3;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.Psi = 0.1;

            polygon.Add(3, 6);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.WallThickness = 0.3;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.Psi = 0.1;

            polygon.Add(0, 6);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.WallThickness = 0.3;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.Psi = 0.1;

            square = new Square(shapeColection);
            square.Point = new PointMy(6,0);
            square.Size = new SizeMy(6,4);
            square.Skladba.Value = 4;

            square.EdgeShells[0].EdgeValues.InContact = false;
            square.EdgeShells[0].EdgeValues.WallThickness = 0.3;
            square.EdgeShells[0].EdgeValues.Psi = 0.1;
            square.EdgeShells[0].EdgeValues.PsiEdge = -0.4;

            square.EdgeShells[1].EdgeValues.InContact = false;
            square.EdgeShells[1].EdgeValues.WallThickness = 0.3;
            square.EdgeShells[1].EdgeValues.Psi = 0.1;

            square.EdgeShells[2].EdgeValues.InContact = false;
            square.EdgeShells[2].EdgeValues.WallThickness = 0.3;
            square.EdgeShells[2].EdgeValues.Psi = 0.1;
            square.EdgeShells[2].EdgeValues.PsiEdge = -0.2;

            square.EdgeShells[3].EdgeValues.InContact = true;

        }


        [Test]
        public void TepelnyTok_VzorovyPriklad_RRVTests()
        {
            var res = polygon.TepelnyTok;

            AssertDouble(7.034, res);

        }

        [Test]
        public void TepelnyTok_VzorovyPriklad2_RRVTests()
        {

            var res = square.TepelnyTok;

            AssertDouble(2.512,res);
        }


        [Test]
        public void TepelnyTok_VzorovyPriklad_Plocha()
        {

            var res = shapeColection.Plocha;

            AssertDouble(18+24, res);
        }

        [Test]
        public void TepelnyTok_VzorovyPriklad_TepelnyTok()
        {

            var res = shapeColection.TepelnyTok;

            AssertDouble(9.546, res);
        }

        [Test]
        public void TepelnyTok_VzorovyPriklad_TepelnyTok2()
        {

            var res = shapeColection.TepelnyTok;

            var TlSteny = shapeColection.VazenyPrumerTlSteny;
            var RSkladby = shapeColection.TepelnyOdporSkladby;
            var psi = shapeColection.VazenyPrumerLinearnichCinitelu;
            CalculationLogger.ConsoleWriteShapeColectionLog(shapeColection);

            foreach (var shape in shapeColection.GetShapes())
            {
                shape.Skladba.Value = RSkladby;

                foreach (var edge in shape.EdgeShells)
                {
                    edge.EdgeValues.Psi = psi;
                    edge.EdgeValues.PsiEdge = 0;
                    edge.EdgeValues.WallThickness = TlSteny;
                }
            }

            var res2 = shapeColection.TepelnyTok;
            CalculationLogger.ConsoleWriteShapeColectionLog(shapeColection);


            AssertDouble(res, res2);
        }

        private void AssertDouble(double expected, double result)
        {
            Assert.That(result, Is.EqualTo(expected).Within(0.01d));
        }
    }
}