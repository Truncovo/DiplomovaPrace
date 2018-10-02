using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO13370
{
    public static class Konstanty
    {
        public static double RsiDolu = 0.17;
        public static double RsiNahoru = 0.13;
        public static double RsiHorizontal = 0.10;
        public static double GetRsi(SmerToku smerToku)
        {
            switch (smerToku)
            {
                case SmerToku.Dolu:
                    return RsiDolu;
                case SmerToku.Nahoru:
                    return RsiNahoru;
                case SmerToku.Horizontal:
                    return RsiHorizontal;
            }
            throw new Iso13370Exception("GetRsi - SmerToku enum error");
        }
        

        public static double Rse = 0.04;
    }
    public enum SmerToku
    {
        Dolu, Nahoru, Horizontal
    }
}
