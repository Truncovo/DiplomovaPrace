using System.Collections.Generic;
using Engine.Shapes;

namespace Engine

{
    public delegate void ShapeColectionEventHandler(IShapeColection source);

    public interface IShapeColection
    {
        IEnumerable<IShape> GetColection();
        IShape SelectedShape { get; set; }

        void Add(IShape shape);
        void Remove(IShape shape);
        void Clear();

        void SelectShape(IShape shape);

        void MoveShapeTo(IShape shape, int position);
        void MoveShapeUp(IShape shape);
        void MoveShapeDown(IShape shape);

        event ShapeColectionEventHandler ShapeColectionChanged;
    }
}