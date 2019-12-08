using System.IO;
using System.Text;
using SautinSoft;

namespace Exercise.Domain
{
    public class RuleBookPDFParsor
    {
        public string ToHtml(string pdfFilePath)
        {
            return ValidateAndOpenPdf(pdfFilePath).ToHtml();
        }

        public string ToText(string pdfFilePath)
        {
            return ValidateAndOpenPdf(pdfFilePath).ToText();
        }

        public string ToXml(string pdfFilePath)
        {

            return ValidateAndOpenPdf(pdfFilePath).ToXml();
        }

        public byte[] ToWord(string pdfFilePath)
        {
            return ValidateAndOpenPdf(pdfFilePath).ToWord();
        }

        public string ToWordAsString(string pdfFilePath, string encodingName = "utf-8")
        {
            var result = ToWord(pdfFilePath);
            return Encoding.GetEncoding(encodingName).GetString(result);
        }

        private static PdfFocus ValidateAndOpenPdf(string pdfFilePath)
        {
            if (!File.Exists(pdfFilePath))
            {
                throw new FileNotFoundException("Unable to locate Rulebook", pdfFilePath);
            }

            var result = new PdfFocus();
            result.OpenPdf(pdfFilePath);
            return result;
        }

    }
}