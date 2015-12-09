using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using AdventOfCode.Core;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            //SantaElevator.WhereDidHeGo();
            //SantaElevator.WhenEnteredBasement();
            //WrappingPaper.Calculate();

            //var result = Challenge3.NumberOfHousesVisited(InputData.Load("Day3.txt"));
            //Console.WriteLine($"Santa visited {result} houses.");

            //result = Challenge3.NumberOfHousesVisitedWithRoboSantaToo(InputData.Load("Day3.txt"));
            //Console.WriteLine($"Santa and his robot visited {result} houses.");

            //var hashResult = AdventCoins.Mine("yzbqklnj");
            //Console.WriteLine($"Advent Count CalculateValue {hashResult.Input} hashed to {hashResult.Hash}");

            //hashResult = AdventCoins.Mine("yzbqklnj", "000000");
            //Console.WriteLine($"Advent Count CalculateValue {hashResult.Input} hashed to {hashResult.Hash}");

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

            Debug.Assert(a != null, "a != null");
            circuit.OverrideInputNode("b", a.Value);
            circuit.Reset();
            a = circuit.GetWireValue("a");
            Console.WriteLine($"After resetting input b, the new value of a is {a}");


            Console.ReadKey();
        }
    }
}
