using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Engine.ShapeColections;
using Engine.Shapes;
using EngineUnitTests.ShapeColections;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Pdf;

namespace Engine.Logger
{
    public class CalculationPdfLogger
    {
        public static void TestLog()
        {
            var cl = new CalculationPdfLogger();
            var cl2 = new CalculationLogger();

            var shapeColection = new ShapeColection();
            ShapeColectionPresets.Example6(shapeColection);

            cl.ShowPdf(shapeColection);
            cl2.Log(shapeColection);
            Console.WriteLine(CalculationLogger.GetShapeColectionLog(shapeColection));
        }

        public void ShowPdf(ShapeColection shapeColection)
        {

            Document document = CreateDocument(shapeColection);
            document.UseCmykColor = true;
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(false, PdfFontEmbedding.Always);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();

            string filename = GetFileNameWithTmpPath();
            pdfRenderer.PdfDocument.Save(filename);

            Process.Start(filename);
        }

        private Table table;
        private Document CreateDocument(ShapeColection shapeColection)
        {
            var document = new Document();
            Section section = document.AddSection();
            document.DefaultPageSetup.RightMargin = "1cm";
            document.DefaultPageSetup.LeftMargin= "0.9cm";
            Paragraph p;
            p = section.AddParagraph("vytvořeno pomocí softwaru JTDP 2019");
            p.Format.Alignment = ParagraphAlignment.Center;
            p.Format.Font.Color = Colors.DarkGray;
            
            p = section.AddParagraph("software pro posouzení podlahy na zemině dle ČSN EN ISO13370");
            p.Format.Alignment = ParagraphAlignment.Center;
            p.Format.Font.Color = Colors.DarkGray;
            p.Format.Font.Size = 8;


            section.AddParagraph("");
            p = section.AddParagraph("Výpočet ");
            p.Format.Font.Bold = true;

            section.AddParagraph("Tepelná vodivost okolní zeminy = " + shapeColection.LambdaGround + " w/mK" );
            section.AddParagraph();

            table = section.AddTable();
            table.Format.Font.Size = 10;


            var aligment = ParagraphAlignment.Right;

            Counter = 1;
            var column = table.AddColumn("1,9cm");
            for (int i = 1; i < 9; i++)
            {
                column = table.AddColumn("2,18cm");
                column.Format.Alignment = ParagraphAlignment.Right;
            }

            var row = table.AddRow();
            row.Shading.Color = Colors.LightBlue;
            row.Height = "0,55cm";

            //add header
            for (int i = 1; i < 9; i++)
            { 
                if(i != 8)
                    row.Cells[i].AddParagraph(CalculationLogger.Jednotky[i]);
                else
                    row.Cells[i].AddParagraph(CalculationLogger.Jednotky[i+1]);

                row.Cells[i].VerticalAlignment = VerticalAlignment.Center;

            }
            foreach (var shape in shapeColection.GetShapes())
            { 
                var r = AddShape(shape);
                r.Cells[0].AddParagraph(Counter++.ToString());
            }
            row = AddShape(shapeColection);
            row.Shading.Color = Colors.Beige;
            row.Cells[7].Format.Font.Bold = true;
            row.Cells[8].Format.Font.Bold = true;
            row.Cells[6].Format.Font.Bold = true;
            row.Cells[4].Format.Font.Bold = true;



            row.Cells[0].AddParagraph("Výsledek");

            table.SetEdge(0, 0, 9, shapeColection.GetShapes().Count()+2, Edge.Interior, BorderStyle.Single, 0.75, Colors.Black);
            table.SetEdge(0, 0, 9, shapeColection.GetShapes().Count()+2, Edge.Box, BorderStyle.Single, 0.75, Colors.Black);

            p = section.AddParagraph();


            table = section.AddTable();
            table.AddColumn("10cm");
            table.AddColumn("2cm");
            table.AddColumn("2cm");

            string format = "{0,8:####0.000}";

            row = table.AddRow();
            p = row.Cells[0].AddParagraph("Vlastnosti ekvivalentní podlahy");
            p.Format.Font.Size = 11;
            p.Format.Font.Bold = true;
            p.Format.Alignment = ParagraphAlignment.Center;

            row = table.AddRow();
            row.Cells[0].AddParagraph("Vážený průměr tloušťky stěny: ");
            p = row.Cells[1].AddParagraph(string.Format(format, shapeColection.VazenyPrumerTlSteny));
            p.Format.Font.Bold = true;
            row.Cells[2].AddParagraph("m");

            row = table.AddRow();
            row.Cells[0].AddParagraph("Vážený průměr lineárních cinitelů prostupu tepla: ");
            p = row.Cells[1].AddParagraph(string.Format(format, shapeColection.VazenyPrumerLinearnichCinitelu));
            p.Format.Font.Bold = true;
            row.Cells[2].AddParagraph("W/mK");

            row = table.AddRow();
            row.Cells[0].AddParagraph("Ekvivalentní tepelný odpor podlahy:");
            p = row.Cells[1].AddParagraph(string.Format(format, shapeColection.TepelnyOdporSkladby));
            p.Format.Font.Bold = true;
            row.Cells[2].AddParagraph("m2K/W");


            section.AddParagraph();
            table = section.AddTable();
            
            table.AddColumn("1cm");
            table.AddColumn("1,5cm");
            table.AddColumn("8cm");
            table.AddColumn("2,22cm");
            row = table.AddRow();
            row.Height = "0,55cm";
            row = table.AddRow();
            row.Height = "0,55cm";
            
            for (int i = 0; i < 8; i++)
            {
                row = table.AddRow();
                row.Height = "0,55cm";
                row.Cells[1].AddParagraph(_popis[i][0]);
                row.Cells[2].AddParagraph(_popis[i][1]);
                row.Cells[3].AddParagraph(_popis[i][2]);
            }
            return document;
        }

        

