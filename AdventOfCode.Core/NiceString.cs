using System;
using System.Collections.Generic;
using System.Linq;

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
