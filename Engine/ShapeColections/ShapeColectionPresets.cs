using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using Engine.ShapeColections;
using Engine.Shapes;
using Engine.XyObjects;

namespace EngineUnitTests.ShapeColections
{
    public static class ShapeColectionPresets
    {
        public static void Example1(IShapeColection shapeColection)
        {
            shapeColection.Clear();
            shapeColection.ColectionValues = new ColectionValues();
            shapeColection.ColectionValues.LamdaGround = 1.5;


            var polygon = new Polygon(shapeColection);
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

            var square = new Square(shapeColection);
            square.Point = new PointMy(3, 0);
            square.Size = new SizeMy(6, 4);
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

        private static double wallThickness = 0.3;
        private static double psi = 0;

        public static void Example2(IShapeColection shapeColection)
        {
            shapeColection.Clear();
            shapeColection.ColectionValues = new ColectionValues();
            shapeColection.ColectionValues.LamdaGround = 1.5;

           

            var square = new Square(shapeColection);  
            square.Size = new SizeMy(5,5);
            square.Skladba.Value = 4.2;
            foreach (var edge in square.EdgeShells)
            {
                edge.EdgeValues.WallThickness = wallThickness;
                edge.EdgeValues.Psi= psi;
            }
        }

        public static void Example3(IShapeColection shapeColection)
        {
            shapeColection.Clear();
            shapeColection.ColectionValues = new ColectionValues();
            shapeColection.ColectionValues.LamdaGround = 1.5;

            double wallThickness = 0.3;
            double psi = 0.1;

            var square = new Square(shapeColection);
            square.Skladba.Value = 4.2;
            square.Size = new SizeMy(5, 2);
            foreach (var edge in square.EdgeShells)
            {
                edge.EdgeValues.WallThickness = wallThickness;
                edge.EdgeValues.Psi = psi;
            }
        }

        public static void Example4(IShapeColection shapeColection)
        {
            shapeColection.Clear();
            shapeColection.ColectionValues = new ColectionValues();
            shapeColection.ColectionValues.LamdaGround = 1.5;


            var square = new Square(shapeColection);
            square.Skladba.Value = 4.2;
            square.Point = new PointMy(0,5);
            square.Size = new SizeMy(5, 10);

            foreach (var edge in square.EdgeShells)
            {
                edge.EdgeValues.WallThickness = wallThickness;
                edge.EdgeValues.Psi = psi;
                edge.EdgeValues.PsiEdge = 0;

            }


            square = new Square(shapeColection);
            square.Skladba.Value = 7.3;
            square.Point = new PointMy(5,5);
            square.Size = new SizeMy(5, 5);

            foreach (var edge in square.EdgeShells)
            {
                edge.EdgeValues.WallThickness = wallThickness;
                edge.EdgeValues.Psi = psi;
                edge.EdgeValues.PsiEdge = 0;
            }

       
        }

        public static void Example5(IShapeColection shapeColection)
        {
            Example4(shapeColection);
            
            var square = new Square(shapeColection);
            square.Skladba.Value = 5;
            square.Point = new PointMy(10, 5);
            square.Size = new SizeMy(5, 10);

            foreach (var edge in square.EdgeShells)
            {
                edge.EdgeValues.WallThickness = wallThickness;
                edge.EdgeValues.Psi = psi;
                edge.EdgeValues.PsiEdge = 0;

            }
        }

        public static void Example6(IShapeColection shapeColection)
        {
            Example4(shapeColection);

            var square = new Square(shapeColection);
            square.Skladba.Value = 5;
            square.Point = new PointMy(10, 0);
            square.Size = new SizeMy(5, 10);

            foreach (var edge in square.EdgeShells)
            {
                edge.EdgeValues.WallThickness = wallThickness;
                edge.EdgeValues.Psi = psi;
                edge.EdgeValues.PsiEdge = 0;


            }
        }

    }
}