        private readonly string[][] _popis = {
            new[] {"A", "plocha podlahy", "m2"},      
            new [] {"P", "exponovaný obvod podlahy", "m"},
            new [] {"B´", "charakteristický rozměr podalhy", "m"},
            new [] {"w", "tloušťka obvodové stěny", "m"},
            new [] {"dt", "celková ekvivalentní tloušťka", "m"},
            new [] {"Psi", "lineární činitel prostupu tepla", "W/mK"},
            new [] {"Hg", "ustálený tepelný tok zeminou", "W/K"},
            new [] {"R", "Tepelný odpor", "m2K/W" }
        };

        private int Counter;
        private Row AddShape(ICalculatable shape,ParagraphFormat paragraphFormat = null)
        {
            var row = table.AddRow();
            row.Height = "0,55cm";
            string format = "{0,8:####0.000}";
            for (int i = 0; i < 9; i++)
            {
                row.Cells[i].VerticalAlignment = VerticalAlignment.Center;
            }
            row.Cells[1].AddParagraph(string.Format(format, shape.Plocha));
            row.Cells[2].AddParagraph(string.Format(format, shape.ExponovanyObvodPodlahy));
            row.Cells[3].AddParagraph(string.Format(format, shape.CharRozmerPodlahy));
            row.Cells[4].AddParagraph(string.Format(format, shape.VazenyPrumerTlSteny));
            row.Cells[5].AddParagraph(string.Format(format, shape.EkvTloustka));
            row.Cells[6].AddParagraph(string.Format(format, shape.VazenyPrumerLinearnichCinitelu));
            row.Cells[7].AddParagraph(string.Format(format, shape.TepelnyTok));
            row.Cells[8].AddParagraph(string.Format(format, shape.TepelnyOdporSkladby));
            return row;
        }

        public static string GetFileNameWithTmpPath()
        {
            
            Console.WriteLine(Path.GetFullPath("./tr"));
            string tmpPath = Path.GetTempPath();
            tmpPath = Path.Combine(tmpPath, "Truncovo");
            if (!Directory.Exists(tmpPath))
                System.IO.Directory.CreateDirectory(tmpPath);

            string file = Path.Combine(tmpPath, DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".pdf");
            int count = 1;

            while (File.Exists(file))
            {
                if (count > 1)
                    file = file.Remove(file.Length - 7, 7);
                else
                    file = file.Remove(file.Length - 4, 4);

                file = file + "(" + count++ + ").pdf";
                Console.WriteLine(file);
            }
            Console.WriteLine(file);
            return file;
        }
    }
}