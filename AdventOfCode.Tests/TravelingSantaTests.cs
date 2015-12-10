using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Core;

using NFluent;

using Xunit;

namespace AdventOfCode.Tests
{
    public class TravelingSantaTests
    {
        [Fact]
        public void BruteForcesShortestRoute()
        {
            var solver = new TravelingSanta();
            solver.AddDistance("London", "Dublin", 464);
            solver.AddDistance("London", "Belfast", 518);
            solver.AddDistance("Dublin", "Belfast", 141);

            var route = solver.FindShortestRoute();
            Check.That(route.Stops).ContainsExactly("London", "Dublin", "Belfast");
            Check.That(route.TotalDistance).IsEqualTo(605);
        }
    }
}
