using System.Text.Json;
using TomTokenGenerator.Models;

namespace TomTokenGenerator.Serialization
{
    /// <summary>
    /// Настройки для сериализации
    /// </summary>
    public class SerializationOptions
    {
        /// <summary>
        /// Максимальная длина строки в JSON
        /// </summary>
        public int MaxLineLength { get; set; } = 250;

        /// <summary>
        /// Отступ для форматирования JSON
        /// </summary>
        public string Indent { get; set; } = "  ";

        /// <summary>
        /// Использовать ли форматирование JSON
        /// </summary>
        public bool UseFormatting { get; set; } = true;
    }
}
