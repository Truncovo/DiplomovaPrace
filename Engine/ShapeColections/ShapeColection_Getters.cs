using System.Collections.Generic;
using System.Linq;
using Engine.Shapes;
using Engine.Shapes.ShapeParts;

namespace Engine.ShapeColections
{
    public partial class ShapeColection 
    {
        public int CountOfShapes(ShapeStates state)
        {
            return _shapes.Aggregate(0, (i, s) => i + (s.State == state ? 1 : 0));
        }
        public IEnumerable<IShape> GetShapes()
        {
            return _shapes.AsReadOnly();
        }
        public IEnumerable<IShape> GetShapes(ShapeStates state)
        {
            return _shapes.AsReadOnly().Where(s => s.State == state);
        }

        public int CountOfEdges(ShapeStates state)
        {
            return _shapes.Aggregate(0, (i, s) => i + s.EdgesCount(state));
        }
        public IEnumerable<EdgeShell> GetEdges(ShapeStates state)
        {
            List<EdgeShell> res = new List<EdgeShell>();
            foreach (var shape in _shapes)
            foreach (var ep in shape.EdgeShells)
                if (ep.State == state)
                    res.Add(ep);
            return res;
            //return _storage
            //    .Aggregate(res, (current, shape) => current.Concat(shape.EdgeParams))
            //    .Where(e => e.State == state);
        }

        public IShape GetShape(int index)
        {
            return _shapes[index];
        }

    }
}