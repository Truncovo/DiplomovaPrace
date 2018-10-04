using System.Collections.Generic;

namespace Engine.Shapes
{
    public interface INode
    {
        ShapeStates State { get; set; }

        IEnumerable<INode> Childs { get; }
        INode Parent { get; }

        void Delete();

        event NoAtributeEventHandler Edited;
    }
}