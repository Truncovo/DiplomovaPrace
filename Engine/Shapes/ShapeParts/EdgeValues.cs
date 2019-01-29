namespace Engine.Shapes.ShapeParts
{
    public enum EdgeValuesPropertyes
    {
        PsiEdge, Psi, WallThickness
    }

    public class EdgeValues
    {
        
        private double _psiEdge;
        public double PsiEdge
        {
            get => _psiEdge;
            set
            {
                _psiEdge = value; 
                OnEdited();
            }
        }

        private double _psi;
        public double Psi
        {
            get => _psi;
            set
            {
                _psi = value;
                OnEdited();
            }
        }

        private double _wallThickness;
        public double WallThickness
        {
            get => _wallThickness;
            set
            {
                _wallThickness = value;
                OnEdited();
            }
        }

        private bool _inContact;
        public  bool InContact
        {
            get => _inContact;
            set
            {
                _inContact = value;
                OnEdited();
            }
        }

        public EdgeValues GetCopy()
        {
            return new EdgeValues
            {
                PsiEdge = _psiEdge,
                Psi = _psi,
                InContact = _inContact,
                WallThickness = _wallThickness
            };

        }

        //EVENT PARTS
        public event NoAtributeEventHandler Edited;
        protected virtual void OnEdited()
        {
            Edited?.Invoke();
        }

        public override string ToString()
        {
            return "EV: [P: " + _psi + " Pe: " + _psiEdge + " WT: " + _wallThickness + " ]" + InContact;
        }
    }
}