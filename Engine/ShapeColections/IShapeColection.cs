using System.Collections.Generic;
using Engine.Shapes;

namespace Engine.ShapeColections

{
    public delegate void ShapeColectionEventHandler(IShapeColection source);

    public interface IShapeColection : INode
    {
        
        ColectionValues ColectionValues { get; set; }

        IEnumerable<IShape> GetShapes();
        IEnumerable<IShape> GetShapes(ShapeStates state);
        IShape GetShape(int index);

        void AddShape(IShape shape);
        void RemoveShape(IShape shape);
        void Clear();
        void Optimize();

        int CountOfShapes(ShapeStates state);
        int CountOfEdges(ShapeStates state);

        void SetSelectedShapes(Skladba skladba);
        void SplitEdgesForCalculation();

        int IndexOf(IShape shape);
        void ChangeShapes(IShape toDelete, IShape toAdd);
        void ChangeShapes(int indexToDelete, IShape toAdd);

        void MoveShapeTo(IShape shape, int position);
        void MoveShapeUp(IShape shape, int positionChange = 1);
        void MoveShapeDown(IShape shape, int positionChange = 1);

        event ShapeColectionEventHandler ColectionChanged;
    }
}