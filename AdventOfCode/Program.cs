using System;
using System.Collections.Generic;
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

            //var circuit = new Circuit();
            //circuit.EvaluateAll(InputData.LoadLines("Wires.txt"));
            //var a = circuit.GetWireValue("a");
            //Console.WriteLine($"The wire 'a' has value {a}");

            //Debug.Assert(a != null, "a != null");
            //circuit.OverrideInputNode("b", a.Value);
            //circuit.Reset();
            //a = circuit.GetWireValue("a");
            //Console.WriteLine($"After resetting input b, the new value of a is {a}");

            //int total = InputData.LoadLines("escapedstrings.txt")
            //    .Select(StringEscaper.UnEscape)
            //    .Sum(r => r.CharactersOfCode - r.CharactersInEscapedString);
            //Console.WriteLine($"There are {total} differences between number of characters of code and number of characters in escaped strings.");

            //var total2 = InputData.LoadLines("escapedstrings.txt")
            //    .Select(s => StringEscaper.Escape(s).Length - s.Length)
            //    .Sum();
            //Console.WriteLine($"That weird sum of escaped differences is {total2} characters.");

            //var navigator = new TravelingSanta();
            //navigator.AddDistances(InputData.LoadLines("citydistances.txt"));

            //var route = navigator.FindShortestRoute();
            //Console.WriteLine($"Santas best route is {route} for {route.TotalDistance} miles.");

            //route = navigator.FindLongestRoute();
            //Console.WriteLine($"Santas worst route is {route} for {route.TotalDistance} miles.");

            //var result = LookAndSay.Transform("1321131112", 40);
            //Console.WriteLine($"1321131112 transformed 40 times has a length of {result.Length}");
            //result = LookAndSay.Transform("1321131112", 50);
            //Console.WriteLine($"1321131112 transformed 50 times has a length of {result.Length}");


            //var next = PasswordGenerator.FindNextPassword("vzbxkghb");
            //Console.WriteLine(next);
            //next = PasswordGenerator.FindNextPassword(next);
            //Console.WriteLine(next);

            //var sum = ElfAccounting.SumAllNumbers(InputData.Load("elf-accounting.json"));
            //Console.WriteLine(sum);

            //sum = ElfAccounting.SumAllNonRedObjects(InputData.Load("elf-accounting.json"));
            //Console.WriteLine(sum);

            var table = new DinnerTable();
            table.AddGuestRequirements(SeatingRequirement.ParseMultiple(InputData.LoadLines("dinnertable.txt")));
            var result = table.DetermineOptimalArrangement();
            var total = result.GetTotalHappiness();
            Console.WriteLine(total);

            table.AddSelf();
            var result1 = table.DetermineOptimalArrangement();
            var total1 = result1.GetTotalHappiness();
            Console.WriteLine($"New total is {total1} which is {total1 - total} different.");

            Console.ReadKey();
        }
    }
}
