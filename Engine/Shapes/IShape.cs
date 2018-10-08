using System;
using System.Collections.Generic;
using Engine.ShapeColections;
using Engine.Shapes.ShapeParts;
using Engine.XyObjects;

namespace Engine.Shapes
{
    public interface IShape : INode
    {
        Skladba Skladba { get; set; }
        IShapeColection ShapeColectionParent { get;}

        int EdgesCount(ShapeStates state);
        void SetSelectedEdges(EdgeValues values);
        IReadOnlyList<NodePoint> Points { get; }
        IReadOnlyList<EdgeParams> EdgeParams { get; }
        
        void Clear();
        void DeletePoint(NodePoint nodePoint);
        void DeleteEdge(EdgeParams nodePoint);

    }
}