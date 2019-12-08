using System;
using System.IO;
using System.Text;
using SautinSoft;

namespace Exercise.Domain
{
    public class RuleBookPDFParsor
    {
        private const string UntitledDocument = "Untitled document";

        public string ToHtml(string pdfFilePath, string title = UntitledDocument)
        {
            return ValidateAndOpenPdf(pdfFilePath, title).ToHtml();
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

        private static PdfFocus ValidateAndOpenPdf(string pdfFilePath, string title = UntitledDocument)
        {
            if (!File.Exists(pdfFilePath))
            {
                throw new FileNotFoundException("Unable to locate Rulebook", pdfFilePath);
            }

            var result = new PdfFocus
            {
                HtmlOptions = CreateFlowingHtmlOptions(title)
            };

            result.OpenPdf(pdfFilePath);
            return result;
        }

        private static PdfFocus.CHtmlOptions CreateFlowingHtmlOptions(string title)
        {
            return new PdfFocus.CHtmlOptions
            {
                RenderMode = PdfFocus.CHtmlOptions.eHtmlRenderMode.Fixed,
                // RenderMode = PdfFocus.CHtmlOptions.eHtmlRenderMode.Flowing
                Title = title
            };
        }
    }
}