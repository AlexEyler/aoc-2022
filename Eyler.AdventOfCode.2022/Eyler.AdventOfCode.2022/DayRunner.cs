using System;
namespace Eyler.AdventOfCode._2022;

public class DayRunner
{
    private const string InputFileName = "input.txt";
    private const string TestFileName = "test.txt";
    private readonly string rootFilePath;

    public DayRunner()
        : this("/Users/alexeyler/Development/aoc/2022/Eyler.AdventOfCode.2022/Eyler.AdventOfCode.2022/Data")
    {
    }

    public DayRunner(string rootFilePath)
    {
        this.rootFilePath = rootFilePath;
    }

    public Task TestAsync(IDay day, int part)
    {
        using var file = OpenTestFile(day);
        if (part == 2)
        {
            return day.RunPartTwoAsync(file);
        }
        else
        {
            return day.RunAsync(file);
        }
    }

    public async Task RunAsync(IDay day, int part)
    {
        using var file = OpenInputFile(day);
        if (part == 2)
        {
            await day.RunPartTwoAsync(file);
        }
        else
        {
            await day.RunAsync(file);
        }
    }

    private Stream OpenTestFile(IDay day)
    {
        return File.OpenRead(Path.Combine(this.rootFilePath, day.Name, TestFileName));
    }

    private Stream OpenInputFile(IDay day)
    {
        return File.OpenRead(Path.Combine(this.rootFilePath, day.Name, InputFileName));
    }
}

