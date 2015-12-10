using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Core;

using NFluent;

using Xunit;

namespace AdventOfCode.Tests
{
    public class LookAndSayTests
    {
        [Theory]
        [InlineData("1", 1, "11")]
        [InlineData("1", 2, "21")]
        [InlineData("1", 3, "1211")]
        [InlineData("1", 4, "111221")]
        [InlineData("1", 5, "312211")]
        public void TransformationChecks(string input, int iterations, string output)
        {
            var result = LookAndSay.Transform(input, iterations);
            Check.That(result).IsEqualTo(output);
        }
    }
}
