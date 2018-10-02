using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Engine;

namespace Visual
{

    public delegate void ShapeInputEventHandler(IShapeInput shapeInput);
    public interface IShapeInput 
    {
        void Reset();
    }
}