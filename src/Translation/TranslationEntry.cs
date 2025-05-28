using System.Collections.Generic;
using System.Text.Json.Serialization;
using TomTokenGenerator.Models;

namespace TomTokenGenerator.Translation
{
    /// <summary>
    /// Entry in the translation table
    /// </summary>
    public class TranslationEntry
    {
        /// <summary>
        /// Reference identifier
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Token referenced by the identifier
        /// </summary>
        [JsonPropertyName("token")]
        public TomToken Token { get; set; }

        public TranslationEntry(int id, TomToken token)
        {
            Id = id;
            Token = token;
        }

        // Parameterless constructor for deserialization
        public TranslationEntry()
        {
            Id = 0;
            Token = null!;
        }
    }
}
