using System.IO;
using IronPdf;

namespace Exercise.Domain
{
    public class RuleBookPDFParsor
    {
        public string Parse(string pdfFilePath)
        {
            if (!File.Exists(pdfFilePath))
            {
                throw new FileNotFoundException("Unable to locate Rulebook", pdfFilePath);
            }

            var pdfDocument = new PdfDocument(pdfFilePath);
            return pdfDocument.ExtractAllText();
        }
    }
}