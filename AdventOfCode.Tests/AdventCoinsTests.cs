using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Core;

using NFluent;

using Xunit;

namespace AdventOfCode.Tests
{
    public class AdventCoinsTests
    {
        [Fact]
        public void ResyktFor_abcdef_is_609043()
        {
            var result = AdventCoins.Mine("abcdef");
            Check.That(result.Hash).StartsWith("000001DBBFA");
            Check.That(result.Input).IsEqualTo(609043);
        }
    }
}
