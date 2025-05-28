using System.Collections.Generic;
using System.Text.Json.Serialization;
using TomTokenGenerator.Models;

namespace TomTokenGenerator.Translation
{
    /// <summary>
    /// Translation table for numeric references
    /// </summary>
    public class TranslationTable
    {
        /// <summary>
        /// Entries in the translation table
        /// </summary>
        [JsonPropertyName("entries")]
        public List<TranslationEntry> Entries { get; set; }

        /// <summary>
        /// Dictionary for quick access to entries by identifier
        /// </summary>
        [JsonIgnore]
        private Dictionary<int, TomToken> _idToTokenMap;

        public TranslationTable()
        {
            Entries = new List<TranslationEntry>();
            _idToTokenMap = new Dictionary<int, TomToken>();
        }

        /// <summary>
        /// Adds an entry to the translation table
        /// </summary>
        /// <param name="id">Reference identifier</param>
        /// <param name="token">Token referenced by the identifier</param>
        public void AddEntry(int id, TomToken token)
        {
            var entry = new TranslationEntry(id, token);
            Entries.Add(entry);
            _idToTokenMap[id] = token;
        }

        /// <summary>
        /// Gets a token by reference identifier
        /// </summary>
        /// <param name="id">Reference identifier</param>
        /// <returns>Token referenced by the identifier</returns>
        public TomToken GetTokenById(int id)
        {
            if (_idToTokenMap.TryGetValue(id, out var token))
            {
                return token;
            }
            return null;
        }

        /// <summary>
        /// Initializes the translation table with default values
        /// </summary>
        public void InitializeDefaultEntries()
        {
            // Add standard entries for spaces and other commonly used characters
            AddEntry(1, new TextToken(" "));  // Space
            AddEntry(2, new TextToken("\t")); // Tab
            AddEntry(3, new NewLineToken(NewLineType.LF));  // Line Feed
            AddEntry(4, new NewLineToken(NewLineType.CRLF)); // Carriage Return + Line Feed
            AddEntry(5, new TextToken(",")); // Comma
            AddEntry(6, new TextToken(".")); // Period
            AddEntry(7, new TextToken("!")); // Exclamation mark
            AddEntry(8, new TextToken("?")); // Question mark
            AddEntry(9, new TextToken(":")); // Colon
            AddEntry(10, new TextToken(";")); // Semicolon
        }
    }
}
