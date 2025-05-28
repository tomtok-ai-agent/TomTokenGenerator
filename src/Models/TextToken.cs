using System.Text.Json.Serialization;

namespace TomTokenGenerator.Models
{
    /// <summary>
    /// "Text" type token
    /// </summary>
    public class TextToken : TomToken
    {
        /// <summary>
        /// Token type - text
        /// </summary>
        [JsonPropertyName("type")]
        public override TokenType Type => TokenType.Text;

        /// <summary>
        /// Text content of the token
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        public TextToken(string value)
        {
            Value = value;
        }

        // Parameterless constructor for deserialization
        public TextToken()
        {
            Value = string.Empty;
        }
    }
}
