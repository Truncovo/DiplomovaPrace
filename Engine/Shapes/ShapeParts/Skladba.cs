using System;
using System.Windows.Navigation;

namespace Engine
{
    public class Skladba : IEquatable<Skladba>
    {
        private double _value;

        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChanged();
            }
        }

        public Skladba()
        {
        }

        public Skladba(double value)
        {
            Value = value;
        }

        public event NoAtributeEventHandler Changed;

        public override string ToString()
        {
            return "Skladba";
        }

        protected virtual void OnChanged()
        {
            Changed?.Invoke();
        }

        public bool Equals(Skladba other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Skladba) obj);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}