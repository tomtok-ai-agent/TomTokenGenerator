using TomTokenGenerator.Models;

namespace TomTokenGenerator.Generators
{
    /// <summary>
    /// Settings for token generator
    /// </summary>
    public class TokenGeneratorOptions
    {
        /// <summary>
        /// Minimum length of text token
        /// </summary>
        public int MinTextLength { get; set; } = 2;

        /// <summary>
        /// Maximum length of text token
        /// </summary>
        public int MaxTextLength { get; set; } = 10;

        /// <summary>
        /// Probability of generating text token (0-100)
        /// </summary>
        public int TextTokenProbability { get; set; } = 60;

        /// <summary>
        /// Probability of generating newline token (0-100)
        /// </summary>
        public int NewLineTokenProbability { get; set; } = 10;

        /// <summary>
        /// Probability of generating repeat token (0-100)
        /// </summary>
        public int RepeatTokenProbability { get; set; } = 10;

        /// <summary>
        /// Probability of generating reference token (0-100)
        /// </summary>
        public int ReferenceTokenProbability { get; set; } = 20;

        /// <summary>
        /// Maximum number of repetitions for repeat token
        /// </summary>
        public int MaxRepeatCount { get; set; } = 5;

        /// <summary>
        /// Maximum number of references
        /// </summary>
        public int MaxReferenceId { get; set; } = 10;

        /// <summary>
        /// Dictionary for generating text tokens
        /// </summary>
        public string[] WordDictionary { get; set; } = new string[]
        {
            "the", "be", "to", "of", "and", "a", "in", "that", "have", "I",
            "it", "for", "not", "on", "with", "he", "as", "you", "do", "at",
            "this", "but", "his", "by", "from", "they", "we", "say", "her", "she",
            "or", "an", "will", "my", "one", "all", "would", "there", "their", "what",
            "so", "up", "out", "if", "about", "who", "get", "which", "go", "me"
        };
    }
}
