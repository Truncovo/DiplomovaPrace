using System.Collections.Generic;

namespace Engine.Shapes
{
    public interface INode
    {
        ShapeStates State { get; set; }

        void SetStateToAllChilds(ShapeStates state);

        IEnumerable<INode> Childs { get; }

        INode Parent { get; }

        void DeleteYourself();

        event NoAtributeEventHandler Edited;
    }
}