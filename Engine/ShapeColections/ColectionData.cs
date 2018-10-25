namespace Engine.ShapeColections
{
    public class ColectionValues
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
}