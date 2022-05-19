using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using TextAnalyzer.Contracts.Requests;
using TextAnalyzer.Contracts.Responses;
using TextAnalyzer.Services;

namespace TextAnalyzer.Endpoints;

[HttpGet("statistics/{Text}"), AllowAnonymous]
public class CreateStatisticsEndpoint : Endpoint<CreateStatisticsRequest, StatisticsResponse>
{
    private readonly ITextAnalyzerService _textAnalyzerService;

    public CreateStatisticsEndpoint(ITextAnalyzerService textAnalyzerService)
    {
        _textAnalyzerService = textAnalyzerService;
    }

    public override async Task HandleAsync(CreateStatisticsRequest req, CancellationToken ct)
    {
        if (!string.IsNullOrEmpty(req.Text))
        {
            List<int> res = _textAnalyzerService.Statistics(req.Text);
            if (res.Any())
            {
                StatisticsResponse statRes = new StatisticsResponse();
                statRes.hyphens = res[0];
                statRes.words = res[1];
                statRes.spaces = res[2];
                await SendOkAsync(statRes, ct);
            }
        }
    }
}
