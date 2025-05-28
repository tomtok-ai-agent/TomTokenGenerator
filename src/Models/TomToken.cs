using System.Text.Json.Serialization;

namespace TomTokenGenerator.Models
{
    /// <summary>
    /// Абстрактный базовый класс для всех типов токенов
    /// </summary>
    [JsonDerivedType(typeof(TextToken), typeDiscriminator: "text")]
    [JsonDerivedType(typeof(NewLineToken), typeDiscriminator: "newline")]
    [JsonDerivedType(typeof(RepeatToken), typeDiscriminator: "repeat")]
    [JsonDerivedType(typeof(ReferenceToken), typeDiscriminator: "reference")]
    public abstract class TomToken
    {
        /// <summary>
        /// Тип токена
        /// </summary>
        [JsonPropertyName("type")]
        public abstract TokenType Type { get; }
    }
}
