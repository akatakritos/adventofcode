using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

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
    }
}
