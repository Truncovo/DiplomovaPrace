using System;

namespace ISO13370
{
    public partial class Vzorce
    {
        public static double F1_B(double A, double P)
        {
            return A / (0.5 * P);
        }

        //A - Plocha podlahy
        //P - Exponovaný obvod podlahy
        //B - Charakteristický rozměr podlahy
        //Dt - EkvivalentniDifzniTloustka
        //w - Tloustka obvodovych sten
        //lambda - tepelná vodivost zmrzlé zeminy
        //Rf - tepelný odpor skladby
        //Uo -
        //U - 
        //deltaPsi
        //Ls - ustálená tepelná propustnost
        public static double F2_Dt_(double w, double lambda, double Rf, SmerToku smerToku)
        {
            return w + (lambda * (Konstanty.GetRsi(smerToku) + Konstanty.Rse + Rf));
        }

        public static double F2_Dt(double w, double lambda, double Rf, double Rsi, double Rse)
        {
            return w + (lambda * (Rsi + Rse + Rf));
        }

        public static double F2a3_Uo(double lambda, double B, double Dt)
        {
            if (Dt > B)
                return F3_Uo(lambda, B, Dt);
            return F4_Uo(lambda, B, Dt);
        }

        private static double F3_Uo(double lambda, double B, double Dt)
        {
            double prvniCast = (2 * lambda) / (Math.PI * B + Dt);
            double druhaCast = Math.Log(((Math.PI * B) / Dt) + 1);
            return (prvniCast * druhaCast);
        }

        private static double F4_Uo(double lambda, double B, double Dt)
        {
            return lambda / (0.475 * B + Dt);
        }

        public static double F5_U(double Uo)
        {
            return Uo;
        }

        public static double F6_U(double Uo, double deltaPsi, double B)
        {
            return Uo + 2 * (deltaPsi / B);
        }

        public static double F7_Ls(double A, double Uo, double P, double deltaPsi)
        {
            return A * Uo + P * deltaPsi;
        }

    }

    
}