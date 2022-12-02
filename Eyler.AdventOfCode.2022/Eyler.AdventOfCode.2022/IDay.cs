namespace Eyler.AdventOfCode._2022;

public interface IDay
{
    Task TestAsync(Stream fileStream);
    Task RunAsync(Stream fileStream);
}

