using System.Collections.Generic;
using Engine.Shapes;

namespace Engine

{
    public delegate void ShapeColectionEventHandler(IShapeColection source);

    public interface IShapeColection
    {
        //todo remove this
        IShape SelectedShape { get; set; }

        IEnumerable<IShape> GetColection();
        IEnumerable<IShape> GetColection(ShapeStates state);

        void Add(IShape shape);
        void Remove(IShape shape);
        void Clear();

        //todo remove this
        void SelectShape(IShape shape);

        void MoveShapeTo(IShape shape, int position);
        void MoveShapeUp(IShape shape, int positionChange = 1);
        void MoveShapeDown(IShape shape, int positionChange = 1);



        event ShapeColectionEventHandler ShapeColectionChanged;
    }
}