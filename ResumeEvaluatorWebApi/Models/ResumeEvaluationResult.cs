namespace ResumeEvaluatorAPI.Models
{
    public class ResumeEvaluationResult
    {
        public string OverallScore { get; set; } = "N/A";
        public string FitStatus { get; set; } = "N/A";
        public string ExperienceMatch { get; set; } = "N/A";
        public string SkillMatch { get; set; } = "N/A";
        public List<string> MissingSkills { get; set; } = new List<string>();
        public List<string> ExtraSkills { get; set; } = new List<string>();
        public List<string> Strengths { get; set; } = new List<string>();
        public List<string> ImprovementAreas { get; set; } = new List<string>();
        public List<string> Suggestions { get; set; } = new List<string>();
        public string ResumeSummary { get; set; } = "N/A";
    }
}
