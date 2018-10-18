

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Engine;
using Engine.Shapes;
namespace Engine.ShapeColections
{
    public partial class ShapeColection 
    {
        public ShapeStates State { get; set; }

        public void SetStateToAllChilds(ShapeStates state)
        {
            State = state;
            foreach (var shape in _storage)
                shape.SetStateToAllChilds(state);
        }

        public IEnumerable<INode> Childs => _storage;

        public INode Parent => null;

        public void DeleteYourself()
        {
            Clear();
        }

        public event NoAtributeEventHandler Edited;


        public bool CheckChilds()
        {
            try
            {
                CheckChilds(this, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
        private static void CheckChilds(INode nodeToCheck, INode expectedParent)
        {
            if (!ReferenceEquals(nodeToCheck.Parent, expectedParent))
                throw new Exception("Tree integrity is broken");


            foreach (var child in nodeToCheck.Childs)
            {
                CheckChilds(child, nodeToCheck);
            }
        }
    }
}