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
            return _storage.Aggregate(0, (i, s) => i + (s.State == state ? 1 : 0));
        }
        public IEnumerable<IShape> GetShapes()
        {
            return _storage.AsReadOnly();
        }
        public IEnumerable<IShape> GetShapes(ShapeStates state)
        {
            return _storage.AsReadOnly().Where(s => s.State == state);
        }

        public int CountOfEdges(ShapeStates state)
        {
            return _storage.Aggregate(0, (i, s) => i + s.EdgesCount(state));
        }
        public IEnumerable<EdgeParams> GetEdges(ShapeStates state)
        {
            List<EdgeParams> res = new List<EdgeParams>();
            foreach (var shape in _storage)
            foreach (var ep in shape.EdgeParams)
            {
                if (ep.State == state)
                    res.Add(ep);
            }
            return res;
            //return _storage
            //    .Aggregate(res, (current, shape) => current.Concat(shape.EdgeParams))
            //    .Where(e => e.State == state);
        }
    }
}