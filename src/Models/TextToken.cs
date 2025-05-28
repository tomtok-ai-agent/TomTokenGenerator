using System.Text.Json.Serialization;

namespace TomTokenGenerator.Models
{
    /// <summary>
    /// Токен типа "текст"
    /// </summary>
    public class TextToken : TomToken
    {
        /// <summary>
        /// Тип токена - текст
        /// </summary>
        [JsonPropertyName("type")]
        public override TokenType Type => TokenType.Text;

        /// <summary>
        /// Текстовое содержимое токена
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        public TextToken(string value)
        {
            Value = value;
        }

        // Конструктор без параметров для десериализации
        public TextToken()
        {
            Value = string.Empty;
        }
    }
}
