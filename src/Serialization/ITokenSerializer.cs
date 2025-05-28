using System.Text.Json;
using TomTokenGenerator.Models;

namespace TomTokenGenerator.Serialization
{
    /// <summary>
    /// Interface for token serializers
    /// </summary>
    public interface ITokenSerializer
    {
        /// <summary>
        /// Serializes a sequence of tokens to a stream
        /// </summary>
        /// <param name="tokens">Sequence of tokens</param>
        /// <param name="outputStream">Output stream</param>
        /// <param name="metadata">Metadata</param>
        void Serialize(IEnumerable<TomToken> tokens, Stream outputStream, object metadata);
    }
}
