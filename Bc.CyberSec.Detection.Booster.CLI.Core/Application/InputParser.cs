namespace Bc.CyberSec.Detection.Booster.CLI.Application.Application;

internal class Range
{
    public int Start { get; set; }
    public int End { get; set; }

    public List<int> Generate()
    {
        return Enumerable.Range(Start, End - Start + 1).ToList();
    }
}

public static class InputParser
{

    public static List<int> Range(string input)
    {
        var ranges = input.Split(',')
            .Select(part => part.Split(".."))
            .Select(parts => new Range { Start = int.Parse(parts[0]), End = int.Parse(parts[1]) });

        return ranges.SelectMany(range => range.Generate()).ToList();
    }

    public static List<int> Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new InvalidDataException("Define use case Ids!");

        return input.Where(c => c != ',' && c != ';' && c != ' ' && c != ':').Select(c => int.Parse(c.ToString())).ToList();
    }
}