namespace Eyler.AdventOfCode._2022;

public interface IDay
{
    string Name { get; }
    Task RunAsync(Stream fileStream);
    Task RunPartTwoAsync(Stream fileStream) { return Task.CompletedTask; }
}

