using Microsoft.SemanticKernel;
using ResumeEvaluatorAPI.Plugins;
using ResumeEvaluatorAPI.SemanticKernel;
using ResumeEvaluatorAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add controllers, swagger, etc.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors((options) =>
{
    options.AddDefaultPolicy((policy) =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var config = builder.Configuration;
string endpoint = Environment.GetEnvironmentVariable("endpoint")??"";  
string deploymentName = Environment.GetEnvironmentVariable("deploymentName")??"";              
string apiKey = Environment.GetEnvironmentVariable("apiKey")??"";

Console.WriteLine("endpoint: " + Environment.GetEnvironmentVariable("endpoint"));
Console.WriteLine("deploymentName: " + Environment.GetEnvironmentVariable("deploymentName"));
Console.WriteLine("apiKey: " + Environment.GetEnvironmentVariable("apiKey"));
// Register Semantic Kernel via factory
builder.Services.AddSingleton<Kernel>(sp =>
    KernelBuilderFactory.BuildKernel(endpoint!, apiKey!, deploymentName!)
);

// Register your services
builder.Services.AddSingleton<ResumeEvaluatorPlugin>();
builder.Services.AddSingleton<ResumeEvaluationService>();
builder.Services.AddSingleton<PdfParserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
