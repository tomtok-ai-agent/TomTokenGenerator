using System.Text.Json.Serialization;

namespace TomTokenGenerator.Models
{
    /// <summary>
    /// Abstract base class for all token types
    /// </summary>
    [JsonDerivedType(typeof(TextToken), typeDiscriminator: "text")]
    [JsonDerivedType(typeof(NewLineToken), typeDiscriminator: "newline")]
    [JsonDerivedType(typeof(RepeatToken), typeDiscriminator: "repeat")]
    [JsonDerivedType(typeof(ReferenceToken), typeDiscriminator: "reference")]
    public abstract class TomToken
    {
        /// <summary>
        /// Token type
        /// </summary>
        [JsonPropertyName("type")]
        public abstract TokenType Type { get; }
    }
}
