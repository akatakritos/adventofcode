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
            var result = StringEscaper.Escape(@"""""");
            Check.That(result).IsEqualTo(new StringEscaperResult("", 2));
        }

        [Fact]
        public void SimpleString()
        {
            var result = StringEscaper.Escape(@"""abc""");
            Check.That(result).IsEqualTo(new StringEscaperResult("abc", 5));
        }

        [Fact]
        public void EscapedDoubleQuote()
        {
            var result = StringEscaper.Escape(@"""aaa\""aaa""");
            Check.That(result).IsEqualTo(new StringEscaperResult("aaa\"aaa", 10));
        }

        [Fact]
        public void EscapedHexCodes()
        {
            var result = StringEscaper.Escape(@"""\x27""");
            Check.That(result).IsEqualTo(new StringEscaperResult("'", 6));
        }

        [Fact]
        public void MixedString()
        {
            var result = StringEscaper.Escape(@"""byc\x9dyxuafof\\\xa6uf\\axfozomj\\olh\x6a""");
            Check.That(result).IsEqualTo(new StringEscaperResult("byc\u009dyxuafof\\¦uf\\axfozomj\\olhj", 43));
        }
    }
}
