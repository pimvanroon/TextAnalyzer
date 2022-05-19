namespace TextAnalyzer.Contracts.Requests;

public class CreateStatisticsRequest
{
    public string Text { get; init; } = default!;
}
