using System;
using System.Collections.Generic;
using TomTokenGenerator.Models;

namespace TomTokenGenerator.Generators
{
    /// <summary>
    /// Генератор случайных токенов
    /// </summary>
    public class RandomTokenGenerator : ITokenGenerator
    {
        private readonly Random _random;
        private readonly TokenGeneratorOptions _options;
        private readonly List<TomToken> _recentTokens;
        private readonly int _maxRecentTokens = 10;

        public RandomTokenGenerator(TokenGeneratorOptions options)
        {
            _random = new Random();
            _options = options ?? new TokenGeneratorOptions();
            _recentTokens = new List<TomToken>();
        }

        /// <summary>
        /// Генерирует последовательность токенов
        /// </summary>
        /// <param name="count">Количество токенов для генерации</param>
        /// <returns>Последовательность токенов</returns>
        public IEnumerable<TomToken> Generate(long count)
        {
            for (long i = 0; i < count; i++)
            {
                var token = GenerateNextToken();
                _recentTokens.Add(token);
                if (_recentTokens.Count > _maxRecentTokens)
                {
                    _recentTokens.RemoveAt(0);
                }
                yield return token;
            }
        }

        /// <summary>
        /// Генерирует следующий токен
        /// </summary>
        /// <returns>Сгенерированный токен</returns>
        private TomToken GenerateNextToken()
        {
            int probability = _random.Next(1, 101);
            int cumulativeProbability = 0;

            // Текстовый токен
            cumulativeProbability += _options.TextTokenProbability;
            if (probability <= cumulativeProbability)
            {
                return GenerateTextToken();
            }

            // Токен перевода строки
            cumulativeProbability += _options.NewLineTokenProbability;
            if (probability <= cumulativeProbability)
            {
                return GenerateNewLineToken();
            }

            // Токен повтора
            cumulativeProbability += _options.RepeatTokenProbability;
            if (probability <= cumulativeProbability)
            {
                return GenerateRepeatToken();
            }

            // Токен ссылки
            return GenerateReferenceToken();
        }

        /// <summary>
        /// Генерирует текстовый токен
        /// </summary>
        /// <returns>Текстовый токен</returns>
        private TextToken GenerateTextToken()
        {
            int index = _random.Next(0, _options.WordDictionary.Length);
            return new TextToken(_options.WordDictionary[index]);
        }

        /// <summary>
        /// Генерирует токен перевода строки
        /// </summary>
        /// <returns>Токен перевода строки</returns>
        private NewLineToken GenerateNewLineToken()
        {
            NewLineType newLineType = (NewLineType)_random.Next(0, 3);
            return new NewLineToken(newLineType);
        }

        /// <summary>
        /// Генерирует токен повтора
        /// </summary>
        /// <returns>Токен повтора</returns>
        private RepeatToken GenerateRepeatToken()
        {
            // Если нет предыдущих токенов, генерируем текстовый токен
            if (_recentTokens.Count == 0)
            {
                return new RepeatToken(GenerateTextToken(), _random.Next(2, _options.MaxRepeatCount + 1));
            }

            // Выбираем случайный токен из недавних
            int index = _random.Next(0, _recentTokens.Count);
            int count = _random.Next(2, _options.MaxRepeatCount + 1);
            
            // Создаем копию токена для повтора
            TomToken tokenToRepeat;
            
            // Избегаем вложенных повторов для упрощения
            if (_recentTokens[index] is RepeatToken)
            {
                tokenToRepeat = GenerateTextToken();
            }
            else
            {
                tokenToRepeat = _recentTokens[index];
            }
            
            return new RepeatToken(tokenToRepeat, count);
        }

        /// <summary>
        /// Генерирует токен ссылки
        /// </summary>
        /// <returns>Токен ссылки</returns>
        private ReferenceToken GenerateReferenceToken()
        {
            int referenceId = _random.Next(1, _options.MaxReferenceId + 1);
            return new ReferenceToken(referenceId);
        }
    }
}
