using System.Text;
using Mutsuki.Lib;
using ShellProgressBar;

namespace Mutsuki;

using CommandLine;

public abstract class Program
{
    private class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input file to be processed.")]
        public string Input { set; get; } = null!;

        [Option('o', "output", Required = true, HelpText = "Output folder to be written.")]
        public string Output { set; get; } = null!;
    }

    private static void Main(string[] args)
    {
        Parser
            .Default.ParseArguments<Options>(args)
            .WithParsed(opts =>
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                if (!Directory.Exists(opts.Output))
                {
                    Directory.CreateDirectory(opts.Output);
                }

                var inputFile = File.OpenRead(opts.Input);
                var parser = new SeenParser(inputFile);

                var splitFolder = Path.Combine(opts.Output, "01_SPLIT");
                Directory.CreateDirectory(splitFolder);

                var decompressedFolder = Path.Combine(opts.Output, "02_DECOMPRESSED");
                Directory.CreateDirectory(decompressedFolder);

                var parsedFolder = Path.Combine(opts.Output, "03_PARSED");
                Directory.CreateDirectory(parsedFolder);

                var options = new ProgressBarOptions
                {
                    ProgressBarOnBottom = true,
                    ProgressCharacter = '─'
                };

                using var progressBar = new ProgressBar(
                    parser.Files.Count * 3,
                    "Starting",
                    options
                );
                foreach (var (name, data) in parser.Files)
                {
                    progressBar.Tick($"Writing Split {name}...");
                    var splitFilePath = Path.Combine(splitFolder, name);
                    File.WriteAllBytes(splitFilePath, data);

                    try
                    {
                        progressBar.Tick($"Decompressing {name}...");
                        var decompressedFilePath = Path.Combine(decompressedFolder, name);
                        var decompressed = Decompress.UnPack(data);
                        File.WriteAllBytes(decompressedFilePath, decompressed);

                        progressBar.Tick($"Parsing {name}...");
                        var parsedFilePath = Path.Combine(parsedFolder, name);
                        var parsed = new ScenarioParser(new MemoryStream(decompressed));
                        File.WriteAllText(parsedFilePath, parsed.FinalContent);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Failed to decompress/parse {name}: {e.Message}");
                        throw;
                    }
                }
            });
    }
}
