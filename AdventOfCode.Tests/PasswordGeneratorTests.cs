using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Core;

using NFluent;

using Xunit;

namespace AdventOfCode.Tests
{
    public class PasswordGeneratorTests
    {
        [Theory]
        [InlineData("hijklmn", false)]
        [InlineData("abbceffg", false)]
        [InlineData("abbcegjk", false)]
        [InlineData("abcdffaa", true)]
        [InlineData("ghjaabcc", true)]
        public void DetectsValidAndInvalidPasswords(string password, bool isValid)
        {
            Check.That(PasswordGenerator.IsValid(password)).IsEqualTo(isValid);
        }

        [Theory]
        [InlineData("abcdefgh", "abcdffaa")]
        [InlineData("ghijklmn", "ghjaabcc")]
        public void NextPasswords(string current, string validNext)
        {
            Check.That(PasswordGenerator.FindNextPassword(current))
                .IsEqualTo(validNext);
        }
    }
}
