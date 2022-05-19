namespace TextAnalyzer.Contracts.Responses;

public class StatisticsResponse
{
    public int hyphens { get; set; } = default!;
    public int words { get; set; } = default!;
    public int spaces { get; set; } = default!;
}
