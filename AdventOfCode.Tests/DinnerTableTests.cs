using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using AdventOfCode.Core;

using NFluent;

using Xunit;

namespace AdventOfCode.Tests
{
    public class DinnerTableTests
    {
        [Fact]
        public void ParsesGainLine()
        {
            var result = HappinessPotential.Parse("Alice would gain 54 happiness units by sitting next to Bob.");

            Check.That(result)
                .IsEqualTo(new HappinessPotential("Alice", 54, "Bob"));
        }

        [Fact]
        public void ParseLoseLine()
        {
            var result = HappinessPotential.Parse("Alice would lose 79 happiness units by sitting next to Carol.");

            Check.That(result)
                .IsEqualTo(new HappinessPotential("Alice", -79, "Carol"));
        }
    }

    public class PermutatorTests
    {
        [Fact]
        public void PermutatesAnyArray()
        {
            var input = new[] { 'A', 'B', 'C' };

            var list = new List<string>();

            Permutator.Permutate(input, element =>
            {
                list.Add(new string(element));
            });

            Check.That(list).Contains(
                "ABC", "BCA", "BAC", "ACB", "CBA", "CAB");
        }
    }

    public class DinnerTableTests
    {
        [Fact]
        public void SampleProblem()
        {
            var subject = new DinnerTable();
            subject.AddGuestRequirement(HappinessPotential.Parse("Alice would gain 54 happiness units by sitting next to Bob."));
            subject.AddGuestRequirement(HappinessPotential.Parse("Alice would lose 79 happiness units by sitting next to Carol."));
            subject.AddGuestRequirement(HappinessPotential.Parse("Alice would lose 2 happiness units by sitting next to David."));
            subject.AddGuestRequirement(HappinessPotential.Parse("Bob would gain 83 happiness units by sitting next to Alice."));
            subject.AddGuestRequirement(HappinessPotential.Parse("Bob would lose 7 happiness units by sitting next to Carol."));
            subject.AddGuestRequirement(HappinessPotential.Parse("Bob would lose 63 happiness units by sitting next to David."));
            subject.AddGuestRequirement(HappinessPotential.Parse("Carol would lose 62 happiness units by sitting next to Alice."));
            subject.AddGuestRequirement(HappinessPotential.Parse("Carol would gain 60 happiness units by sitting next to Bob."));
            subject.AddGuestRequirement(HappinessPotential.Parse("Carol would gain 55 happiness units by sitting next to David."));
            subject.AddGuestRequirement(HappinessPotential.Parse("David would gain 46 happiness units by sitting next to Alice."));
            subject.AddGuestRequirement(HappinessPotential.Parse("David would lose 7 happiness units by sitting next to Bob."));
            subject.AddGuestRequirement(HappinessPotential.Parse("David would gain 41 happiness units by sitting next to Carol."));

            var result = subject.DetermineOptimalArrangement();

            Check.That(result.TablePositions(startingAt: "David").Select(s => s.Name))
                .ContainsExactly("David", "Alice", "Bob", "Carol");
        }
    }
}


