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
    /// Основной класс приложения
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Парсинг аргументов командной строки
                var options = CommandLineOptions.Parse(args);

                // Создание генератора токенов
                var generatorOptions = new TokenGeneratorOptions();
                var tokenGenerator = new RandomTokenGenerator(generatorOptions);

                // Создание таблицы трансляции
                var translationTable = new TranslationTable();
                translationTable.InitializeDefaultEntries();

                // Создание метаданных
                var metadata = new Metadata.Metadata(
                    $"TomToken Sequence - {DateTime.UtcNow:yyyy-MM-dd}",
                    options.TokenCount
                );

                // Создание сериализатора
                var serializationOptions = new SerializationOptions
                {
                    MaxLineLength = 250,
                    UseFormatting = true
                };
                var serializer = new JsonTokenSerializer(serializationOptions);

                // Создание объекта метаданных с таблицей трансляции
                var metadataWithTranslation = new
                {
                    metadata.Name,
                    metadata.CreatedAt,
                    metadata.GeneratorVersion,
                    metadata.TokenCount,
                    TranslationTable = translationTable.Entries
                };

                Console.WriteLine($"Генерация {options.TokenCount} токенов...");

                // Генерация и сериализация токенов
                using (var outputStream = options.CreateOutputStream())
                {
                    // Генерируем токены и сериализуем их в поток
                    var tokens = tokenGenerator.Generate(options.TokenCount);
                    serializer.Serialize(tokens, outputStream, metadataWithTranslation);
                }

                if (!options.OutputToStdout)
                {
                    Console.WriteLine($"Токены успешно записаны в файл: {options.OutputFilePath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Ошибка: {ex.Message}");
                CommandLineOptions.PrintHelp();
                Environment.Exit(1);
            }
        }
    }
}
