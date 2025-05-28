using System;
using System.IO;
using System.Text.Json;
using TomTokenGenerator.CommandLine;
using TomTokenGenerator.Generators;
using TomTokenGenerator.Metadata;
using TomTokenGenerator.Serialization;
using TomTokenGenerator.Translation;

namespace TomTokenGenerator
{
    /// <summary>
    /// Main application class
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Parse command line arguments
                var options = CommandLineOptions.Parse(args);

                // Create token generator
                var generatorOptions = new TokenGeneratorOptions();
                var tokenGenerator = new RandomTokenGenerator(generatorOptions);

                // Create translation table
                var translationTable = new TranslationTable();
                translationTable.InitializeDefaultEntries();

                // Create metadata
                var metadata = new Metadata.Metadata(
                    $"TomToken Sequence - {DateTime.UtcNow:yyyy-MM-dd}",
                    options.TokenCount
                );

                // Create serializer
                var serializationOptions = new SerializationOptions
                {
                    MaxLineLength = 250,
                    UseFormatting = true
                };
                var serializer = new JsonTokenSerializer(serializationOptions);

                // Create metadata object with translation table
                var metadataWithTranslation = new
                {
                    metadata.Name,
                    metadata.CreatedAt,
                    metadata.GeneratorVersion,
                    metadata.TokenCount,
                    TranslationTable = translationTable.Entries
                };

                Console.WriteLine($"Generating {options.TokenCount} tokens...");

                // Generate and serialize tokens
                using (var outputStream = options.CreateOutputStream())
                {
                    // Generate tokens and serialize them to the stream
                    var tokens = tokenGenerator.Generate(options.TokenCount);
                    serializer.Serialize(tokens, outputStream, metadataWithTranslation);
                }

                if (!options.OutputToStdout)
                {
                    Console.WriteLine($"Tokens successfully written to file: {options.OutputFilePath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                CommandLineOptions.PrintHelp();
                Environment.Exit(1);
            }
        }
    }
}
