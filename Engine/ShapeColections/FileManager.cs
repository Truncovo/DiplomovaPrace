using System;
using System.Linq.Expressions;

namespace Engine.ShapeColection
{
    public class FileManager
    {

        private  ShapeColection _shapeColection;
        public ShapeColection ShapeColection => _shapeColection;

        public FileManager()
        {
        }

        public void New()
        {
            _shapeColection = new ShapeColection();
        }

        public void LoadFromFile(string path)
        {
            throw new NotImplementedException();
        }

        public void SaveToFile(string path)
        {
            throw new NotImplementedException();
        }

    }
}