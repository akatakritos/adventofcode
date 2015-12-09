using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

/*
--- Day 4: The Ideal Stocking Stuffer ---

Santa needs help mining some AdventCoins (very similar to bitcoins) to use as gifts for all the economically forward-thinking little girls and boys.

To do this, he needs to find MD5 hashes which, in hexadecimal, start with at least five zeroes. The input to the MD5 hash is some secret key (your puzzle input, given below) followed by a number in decimal. To mine AdventCoins, you must find Santa the lowest positive number (no leading zeroes: 1, 2, 3, ...) that produces such a hash.

For example:

If your secret key is abcdef, the answer is 609043, because the MD5 hash of abcdef609043 starts with five zeroes (000001dbbfa...), and it is the lowest such number to do so.
If your secret key is pqrstuv, the lowest number it combines with to make an MD5 hash starting with five zeroes is 1048970; that is, the MD5 hash of pqrstuv1048970 looks like 000006136ef....

--- Part Two ---

Now find one that starts with six zeroes.
*/
namespace AdventOfCode.Core
{
    public class AdventCoins
    {
        public struct HashResult
        {
            public HashResult(int input, string hash)
            {
                Input = input;
                Hash = hash;
            }

            public string Hash { get; }
            public int Input { get; }
        }

        private static readonly MD5 hasher;

        static AdventCoins()
        {
            hasher = MD5.Create();
        }

        public static HashResult Mine(string key, string prefix = "00000")
        {
            int i = 0;
            string hash;
            do
            {
                i++;
                hash = Hash(key + i);
            } while (!hash.StartsWith(prefix, StringComparison.InvariantCulture));

            return new HashResult(i, hash);
        }

        private static string Hash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = hasher.ComputeHash(bytes);
            var sb = new StringBuilder(32);

            foreach (byte b in hash)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
    }
}
