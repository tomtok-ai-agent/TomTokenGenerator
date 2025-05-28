using System;
using System.IO;

namespace TomTokenGenerator.CommandLine
{
    /// <summary>
    /// Class for parsing command line arguments
    /// </summary>
    public class CommandLineOptions
    {
        /// <summary>
        /// Number of tokens to generate
        /// </summary>
        public long TokenCount { get; private set; } = 1000;

        /// <summary>
        /// Path to the output file
        /// If null, output to stdout
        /// </summary>
        public string OutputFilePath { get; private set; } = null;

        /// <summary>
        /// Flag indicating that output should be to stdout
        /// </summary>
        public bool OutputToStdout => string.IsNullOrEmpty(OutputFilePath);

        /// <summary>
        /// Parses command line arguments
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>Object with command line options</returns>
        public static CommandLineOptions Parse(string[] args)
        {
            var options = new CommandLineOptions();

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                if (arg == "--count" || arg == "-c")
                {
                    if (i + 1 < args.Length && long.TryParse(args[i + 1], out long count) && count > 0)
                    {
                        options.TokenCount = count;
                        i++;
                    }
                    else
                    {
                        throw new ArgumentException("Token count must be a positive number");
                    }
                }
                else if (arg == "--output" || arg == "-o")
                {
                    if (i + 1 < args.Length)
                    {
                        options.OutputFilePath = args[i + 1];
                        i++;
                    }
                    else
                    {
                        throw new ArgumentException("Output file path not specified");
                    }
                }
                else if (arg == "--stdout")
                {
                    options.OutputFilePath = null;
                }
                else if (arg == "--help" || arg == "-h")
                {
                    PrintHelp();
                    Environment.Exit(0);
                }
                else
                {
                    throw new ArgumentException($"Unknown argument: {arg}");
                }
            }

            return options;
        }

        /// <summary>
        /// Displays help information about program usage
        /// </summary>
        public static void PrintHelp()
        {
            Console.WriteLine("Usage: TomTokenGenerator [options]");
            Console.WriteLine("Options:");
            Console.WriteLine("  --count, -c <number>    Number of tokens to generate (default: 1000)");
            Console.WriteLine("  --output, -o <path>     Path to output file");
            Console.WriteLine("  --stdout                Output to standard output (default)");
            Console.WriteLine("  --help, -h              Display help");
        }

        /// <summary>
        /// Creates a stream for writing the result
        /// </summary>
        /// <returns>Output stream</returns>
        public Stream CreateOutputStream()
        {
            if (OutputToStdout)
            {
                return Console.OpenStandardOutput();
            }
            else
            {
                return new FileStream(OutputFilePath, FileMode.Create, FileAccess.Write);
            }
        }
    }
}
