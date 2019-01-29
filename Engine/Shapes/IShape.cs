using System.Collections.Generic;
using Engine.ShapeColections;
using Engine.Shapes.ShapeParts;
using Engine.XyObjects;

namespace Engine.Shapes
{
    public interface IShape : INode , ICalculatable
    {
        Skladba Skladba { get; set; }
        IShapeColection ShapeColectionParent { get;}

        IReadOnlyList<PointShell> PointShells { get; }
        IReadOnlyList<EdgeShell> EdgeShells { get; }

        int EdgesCount();
        int EdgesCount(ShapeStates state);

        EdgeShell GetEdgeShell(int index);
        PointShell GetPointShell(int index);

        void SetAllEdgesToNotInContact();
        int SplitEdges(IShape shape);
        void SplitEdgeAsSecondFromResult(int index, List<bool> edgesInContact, List<PointMy> pointsToAdd);

        void Optimize();
        PointMy MaxLeftMaxTopPoint();
        bool InvokeEdtited { get; set; }

        void Clear();
        void DeletePoint(PointShell nodePoint);
        void DeleteEdge(EdgeShell nodePoint);
        PointMy FirstPoint();
        object Clone(ShapeColection sc = null);
    }
}