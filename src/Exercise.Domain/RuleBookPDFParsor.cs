using System.IO;
using SautinSoft;

namespace Exercise.Domain
{
    public class RuleBookPDFParsor
    {
        public string ToHtml(string pdfFilePath)
        {
            if (!File.Exists(pdfFilePath))
            {
                throw new FileNotFoundException("Unable to locate Rulebook", pdfFilePath);
            }

            var pdfFocus = new PdfFocus();
            pdfFocus.OpenPdf(pdfFilePath);
            return pdfFocus.ToHtml();
        }
    }
}