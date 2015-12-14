using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Core
{
    internal struct HappinessPotential : IEquatable<HappinessPotential>
    {
        public string Name { get; }
        public int HappinessDelta { get; }
        public string Neighbor { get; }

        public HappinessPotential(string name, int happinessDelta, string neighbor)
        {
            Name = name;
            HappinessDelta = happinessDelta;
            Neighbor = neighbor;
        }

        public static HappinessPotential Parse(string input)
        {
            var result = Regex.Match(input, @"^(?<name>\w+) would (?<verb>lose|gain) (?<change>\d+) happiness units by sitting next to (?<neighbor>\w+)\.$");

            if (!result.Success)
                throw new FormatException("Could not parse happiness line.");

            var name = result.Groups["name"].Value;
            var verb = result.Groups["verb"].Value;
            var change = int.Parse(result.Groups["change"].Value);
            var neighbor = result.Groups["neighbor"].Value;

            return new HappinessPotential(
                name,
                verb == "lose" ? -1 * change : change,
                neighbor);
        }

        public override string ToString()
        {
            var change = Math.Abs(HappinessDelta);
            var verb = HappinessDelta < 0 ? "lose" : "gain";
            return $"{Name} would {verb} {change} happiness units by sitting next to {Neighbor}.";
        }

        #region R# Equality Members

        public bool Equals(HappinessPotential other)
        {
            return string.Equals(Name, other.Name) && HappinessDelta == other.HappinessDelta && string.Equals(Neighbor, other.Neighbor);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is HappinessPotential && Equals((HappinessPotential)obj);
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

        public static bool operator ==(HappinessPotential left, HappinessPotential right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HappinessPotential left, HappinessPotential right)
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
    }

    public class TableArrangement
    {
        public IEnumerable<TableSeat> TablePositions(string startingAt)
        {
            throw new NotImplementedException();
        }
    }

    public class DinnerTable
    {
        public void AddGuestRequirement(HappinessPotential parse)
        {
            throw new NotImplementedException();
        }

        public TableArrangement DetermineOptimalArrangement()
        {

        }
    }
}
