using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.ShapeColections;
using Engine.Shapes;

namespace Engine.Logger
{
    public class CalculationLogger
    {
        public static string[] Jednotky = new string[] { "lm[W/mK]", "A[m2]", "P[m]", "B´[m]", "w[m]", "dt[m]", "Psi[W/mK]", "Hg[W/K]", "U[W/m2K]", "R[m2K/W]", "Well/Bad" };

        public string OneToString(ICalculatable calculatable, bool printToString = false)
        {
            var sb = new StringBuilder();
            if (printToString)
                sb.Append(calculatable);
            else
                sb.Append(calculatable.Header.ToUpper());
            sb.Append("\n");

            sb.Append(string.Format("Lambda:{0,6:##0.000} m2\n", calculatable.LambdaGround));
            sb.Append(string.Format("R:     {0,6:##0.000} m2\n", calculatable.TepelnyOdporSkladby));
            sb.Append(string.Format("A:     {0,6:##0.000} m2\n", calculatable.Plocha));
            sb.Append(string.Format("P:     {0,6:##0.000} m\n", calculatable.ExponovanyObvodPodlahy));
            sb.Append(string.Format("B´:    {0,6:##0.000} m\n", calculatable.CharRozmerPodlahy));
            sb.Append(string.Format("TlSt:  {0,6:##0.000} m\n", calculatable.VazenyPrumerTlSteny));
            sb.Append(string.Format("dt:    {0,6:##0.000} m\n", calculatable.EkvTloustka));
            sb.Append(string.Format("LinCin:{0,6:##0.000} m\n", calculatable.VazenyPrumerLinearnichCinitelu));
            sb.Append(string.Format("Hg:    {0,6:##0.000} m\n", calculatable.TepelnyTok));
            //sb.Replace('0', ' ');
            return sb.ToString();
        }

        public string CompareTwoToString(ICalculatable c1, ICalculatable c2)
        {
            var sb = new StringBuilder();
            sb.Append("1: ").Append(c1.Header.ToUpper()).Append(" 2: ").Append(c2.Header);
            sb.Append("\n");
            sb.Append(string.Format("Lambda:{0,6:##0.000}  {1,6:##0.000} m2\n", c1.LambdaGround, c2.LambdaGround));

            sb.Append(string.Format("A:     {0,6:##0.000}  {1,6:##0.000} m2\n", c1.Plocha, c2.Plocha));
            sb.Append(string.Format("P:     {0,6:##0.000}  {1,6:##0.000} m \n", c1.ExponovanyObvodPodlahy,
                c2.ExponovanyObvodPodlahy));
            sb.Append(string.Format("B´:    {0,6:##0.000}  {1,6:##0.000} m \n", c1.CharRozmerPodlahy,
                c2.CharRozmerPodlahy));
            sb.Append(string.Format("TlSt:  {0,6:##0.000}  {1,6:##0.000} m \n", c1.VazenyPrumerTlSteny,
                c2.VazenyPrumerTlSteny));
            sb.Append(string.Format("dt:    {0,6:##0.000}  {1,6:##0.000} m \n", c1.EkvTloustka, c2.EkvTloustka));
            sb.Append(string.Format("LinCin:{0,6:##0.000}  {1,6:##0.000} m \n", c1.VazenyPrumerLinearnichCinitelu,
                c2.VazenyPrumerLinearnichCinitelu));
            sb.Append(string.Format("Hg:    {0,6:##0.000}  {1,6:##0.000} m \n", c1.TepelnyTok, c2.TepelnyTok));
            sb.Append(string.Format("R:     {0,6:##0.000}  {1,6:##0.000} m2\n", c1.TepelnyOdporSkladby,
                c2.TepelnyOdporSkladby));
            //sb.Replace('0', ' ');
            return sb.ToString();
        }

        private List<string> _strings = new List<string>();

        public int DescriptionLength { get; set; } = 10;
        public string ColumnSeparator { get; set; } = " | ";
        public int SizeOfColumn { get; set; } = 8;

        public void LogParams(params ICalculatable[] source)
        {
            if (_strings.Count == 0)
                CreateTitle();
            foreach (var calculatable in source)
                Log(calculatable);
        }

