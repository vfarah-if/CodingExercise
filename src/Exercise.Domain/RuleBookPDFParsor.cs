using PdfSharp.Pdf.Content;
using PdfSharp.Pdf.Content.Objects;
using PdfSharp.Pdf.IO;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

            var result = new StringBuilder();

            var document = PdfReader.Open(pdfFilePath, PdfDocumentOpenMode.ReadOnly);
            foreach (var documentPage in document.Pages)
            {
                var content = ContentReader.ReadContent(documentPage);
                foreach (var text in ExtractText(content))
                {
                    result.Append(text);
                }
            }
            return result.ToString();
        }

        private static IEnumerable<string> ExtractText(CObject cObject)
        {
            if (cObject is COperator cOperator)
            {
                if (cOperator.OpCode.Name == OpCodeName.Tj.ToString() ||
                    cOperator.OpCode.Name == OpCodeName.TJ.ToString())
                {
                    foreach (var cOperand in cOperator.Operands)
                        foreach (var txt in ExtractText(cOperand))
                            yield return txt;
                }
            }
            else if (cObject is CSequence cSequence)
            {
                foreach (var element in cSequence)
                    foreach (var txt in ExtractText(element))
                        yield return txt;
            }
            else if (cObject is CString cString)
            {
                yield return cString.Value;
            }
        }
    }
}