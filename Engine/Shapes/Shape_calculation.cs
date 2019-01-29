using System;
using Engine.XyObjects;

namespace Engine.Shapes
{
    public abstract partial class Shape
    {
        public double Plocha
        {
            get
            {
                if (_pointsShells.Count == 0)
                    return 0;

                double prevX = _pointsShells[0].Point.X;
                double sum = 0;
                for (int i = 1; i < _pointsShells.Count; i++)
                {

                    sum += prevX * _pointsShells[i].Point.Y;
                    prevX = _pointsShells[i].Point.X;
                }
                sum += prevX * _pointsShells[0].Point.Y;

                double prevY = _pointsShells[0].Point.Y;
                for (int i = 1; i < _pointsShells.Count; i++)
                {
                    sum -= prevY * _pointsShells[i].Point.X;
                    prevY = _pointsShells[i].Point.Y;
                }
                sum -= prevY * _pointsShells[0].Point.X;

                return Math.Abs(sum/2);
            }
        }
        
        public double ObvodPodlahy
        {
            get
            {
                if (_pointsShells.Count == 0)
                    return 0;
                PointMy prevPoint = _pointsShells[0].Point;
                double result = 0;
                for (int i = 1; i < _pointsShells.Count; i++)
                {
                    result += PointMy.LengthOfLine(prevPoint,_pointsShells[i].Point);
                    prevPoint = _pointsShells[i].Point;
                }

                result += PointMy.LengthOfLine(_pointsShells[0].Point, _pointsShells[_pointsShells.Count - 1].Point);
                return result;
            }
        }
        
        public double ExponovanyObvodPodlahy
        {
            get
            {
                if (_pointsShells.Count == 0)
                    return 0;
                PointMy prevPoint = _pointsShells[0].Point;
                double result = 0;
                for (int i = 1; i < _pointsShells.Count; i++)
                {
                    if(!_edgeShells[i-1].EdgeValues.InContact)
                        result += PointMy.LengthOfLine(prevPoint, _pointsShells[i].Point);
                    prevPoint = _pointsShells[i].Point;
                }
                if (!_edgeShells[_edgeShells.Count -1].EdgeValues.InContact)
                    result += PointMy.LengthOfLine(_pointsShells[0].Point, _pointsShells[_pointsShells.Count - 1].Point);
                return result;
            }
        }

        public double CharRozmerPodlahy => Plocha / (0.5d * ExponovanyObvodPodlahy);

        public double VazenyPrumerTlStenyPredDelenim
        {
            get
            {
                if (_pointsShells.Count == 0)
                    return 0;
                PointMy prevPoint = _pointsShells[0].Point;
                double result = 0;
                for (int i = 1; i < _pointsShells.Count; i++)
                {
                    if (!_edgeShells[i - 1].EdgeValues.InContact)
                        result += PointMy.LengthOfLine(prevPoint, _pointsShells[i].Point)
                                  * _edgeShells[i-1].EdgeValues.WallThickness; 
                    prevPoint = _pointsShells[i].Point;
                }
                if (!_edgeShells[_edgeShells.Count - 1].EdgeValues.InContact)
                    result += PointMy.LengthOfLine(_pointsShells[0].Point, _pointsShells[_pointsShells.Count - 1].Point) *
                              _edgeShells[_edgeShells.Count - 1].EdgeValues.WallThickness;

                return result;
            }
        }

        public double PrumernySoucinitelProstupTepla => TepelnyTok / Plocha;

        public double VazenyPrumerTlSteny => VazenyPrumerTlStenyPredDelenim / ExponovanyObvodPodlahy;

        public double VazenyPrumerLinearnichCiniteluPredDelenim
        {
            get
            {
                if (_pointsShells.Count == 0)
                    return 0;
                PointMy prevPoint = _pointsShells[0].Point;
                double result = 0;
                for (int i = 1; i < _pointsShells.Count; i++)
                {
                    if (!_edgeShells[i - 1].EdgeValues.InContact)
                        result += PointMy.LengthOfLine(prevPoint, _pointsShells[i].Point)
                                  * (_edgeShells[i - 1].EdgeValues.Psi
                                  + _edgeShells[i - 1].EdgeValues.PsiEdge); 
                    prevPoint = _pointsShells[i].Point;
                }
                if (!_edgeShells[_edgeShells.Count - 1].EdgeValues.InContact)
                    result += PointMy.LengthOfLine(_pointsShells[0].Point, _pointsShells[_pointsShells.Count - 1].Point) *
                             ( _edgeShells[_edgeShells.Count - 1].EdgeValues.PsiEdge
                              + _edgeShells[_edgeShells.Count - 1].EdgeValues.Psi);

                return result;
            }
        }

        public double VazenyPrumerLinearnichCinitelu => VazenyPrumerLinearnichCiniteluPredDelenim / ExponovanyObvodPodlahy;

        public double EkvTloustka => VazenyPrumerTlSteny + LambdaGround * (0.17+Skladba.Value+0.04);

        public double SoucinitelProstupuTepla
        {
            get
            {
                double charRozmerPodlahy = CharRozmerPodlahy;
                if (JePodlahaDobreIzolovana)
                    return LambdaGround/(0.457*charRozmerPodlahy + EkvTloustka);

                double preLn = 2 * LambdaGround / (Math.PI * CharRozmerPodlahy + EkvTloustka);
                double ln = Math.Log(((Math.PI * CharRozmerPodlahy) / EkvTloustka) + 1);

                return preLn * ln;
            }
        }

        public double SoucinitelProstupuTepla2 => 1/(Skladba.Value + (VazenyPrumerTlSteny/LambdaGround) + EfektivniTepelnyOdporZeminy);
        
        public bool JePodlahaDobreIzolovana => EkvTloustka >= CharRozmerPodlahy;

        public bool JePodlahaSpatneIzolovana => !JePodlahaDobreIzolovana;

        public double TepelnyOdporSkladby => Skladba.Value;

        public double EfektivniTepelnyOdporZeminy => (0.457 * CharRozmerPodlahy )/ LambdaGround;

        public double TepelnyTok => Plocha * SoucinitelProstupuTepla + VazenyPrumerLinearnichCiniteluPredDelenim;

        public string Header => "Shape";
        public double LambdaGround => _parent.ColectionValues.LamdaGround;
    }
}