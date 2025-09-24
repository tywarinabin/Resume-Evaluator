using ResumeEvaluatorAPI.Models;
using System.Text.Json;

namespace ResumeEvaluatorAPI.Utilities
{
    public static class JsonHelper
    {
        public static ResumeEvaluationResult ParseResumeEvaluationResult(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return new ResumeEvaluationResult { ResumeSummary = "⚠️ Empty response" };

            try
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                return new ResumeEvaluationResult
                {
                    OverallScore = root.TryGetProperty("overallScore", out var s) ? s.ToString() : "N/A",
                    FitStatus = root.TryGetProperty("fitStatus", out var f) ? f.ToString() : "N/A",
                    ExperienceMatch = root.TryGetProperty("experienceMatch", out var e) ? e.ToString() : "N/A",
                    SkillMatch = root.TryGetProperty("skillMatch", out var sm) ? sm.ToString() : "N/A",
                    MissingSkills = root.TryGetProperty("missingSkills", out var ms) && ms.ValueKind == JsonValueKind.Array
                        ? ms.EnumerateArray().Select(x => x.ToString()).ToList()
                        : new List<string>(),
                    ExtraSkills = root.TryGetProperty("extraSkills", out var es) && es.ValueKind == JsonValueKind.Array
                        ? es.EnumerateArray().Select(x => x.ToString()).ToList()
                        : new List<string>(),
                    Strengths = root.TryGetProperty("strengths", out var str) && str.ValueKind == JsonValueKind.Array
                        ? str.EnumerateArray().Select(x => x.ToString()).ToList()
                        : new List<string>(),
                    ImprovementAreas = root.TryGetProperty("improvementAreas", out var ia) && ia.ValueKind == JsonValueKind.Array
                        ? ia.EnumerateArray().Select(x => x.ToString()).ToList()
                        : new List<string>(),
                    Suggestions = root.TryGetProperty("suggestions", out var sg) && sg.ValueKind == JsonValueKind.Array
                        ? sg.EnumerateArray().Select(x => x.ToString()).ToList()
                        : new List<string>(),
                    ResumeSummary = root.TryGetProperty("resumeSummary", out var rs) ? rs.ToString() : "⚠️ Failed to parse LLM output"
                };
            }
            catch (JsonException)
            {
                return new ResumeEvaluationResult { ResumeSummary = "⚠️ Failed to parse LLM output" };
            }
        }
    }
}
