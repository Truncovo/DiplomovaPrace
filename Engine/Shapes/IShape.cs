using System.Collections.Generic;
using Engine.XyObjects;

namespace Engine.Shapes
{
    public interface IShape
    {

        event ShapeEventHandler ShapeEdited;
        IReadOnlyCollection<PointMy> GetPoints();

        ShapeStates State { get; set; }

        Skladba Skladba { get; set; }
        void Clear();
    }
}