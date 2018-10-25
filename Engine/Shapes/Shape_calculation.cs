namespace Engine.Shapes
{
    public abstract partial class Shape
    {
        public double Plocha { get; }
        public double ObvodPodlahy { get; }
        public double ExponovanyObvodPodlahy { get; }
        public double CharRozmerPodlahy { get; }
        public double VaPrTlSteny { get; }
        public double VaPrLinearnichCinitelu { get; }
        public double EkvTloustka { get; }
        public double SoucinitelProstupuTepla { get; }
        public double TepelnyTok { get; }

    }
}