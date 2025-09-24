using Microsoft.SemanticKernel;

namespace ResumeEvaluatorAPI.SemanticKernel
{
    public static class KernelBuilderFactory
    {
        public static Kernel BuildKernel(string endpoint, string apiKey, string deploymentName)
        {
            var builder = Kernel.CreateBuilder();

            builder.AddAzureOpenAIChatCompletion(
                deploymentName: deploymentName,
                endpoint: endpoint,
                apiKey: apiKey
            );

            return builder.Build();
        }
    }
}
