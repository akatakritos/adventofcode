using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
/*
--- Day 13: Knights of the Dinner Table ---

In years past, the holiday feast with your family hasn't gone so well. Not everyone gets along! This year, you resolve, will be different. You're going to find the optimal seating arrangement and avoid all those awkward conversations.

You start by writing up a list of everyone invited and the amount their happiness would increase or decrease if they were to find themselves sitting next to each other person. You have a circular table that will be just big enough to fit everyone comfortably, and so each person will have exactly two neighbors.

For example, suppose you have only four attendees planned, and you calculate their potential happiness as follows:

Alice would gain 54 happiness units by sitting next to Bob.
Alice would lose 79 happiness units by sitting next to Carol.
Alice would lose 2 happiness units by sitting next to David.
Bob would gain 83 happiness units by sitting next to Alice.
Bob would lose 7 happiness units by sitting next to Carol.
Bob would lose 63 happiness units by sitting next to David.
Carol would lose 62 happiness units by sitting next to Alice.
Carol would gain 60 happiness units by sitting next to Bob.
Carol would gain 55 happiness units by sitting next to David.
David would gain 46 happiness units by sitting next to Alice.
David would lose 7 happiness units by sitting next to Bob.
David would gain 41 happiness units by sitting next to Carol.
Then, if you seat Alice next to David, Alice would lose 2 happiness units (because David talks so much), but David would gain 46 happiness units (because Alice is such a good listener), for a total change of 44.

If you continue around the table, you could then seat Bob next to Alice (Bob gains 83, Alice gains 54). Finally, seat Carol, who sits next to Bob (Carol gains 60, Bob loses 7) and David (Carol gains 55, David gains 41). The arrangement looks like this:

     +41 +46
+55   David    -2
Carol       Alice
+60    Bob    +54
     -7  +83
After trying every other seating arrangement in this hypothetical scenario, you find that this one is the most optimal, with a total change in happiness of 330.

What is the total change in happiness for the optimal seating arrangement of the actual guest list?

Your puzzle answer was 618.

The first half of this puzzle is complete! It provides one gold star: *

--- Part Two ---

In all the commotion, you realize that you forgot to seat yourself. At this point, you're pretty apathetic toward the whole thing, and your happiness wouldn't really go up or down regardless of who you sit next to. You assume everyone else would be just as ambivalent about sitting next to you, too.

So, add yourself to the list, and give all happiness relationships that involve you a score of 0.

What is the total change in happiness for the optimal seating arrangement that actually includes yourself?
*/
namespace AdventOfCode.Core
{
    public struct SeatingRequirement : IEquatable<SeatingRequirement>
    {
        public string Name { get; }
        public int HappinessDelta { get; }
        public string Neighbor { get; }

        public SeatingRequirement(string name, int happinessDelta, string neighbor)
        {
            Name = name;
            HappinessDelta = happinessDelta;
            Neighbor = neighbor;
        }

        public static SeatingRequirement Parse(string input)
        {
            var result = Regex.Match(input, @"^(?<name>\w+) would (?<verb>lose|gain) (?<change>\d+) happiness units by sitting next to (?<neighbor>\w+)\.$");

            if (!result.Success)
            {
                throw new FormatException("Could not requirement happiness line.");
            }

            var name = result.Groups["name"].Value;
            var verb = result.Groups["verb"].Value;
            var change = int.Parse(result.Groups["change"].Value);
            var neighbor = result.Groups["neighbor"].Value;

            return new SeatingRequirement(
                name,
                verb == "lose" ? -1 * change : change,
                neighbor);
        }

        public static IEnumerable<SeatingRequirement> ParseMultiple(IEnumerable<string> input)
        {
            return input.Select(Parse);
        }

        public override string ToString()
        {
            var change = Math.Abs(HappinessDelta);
            var verb = HappinessDelta < 0 ? "lose" : "gain";
            return $"{Name} would {verb} {change} happiness units by sitting next to {Neighbor}.";
        }

        #region R# Equality Members

