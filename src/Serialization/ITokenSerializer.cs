using System.Text.Json;
using TomTokenGenerator.Models;

namespace TomTokenGenerator.Serialization
{
    /// <summary>
    /// Интерфейс для сериализаторов токенов
    /// </summary>
    public interface ITokenSerializer
    {
        /// <summary>
        /// Сериализует последовательность токенов в поток
        /// </summary>
        /// <param name="tokens">Последовательность токенов</param>
        /// <param name="outputStream">Поток для записи</param>
        /// <param name="metadata">Метаданные</param>
        void Serialize(IEnumerable<TomToken> tokens, Stream outputStream, object metadata);
    }
}
