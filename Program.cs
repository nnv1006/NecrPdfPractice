using drawer.Models;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Filespec;
using iText.Layout;
using iText.Layout.Element;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
namespace ITexttest;
internal static class Program
{

    [STAThread]
    static void Main()
    {

        var font = PdfFontFactory.CreateFont(
                System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gosttypeb.ttf"),
                PdfEncodings.IDENTITY_H);
        string path = "D:\\pdf\\input.pdf";
        var dest = "d:\\pdf\\doc1.pdf";
        var file = new FileInfo(dest);
        file.Directory?.Create();



        var docPdf = new PdfDocument(new PdfWriter(dest));
        var doc = new Document(docPdf);
        docPdf.SetDefaultPageSize(PageSize.A4.Rotate());
        


        /// добавление текста с i страницы в конструктор строк из парсера пдф текста
        StringBuilder sb = new StringBuilder();



        var pdfDocument = new PdfDocument(new PdfReader(path));
        var strategy = new LocationTextExtractionStrategy();
        var textEventListener = new LocationTextExtractionStrategy();






        FilteredEventListener listener = new FilteredEventListener();
        var strat = listener.AttachEventListener(new TextLocationStrategy());
        PdfCanvasProcessor processor = new PdfCanvasProcessor(listener);
        
        for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)

        {
            float pdfHei = docPdf.GetDefaultPageSize().GetHeight() ;
            var page = pdfDocument.GetPage(i);
            string text = PdfTextExtractor.GetTextFromPage(page, strategy);
            string text2 = PdfTextExtractor.GetTextFromPage(page, strat);
            List<textChunk> a = strat.objectResult;

            //Console.WriteLine(text+"gfhfghfgh");
            string[] words = text.Split();
            for (int j = 0; j < a.LongCount(); j++)
            {

                doc.Add(new Paragraph(a[j].text).SetFixedPosition((a[j].rect.GetY()),pdfHei- a[j].rect.GetX()-2, 1000).SetFont(font).SetFontSize(a[j].fontSize));
                Console.WriteLine(pdfHei + "\n " + a[j].rect.GetX()+"\n");
            }


            //sb.Append(text);
        }
        //PdfDocument reader = new PdfDocument(new PdfReader(filepath));
        //Console.WriteLine(a[0].text+" "+ a[0].fontSize + " " + a[0].fontFamily + " " + a[0].rect.GetX() + " " + a[0].rect.GetX());





        //doc.SetFont(font);
        /// создание параграфа и добавление в него текста после чего добавление в документ
        doc.Add(new Paragraph(sb.ToString()).SetFont(font).SetFixedPosition(200, 250, 200));
        Console.WriteLine(sb.ToString(), font);/// текст не распознается из за отсутствия шрифта в консоли


        /// закрытие всех объектов
        doc.Close();
    }
}



    

























/*doc.Add(new Paragraph("1 It wight to happy sighed into begun change which nor tales not from would run had plain bliss nor but")
    .SetFontSize(100));

var style = new Style()
    .SetFontSize(50)
    .SetBold()
    .SetPaddingLeft(100)
    .SetUnderline()
    .SetOpacity(0.5f)
    .SetFontColor(new DeviceRgb(255, 0, 0));

doc.Add(new Paragraph("2 It wight to happy sighed into begun change which nor tales not from would run had plain bliss nor but").AddStyle(style));
doc.Add(new Paragraph("3 It wight to happy sighed into begun change which nor tales not from would run had plain bliss nor but").AddStyle(style));
doc.Add(new Paragraph("4 It wight to happy sighed into begun change which nor tales not from would run had plain bliss nor but").AddStyle(style));

doc.Add(
    new Paragraph()
        .AddStyle(style)
        .Add(new Div()
            .Add(new Image(ImageDataFactory.Create(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo_146.png")))))
    );

docPdf.Close();*/

//PdfWriter writer = PdfWriter.GetInstance(doc1, new FileStream(Application.StartupPath + @path1, FileMode.OpenOrCreate));






