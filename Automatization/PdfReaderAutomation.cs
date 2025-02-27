using System;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

public class PdfReaderAutomation
{
    public string ReadPdf(string pdfFile)
    {
        using (PdfReader pdfReader = new PdfReader(pdfFile))
        using (PdfDocument pdfDoc = new PdfDocument(pdfReader))
        {
            return ExtractForSIAF(ExtractText(pdfDoc));
        }
    }

    public string ExtractText(PdfDocument pdfDoc)
    {
        StringBuilder content = new StringBuilder();
        for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
        {
            var page = pdfDoc.GetPage(i);
            var strategy = new SimpleTextExtractionStrategy();
            content.Append(PdfTextExtractor.GetTextFromPage(page, strategy));
        }
        return content.ToString();
    }

    public string ExtractForSIAF(string content)
    {
        int start = content.IndexOf("OBJETO DE LA LICITACION O EL CONTRATO");
        if (start == -1) return "Campo no encontrado.";

        int end = content.IndexOf("\n------", start);
        return content.Substring(start, (end == -1 ? content.Length : end) - start).Trim();
    }
}