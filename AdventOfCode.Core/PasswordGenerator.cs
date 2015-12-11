using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Core
{
    public static class PasswordGenerator
    {
        public static bool IsValid(string password)
        {
            var sb = new StringBuilder(password);
            return !ContainsBadLetter(sb)
                   && ContainsTwoPairs(sb)
                   && ContainsStraight(sb);
        }

        private static bool ContainsBadLetter(StringBuilder password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                var c = password[i];
                if (c == 'i' || c == 'o' || c == 'l')
                    return true;
            }

            return false;

        }

        private static bool ContainsTwoPairs(StringBuilder password)
        {
            var pairCount = 0;
            var previousPair = (char)0;

            for (int i = 0; i < password.Length - 1; i++)
            {
                if (password[i] == password[i + 1] && previousPair != password[i])
                {
                    previousPair = password[i];
                    pairCount++;
                }

                if (pairCount == 2)
                    return true;
            }

            return false;
        }

        private static bool ContainsStraight(StringBuilder password)
        {
            for (int i = 0; i < password.Length - 2; i++)
            {
                if (password[i] == (char)(password[i + 1] - 1) &&
                    password[i] == (char)(password[i + 2] - 2))
                    return true;
            }

            return false;
        }

        public static string FindNextPassword(string current)
        {
            var sb = new StringBuilder(current);
            do
            {
                Increment(sb);

            } while (!IsValid(sb.ToString()));

            return sb.ToString();
        }

        private static void Increment(StringBuilder sb)
        {
            for (int i = sb.Length - 1; i >= 0; i--)
            {
                var c = sb[i];
                if (CanIncrement(c))
                {
                    sb[i] = (char)(c + 1);
                    return;
                }
                else
                {
                    sb[i] = 'a';
                }
            }

        }

        private static bool CanIncrement(char c)
        {
            return c != 'z';
        }
    }
}
