using FluentValidation;
using System.Text.RegularExpressions;

namespace TextAnalyzer.Services;

public class TextAnalyzerService : ITextAnalyzerService
{
    public TextAnalyzerService() { }

    public string Sort(string TextToSort, string SortOption)
    {
        IEnumerable<string> arr = GetMatches(TextToSort, Regex.Matches(TextToSort, @"\b[\w']*\b"));
        if (SortOption == "Ascending")
            return string.Join(" ", arr.OrderBy(x => x));
        if (SortOption == "Descending")
            return string.Join(" ", arr.OrderByDescending(x => x));
        else
            return string.Join(" ", arr);
    }
    public List<int> Statistics(string Text)
    {
        List<int> results = new List<int>();
        // get hypens
        results.Add(GetMatches(Text, Regex.Matches(Text, @"")).Count());
        // get words
        results.Add(GetMatches(Text, Regex.Matches(Text, @"\b[\w']*\b")).Count());
        // get spaces
        results.Add(Text.Split(' ').Length);
        return results;
    }

    static IEnumerable<string> GetMatches(string input, MatchCollection matches)
    {
        return from m in matches.Cast<Match>()
               where !string.IsNullOrEmpty(m.Value)
               select TrimSuffix(m.Value);
    }

    static string TrimSuffix(string word)
    {
        int apostropheLocation = word.IndexOf('\'');
        if (apostropheLocation != -1)
            word = word.Substring(0, apostropheLocation);

        return word;
    }
}
