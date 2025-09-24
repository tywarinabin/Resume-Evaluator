using Microsoft.AspNetCore.Http;

namespace ResumeEvaluatorAPI.Models
{
    public class ResumeEvaluationRequest
    {
        public IFormFile ResumeFile { get; set; }
        public string JobDescription { get; set; }
    }
}
