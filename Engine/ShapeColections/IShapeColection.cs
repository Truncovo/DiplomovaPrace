using System.Collections.Generic;
using Engine.Shapes;

namespace Engine.ShapeColections

{
    public delegate void ShapeColectionEventHandler(IShapeColection source);

    public interface IShapeColection : INode
    {
        IEnumerable<IShape> GetColection();
        IEnumerable<IShape> GetColection(ShapeStates state);

        void Add(IShape shape);
        void Remove(IShape shape);
        void Clear();

        int CountOfShapes(ShapeStates state);
        int CountOfEdges(ShapeStates state);
        void DeleteShape(IShape shape);


        void SetSelectedShapes(Skladba skladba);

        void MoveShapeTo(IShape shape, int position);
        void MoveShapeUp(IShape shape, int positionChange = 1);
        void MoveShapeDown(IShape shape, int positionChange = 1);

        event ShapeColectionEventHandler ColectionChanged;
    }
}