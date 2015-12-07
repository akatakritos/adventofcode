using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core
{
    public static class NicerString
    {
        public static bool Test(string input)
        {
            input = input.ToLowerInvariant();

            var result =  ContainsRepeatingPairWithoutOverlapping(input)
                   & ContainsSandwich(input);
            return result;
        }

        public static int TestMultiple(string[] words)
        {
            return words.Count(Test);
        }

        private static bool ContainsSandwich(string input)
        {
            for (int i = 0; i < input.Length - 2; i++)
            {
                if (input[i] == input[i + 2])
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsRepeatingPairWithoutOverlapping(string input)
        {
            var pairs = new List<string>(input.Length);
            string lastPair = "";
            for (int i = 0; i < input.Length - 1; i++)
            {
                var pair = input.Substring(i, 2);
                if (lastPair == pair)
                {
                    //skip ahead and reset pair
                    lastPair = "";
                    continue;
                }

                if (pairs.Contains(pair) && lastPair != pair)
                {
                    return true;
                }

                lastPair = pair;
                pairs.Add(pair);
            }

            return false;
        }
    }
}
