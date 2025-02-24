using System;
using System.Text;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

class PdfReaderAutomation
{
    public string ReadPdf(string pdfFile)
    {
        using (PdfReader pdfReader = new PdfReader(pdfFile))
        {
            using (PdfDocument pdfDoc = new PdfDocument(pdfReader))
            {
                string pdfContent = ExtractText(pdfDoc);

                Console.WriteLine("Contenido completo del PDF:");
                Console.WriteLine(pdfContent);

                string toSIAF = ExtractForSIAF(pdfContent);
                Console.WriteLine("\nInformacion importante encontrada:");
                Console.WriteLine(" ");
                Console.WriteLine(toSIAF);
                return toSIAF;
            }
        }
    }

    private string ExtractText(PdfDocument pdfDoc)
    {
        StringBuilder content = new StringBuilder();
        for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
        {
            var page = pdfDoc.GetPage(i);
            var strategy = new SimpleTextExtractionStrategy();
            var pageContent = PdfTextExtractor.GetTextFromPage(page, strategy);
            content.Append(pageContent);
        }
        return content.ToString();
    }

    private string ExtractForSIAF(string content)
    {
        int start = content.IndexOf("OBJETO DE LA LICITACION O EL CONTRATO");
        if (start == -1)
        {
            return "Campo no encontrado. Rivise si en la poliza modificaron el titulo 'OBJETO DE LA LICITACION'";
        }

        int end = content.IndexOf("\n------", start);
        if (end == -1)
        {
            end = content.Length; // ojo, si no hay delimitador, se toma hasta el final
        }

        return content.Substring(start, end - start).Trim();
    }

}