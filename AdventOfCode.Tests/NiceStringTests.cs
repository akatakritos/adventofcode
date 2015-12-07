using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Core;

using NFluent;

using Xunit;

namespace AdventOfCode.Tests
{
    public class NiceStringTests
    {
        [Fact]
        public void NiceStringIsNice()
        {
            Check.That(NiceString.Test("ugknbfddgicrmopn")).IsTrue();
        }

        [Fact]
        public void ThreeVowelsIsNiceEvenThoughTheyOverlapRules()
        {
            Check.That(NiceString.Test("aaa")).IsTrue();
        }

        [Fact]
        public void StringWithoutDoubleLetterIsNaughty()
        {
            Check.That(NiceString.Test("jchzalrnumimnmhp")).IsFalse();
        }

        [Fact]
        public void StringContainingNaughtyWordIsNaughty()
        {
            Check.That(NiceString.Test("haegwjzuvuyypxyu")).IsFalse();
        }

        [Fact]
        public void StringWithuotThreeVowelsIsNaughty()
        {
            Check.That(NiceString.Test("dvszwmarrgswjxmb")).IsFalse();
        }
    }
}
