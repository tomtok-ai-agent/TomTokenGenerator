using System.Text.Json.Serialization;

namespace TomTokenGenerator.Models
{
    /// <summary>
    /// Токен типа "повтор другого токена N раз"
    /// </summary>
    public class RepeatToken : TomToken
    {
        /// <summary>
        /// Тип токена - повтор
        /// </summary>
        [JsonPropertyName("type")]
        public override TokenType Type => TokenType.Repeat;

        /// <summary>
        /// Токен, который нужно повторить
        /// </summary>
        [JsonPropertyName("token")]
        public TomToken Token { get; set; }

        /// <summary>
        /// Количество повторений
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; }

        public RepeatToken(TomToken token, int count)
        {
            Token = token;
            Count = count;
        }

        // Конструктор без параметров для десериализации
        public RepeatToken()
        {
            Token = null!;
            Count = 0;
        }
    }
}
