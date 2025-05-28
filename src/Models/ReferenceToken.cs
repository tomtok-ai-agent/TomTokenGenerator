using System.Text.Json.Serialization;

namespace TomTokenGenerator.Models
{
    /// <summary>
    /// "Numeric reference to another entity" type token
    /// </summary>
    public class ReferenceToken : TomToken
    {
        /// <summary>
        /// Token type - reference
        /// </summary>
        [JsonPropertyName("type")]
        public override TokenType Type => TokenType.Reference;

        /// <summary>
        /// Reference identifier
        /// </summary>
        [JsonPropertyName("reference_id")]
        public int ReferenceId { get; set; }

        public ReferenceToken(int referenceId)
        {
            ReferenceId = referenceId;
        }

        // Parameterless constructor for deserialization
        public ReferenceToken()
        {
            ReferenceId = 0;
        }
    }
}
