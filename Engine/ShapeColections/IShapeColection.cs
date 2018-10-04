using System.Collections.Generic;
using Engine.Shapes;

namespace Engine.ShapeColections

{
    public delegate void ShapeColectionEventHandler(IShapeColection source);

    public interface IShapeColection
    {
        IEnumerable<IShape> GetColection();
        IEnumerable<IShape> GetColection(ShapeStates state);

        void Add(IShape shape);
        void Remove(IShape shape);
        void Clear();

        void MoveShapeTo(IShape shape, int position);
        void MoveShapeUp(IShape shape, int positionChange = 1);
        void MoveShapeDown(IShape shape, int positionChange = 1);

        event ShapeColectionEventHandler Changed;
    }
}