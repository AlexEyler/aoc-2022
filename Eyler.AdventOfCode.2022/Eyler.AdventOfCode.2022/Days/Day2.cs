using static Eyler.AdventOfCode._2022.Utilities;

namespace Eyler.AdventOfCode._2022.Days;

public class Day2 : IDay
{
	public Day2()
	{
	}

    public string Name => nameof(Day2);

    public Task RunAsync(Stream fileStream)
    {
        return RunAsync(fileStream, (fs) =>
        {
            return ReadLines(fileStream, (line) =>
            {
                string[] split = line.Split(" ", StringSplitOptions.None);
                if (split.Length != 2)
                {
                    throw new NotSupportedException("Expected format to be \"A|B|C X|Y|Z\"");
                }

                return new Turn
                {
                    Opponent = split[0].ToPlay(),
                    My = split[1].ToPlay(),
                };
            });
        });
    }

    public Task RunPartTwoAsync(Stream fileStream)
    {
        return RunAsync(fileStream, (fs) =>
        {
            return ReadLines(fileStream, (line) =>
            {
                string[] split = line.Split(" ", StringSplitOptions.None);
                if (split.Length != 2)
                {
                    throw new NotSupportedException("Expected format to be \"A|B|C X|Y|Z\"");
                }

                var opponent = split[0].ToPlay();
                return new Turn
                {
                    Opponent = opponent,
                    My = split[1].ToOutcome().GetPlay(opponent)
                };
            });
        });
    }

    private async Task RunAsync(Stream fileStream, Func<Stream, Task<IEnumerable<Turn>>> getTurns)
    {
        var turns = await getTurns(fileStream);
        var totalScore = 0;
        foreach (var turn in turns)
        {
            totalScore += turn.GetScore();
        }

        Console.WriteLine(totalScore);
    }
}

public record Turn
{
    public Play Opponent { get; init; }

    public virtual Play My { get; init; }

    public int GetScore()
    {
        var score = My.GetScore();
        var outcome = GetOutcome().GetScore();
        return score + outcome;
    }

    public Outcome GetOutcome()
    {
        return My switch
        {
            Play.Rock => Opponent == Play.Rock ? Outcome.Tie : (Opponent == Play.Scissors ? Outcome.Win : Outcome.Loss),
            Play.Paper => Opponent == Play.Paper ? Outcome.Tie : (Opponent == Play.Rock ? Outcome.Win : Outcome.Loss),
            Play.Scissors => Opponent == Play.Scissors ? Outcome.Tie : (Opponent == Play.Paper ? Outcome.Win : Outcome.Loss),
            _ => throw new NotSupportedException($"Unknown play {My}")
        };
    }
}

public enum Play
{
    Rock,
    Paper,
    Scissors
}

public enum Outcome
{
    Loss,
    Tie,
    Win
}

public static class PlayExtensions
{
    public static Play ToPlay(this string s) => s switch
    {
        "A" or "X" => Play.Rock,
        "B" or "Y" => Play.Paper,
        "C" or "Z" => Play.Scissors,
        _ => throw new NotSupportedException($"Play {s} is not supported")
    };

    public static int GetScore(this Play p) => p switch
    {
        Play.Rock => 1,
        Play.Paper => 2,
        Play.Scissors => 3,
        _ => throw new NotSupportedException($"Unknown play {p}")
    };
}

public static class OutcomeExtensions
{
    public static int GetScore(this Outcome o) => o switch
    {
        Outcome.Loss => 0,
        Outcome.Tie => 3,
        Outcome.Win => 6,
        _ => throw new NotSupportedException($"Unknown outcome {o}")
    };

    public static Outcome ToOutcome(this string s) => s switch
    {
        "X" => Outcome.Loss,
        "Y" => Outcome.Tie,
        "Z" => Outcome.Win,
        _ => throw new NotSupportedException($"Outcome {s} is not supported")
    };

    public static Play GetPlay(this Outcome o, Play opponent) => o switch
    {
        Outcome.Loss => opponent == Play.Paper ? Play.Rock : (opponent == Play.Rock ? Play.Scissors : Play.Paper),
        Outcome.Tie => opponent,
        Outcome.Win => opponent == Play.Paper ? Play.Scissors : (opponent == Play.Rock ? Play.Paper : Play.Rock),
        _ => throw new NotSupportedException($"Unknown outcome {o}")
    };
}

