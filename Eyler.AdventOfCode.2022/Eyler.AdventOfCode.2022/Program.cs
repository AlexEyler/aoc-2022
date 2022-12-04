// See https://aka.ms/new-console-template for more information
using System.CommandLine;
using Eyler.AdventOfCode._2022;

var dayRunner = new DayRunner();
var dayDiscoverer = new DayDiscoverer();

var dayOption = new Option<IDay?>(
    "--day",
    description: "Day to run",
    parseArgument: result =>
    {
        var token = result.Tokens.Single().Value;
        if (string.IsNullOrEmpty(token))
        {
            result.ErrorMessage = "Missing required day option";
            return null;
        }

        return dayDiscoverer.GetDay(result.Tokens.Single().Value);
    });

var partOption = new Option<int>(
    "--part",
    description: "Part to run in the day",
    parseArgument: result =>
    {
        var token = result.Tokens.Single().Value;
        if (!int.TryParse(token, out var value))
        {
            result.ErrorMessage = "Missing part option";
            return -1;
        }

        return value;
    });

var testCommand = new Command("test", "Test the day")
{
    dayOption,
    partOption
};

var rootCommand = new RootCommand("Run the day")
{
    dayOption,
    partOption,
    testCommand
};


rootCommand.SetHandler(async (day, part) =>
{
    await dayRunner.RunAsync(day!, part);
}, dayOption, partOption);
testCommand.SetHandler(async (day, part) =>
{
    await dayRunner.TestAsync(day!, part);
}, dayOption, partOption);

return await rootCommand.InvokeAsync(args);