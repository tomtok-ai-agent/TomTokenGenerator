using System;
using System.Text.Json.Serialization;

namespace TomTokenGenerator.Metadata
{
    /// <summary>
    /// Metadata for JSON
    /// </summary>
    public class Metadata
    {
        /// <summary>
        /// Data name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Data creation date
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Generator version
        /// </summary>
        [JsonPropertyName("generator_version")]
        public string GeneratorVersion { get; set; }

        /// <summary>
        /// Token count
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
