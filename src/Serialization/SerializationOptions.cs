using System.Text.Json;
using TomTokenGenerator.Models;

namespace TomTokenGenerator.Serialization
{
    /// <summary>
    /// Serialization settings
    /// </summary>
    public class SerializationOptions
    {
        /// <summary>
        /// Maximum line length in JSON
        /// </summary>
        public int MaxLineLength { get; set; } = 250;

        /// <summary>
        /// Indentation for JSON formatting
        /// </summary>
        public string Indent { get; set; } = "  ";

        /// <summary>
        /// Whether to use JSON formatting
        /// </summary>
        public bool UseFormatting { get; set; } = true;
    }
}
