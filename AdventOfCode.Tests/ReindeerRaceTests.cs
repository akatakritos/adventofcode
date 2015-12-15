using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Core;

using NFluent;

using Xunit;

namespace AdventOfCode.Tests
{
    public class ReindeerRaceTests
    {
        [Fact]
        public void SampleParse()
        {
            var result = ReindeerStats.Parse("Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.");

            Check.That(result).IsEqualTo(new ReindeerStats("Comet", 14, 10, 127));
        }

        [Fact]
        public void Scenario()
        {
            var subject = new ReindeerRace();
            subject.AddMultipleReindeer(new[]
            {
                "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
                "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds."
            });

            var winner = subject.GetWinner(1000);

            Check.That(winner.Name).IsEqualTo("Comet");
            Check.That(winner.Distance).IsEqualTo(1120);
        }

        [Fact]
        public void GetWinnerByRoundRules()
        {
            var subject = new ReindeerRace();
            subject.AddMultipleReindeer(new[]
            {
                "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
                "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds."
            });

            var winner = subject.GetWinnerByRoundRules(1000);

            Check.That(winner.Name).IsEqualTo("Dancer");
            Check.That(winner.Points).IsEqualTo(689);
        }
    }
}
