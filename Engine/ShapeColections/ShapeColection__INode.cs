

using System;
using System.Collections.Generic;
using Engine;
using Engine.Shapes;
namespace Engine.ShapeColections
{
    public partial class ShapeColection : INode
    {
        public ShapeStates State { get; set; }

        public IEnumerable<INode> Childs => _storage;
        public INode Parent => null;

        public void Delete()
        {
            throw new System.NotImplementedException();
        }

        public event NoAtributeEventHandler Edited;
    }
}