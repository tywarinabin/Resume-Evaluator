# Resume Evaluator Project

This project is a **Resume Evaluator** that matches job descriptions (JD) with candidate resumes using AI. It leverages Semantic Kernel to analyze skills, experience, and other parameters to score and rank resumes. It helps HR teams quickly shortlist candidates based on skill match, experience, and keywords.

## Features
- Analyze resumes against job descriptions
- Score resumes based on skill match, experience, and keywords
- Generate summary reports for hiring decisions
- Designed for scalability and integration with HR systems

## Prerequisites
- [.NET 7 SDK](https://dotnet.microsoft.com/download)
- C# development environment (Visual Studio, VS Code, or JetBrains Rider)
- Azure OpenAI account (or equivalent LLM provider)
- Resume and JD data in `.txt` or `.pdf` format

## Installation
```bash
# Clone the repository
git clone https://github.com/yourusername/resume-evaluator.git
cd resume-evaluator

## Install dependencies
dotnet restore
## Set your Environment variable with your AI Model Credentials
- $env:AZURE_OPENAI_KEY="your_api_key_here"
- $env:AZURE_OPENAI_DEPLOYMENT_NAME="your_deployment_name_here"
- $env:AZURE_OPENAI_ENDPOINT="https://your-endpoint.azure.com/"
