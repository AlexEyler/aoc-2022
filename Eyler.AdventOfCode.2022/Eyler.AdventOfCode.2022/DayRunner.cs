using System;
namespace Eyler.AdventOfCode._2022;

public class DayRunner
{
    private const string InputFileName = "input.txt";
    private const string TestFileName = "test.txt";
    private readonly string rootFilePath;

    public DayRunner()
        : this("/Users/alexeyler/Development/aoc/2022/Eyler.AdventOfCode.2022/Eyler.AdventOfCode.2022")
    {
    }

    public DayRunner(string rootFilePath)
    {
        this.rootFilePath = rootFilePath;
    }

    public Task TestAsync(IDay day)
    {
        return day.TestAsync(OpenTestFile());
    }

    public Task RunAsync(IDay day)
    {
        return day.RunAsync(OpenInputFile());
    }

    private Stream OpenTestFile()
    {
        return File.OpenRead(Path.Combine(this.rootFilePath, TestFileName));
    }

    private Stream OpenInputFile()
    {
        return File.OpenRead(Path.Combine(this.rootFilePath, InputFileName));
    }
}

