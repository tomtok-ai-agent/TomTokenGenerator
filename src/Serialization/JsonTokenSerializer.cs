using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using TomTokenGenerator.Models;

namespace TomTokenGenerator.Serialization
{
    /// <summary>
    /// JSON token serializer with streaming writing support and line length limitation
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
        /// Serializes a sequence of tokens to a stream
        /// </summary>
        /// <param name="tokens">Sequence of tokens</param>
        /// <param name="outputStream">Output stream</param>
        /// <param name="metadata">Metadata</param>
        public void Serialize(IEnumerable<TomToken> tokens, Stream outputStream, object metadata)
        {
            using var writer = new Utf8JsonWriter(outputStream, _jsonWriterOptions);
            
            // Start JSON object
            writer.WriteStartObject();
            
            // Write metadata
            writer.WritePropertyName("metadata");
            JsonSerializer.Serialize(writer, metadata);
            
            // Start tokens array
            writer.WritePropertyName("content");
            writer.WriteStartArray();
            
            // Character counter in current line
            int currentLineLength = 0;
            
            // Buffer for accumulating current line
            StringBuilder lineBuffer = new StringBuilder();
            
            // Serialization of each token
            foreach (var token in tokens)
            {
                // Serialize token to temporary stream
                using var tokenStream = new MemoryStream();
                using var tokenWriter = new Utf8JsonWriter(tokenStream, _jsonWriterOptions);
                
                JsonSerializer.Serialize(tokenWriter, token);
                tokenWriter.Flush();
                
                // Get string representation of the token
                string tokenJson = Encoding.UTF8.GetString(tokenStream.ToArray());
                
                // Check if adding the token will exceed the maximum line length
                if (currentLineLength + tokenJson.Length > _options.MaxLineLength && currentLineLength > 0)
                {
                    // Write accumulated line and reset buffer
                    outputStream.Write(Encoding.UTF8.GetBytes(lineBuffer.ToString()));
                    outputStream.Write(Encoding.UTF8.GetBytes(Environment.NewLine));
                    lineBuffer.Clear();
                    currentLineLength = 0;
                }
                
                // Add token to buffer
                lineBuffer.Append(tokenJson);
                currentLineLength += tokenJson.Length;
                
                // If token is not the last one, add comma
                lineBuffer.Append(",");
                currentLineLength += 1;
                
                // If line length approaches maximum, write and reset buffer
                if (currentLineLength >= _options.MaxLineLength * 0.9)
                {
                    outputStream.Write(Encoding.UTF8.GetBytes(lineBuffer.ToString()));
                    outputStream.Write(Encoding.UTF8.GetBytes(Environment.NewLine));
                    lineBuffer.Clear();
                    currentLineLength = 0;
                }
            }
            
            // Write remaining data in buffer
            if (lineBuffer.Length > 0)
            {
                // Remove last comma
                if (lineBuffer[lineBuffer.Length - 1] == ',')
                {
                    lineBuffer.Length--;
                }
                
                outputStream.Write(Encoding.UTF8.GetBytes(lineBuffer.ToString()));
            }
            
            // End array and object
            writer.WriteEndArray();
            writer.WriteEndObject();
            
            // Flush write buffer
            writer.Flush();
        }
    }
}
