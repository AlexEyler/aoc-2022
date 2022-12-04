using System;
namespace Eyler.AdventOfCode._2022;

public static class Utilities
{
    public static async Task<IEnumerable<string>> ReadLines(Stream stream)
    {
        List<string> lines = new();
        using var streamReader = new StreamReader(stream, leaveOpen: true);
        string? line;
        while ((line = await streamReader.ReadLineAsync()) != null)
        {
            lines.Add(line!);
        }

        return lines;
    }

    public static async Task<IEnumerable<T>> ReadLines<T>(Stream stream, Func<string, T> parser)
    {
        List<T> lines = new();
        using var streamReader = new StreamReader(stream, leaveOpen: true);
        string? line;
        while((line = await streamReader.ReadLineAsync()) != null)
        {
            lines.Add(parser(line!));
        }

        return lines;
    }
}

