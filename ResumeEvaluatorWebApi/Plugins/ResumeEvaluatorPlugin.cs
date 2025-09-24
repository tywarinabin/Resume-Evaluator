using Microsoft.SemanticKernel;
using ResumeEvaluatorAPI.Models;
using ResumeEvaluatorAPI.Utilities;

namespace ResumeEvaluatorAPI.Plugins
{
    public class ResumeEvaluatorPlugin
    {
        private readonly Kernel _kernel;

        public ResumeEvaluatorPlugin(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task<ResumeEvaluationResult> EvaluateResumeAsync(string resumeText, string jdText)
        {
            // Fill the prompt with resume + JD
            string prompt = PromptTemplates.ResumeEvaluationPrompt
                .Replace("{resumeText}", resumeText)
                .Replace("{jdText}", jdText);

            // Create the function dynamically
            var func = _kernel.CreateFunctionFromPrompt(prompt);

            // Call the kernel directly
            var response = await _kernel.InvokeAsync(func);
            Console.WriteLine(response.GetValue<string>());
            // Extract raw text from model response
            var rawJson = response.GetValue<string>() ?? "{}";

            // Delegate parsing to helper
            return JsonHelper.ParseResumeEvaluationResult(rawJson);
        }
    }
}
