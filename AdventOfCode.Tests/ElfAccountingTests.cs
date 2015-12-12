using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Core;

using NFluent;

using Xunit;

namespace AdventOfCode.Tests
{
    public class ElfAccountingTests
    {
        [Theory]
        [InlineData(@"[1,2,3]", 6)]
        [InlineData(@"{""a"":2,""b"":4}", 6)]
        [InlineData(@"[[[3]]]", 3)]
        [InlineData(@"{""a"":{""b"":4},""c"":-1}", 3)]
        [InlineData(@"{""a"":[-1,1]}", 0)]
        [InlineData(@"[-1,{""a"":1}]", 0)]
        [InlineData(@"[]", 0)]
        [InlineData(@"{}", 0)]
        public void SumAllNumbers(string json, int sum)
        {
            var result = ElfAccounting.SumAllNumbers(json);

            Check.That(result).IsEqualTo(sum);
        }

        [Theory]
        [InlineData("[1,2,3]", 6)]
        [InlineData(@"[1,{""c"":""red"",""b"":2},3]", 4)]
        [InlineData(@"{""d"":""red"",""e"":[1,2,3,4],""f"":5}", 0)]
        [InlineData(@"[1,""red"",5]", 6)]
        public void SumAllNonRedObjects(string json, int sum)
        {
            var result = ElfAccounting.SumAllNonRedObjects(json);

            Check.That(result).IsEqualTo(sum);
        }
    }
}
