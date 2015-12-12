using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Core;

using NFluent;

using Xunit;

namespace AdventOfCode.Tests
{
    public class DrunkElfDirectionTests
    {
        [Theory]
        [InlineData(">", 2)]
        [InlineData("^>v<", 4)]
        [InlineData("^v^v^v^v^v", 2)]
        public void NumberOfHousesVisited(string directions, int number)
        {
            Check.That(DrunkElfDirections.NumberOfHousesVisited(directions)).IsEqualTo(number);
        }

        [Theory]
        [InlineData("^v", 3)]
        [InlineData("^>v<", 3)]
        [InlineData("^v^v^v^v^v", 11)]
        public void NumberOfHousesVisitedWithRoboSantaToo(string directions, int number)
        {
            Check.That(DrunkElfDirections.NumberOfHousesVisitedWithRoboSantaToo(directions))
                .IsEqualTo(number);
        }
    }
}
