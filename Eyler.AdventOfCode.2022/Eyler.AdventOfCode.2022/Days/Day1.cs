using System;
using static Eyler.AdventOfCode._2022.Utilities;

namespace Eyler.AdventOfCode._2022.Days;

public class Day1 : IDay
{
    public string Name => nameof(Day1);

    public async Task RunAsync(Stream stream)
    {
        var lines = await ReadLines(stream);
        var maxSum = int.MinValue;
        var runningSum = 0;
        foreach (var line in lines)
        {
            if (line.Trim() == string.Empty)
            {
                if (runningSum > maxSum)
                {
                    maxSum = runningSum;
                }

                runningSum = 0;
            }
            else
            {
                runningSum += int.Parse(line);
            }
        }

        if (runningSum > maxSum)
        {
            maxSum = runningSum;
        }

        Console.WriteLine(maxSum);
    }

    public async Task RunPartTwoAsync(Stream stream)
    {
        var lines = await ReadLines(stream);
        var maxSums = new int[3] { 0, 0, 0 };
        var runningSum = 0;

        static void CheckAndReplaceSum(int[] maxSums, int runningSum)
        {
            int minValue = int.MaxValue;
            int minIndex = -1;
            for (int i = 0; i < maxSums.Length; i++)
            {
                if (maxSums[i] < minValue)
                {
                    minValue = maxSums[i];
                    minIndex = i;
                }
            }

            if (runningSum > minValue)
            {
                maxSums[minIndex] = runningSum;
            }
        }

        foreach (var line in lines)
        {
            if (line.Trim() == string.Empty)
            {
                CheckAndReplaceSum(maxSums, runningSum);
                runningSum = 0;
            }
            else
            {
                runningSum += int.Parse(line);
            }
        }

        CheckAndReplaceSum(maxSums, runningSum);
        Console.WriteLine(maxSums.Sum());
    }
}