        public bool Equals(SeatingRequirement other)
        {
            return string.Equals(Name, other.Name) && HappinessDelta == other.HappinessDelta && string.Equals(Neighbor, other.Neighbor);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is SeatingRequirement && Equals((SeatingRequirement)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ HappinessDelta;
                hashCode = (hashCode * 397) ^ (Neighbor != null ? Neighbor.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(SeatingRequirement left, SeatingRequirement right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SeatingRequirement left, SeatingRequirement right)
        {
            return !left.Equals(right);
        }

        #endregion
    }

    internal static class Permutator
    {
        public static void Permutate<T>(T[] input, Action<T[]> action)
        {
            PermutateImpl(input.ToArray(), action, 0, input.Length - 1);
        }

        private static void PermutateImpl<T>(T[] input, Action<T[]> action, int k, int m)
        {
            if (k == m)
            {
                action(input.ToArray());
            }
            else
            {
                for (var i = k; i <= m; i++)
                {
                    Swap(ref input[k], ref input[i]);
                    PermutateImpl(input, action, k + 1, m);
                    Swap(ref input[k], ref input[i]);
                }
            }
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }
    }

    public struct TableSeat
    {
        public string Name { get; }
        public int LeftHappiness { get; }
        public int RightHappiness { get; }

        public TableSeat(string name, int left, int right)
        {
            Name = name;
            LeftHappiness = left;
            RightHappiness = right;
        }

        public override string ToString()
        {
            return $"{Name} ({LeftHappiness}, {RightHappiness})";
        }
    }

    public class TableArrangement
    {
        private readonly TableSeat[] _seats;

        public TableArrangement(TableSeat[] seats)
        {
            _seats = seats;
        }

        public IEnumerable<TableSeat> TablePositions(string startingAt)
        {
            var start = FindSeatIndex(startingAt);
            for (int i = 0; i < _seats.Length; i++)
            {
                yield return _seats[(start + i) % _seats.Length];
            }
        }

        private int FindSeatIndex(string name)
        {
            for (var i = 0; i < _seats.Length; i++)
            {
                if (_seats[i].Name == name)
                {
                    return i;
                }
            }

            throw new KeyNotFoundException($"Couldnt find seat for {name}.");
        }

        public int GetTotalHappiness()
        {
            return _seats.Sum(s => s.LeftHappiness + s.RightHappiness);
        }
    }

    public class DinnerTable
    {
        private readonly List<SeatingRequirement> _requirements = new List<SeatingRequirement>();

        public void AddGuestRequirement(SeatingRequirement requirement)
        {
            _requirements.Add(requirement);
        }

        public void AddGuestRequirements(IEnumerable<SeatingRequirement> requirements)
        {
            foreach (var req in requirements)
                AddGuestRequirement(req);
        }

        public void AddSelf()
        {
            var names = _requirements
                .Select(r => r.Name)
                .Distinct()
                .ToArray();

            foreach (var name in names)
            {
                AddGuestRequirement(new SeatingRequirement("Me", 0, name));
                AddGuestRequirement(new SeatingRequirement(name, 0, "Me"));
            }
        }

        public TableArrangement DetermineOptimalArrangement()
        {
            var names = _requirements
                .Select(r => r.Name)
                .Distinct()
                .ToArray();

            var max = new TableArrangement(Array.Empty<TableSeat>());

            Permutator.Permutate(names, test =>
            {
                var seats = FromNames(test);

                if (seats.GetTotalHappiness() >= max.GetTotalHappiness())
                {
                    max = seats;
                }
            });

            return max;
        }

        private TableArrangement FromNames(string[] names)
        {
            var seats = new TableSeat[names.Length];

            for (var i = 0; i < seats.Length; i++)
            {
                var person = names[i];
                var left = GetHappinessDelta(person, names[i == 0 ? names.Length - 1 : i - 1]);
                var right = GetHappinessDelta(person, names[(i + 1) % names.Length]);
                seats[i] = new TableSeat(person, left, right);
            }

            return new TableArrangement(seats);
        }

        private int GetHappinessDelta(string person, string neighbor)
        {
            return _requirements
                .Single(r => r.Name == person && r.Neighbor == neighbor)
                .HappinessDelta;
        }
    }
}
