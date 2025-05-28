using System;
using System.Collections.Generic;
using TomTokenGenerator.Models;

namespace TomTokenGenerator.Generators
{
    /// <summary>
    /// Random token generator
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
        /// Generates a sequence of tokens
        /// </summary>
        /// <param name="count">Number of tokens to generate</param>
        /// <returns>Sequence of tokens</returns>
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
        /// Generates the next token
        /// </summary>
        /// <returns>Generated token</returns>
        private TomToken GenerateNextToken()
        {
            int probability = _random.Next(1, 101);
            int cumulativeProbability = 0;

            // Text token
            cumulativeProbability += _options.TextTokenProbability;
            if (probability <= cumulativeProbability)
            {
                return GenerateTextToken();
            }

            // Newline token
            cumulativeProbability += _options.NewLineTokenProbability;
            if (probability <= cumulativeProbability)
            {
                return GenerateNewLineToken();
            }

            // Repeat token
            cumulativeProbability += _options.RepeatTokenProbability;
            if (probability <= cumulativeProbability)
            {
                return GenerateRepeatToken();
            }

            // Reference token
            return GenerateReferenceToken();
        }

        /// <summary>
        /// Generates a text token
        /// </summary>
        /// <returns>Text token</returns>
        private TextToken GenerateTextToken()
        {
            int index = _random.Next(0, _options.WordDictionary.Length);
            return new TextToken(_options.WordDictionary[index]);
        }

        /// <summary>
        /// Generates a newline token
        /// </summary>
        /// <returns>Newline token</returns>
        private NewLineToken GenerateNewLineToken()
        {
            NewLineType newLineType = (NewLineType)_random.Next(0, 3);
            return new NewLineToken(newLineType);
        }

        /// <summary>
        /// Generates a repeat token
        /// </summary>
        /// <returns>Repeat token</returns>
        private RepeatToken GenerateRepeatToken()
        {
            // If there are no previous tokens, generate a text token
            if (_recentTokens.Count == 0)
            {
                return new RepeatToken(GenerateTextToken(), _random.Next(2, _options.MaxRepeatCount + 1));
            }

            // Choose a random token from recent ones
            int index = _random.Next(0, _recentTokens.Count);
            int count = _random.Next(2, _options.MaxRepeatCount + 1);
            
            // Create a copy of the token to repeat
            TomToken tokenToRepeat;
            
            // Avoid nested repeats for simplicity
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
        /// Generates a reference token
        /// </summary>
        /// <returns>Reference token</returns>
        private ReferenceToken GenerateReferenceToken()
        {
            int referenceId = _random.Next(1, _options.MaxReferenceId + 1);
            return new ReferenceToken(referenceId);
        }
    }
}
