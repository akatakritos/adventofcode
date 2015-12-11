using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Core;

using NFluent;

using Xunit;

namespace AdventOfCode.Tests
{
    public class StringEscaperTests
    {
        [Fact]
        public void EmptyStringIsTwoCharactersOfCode()
        {
            var result = StringEscaper.UnEscape(@"""""");
            Check.That(result).IsEqualTo(new UnEscapedStringResult("", 2));
        }

        [Fact]
        public void SimpleString()
        {
            var result = StringEscaper.UnEscape(@"""abc""");
            Check.That(result).IsEqualTo(new UnEscapedStringResult("abc", 5));
        }

        [Fact]
        public void EscapedDoubleQuote()
        {
            var result = StringEscaper.UnEscape(@"""aaa\""aaa""");
            Check.That(result).IsEqualTo(new UnEscapedStringResult("aaa\"aaa", 10));
        }

        [Fact]
        public void EscapedHexCodes()
        {
            var result = StringEscaper.UnEscape(@"""\x27""");
            Check.That(result).IsEqualTo(new UnEscapedStringResult("'", 6));
        }

        [Fact]
        public void MixedString()
        {
            var result = StringEscaper.UnEscape(@"""byc\x9dyxuafof\\\xa6uf\\axfozomj\\olh\x6a""");
            Check.That(result).IsEqualTo(new UnEscapedStringResult("byc\u009dyxuafof\\¦uf\\axfozomj\\olhj", 43));
        }

        [Fact]
        public void EncodeEmptyString()
        {
            const string input = @"""""";

            var result = StringEscaper.Escape(input);

            Check.That(input).HasSize(2);
            Check.That(result).HasSize(6);
            Check.That(result).IsEqualTo("\"\\\"\\\"\"");
        }

        [Fact]
        public void EncodesSimpleString()
        {
            const string input = "\"abc\"";

            var result = StringEscaper.Escape(input);

            Check.That(input).HasSize(5);
            Check.That(result).HasSize(9);
            Check.That(result).IsEqualTo(@"""\""abc\""""");
        }

        [Fact]
        public void EscapesStringWithEscapedQuote()
        {
            const string input = "\"aaa\\\"aaa\"";

            var result = StringEscaper.Escape(input);

            Check.That(input).HasSize(10);
            Check.That(result).HasSize(16);
            Check.That(result).IsEqualTo(@"""\""aaa\\\""aaa\""""");
        }

        [Fact]
        public void EscapesStringWithEscapedHex()
        {
            const string input = @"""\x27""";

            var result = StringEscaper.Escape(input);

            Check.That(input).HasSize(6);
            Check.That(result).HasSize(11);
            Check.That(result).IsEqualTo(@"""\""\\x27\""""");
        }
    }
}
