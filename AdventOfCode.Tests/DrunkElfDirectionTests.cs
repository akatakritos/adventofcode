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
        [Fact]
        public void OneLocationIsTwoDeliveries()
        {
            Check.That(DrunkElfDirections.NumberOfHousesVisited(">")).IsEqualTo(2);
        }

        [Fact]
        public void SquareDirectionsIsFourHouses()
        {
            Check.That(DrunkElfDirections.NumberOfHousesVisited("^>v<")).IsEqualTo(4);
        }

        [Fact]
        public void ZigZagIsTwoHouses()
        {
            Check.That(DrunkElfDirections.NumberOfHousesVisited("^v^v^v^v^v")).IsEqualTo(2);
        }

        [Fact]
        public void Robo_OppositeDirectsGetsThreeHouses()
        {
            Check.That(DrunkElfDirections.NumberOfHousesVisitedWithRoboSantaToo("^v"))
                .IsEqualTo(3);
        }

        [Fact]
        public void Robo_BothZigZagIsThreeHouses()
        {
            Check.That(DrunkElfDirections.NumberOfHousesVisitedWithRoboSantaToo("^>v<"))
                .IsEqualTo(3);
        }

        [Fact]
        public void Robo_BothGoStraightIsElevenHouses()
        {
            Check.That(DrunkElfDirections.NumberOfHousesVisitedWithRoboSantaToo("^v^v^v^v^v"))
                .IsEqualTo(11);
        }
    }
}
