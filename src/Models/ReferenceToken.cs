using System.Text.Json.Serialization;

namespace TomTokenGenerator.Models
{
    /// <summary>
    /// Токен типа "числовая ссылка на другую сущность"
    /// </summary>
    public class ReferenceToken : TomToken
    {
        /// <summary>
        /// Тип токена - ссылка
        /// </summary>
        [JsonPropertyName("type")]
        public override TokenType Type => TokenType.Reference;

        /// <summary>
        /// Идентификатор ссылки
        /// </summary>
        [JsonPropertyName("reference_id")]
        public int ReferenceId { get; set; }

        public ReferenceToken(int referenceId)
        {
            ReferenceId = referenceId;
        }

        // Конструктор без параметров для десериализации
        public ReferenceToken()
        {
            ReferenceId = 0;
        }
    }
}
