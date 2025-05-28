using System.Text.Json.Serialization;

namespace TomTokenGenerator.Models
{
    /// <summary>
    /// Newline type
    /// </summary>
    public enum NewLineType
    {
        LF,    // Line Feed (\n)
        CR,    // Carriage Return (\r)
        CRLF   // Carriage Return + Line Feed (\r\n)
    }

    /// <summary>
    /// "Newline" type token
    /// </summary>
    public class NewLineToken : TomToken
    {
        /// <summary>
        /// Token type - newline
        /// </summary>
        [JsonPropertyName("type")]
        public override TokenType Type => TokenType.NewLine;

        /// <summary>
        /// Newline type (LF, CR, CRLF)
        /// </summary>
        [JsonPropertyName("newline_type")]
        public NewLineType NewLineType { get; set; }

        public NewLineToken(NewLineType newLineType)
        {
            NewLineType = newLineType;
        }

        // Parameterless constructor for deserialization
        public NewLineToken()
        {
            NewLineType = NewLineType.LF;
        }

        /// <summary>
        /// Get string representation of the newline
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
