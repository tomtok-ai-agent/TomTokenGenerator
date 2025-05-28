using TomTokenGenerator.Models;

namespace TomTokenGenerator.Generators
{
    /// <summary>
    /// Интерфейс для генераторов токенов
    /// </summary>
    public interface ITokenGenerator
    {
        /// <summary>
        /// Генерирует последовательность токенов
        /// </summary>
        /// <param name="count">Количество токенов для генерации</param>
        /// <returns>Последовательность токенов</returns>
        IEnumerable<TomToken> Generate(long count);
    }
}
