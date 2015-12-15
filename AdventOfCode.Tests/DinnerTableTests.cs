using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Core;

using NFluent;

using Xunit;

namespace AdventOfCode.Tests
{
    public class SeatingRequirementTests
    {
        [Fact]
        public void ParsesGainLine()
        {
            var result = SeatingRequirement.Parse("Alice would gain 54 happiness units by sitting next to Bob.");

            Check.That(result)
                .IsEqualTo(new SeatingRequirement("Alice", 54, "Bob"));
        }

        [Fact]
        public void ParseLoseLine()
        {
            var result = SeatingRequirement.Parse("Alice would lose 79 happiness units by sitting next to Carol.");

            Check.That(result)
                .IsEqualTo(new SeatingRequirement("Alice", -79, "Carol"));
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
            subject.AddGuestRequirement(SeatingRequirement.Parse("Alice would gain 54 happiness units by sitting next to Bob."));
            subject.AddGuestRequirement(SeatingRequirement.Parse("Alice would lose 79 happiness units by sitting next to Carol."));
            subject.AddGuestRequirement(SeatingRequirement.Parse("Alice would lose 2 happiness units by sitting next to David."));
            subject.AddGuestRequirement(SeatingRequirement.Parse("Bob would gain 83 happiness units by sitting next to Alice."));
            subject.AddGuestRequirement(SeatingRequirement.Parse("Bob would lose 7 happiness units by sitting next to Carol."));
            subject.AddGuestRequirement(SeatingRequirement.Parse("Bob would lose 63 happiness units by sitting next to David."));
            subject.AddGuestRequirement(SeatingRequirement.Parse("Carol would lose 62 happiness units by sitting next to Alice."));
            subject.AddGuestRequirement(SeatingRequirement.Parse("Carol would gain 60 happiness units by sitting next to Bob."));
            subject.AddGuestRequirement(SeatingRequirement.Parse("Carol would gain 55 happiness units by sitting next to David."));
            subject.AddGuestRequirement(SeatingRequirement.Parse("David would gain 46 happiness units by sitting next to Alice."));
            subject.AddGuestRequirement(SeatingRequirement.Parse("David would lose 7 happiness units by sitting next to Bob."));
            subject.AddGuestRequirement(SeatingRequirement.Parse("David would gain 41 happiness units by sitting next to Carol."));

            var result = subject.DetermineOptimalArrangement();

            Check.That(result.TablePositions(startingAt: "David").Select(s => s.Name))
                .ContainsExactly("David", "Alice", "Bob", "Carol");
        }
    }
}


