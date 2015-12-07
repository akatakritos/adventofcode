using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
