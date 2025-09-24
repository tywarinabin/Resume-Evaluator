using ResumeEvaluatorAPI.Models;
using ResumeEvaluatorAPI.Plugins;
using ResumeEvaluatorAPI.Utilities;

namespace ResumeEvaluatorAPI.Services
{
    public class ResumeEvaluationService
    {
        private readonly ResumeEvaluatorPlugin _plugin;
        private readonly PdfParserService _pdfParser;

        public ResumeEvaluationService(ResumeEvaluatorPlugin plugin, PdfParserService pdfParser)
        {
            _plugin = plugin;
            _pdfParser = pdfParser;
        }

        public async Task<ResumeEvaluationResult> EvaluateAsync(IFormFile resumeFile, string jdText)
        {
            var resumeText = await _pdfParser.ExtractTextAsync(resumeFile);
            var jsonResult = await _plugin.EvaluateResumeAsync(resumeText, jdText);

            return jsonResult;
        }
    }
}
