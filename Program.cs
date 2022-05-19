using Serilog;
using FastEndpoints;
using FastEndpoints.Swagger;
using TextAnalyzer.Validation;
using TextAnalyzer.Contracts.Responses;
using TextAnalyzer.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// remove default logging providers
builder.Logging.ClearProviders();

// Serilog configuration		
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// Register Serilog
builder.Logging.AddSerilog(logger);

// Add Services
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();
builder.Services.AddSingleton<ITextAnalyzerService, TextAnalyzerService>();

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseFastEndpoints(x =>
{
    x.ErrorResponseBuilder = (failures, _) =>
    {
        return new ValidationFailureResponse
        {
            Errors = failures.Select(y => y.ErrorMessage).ToList()
        };
    };
});

app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());

app.Run();