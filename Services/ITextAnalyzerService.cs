namespace TextAnalyzer.Services;

public interface ITextAnalyzerService
{
    public string Sort(string TextToSort, string SortOption);

    public List<int> Statistics(string Text);
}
