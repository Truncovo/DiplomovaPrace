using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Engine.Logger;
using Engine.ShapeColections;
using Engine.Shapes;
using Engine.XyObjects;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EngineUnitTests.Shapes
{

    //RRN - returns right number

    [TestFixture]
    class Shape_CalculationTests
    {
        private double Tolerance = 0.0001d;

        [Test]
        public void SoucinitelProstupuTepla_RRNTests()
        {
            var shapeCollection = new ShapeColection();
            shapeCollection.ColectionValues.LamdaGround = 1.5d;

            var square = new Square(shapeCollection);
            square.Point = new PointMy(0, 0);
            square.Size = new SizeMy(10, 10);
            square.Skladba.Value = 1;

            var res = square.SoucinitelProstupuTepla;

            AssertDouble(0.3881923d,res);

        }

        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(5, 5)]
        [TestCase(-5, -4)]
        public void Plochta_OfSquare_RRNTests0(int sizeX, int sizeY)
        {
            var square = new Square();
            square.Point = new PointMy(0, 0);
            square.Size = new SizeMy(sizeX, sizeY);

            var res = square.Plocha;

            AssertDouble(sizeX * sizeY,res);
        }
        [Test]
        public void Plochta_ofPolygonTriangle_RRNTests()
        {
            var polygon = new Polygon();

            polygon.Add(0, 0);
            polygon.Add(0, 3);
            polygon.Add(3, 0);

            var res = polygon.Plocha;

            AssertDouble((3d * 3d) / 2d,res);
        }

        [Test]
        public void Plochta_ofPolygon_RRNTests()
        {
            var polygon = new Polygon();
            polygon.Add(0+2, 0+2);
            polygon.Add(6+2, 0+2);
            polygon.Add(4+2, 2+2);
            polygon.Add(2+2, 2+2);
            polygon.Add(2+2, 4+2);
            polygon.Add(0+2, 4+2);


            var res = polygon.Plocha;

            AssertDouble((2 * 2) * 3.5,res);
        }

        [Test]
        public void Plochta_ofPolygonZeroPoints_RRNTests()
        {
            var polygon = new Polygon();

            var res = polygon.Plocha;

            AssertDouble(0,res);
        }

        [TestCase(2, 2, 8)]
        [TestCase(0, 0, 0)]
        [TestCase(2, 3, 10)]
        [TestCase(1, 1, 4)]
        [TestCase(99999,88888 , 377774)]

        public void ObvodPodlahy_ofSquare_RRMTests(double x, double y, double expecteResult)
        {
            var square = new Square();
            square.Point = new PointMy(2,3);
            square.Size = new SizeMy(x,y);
            var polygon = square.ConvertToPolygon();

            var res2 = square.ObvodPodlahy;
            var res = polygon.ObvodPodlahy;

            AssertDouble(expecteResult,res);
            AssertDouble(expecteResult, res2);

        }
        [Test]
        public void ObvodPodlahy_ofPolygon_RRMTests()
        {
            var polygon = new Polygon();
            polygon.Add(0, 0);
            polygon.Add(6, 0);
            polygon.Add(4, 2);
            polygon.Add(2, 2);
            polygon.Add(2, 4);
            polygon.Add(0, 4);

            var res = polygon.ObvodPodlahy;

            AssertDouble(18.82842d, res);
        }
        private void AssertDouble(double expected, double result)
        {
            Assert.That(result, Is.EqualTo(expected).Within(0.001d));
        }

        [Test]
        public void ExponovanyObvodPodlahy_ofPolygon_RRMTests()
        {


            var polygon = new Polygon();
            polygon.Add(0, 0);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = true;
            polygon.Add(6, 0);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.Add(4, 2);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = true;
            polygon.Add(2, 2);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.Add(2, 4);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = true;
            polygon.Add(0, 4);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;


            var res = polygon.ExponovanyObvodPodlahy;

            AssertDouble(8.82842d, res);
        }
        [Test]
        public void ExponovanyObvodPodlahy_ofPolygon2_RRMTests()
        {


            var polygon = new Polygon();
            polygon.Add(0, 0);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = true;
            polygon.Add(6, 0);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = true;
            polygon.Add(4, 2);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = true;
            polygon.Add(2, 2);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = true;
            polygon.Add(2, 4);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = true;
            polygon.Add(0, 4);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = true;


            var res = polygon.ExponovanyObvodPodlahy;

            AssertDouble(0, res);
        }
        [Test]
        public void ExponovanyObvodPodlahy_ofPolygon3_RRMTests()
        {


            var polygon = new Polygon();
            polygon.Add(0, 0);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.Add(6, 0);                                                      
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.Add(4, 2);                                                      
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.Add(2, 2);                                                      
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.Add(2, 4);                                                      
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.Add(0, 4);                                                      
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;


            var res = polygon.ExponovanyObvodPodlahy;

            AssertDouble(18.82842d, res);
        }

        [Test]
        public void VazenyPrumerTlSteny_ofPolygon_RRMTests()
        {
            var polygon = new Polygon();
            polygon.Add(0, 0);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.WallThickness = 3;

            polygon.Add(6, 0);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.WallThickness = 3;

            polygon.Add(4, 2);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.WallThickness = 3;

            polygon.Add(2, 2);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.WallThickness = 3;

            polygon.Add(2, 4);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.WallThickness = 3;

            polygon.Add(0, 4);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.WallThickness = 3;

            var res = polygon.VazenyPrumerTlSteny;

            AssertDouble(3,res);
        }

        [Test]
        public void VazenyPrumerTlSteny_ofPolygon2_RRMTests()
        {
            var polygon = new Polygon();
            polygon.Add(0, 0);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.WallThickness = 3;

            polygon.Add(6, 0);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = true;

            polygon.Add(4, 2);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.WallThickness = 2;

            polygon.Add(2, 2);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.WallThickness = 2;

            polygon.Add(2, 4);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.WallThickness = 4;

            polygon.Add(0, 4);
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.InContact = false;
            polygon.EdgeShells[polygon.EdgeShells.Count - 1].EdgeValues.WallThickness = 5;


            var res = polygon.VazenyPrumerTlStenyPredDelenim;
            AssertDouble(54, res);
        }

    }
}
