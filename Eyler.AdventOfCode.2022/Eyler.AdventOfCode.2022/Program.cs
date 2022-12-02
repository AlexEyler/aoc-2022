// See https://aka.ms/new-console-template for more information
using System.CommandLine;

var dayOption = new Option<string>(
    "--day",
    description: "Day to run");

var fileOption = new Option<FileInfo>(
    "--input",
    description: "Input file to run",
    parseArgument: result =>
    {
        var filePath = result.Tokens.Single().Value;
        if (!File.Exists(filePath))
        {
            result.ErrorMessage = $"File {filePath} does not exist";
        }

        return new FileInfo(filePath);
    });

var rootCommand = new RootCommand("Run the day")
{
    dayOption,
    fileOption
};

rootCommand.SetHandler(async (day, file) =>
{
    Console.WriteLine(day);
    Console.WriteLine(file);
}, dayOption, fileOption);

return await rootCommand.InvokeAsync(args);