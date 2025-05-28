using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using TomTokenGenerator.Models;

namespace TomTokenGenerator.Serialization
{
    /// <summary>
    /// Сериализатор токенов в JSON с поддержкой потоковой записи и ограничением длины строки
    /// </summary>
    public class JsonTokenSerializer : ITokenSerializer
    {
        private readonly SerializationOptions _options;
        private readonly JsonWriterOptions _jsonWriterOptions;

        public JsonTokenSerializer(SerializationOptions options = null)
        {
            _options = options ?? new SerializationOptions();
            _jsonWriterOptions = new JsonWriterOptions
            {
                Indented = _options.UseFormatting,
                SkipValidation = false
            };
        }

        /// <summary>
        /// Сериализует последовательность токенов в поток
        /// </summary>
        /// <param name="tokens">Последовательность токенов</param>
        /// <param name="outputStream">Поток для записи</param>
        /// <param name="metadata">Метаданные</param>
        public void Serialize(IEnumerable<TomToken> tokens, Stream outputStream, object metadata)
        {
            using var writer = new Utf8JsonWriter(outputStream, _jsonWriterOptions);
            
            // Начало JSON объекта
            writer.WriteStartObject();
            
            // Запись метаданных
            writer.WritePropertyName("metadata");
            JsonSerializer.Serialize(writer, metadata);
            
            // Начало массива токенов
            writer.WritePropertyName("content");
            writer.WriteStartArray();
            
            // Счетчик символов в текущей строке
            int currentLineLength = 0;
            
            // Буфер для накопления текущей строки
            StringBuilder lineBuffer = new StringBuilder();
            
            // Сериализация каждого токена
            foreach (var token in tokens)
            {
                // Сериализуем токен во временный поток
                using var tokenStream = new MemoryStream();
                using var tokenWriter = new Utf8JsonWriter(tokenStream, _jsonWriterOptions);
                
                JsonSerializer.Serialize(tokenWriter, token);
                tokenWriter.Flush();
                
                // Получаем строковое представление токена
                string tokenJson = Encoding.UTF8.GetString(tokenStream.ToArray());
                
                // Проверяем, не превысит ли добавление токена максимальную длину строки
                if (currentLineLength + tokenJson.Length > _options.MaxLineLength && currentLineLength > 0)
                {
                    // Записываем накопленную строку и сбрасываем буфер
                    outputStream.Write(Encoding.UTF8.GetBytes(lineBuffer.ToString()));
                    outputStream.Write(Encoding.UTF8.GetBytes(Environment.NewLine));
                    lineBuffer.Clear();
                    currentLineLength = 0;
                }
                
                // Добавляем токен в буфер
                lineBuffer.Append(tokenJson);
                currentLineLength += tokenJson.Length;
                
                // Если токен не последний, добавляем запятую
                lineBuffer.Append(",");
                currentLineLength += 1;
                
                // Если длина строки приближается к максимальной, записываем и сбрасываем буфер
                if (currentLineLength >= _options.MaxLineLength * 0.9)
                {
                    outputStream.Write(Encoding.UTF8.GetBytes(lineBuffer.ToString()));
                    outputStream.Write(Encoding.UTF8.GetBytes(Environment.NewLine));
                    lineBuffer.Clear();
                    currentLineLength = 0;
                }
            }
            
            // Записываем оставшиеся данные в буфере
            if (lineBuffer.Length > 0)
            {
                // Удаляем последнюю запятую
                if (lineBuffer[lineBuffer.Length - 1] == ',')
                {
                    lineBuffer.Length--;
                }
                
                outputStream.Write(Encoding.UTF8.GetBytes(lineBuffer.ToString()));
            }
            
            // Завершение массива и объекта
            writer.WriteEndArray();
            writer.WriteEndObject();
            
            // Сбрасываем буфер записи
            writer.Flush();
        }
    }
}
