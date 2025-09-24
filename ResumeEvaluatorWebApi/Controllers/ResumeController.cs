using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using ResumeEvaluatorAPI.Models;
using ResumeEvaluatorAPI.Services;

namespace ResumeEvaluatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResumeController : ControllerBase
    {
        private readonly ResumeEvaluationService _evaluationService;
        private readonly Kernel _kernel;

        public ResumeController(ResumeEvaluationService evaluationService, Kernel k)
        {
            _evaluationService = evaluationService;
            _kernel = k;
        }

        [HttpPost("evaluate")]
        public async Task<IActionResult> Evaluate([FromForm] ResumeEvaluationRequest request)
        {
            if (request.ResumeFile == null || string.IsNullOrWhiteSpace(request.JobDescription))
                return BadRequest("Resume and Job Description are required.");

            var result = await _evaluationService.EvaluateAsync(request.ResumeFile, request.JobDescription);
            return Ok(result);
        }
    }
}
