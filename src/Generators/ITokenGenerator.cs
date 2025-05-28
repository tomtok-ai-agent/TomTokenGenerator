using TomTokenGenerator.Models;

namespace TomTokenGenerator.Generators
{
    /// <summary>
    /// Interface for token generators
    /// </summary>
    public interface ITokenGenerator
    {
        /// <summary>
        /// Generates a sequence of tokens
        /// </summary>
        /// <param name="count">Number of tokens to generate</param>
        /// <returns>Sequence of tokens</returns>
        IEnumerable<TomToken> Generate(long count);
    }
}
