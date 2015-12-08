using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Core;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Day1Part1();
            Day1Part2();
            Day2();

            //var result = Challenge3.NumberOfHousesVisited(InputData.Load("Day3.txt"));
            //Console.WriteLine($"Santa visited {result} houses.");

            //result = Challenge3.NumberOfHousesVisitedWithRoboSantaToo(InputData.Load("Day3.txt"));
            //Console.WriteLine($"Santa and his robot visited {result} houses.");

            //var hashResult = AdventCoins.Mine("yzbqklnj");
            //Console.WriteLine($"Advent Count Value {hashResult.Input} hashed to {hashResult.Hash}");

            //hashResult = AdventCoins.Mine("yzbqklnj", "000000");
            //Console.WriteLine($"Advent Count Value {hashResult.Input} hashed to {hashResult.Hash}");

            //var niceWords = NiceString.TestMultiple(InputData.LoadLines("NaughtyWords.txt"));
            //Console.WriteLine($"There are {niceWords} nice words in the list.");

            //var nicerWords = NicerString.TestMultiple(InputData.LoadLines("NaughtyWords.txt"));
            //Console.WriteLine($"There are {nicerWords} nicer words in the list.");

            //var lights = ChristmasLights.CountActiveLights(InputData.LoadLines("LightCommands.txt"));
            //Console.WriteLine($"There are {lights} lights turned on.");

            //var brightness = ChristmasLights.GetTotalBrightnessFromAncientNordicElvish(InputData.LoadLines("LightCommands.txt"));
            //Console.WriteLine($"After translating to nordic elvish, the total brightness is {brightness}");

            var circuit = new Circuit();
            circuit.EvaluateAll(InputData.LoadLines("Wires.txt"));
            var a = circuit.GetWireValue("a");
            Console.WriteLine($"The wire 'a' has value {a}");

            Console.ReadKey();
        }

        public static void Day1Part1()
        {
            var s = InputData.Load("Day1Part1.txt");
            int up = s.Count(c => c == '(');
            int down = s.Count(c => c == ')');
            Console.WriteLine(up - down);
        }

        public static void Day1Part2()
        {
            var s = InputData.Load("Day1Part2.txt");
            int position = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                    position++;
                else if (s[i] == ')')
                    position--;

                if (position == -1)
                {
                    Console.WriteLine($"Entered basement at step {i+1}");
                    return;
                }
            }
        }

        public static void Day2()
        {
            var input = InputData.LoadLines("Day2Part1.txt");
            //var input = new[] { "1x1x10" };
            int totalRibbon = 0;
            int totalPaper = 0;
            foreach (var line in input)
            {
                var sizes = line.Split('x').Select(int.Parse).ToArray();
                int l = sizes[0];
                int w = sizes[1];
                int h = sizes[2];

                //smallest side:
                Array.Sort(sizes);
                int s1 = sizes[0];
                int s2 = sizes[1];

                int surfaceArea = 2 * l * w + 2 * w * h + 2 * h * l;
                int areaSmallestSide = s1 * s2;
                int shortestPermiter = 2 * s1 + 2 * s2;
                int volume = l * w * h;

                int paper = surfaceArea + areaSmallestSide;
                int ribbon = shortestPermiter + volume;


                totalPaper += paper;
                totalRibbon += ribbon;
            }

            Console.WriteLine($"The elves should buy {totalPaper} feet of paper and {totalRibbon} ribbon.");
        }
    }
}
