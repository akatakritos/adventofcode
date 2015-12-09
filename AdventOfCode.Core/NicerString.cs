using System;
using System.Collections.Generic;
using System.Linq;

/*

--- Day 5: Doesn't He Have Intern-Elves For This? ---

--- Part Two ---

Realizing the error of his ways, Santa has switched to a better model of determining whether a string is naughty or nice. None of the old rules apply, as they are all clearly ridiculous.

Now, a nice string is one with all of the following properties:

It contains a pair of any two letters that appears at least twice in the string without overlapping, like xyxy (xy) or aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
It contains at least one letter which repeats with exactly one letter between them, like xyx, abcdefeghi (efe), or even aaa.
For example:

qjhvhtzxzqqjkmpb is nice because is has a pair that appears twice (qj) and a letter that repeats with exactly one letter between them (zxz).
xxyxx is nice because it has a pair that appears twice and a letter that repeats with one between, even though the letters used by each rule overlap.
uurcxstgmygtbstg is naughty because it has a pair (tg) but no repeat with a single letter between them.
ieodomkazucvgmuy is naughty because it has a repeating letter with one between (odo), but no pair that appears twice.
How many strings are nice under these new rules?
*/
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
