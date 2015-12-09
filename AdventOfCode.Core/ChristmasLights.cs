using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

/*
--- Day 6: Probably a Fire Hazard ---

Because your neighbors keep defeating you in the holiday house decorating contest year after year, you've decided to deploy one million lights in a 1000x1000 grid.

Furthermore, because you've been especially nice this year, Santa has mailed you instructions on how to display the ideal lighting configuration.

Lights in your grid are numbered from 0 to 999 in each direction; the lights at each corner are at 0,0, 0,999, 999,999, and 999,0. The instructions include whether to turn on, turn off, or toggle various inclusive ranges given as coordinate pairs. Each coordinate pair represents opposite corners of a rectangle, inclusive; a coordinate pair like 0,0 through 2,2 therefore refers to 9 lights in a 3x3 square. The lights all start turned off.

To defeat your neighbors this year, all you have to do is set up your lights by doing the instructions Santa sent you in order.

For example:

turn on 0,0 through 999,999 would turn on (or leave on) every light.
toggle 0,0 through 999,0 would toggle the first line of 1000 lights, turning off the ones that were on, and turning on the ones that were off.
turn off 499,499 through 500,500 would turn off (or leave off) the middle four lights.
After following the instructions, how many lights are lit?


--- Part Two ---

You just finish implementing your winning light pattern when you realize you mistranslated Santa's message from Ancient Nordic Elvish.

The light grid you bought actually has individual brightness controls; each light can have a brightness of zero or more. The lights all start at zero.

The phrase turn on actually means that you should increase the brightness of those lights by 1.

The phrase turn off actually means that you should decrease the brightness of those lights by 1, to a minimum of zero.

The phrase toggle actually means that you should increase the brightness of those lights by 2.

What is the total brightness of all lights combined after following Santa's instructions?

For example:

turn on 0,0 through 0,0 would increase the total brightness by 1.
toggle 0,0 through 999,999 would increase the total brightness by 2000000.
*/
namespace AdventOfCode.Core
{
    public struct LightLocation : IEquatable<LightLocation>
    {
        public int X { get; }
        public int Y { get; }
        public LightLocation(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        #region R# equality members
        public bool Equals(LightLocation other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is LightLocation && Equals((LightLocation)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static bool operator ==(LightLocation left, LightLocation right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LightLocation left, LightLocation right)
        {
            return !left.Equals(right);
        }

        #endregion
    }

    public struct LightBlock : IEquatable<LightBlock>
    {
        public LightLocation Corner1 { get; }
        public LightLocation Corner2 { get; }

        public LightBlock(LightLocation corner1, LightLocation corner2)
        {
            Corner1 = corner1;
            Corner2 = corner2;
        }

        public IEnumerable<LightLocation> GetLocations()
        {
            for (int x = Corner1.X; x <= Corner2.X; x++)
            {
                for (int y = Corner1.Y; y <= Corner2.Y; y++)
                {
                    yield return new LightLocation(x, y);
                }
            }
        }

        #region R# equality members
        public bool Equals(LightBlock other)
        {
            return Corner1.Equals(other.Corner1) && Corner2.Equals(other.Corner2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is LightBlock && Equals((LightBlock)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Corner1.GetHashCode() * 397) ^ Corner2.GetHashCode();
            }
        }

        public static bool operator ==(LightBlock left, LightBlock right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LightBlock left, LightBlock right)
        {
            return !left.Equals(right);
        }
        #endregion
    }

    public enum LightCommandType
    {
        TurnOn,
        TurnOff,
        Toggle
    }

    public struct LightCommand : IEquatable<LightCommand>
    {
        public LightCommandType Type { get; }
        public LightBlock Block { get; }

        public LightCommand(LightCommandType type, LightBlock block)
        {
            Type = type;
            Block = block;
        }

        public LightCommand(LightCommandType type, int x1, int y1, int x2, int y2)
        {
            Type = type;
            Block = new LightBlock(
                new LightLocation(x1, y1),
                new LightLocation(x2, y2));
        }

        public static LightCommand Parse(string command)
        {
            var match = Regex.Match(command, @"(turn on|toggle|turn off) (\d+),(\d+) through (\d+),(\d+)");
            if (!match.Success)
                throw new FormatException("command not in expected format");

            var cmd = match.Groups[1].Value;
            var x1 = int.Parse(match.Groups[2].Value);
            var y1 = int.Parse(match.Groups[3].Value);
            var x2 = int.Parse(match.Groups[4].Value);
            var y2 = int.Parse(match.Groups[5].Value);

            var block = new LightBlock(new LightLocation(x1, y1), new LightLocation(x2, y2));
            switch (cmd)
            {
                case "turn on": return new LightCommand(LightCommandType.TurnOn, block);
                case "turn off": return new LightCommand(LightCommandType.TurnOff, block);
                case "toggle": return new LightCommand(LightCommandType.Toggle, block);
            }

            throw new FormatException("command not in expected format");
        }

        public override string ToString()
        {
            return $"{Type} {Block.Corner1} to {Block.Corner2}";
        }

        #region R# equality members
        public bool Equals(LightCommand other)
        {
            return Type == other.Type && Block.Equals(other.Block);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is LightCommand && Equals((LightCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Type * 397) ^ Block.GetHashCode();
            }
        }

        public static bool operator ==(LightCommand left, LightCommand right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LightCommand left, LightCommand right)
        {
            return !left.Equals(right);
        }
        #endregion
    }

    public class ChristmasLights
    {
        public static int CountActiveLights(string[] commands)
        {
            var lights = new Dictionary<LightLocation, bool>(10000);

            foreach (var cmd in commands.Select(LightCommand.Parse))
            {
                foreach (var location in cmd.Block.GetLocations())
                {
                    bool on;
                    if (!lights.TryGetValue(location, out on))
                        on = false;

                    switch (cmd.Type)
                    {
                        case LightCommandType.Toggle:
                            on = !on;
                            break;
                        case LightCommandType.TurnOff:
                            on = false;
                            break;
                        case LightCommandType.TurnOn:
                            on = true;
                            break;
                    }

                    lights[location] = on;
                }
            }

            return lights.Values.Count(b => b);
        }

        public static int GetTotalBrightnessFromAncientNordicElvish(string[] commands)
        {
            var lights = new Dictionary<LightLocation, int>(10000);

            foreach (var cmd in commands.Select(LightCommand.Parse))
            {
                foreach (var location in cmd.Block.GetLocations())
                {
                    int brightness;
                    if (!lights.TryGetValue(location, out brightness))
                        brightness = 0;

                    switch (cmd.Type)
                    {
                        case LightCommandType.Toggle:
                            brightness += 2;
                            break;
                        case LightCommandType.TurnOff:
                            brightness = Math.Max(brightness - 1, 0);
                            break;
                        case LightCommandType.TurnOn:
                            brightness += 1;
                            break;
                    }

                    lights[location] = brightness;
                }
            }

            return lights.Values.Sum();
        }
    }
}
