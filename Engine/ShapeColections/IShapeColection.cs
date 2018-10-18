using System.Collections.Generic;
using Engine.Shapes;

namespace Engine.ShapeColections

{
    public delegate void ShapeColectionEventHandler(IShapeColection source);

    public class ColectionData
    {

        private double _lamdaGround;
        public double LamdaGround
        {
            get => _lamdaGround;
            set
            {
                _lamdaGround = value; 
                OnEdited();
            }
        }

        //EVENT PARTS
        public event NoAtributeEventHandler Edited;
        protected virtual void OnEdited()
        {
            Edited?.Invoke();
        }
    }

    public interface IShapeColection : INode
    {
        IEnumerable<IShape> GetShapes();
        IEnumerable<IShape> GetShapes(ShapeStates state);

        void AddShape(IShape shape);
        void RemoveShape(IShape shape);
        void Clear();

        ColectionData ColectionData { get; set; }

        int CountOfShapes(ShapeStates state);
        int CountOfEdges(ShapeStates state);

        void SetSelectedShapes(Skladba skladba);

        void MoveShapeTo(IShape shape, int position);
        void MoveShapeUp(IShape shape, int positionChange = 1);
        void MoveShapeDown(IShape shape, int positionChange = 1);

        event ShapeColectionEventHandler ColectionChanged;
    }
}