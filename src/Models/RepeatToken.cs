using System.Text.Json.Serialization;

namespace TomTokenGenerator.Models
{
    /// <summary>
    /// "Repeat another token N times" type token
    /// </summary>
    public class RepeatToken : TomToken
    {
        /// <summary>
        /// Token type - repeat
        /// </summary>
        [JsonPropertyName("type")]
        public override TokenType Type => TokenType.Repeat;

        /// <summary>
        /// Token to repeat
        /// </summary>
        [JsonPropertyName("token")]
        public TomToken Token { get; set; }

        /// <summary>
        /// Number of repetitions
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; }

        public RepeatToken(TomToken token, int count)
        {
            Token = token;
            Count = count;
        }

        // Parameterless constructor for deserialization
        public RepeatToken()
        {
            Token = null!;
            Count = 0;
        }
    }
}
