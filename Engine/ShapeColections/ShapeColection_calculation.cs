using System;
using System.Linq;
using Engine.Logger;

namespace Engine.ShapeColections
{
    public class LambdaGround0Exception : Exception
    {
    }
    public partial class ShapeColection
    {
        public double TepelnyOdporSkladby => (EkvTloustka-VazenyPrumerTlSteny)/ColectionValues.LamdaGround - 0.17d-0.04d;
        public double PrumernySoucinitelProstupTepla => TepelnyTok / Plocha;
        public string Header => "Shape colection";
        public double LambdaGround => ColectionValues.LamdaGround;
        public double EfektivniTepelnyOdporZeminy => (0.457 * CharRozmerPodlahy) / LambdaGround;
        public double EkvTloustka
        {
            get
            {
                var zateplena = EkvTloustkaZateplena;
                var nezateplena = EkvTloustkaNezateplena;
                if (zateplena >= CharRozmerPodlahy)
                    return zateplena;
                if (nezateplena < CharRozmerPodlahy)
                    return nezateplena;
                throw new LambdaGround0Exception();
            }
        }

        public double EkvTloustkaZateplena => LambdaGround / SoucinitelProstupuTepla - 0.457d * CharRozmerPodlahy;
        public double EkvTloustkaNezateplena => DtCounter.GetDt(LambdaGround, CharRozmerPodlahy, SoucinitelProstupuTepla);
        public double ExponovanyObvodPodlahy => _shapes.Aggregate(0d, (d, s) => d + s.ExponovanyObvodPodlahy);
        public double CharRozmerPodlahy => Plocha / (0.5d * ExponovanyObvodPodlahy);

        public bool JePodlahaDobreIzolovana
        {
            get
            {
                var zateplena = LambdaGround / SoucinitelProstupuTepla - 0.457d * CharRozmerPodlahy;
                if (zateplena >= CharRozmerPodlahy)
                    return true;
                return false;
            }
        }
        public bool JePodlahaSpatneIzolovana
        {
            get
            {
                var nezateplena = DtCounter.GetDt(LambdaGround, CharRozmerPodlahy, SoucinitelProstupuTepla);
                if (nezateplena < CharRozmerPodlahy)
                    return true;
                return false;
            }
        }
        public double Plocha => _shapes.Aggregate(0d, (d, s) => d + s.Plocha);
        public double SoucinitelProstupuTepla => (TepelnyTok - VazenyPrumerLinearnichCiniteluPredDelenim) / Plocha;
        public double TepelnyTok => (_shapes.Aggregate(0d, (d, s) => d + s.TepelnyTok))*1.03d;
        public double VazenyPrumerLinearnichCinitelu =>
            VazenyPrumerLinearnichCiniteluPredDelenim / ExponovanyObvodPodlahy;
        public double VazenyPrumerLinearnichCiniteluPredDelenim =>
            _shapes.Aggregate(0d, (d, s) => d + s.VazenyPrumerLinearnichCiniteluPredDelenim);
        public double VazenyPrumerTlSteny => VazenyPrumerTlStenyPredDelenim / ExponovanyObvodPodlahy;
        public double VazenyPrumerTlStenyPredDelenim =>
            _shapes.Aggregate(0d, (d, s) => d + s.VazenyPrumerTlStenyPredDelenim);
    }
}