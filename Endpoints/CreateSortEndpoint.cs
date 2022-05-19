using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using TextAnalyzer.Contracts.Requests;
using TextAnalyzer.Contracts.Responses;
using TextAnalyzer.Services;

namespace TextAnalyzer.Endpoints;

[HttpGet("sort/{TextToSort}/{sortOption}"), AllowAnonymous]
public class CreateSortEndpoint : Endpoint<CreateSortRequest, SortResponse>
{
    private readonly ITextAnalyzerService _textAnalyzerService;

    public CreateSortEndpoint(ITextAnalyzerService textAnalyzerService)
    {
        _textAnalyzerService = textAnalyzerService;
    }

    public override async Task HandleAsync(CreateSortRequest req, CancellationToken ct)
    {
        if (!string.IsNullOrEmpty(req.TextToSort) || !string.IsNullOrEmpty(req.TextToSort))
        {
            string sortRes = _textAnalyzerService.Sort(req.TextToSort, req.SortOption);
            if (!string.IsNullOrEmpty(sortRes))
            {
                SortResponse res = new SortResponse();
                res.response = sortRes;
                await SendOkAsync(res, ct);
            }
        }
    }
}
