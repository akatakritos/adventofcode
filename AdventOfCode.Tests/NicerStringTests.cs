using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Core;

using NFluent;

using Xunit;

namespace AdventOfCode.Tests
{
    public class NicerStringTests
    {
        [Fact]
        public void NiceStringIsNice()
        {
            Check.That(NicerString.Test("qjhvhtzxzqqjkmpb")).IsTrue();
        }


        [Fact]
        public void OverlappingIsNotAllowedWhenFourRepeat()
        {
            Check.That(NicerString.Test("aaaa")).IsTrue();
        }

        [Fact]
        public void ShorterNiceStringIsNice()
        {
            Check.That(NicerString.Test("xxyxx")).IsTrue();
        }

        [Fact]
        public void LackOfSandwichIsNaughty()
        {
            Check.That(NicerString.Test("uurcxstgmygtbstg")).IsFalse();
        }

        [Fact]
        public void LackOfRepeatedPairIsNaughty()
        {
            Check.That(NicerString.Test("ieodomkazucvgmuy")).IsFalse();
        }
    }
}