        public void Log(ICalculatable c)
        {
            if (_strings.Count == 0)
                CreateTitle();
            var sb = new StringBuilder();
            var header = c.Header.PadRight(DescriptionLength).Substring(0, DescriptionLength);
            sb.Append(header).Append(ColumnSeparator)
                .Append(string.Format("{0,8:####0.000}", c.LambdaGround)).Append(ColumnSeparator)
                .Append(string.Format("{0,8:####0.000}", c.Plocha)).Append(ColumnSeparator)
                .Append(string.Format("{0,8:####0.000}", c.ExponovanyObvodPodlahy)).Append(ColumnSeparator)
                .Append(string.Format("{0,8:####0.000}", c.CharRozmerPodlahy)).Append(ColumnSeparator)
                .Append(string.Format("{0,8:####0.000}", c.VazenyPrumerTlSteny)).Append(ColumnSeparator)
                .Append(string.Format("{0,8:####0.000}", c.EkvTloustka)).Append(ColumnSeparator)
                .Append(string.Format("{0,8:####0.000}", c.VazenyPrumerLinearnichCinitelu)).Append(ColumnSeparator)
                .Append(string.Format("{0,8:####0.000}", c.TepelnyTok)).Append(ColumnSeparator)
                .Append(string.Format("{0,8:####0.000}", c.TepelnyOdporSkladby)).Append(ColumnSeparator)
                .Append(string.Format("{0,8:####0.000}", c.PrumernySoucinitelProstupTepla)).Append(ColumnSeparator)
                .Append(" " + boolToChar(c.JePodlahaDobreIzolovana) +" / "+ boolToChar(c.JePodlahaSpatneIzolovana) + "  ").Append(ColumnSeparator);


            if (c is ShapeColection sc)
            { 
                sb.Append(string.Format("{0,8:####0.000}", sc.EkvTloustkaZateplena)).Append(ColumnSeparator);
                sb.Append(string.Format("{0,8:####0.000}", sc.EkvTloustkaNezateplena)).Append(ColumnSeparator);
            }

            _strings.Add(sb.ToString());
        }

        private char boolToChar(bool b)
        {
            if (b)
                return 'T';
            return 'F';
        }

        public void LogResult(ICalculatable c)
        {
            LineSeparator();
            Log(c);
        }

        private void LineSeparator()
        {
            var sb = new StringBuilder();
            sb.Append('-', DescriptionLength + (ColumnSeparator.Length + SizeOfColumn) * 9 + ColumnSeparator.Length);
            _strings.Add(sb.ToString());
        }

        public string GetLog()
        {
            var sb = new StringBuilder();
            foreach (var s in _strings)
                sb.Append(s).Append('\n');

            return sb.ToString();
        }

        private void CreateTitle()
        {
            var sb = new StringBuilder();
            sb.Append(' ', DescriptionLength)
                .Append(ColumnSeparator)
                .Append(Jednotky[0].PadRight(SizeOfColumn)).Append(ColumnSeparator)
                .Append(Jednotky[1].PadRight(SizeOfColumn)).Append(ColumnSeparator)
                .Append(Jednotky[2].PadRight(SizeOfColumn)).Append(ColumnSeparator)
                .Append(Jednotky[3].PadRight(SizeOfColumn)).Append(ColumnSeparator)
                .Append(Jednotky[4].PadRight(SizeOfColumn)).Append(ColumnSeparator)
                .Append(Jednotky[5].PadRight(SizeOfColumn)).Append(ColumnSeparator)
                .Append(Jednotky[6].PadRight(SizeOfColumn)).Append(ColumnSeparator)
                .Append(Jednotky[7].PadRight(SizeOfColumn)).Append(ColumnSeparator)
                .Append(Jednotky[8].PadRight(SizeOfColumn)).Append(ColumnSeparator)
                .Append(Jednotky[10].PadRight(SizeOfColumn)).Append(ColumnSeparator)
                .Append(Jednotky[9].PadRight(SizeOfColumn)).Append(ColumnSeparator);

            _strings.Add(sb.ToString());

            LineSeparator();
        }


        public static string GetShapeColectionLog(IShapeColection colection)
        {
            var calculationLogger = new CalculationLogger();
            foreach (var shape in colection.GetShapes())
                calculationLogger.Log(shape);
            calculationLogger.LogResult(colection);
            return calculationLogger.GetLog();
        }

        public static void ConsoleWriteShapeColectionLog(IShapeColection colection)
        {
            Console.WriteLine(GetShapeColectionLog(colection));
        }

        public static void ConsoleWrite(ICalculatable calculatable, ICalculatable calculatable2)
        {
            Console.WriteLine(new CalculationLogger().CompareTwoToString(calculatable, calculatable2));
        }

        public static void ConsoleWrite(ICalculatable calculatable, bool printToString = false)
        {
            var s = new ShapeColection();
            Console.WriteLine(new CalculationLogger().OneToString(calculatable, printToString));
        }
    }
}