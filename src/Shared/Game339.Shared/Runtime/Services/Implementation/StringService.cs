using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game339.Shared.Diagnostics;

namespace Game339.Shared.Services.Implementation
{
    public class StringService : IStringService
    {
        private readonly IGameLog _log;

        public StringService(IGameLog log)
        {
            _log = log;
        }

        public string Reverse(string input)
        {
            var output = new string(input.Reverse().ToArray());
            _log.Info($"{nameof(StringService)}.{nameof(Reverse)} - {nameof(input)}: {input} - {nameof(output)}: {output}");
            return output;
        }

        public string ReverseWords(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Length == 0)
            {
                _log.Info($"{nameof(StringService)}.{nameof(ReverseWords)} - {nameof(input)}: {input} - output: {input}");
                return input;
            }

            var tokens = new List<Token>();
            var buffer = new StringBuilder();
            var readingWhitespace = char.IsWhiteSpace(input[0]);

            foreach (var c in input)
            {
                var isWhitespace = char.IsWhiteSpace(c);
                if (isWhitespace != readingWhitespace)
                {
                    tokens.Add(new Token(buffer.ToString(), readingWhitespace));
                    buffer.Clear();
                    readingWhitespace = isWhitespace;
                }

                buffer.Append(c);
            }

            if (buffer.Length > 0)
            {
                tokens.Add(new Token(buffer.ToString(), readingWhitespace));
            }

            var words = tokens.Where(token => !token.IsWhitespace).Select(token => token.Value).Reverse().ToArray();
            var wordIndex = 0;
            var outputBuilder = new StringBuilder(input.Length);

            foreach (var token in tokens)
            {
                if (token.IsWhitespace)
                {
                    outputBuilder.Append(token.Value);
                    continue;
                }

                outputBuilder.Append(words[wordIndex]);
                wordIndex++;
            }

            var output = outputBuilder.ToString();
            _log.Info($"{nameof(StringService)}.{nameof(ReverseWords)} - {nameof(input)}: {input} - {nameof(output)}: {output}");
            return output;
        }

        private readonly struct Token
        {
            public Token(string value, bool isWhitespace)
            {
                Value = value;
                IsWhitespace = isWhitespace;
            }

            public string Value { get; }
            public bool IsWhitespace { get; }
        }
    }
}
