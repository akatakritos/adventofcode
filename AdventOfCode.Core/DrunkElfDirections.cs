using System;
using System.Collections.Generic;
using System.Linq;

/*
--- Day 3: Perfectly Spherical Houses in a Vacuum ---

Santa is delivering presents to an infinite two-dimensional grid of houses.

He begins by delivering a present to the house at his starting location, and then an elf at the North Pole calls him via radio and tells him where to move next. Moves are always exactly one house to the north (^), south (v), east (>), or west (<). After each move, he delivers another present to the house at his new location.

However, the elf back at the north pole has had a little too much eggnog, and so his directions are a little off, and Santa ends up visiting some houses more than once. How many houses receive at least one present?

For example:

> delivers presents to 2 houses: one at the starting location, and one to the east.
^>v< delivers presents to 4 houses in a square, including twice to the house at his starting/ending location.
^v^v^v^v^v delivers a bunch of presents to some very lucky children at only 2 houses.

--- Part Two ---

The next year, to speed up the process, Santa creates a robot version of himself, Robo-Santa, to deliver presents with him.

Santa and Robo-Santa start at the same location (delivering two presents to the same starting house), then take turns moving based on instructions from the elf, who is eggnoggedly reading from the same script as the previous year.

This year, how many houses receive at least one present?

For example:

^v delivers presents to 3 houses, because Santa goes north, and then Robo-Santa goes south.
^>v< now delivers presents to 3 houses, and Santa and Robo-Santa end up back where they started.
^v^v^v^v^v now delivers presents to 11 houses, with Santa going one direction and Robo-Santa going the other.
*/
namespace AdventOfCode.Core
{
    public static class DrunkElfDirections
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
