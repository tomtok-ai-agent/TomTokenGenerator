using System.Text.Json.Serialization;

namespace TomTokenGenerator.Models
{
    /// <summary>
    /// Тип перевода строки
    /// </summary>
    public enum NewLineType
    {
        LF,    // Line Feed (\n)
        CR,    // Carriage Return (\r)
        CRLF   // Carriage Return + Line Feed (\r\n)
    }

    /// <summary>
    /// Токен типа "перевод строки"
    /// </summary>
    public class NewLineToken : TomToken
    {
        /// <summary>
        /// Тип токена - перевод строки
        /// </summary>
        [JsonPropertyName("type")]
        public override TokenType Type => TokenType.NewLine;

        /// <summary>
        /// Тип перевода строки (LF, CR, CRLF)
        /// </summary>
        [JsonPropertyName("newline_type")]
        public NewLineType NewLineType { get; set; }

        public NewLineToken(NewLineType newLineType)
        {
            NewLineType = newLineType;
        }

        // Конструктор без параметров для десериализации
        public NewLineToken()
        {
            NewLineType = NewLineType.LF;
        }

        /// <summary>
        /// Получить строковое представление перевода строки
        /// </summary>
        public string GetNewLineString()
        {
            return NewLineType switch
            {
                NewLineType.LF => "\n",
                NewLineType.CR => "\r",
                NewLineType.CRLF => "\r\n",
                _ => "\n"
            };
        }
    }
}
