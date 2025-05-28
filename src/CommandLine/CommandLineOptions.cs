using System;
using System.IO;

namespace TomTokenGenerator.CommandLine
{
    /// <summary>
    /// Класс для парсинга аргументов командной строки
    /// </summary>
    public class CommandLineOptions
    {
        /// <summary>
        /// Количество токенов для генерации
        /// </summary>
        public long TokenCount { get; private set; } = 1000;

        /// <summary>
        /// Путь к файлу для записи результата
        /// Если null, то вывод в stdout
        /// </summary>
        public string OutputFilePath { get; private set; } = null;

        /// <summary>
        /// Флаг, указывающий, что вывод должен быть в stdout
        /// </summary>
        public bool OutputToStdout => string.IsNullOrEmpty(OutputFilePath);

        /// <summary>
        /// Парсит аргументы командной строки
        /// </summary>
        /// <param name="args">Аргументы командной строки</param>
        /// <returns>Объект с опциями командной строки</returns>
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
                        throw new ArgumentException("Количество токенов должно быть положительным числом");
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
                        throw new ArgumentException("Не указан путь к файлу для вывода");
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
                    throw new ArgumentException($"Неизвестный аргумент: {arg}");
                }
            }

            return options;
        }

        /// <summary>
        /// Выводит справку по использованию программы
        /// </summary>
        public static void PrintHelp()
        {
            Console.WriteLine("Использование: TomTokenGenerator [опции]");
            Console.WriteLine("Опции:");
            Console.WriteLine("  --count, -c <число>    Количество токенов для генерации (по умолчанию: 1000)");
            Console.WriteLine("  --output, -o <путь>    Путь к файлу для записи результата");
            Console.WriteLine("  --stdout               Вывод в стандартный поток вывода (по умолчанию)");
            Console.WriteLine("  --help, -h             Вывод справки");
        }

        /// <summary>
        /// Создает поток для записи результата
        /// </summary>
        /// <returns>Поток для записи</returns>
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
