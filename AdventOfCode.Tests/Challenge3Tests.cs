using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Core;

using NFluent;

using Xunit;

namespace AdventOfCode.Tests
{
    public class Challenge3Tests
    {
        [Fact]
        public void OneLocationIsTwoDeliveries()
        {
            Check.That(Challenge3.NumberOfHousesVisited(">")).IsEqualTo(2);
        }

        [Fact]
        public void SquareDirectionsIsFourHouses()
        {
            Check.That(Challenge3.NumberOfHousesVisited("^>v<")).IsEqualTo(4);
        }

        [Fact]
        public void ZigZagIsTwoHouses()
        {
            Check.That(Challenge3.NumberOfHousesVisited("^v^v^v^v^v")).IsEqualTo(2);
        }

        [Fact]
        public void Robo_OppositeDirectsGetsThreeHouses()
        {
            Check.That(Challenge3.NumberOfHousesVisitedWithRoboSantaToo("^v"))
                .IsEqualTo(3);
        }

        [Fact]
        public void Robo_BothZigZagIsThreeHouses()
        {
            Check.That(Challenge3.NumberOfHousesVisitedWithRoboSantaToo("^>v<"))
                .IsEqualTo(3);
        }

        [Fact]
        public void Robo_BothGoStraightIsElevenHouses()
        {
            Check.That(Challenge3.NumberOfHousesVisitedWithRoboSantaToo("^v^v^v^v^v"))
                .IsEqualTo(11);
        }
    }
}
