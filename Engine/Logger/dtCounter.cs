using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Engine.Logger
{
    public static class DtCounter
    {
  
        public static double GetDt(double lambda, double B, double U)
        {
            double lastRes = -1000;
            double res = lastRes;
            double X, E;
            do
            {
                lastRes = res;
                X = 2 * lambda / (Math.PI * B + lastRes);
                E = Math.Pow(Math.E, U / X);
                res = (Math.PI * B) / (E - 1);

            } while (Math.Abs(lastRes - res) > 0.000002d);
                

            return res;
        }

        public static void Test()
        {
            var res = GetDt(1.5d, 6, 0.27262d);
            Console.WriteLine("Final res: " + res);
        }

    }
}
