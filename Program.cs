using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
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
                PdfEncodings.IDENTITY_H);           //инициализируем и загружаем шрифт в переменную

        string path = "D:\\pdf\\input.pdf"; // указываем путь изначального файла
        var dest = "d:\\pdf\\doc1.pdf"; 
        var file = new FileInfo(dest); //создаем переменную и помещаем в нее инфу о файле, если файла нет то создать
        file.Directory?.Create();



        var docPdf = new PdfDocument(new PdfWriter(dest)); //подключаем режим письма в нашем новом файле и создаем виртуальную версию файла в программе

        var doc = new Document(docPdf);

        docPdf.SetDefaultPageSize(PageSize.A4.Rotate()); // устанавливаем ориентацию страниц по умолчанию

        var pdfDocument = new PdfDocument(new PdfReader(path)); // создаем виртуальную копию изначального файла и указываем путь до него, подключаем режим чтения

        var strategy = new LocationTextExtractionStrategy(); // создаем переменную которая будет описывать вид парсинга исходного файла

        FilteredEventListener listener = new FilteredEventListener();

        var strat = listener.AttachEventListener(new TextLocationStrategy()); //окончательная переменная описывающая алгоритм парсинга файла
        
       
        
        for (int i = 1; i <= 1/*pdfDocument.GetNumberOfPages()*/; ++i) // начало цикла парсинга, i = страница

        {
            float pdfHei = docPdf.GetDefaultPageSize().GetHeight(); // получаем размеры страницы

            var page = pdfDocument.GetPage(i); // создаем переменную с содержимым определенной страницы

            string text = PdfTextExtractor.GetTextFromPage(page, strategy); // из кода с содержимым парсим весь текст

            string text2 = PdfTextExtractor.GetTextFromPage(page, strat); // парсим все формы(не удалять, а то не будет работать)

            List<textChunk> a = strat.objectResult; // записываем все объекты в список чтобы потом получить их координаты

            for (int j = 0; j < a.LongCount(); j++) //цикл написания полученной информации в новый файл 
            {

                doc.Add(new Paragraph(a[j].text).SetFixedPosition(a[j].rect.GetY(),pdfHei- a[j].rect.GetX()-2, 1000).SetFont(font).SetFontSize(a[j].fontSize)); 

                Console.WriteLine(pdfHei + "\n " + a[j].rect.GetX()+"\n");
            }


            
        }
     
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






