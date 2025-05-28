using System.Collections.Generic;
using System.Text.Json.Serialization;
using TomTokenGenerator.Models;

namespace TomTokenGenerator.Translation
{
    /// <summary>
    /// Запись в таблице трансляции
    /// </summary>
    public class TranslationEntry
    {
        /// <summary>
        /// Идентификатор ссылки
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Токен, на который ссылается идентификатор
        /// </summary>
        [JsonPropertyName("token")]
        public TomToken Token { get; set; }

        public TranslationEntry(int id, TomToken token)
        {
            Id = id;
            Token = token;
        }

        // Конструктор без параметров для десериализации
        public TranslationEntry()
        {
            Id = 0;
            Token = null!;
        }
    }
}
