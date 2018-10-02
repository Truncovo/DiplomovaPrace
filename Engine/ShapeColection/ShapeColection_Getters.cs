using System.Collections.Generic;
using System.Linq;
using Engine.Shapes;

namespace Engine.ShapeColection
{
    public partial class ShapeColection
    {
        public IEnumerable<IShape> GetColection()
        {
            return _storage.AsReadOnly();
        }

        public IEnumerable<IShape> GetColection(ShapeStates state)
        {
            return _storage.AsReadOnly().Where(s => s.State == state);
        }
    }
}