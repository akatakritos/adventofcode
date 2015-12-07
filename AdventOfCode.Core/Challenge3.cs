using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core
{
    public static class Challenge3
    {
        private struct HouseLocation : IEquatable<HouseLocation>
        {
            public bool Equals(HouseLocation other)
            {
                return X == other.X && Y == other.Y;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }
                return obj is HouseLocation && Equals((HouseLocation)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (X * 397) ^ Y;
                }
            }

            public static bool operator ==(HouseLocation left, HouseLocation right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(HouseLocation left, HouseLocation right)
            {
                return !left.Equals(right);
            }

            public int X { get; }
            public int Y { get; }

            public HouseLocation(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
        public static int NumberOfHousesVisited(string directions)
        {
            var visited = new List<HouseLocation>(1000);
            var current = new HouseLocation(0, 0);
            visited.Add(current);

            foreach (var d in directions)
            {
                current = DecodeDirection(d, current);

                if(!visited.Contains(current))
                {
                    visited.Add(current);
                }
            }

            return visited.Count;

        }

        private static HouseLocation DecodeDirection(char d, HouseLocation current)
        {
            switch (d)
            {
                case '>':
                    return new HouseLocation(current.X + 1, current.Y);
                case 'v':
                    return new HouseLocation(current.X, current.Y - 1);
                case '<':
                    return new HouseLocation(current.X - 1, current.Y);
                case '^':
                    return new HouseLocation(current.X, current.Y + 1);
            }

            throw new InvalidOperationException($"Couldnt decode command '{d}'");
        }

        public static int NumberOfHousesVisitedWithRoboSantaToo(string directions)
        {
            var visited = new List<HouseLocation>(1000);
            visited.Add(new HouseLocation());
            var santa = new HouseLocation();
            var robot = new HouseLocation();
            bool santasTurn = true;

            foreach (var d in directions)
            {
                HouseLocation current;
                if (santasTurn)
                {
                    santa = DecodeDirection(d, santa);
                    current = santa;
                }
                else
                {
                    robot = DecodeDirection(d, robot);
                    current = robot;
                }

                santasTurn = !santasTurn;
                if (!visited.Contains(current))
                    visited.Add(current);


            }

            return visited.Count;
        }
    }
}
