using System;
using System.Text.Json.Serialization;

namespace TomTokenGenerator.Metadata
{
    /// <summary>
    /// Метаданные для JSON
    /// </summary>
    public class Metadata
    {
        /// <summary>
        /// Название данных
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Дата создания данных
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Версия генератора
        /// </summary>
        [JsonPropertyName("generator_version")]
        public string GeneratorVersion { get; set; }

        /// <summary>
        /// Количество токенов
        /// </summary>
        [JsonPropertyName("token_count")]
        public long TokenCount { get; set; }

        public Metadata()
        {
            Name = "TomToken Sequence";
            CreatedAt = DateTime.UtcNow;
            GeneratorVersion = "1.0.0";
            TokenCount = 0;
        }

        public Metadata(string name, long tokenCount)
        {
            Name = name;
            CreatedAt = DateTime.UtcNow;
            GeneratorVersion = "1.0.0";
            TokenCount = tokenCount;
        }
    }
}
