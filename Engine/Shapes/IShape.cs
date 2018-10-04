using System.Collections.Generic;
using Engine.ShapeColections;
using Engine.Shapes.ShapeParts;
using Engine.XyObjects;

namespace Engine.Shapes
{
    public interface IShape : INode
    {
        Skladba Skladba { get; set; }
        ShapeColection ShapeColectionParent { get;}

        IReadOnlyCollection<PointMy> Points { get; }
        IReadOnlyCollection<EdgeParams> EdgeParams { get; }

        void Clear();
    }
}