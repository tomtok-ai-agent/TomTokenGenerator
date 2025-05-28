using TomTokenGenerator.Models;

namespace TomTokenGenerator.Generators
{
    /// <summary>
    /// Настройки для генератора токенов
    /// </summary>
    public class TokenGeneratorOptions
    {
        /// <summary>
        /// Минимальная длина текстового токена
        /// </summary>
        public int MinTextLength { get; set; } = 2;

        /// <summary>
        /// Максимальная длина текстового токена
        /// </summary>
        public int MaxTextLength { get; set; } = 10;

        /// <summary>
        /// Вероятность генерации текстового токена (0-100)
        /// </summary>
        public int TextTokenProbability { get; set; } = 60;

        /// <summary>
        /// Вероятность генерации токена перевода строки (0-100)
        /// </summary>
        public int NewLineTokenProbability { get; set; } = 10;

        /// <summary>
        /// Вероятность генерации токена повтора (0-100)
        /// </summary>
        public int RepeatTokenProbability { get; set; } = 10;

        /// <summary>
        /// Вероятность генерации токена ссылки (0-100)
        /// </summary>
        public int ReferenceTokenProbability { get; set; } = 20;

        /// <summary>
        /// Максимальное количество повторений для токена повтора
        /// </summary>
        public int MaxRepeatCount { get; set; } = 5;

        /// <summary>
        /// Максимальное количество ссылок
        /// </summary>
        public int MaxReferenceId { get; set; } = 10;

        /// <summary>
        /// Словарь для генерации текстовых токенов
        /// </summary>
        public string[] WordDictionary { get; set; } = new string[]
        {
            "the", "be", "to", "of", "and", "a", "in", "that", "have", "I",
            "it", "for", "not", "on", "with", "he", "as", "you", "do", "at",
            "this", "but", "his", "by", "from", "they", "we", "say", "her", "she",
            "or", "an", "will", "my", "one", "all", "would", "there", "their", "what",
            "so", "up", "out", "if", "about", "who", "get", "which", "go", "me"
        };
    }
}
