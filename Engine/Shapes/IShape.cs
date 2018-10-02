using System.Collections.Generic;
using Engine.XyObjects;

namespace Engine.Shapes
{
    public interface IShape
    {
        ShapeStates State { get; set; }
        Skladba Skladba { get; set; }

        IReadOnlyCollection<PointMy> GetPoints();

        void Clear();

        event ShapeEventHandler ShapeEdited;
    }
}