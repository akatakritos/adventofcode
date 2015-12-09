using System;
using System.Collections.Generic;
using System.Linq;

/*
--- Day 5: Doesn't He Have Intern-Elves For This? ---

Santa needs help figuring out which strings in his text file are naughty or nice.

A nice string is one with all of the following properties:

It contains at least three vowels (aeiou only), like aei, xazegov, or aeiouaeiouaeiou.
It contains at least one letter that appears twice in a row, like xx, abcdde (dd), or aabbccdd (aa, bb, cc, or dd).
It does not contain the strings ab, cd, pq, or xy, even if they are part of one of the other requirements.
For example:

ugknbfddgicrmopn is nice because it has at least three vowels (u...i...o...), a double letter (...dd...), and none of the disallowed substrings.
aaa is nice because it has at least three vowels and a double letter, even though the letters used by different rules overlap.
jchzalrnumimnmhp is naughty because it has no double letter.
haegwjzuvuyypxyu is naughty because it contains the string xy.
dvszwmarrgswjxmb is naughty because it contains only one vowel.
How many strings are nice?
*/
namespace AdventOfCode.Core
{
    public static class NiceString
    {
        public static bool Test(string input)
        {
            var cleaned = input.ToLowerInvariant();
            return HasDoubleLetter(cleaned)
                && !ContainsNaughtyWord(cleaned)
                && ContainsAtLeastThreeVowels(cleaned);
        }

        public static int TestMultiple(string[] words)
        {
            int niceWords = 0;
            foreach (var word in words)
            {
                if (Test(word))
                    niceWords++;
            }

            return niceWords;
        }

        private static bool ContainsAtLeastThreeVowels(string input)
        {
            var vowelCount = 0;
            foreach (var c in input)
            {
                if (IsVowel(c))
                    vowelCount++;

                if (vowelCount >= 3)
                    return true;
            }

            return false;
        }

        private static bool IsVowel(char c)
        {
            return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';
        }

        private static bool ContainsNaughtyWord(string input)
        {
            var naughtyWords = new[] { "ab", "cd", "pq", "xy" };
            foreach (var word in naughtyWords)
            {
                if (input.Contains(word))
                    return true;
            }

            return false;
        }

        private static bool HasDoubleLetter(string input)
        {
            char lastLetter = input[0];
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == lastLetter)
                    return true;
                lastLetter = input[i];
            }

            return false;
        }
    }
}
