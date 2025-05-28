using System.Collections.Generic;
using System.Text.Json.Serialization;
using TomTokenGenerator.Models;

namespace TomTokenGenerator.Translation
{
    /// <summary>
    /// Таблица трансляции для числовых ссылок
    /// </summary>
    public class TranslationTable
    {
        /// <summary>
        /// Записи в таблице трансляции
        /// </summary>
        [JsonPropertyName("entries")]
        public List<TranslationEntry> Entries { get; set; }

        /// <summary>
        /// Словарь для быстрого доступа к записям по идентификатору
        /// </summary>
        [JsonIgnore]
        private Dictionary<int, TomToken> _idToTokenMap;

        public TranslationTable()
        {
            Entries = new List<TranslationEntry>();
            _idToTokenMap = new Dictionary<int, TomToken>();
        }

        /// <summary>
        /// Добавляет запись в таблицу трансляции
        /// </summary>
        /// <param name="id">Идентификатор ссылки</param>
        /// <param name="token">Токен, на который ссылается идентификатор</param>
        public void AddEntry(int id, TomToken token)
        {
            var entry = new TranslationEntry(id, token);
            Entries.Add(entry);
            _idToTokenMap[id] = token;
        }

        /// <summary>
        /// Получает токен по идентификатору ссылки
        /// </summary>
        /// <param name="id">Идентификатор ссылки</param>
        /// <returns>Токен, на который ссылается идентификатор</returns>
        public TomToken GetTokenById(int id)
        {
            if (_idToTokenMap.TryGetValue(id, out var token))
            {
                return token;
            }
            return null;
        }

        /// <summary>
        /// Инициализирует таблицу трансляции стандартными значениями
        /// </summary>
        public void InitializeDefaultEntries()
        {
            // Добавляем стандартные записи для пробелов и других часто используемых символов
            AddEntry(1, new TextToken(" "));  // Пробел
            AddEntry(2, new TextToken("\t")); // Табуляция
            AddEntry(3, new NewLineToken(NewLineType.LF));  // Перевод строки LF
            AddEntry(4, new NewLineToken(NewLineType.CRLF)); // Перевод строки CRLF
            AddEntry(5, new TextToken(",")); // Запятая
            AddEntry(6, new TextToken(".")); // Точка
            AddEntry(7, new TextToken("!")); // Восклицательный знак
            AddEntry(8, new TextToken("?")); // Вопросительный знак
            AddEntry(9, new TextToken(":")); // Двоеточие
            AddEntry(10, new TextToken(";")); // Точка с запятой
        }
    }
}
