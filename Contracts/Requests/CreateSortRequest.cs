namespace TextAnalyzer.Contracts.Requests;

public class CreateSortRequest
{
    public string TextToSort { get; init; } = default!;

    public string SortOption { get; init; } = default!;
}
