using UglyToad.PdfPig;

namespace ResumeEvaluatorAPI.Services
{
    public class PdfParserService
    {
        public async Task<string> ExtractTextAsync(IFormFile pdfFile)
        {
            if (pdfFile == null || pdfFile.Length == 0) return "N/A";

            using var stream = pdfFile.OpenReadStream();
            using var pdf = PdfDocument.Open(stream);

            string text = "";
            foreach (var page in pdf.GetPages())
            {
                text += page.Text + "\n";
            }

            return text;
        }
    }
}
