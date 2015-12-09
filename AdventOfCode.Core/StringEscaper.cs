using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

/*
--- Day 8: Matchsticks ---

Space on the sleigh is limited this year, and so Santa will be bringing his list as a digital copy. He needs to know how much space it will take up when stored.

It is common in many programming languages to provide a way to escape special characters in strings. For example, C, JavaScript, Perl, Python, and even PHP handle special characters in very similar ways.

However, it is important to realize the difference between the number of characters in the code representation of the string literal and the number of characters in the in-memory string itself.

For example:

"" is 2 characters of code (the two double quotes), but the string contains zero characters.
"abc" is 5 characters of code, but 3 characters in the string data.
"aaa\"aaa" is 10 characters of code, but the string itself contains six "a" characters and a single, escaped quote character, for a total of 7 characters in the string data.
"\x27" is 6 characters of code, but the string itself contains just one - an apostrophe ('), escaped using hexadecimal notation.
Santa's list is a file that contains many double-quoted string literals, one on each line. The only escape sequences used are \\ (which represents a single backslash), \" (which represents a lone double-quote character), and \x plus two hexadecimal characters (which represents a single character with that ASCII code).

Disregarding the whitespace in the file, what is the number of characters of code for string literals minus the number of characters in memory for the values of the strings in total for the entire file?

For example, given the four strings above, the total number of characters of string code (2 + 5 + 10 + 6 = 23) minus the total number of characters in memory for string values (0 + 3 + 7 + 1 = 11) is 23 - 11 = 12.

--- Part Two ---

Now, let's go the other way. In addition to finding the number of characters of code, you should now encode each code representation as a new string and find the number of characters of the new encoded representation, including the surrounding double quotes.

For example:

"" encodes to "\"\"", an increase from 2 characters to 6.
"abc" encodes to "\"abc\"", an increase from 5 characters to 9.
"aaa\"aaa" encodes to "\"aaa\\\"aaa\"", an increase from 10 characters to 16.
"\x27" encodes to "\"\\x27\"", an increase from 6 characters to 11.
Your task is to find the total number of characters to represent the newly encoded strings minus the number of characters of code in each original string literal. For example, for the strings above, the total encoded length (6 + 9 + 16 + 11 = 42) minus the characters in the original code representation (23, just like in the first part of this puzzle) is 42 - 23 = 19.
*/
namespace AdventOfCode.Core
{
    public struct UnEscapedStringResult : IEquatable<UnEscapedStringResult>
    {
        public string EscapedString { get; }
        public int CharactersOfCode { get; }

        public int CharactersInEscapedString => EscapedString.Length;

        public UnEscapedStringResult(string escapedString, int charactersOfCode)
        {
            EscapedString = escapedString;
            CharactersOfCode = charactersOfCode;
        }

        public override string ToString()
        {
            return $"Escaped = <{EscapedString}> CodeCount={CharactersOfCode})";
        }

        #region R# equality
        public bool Equals(UnEscapedStringResult other)
        {
            return string.Equals(EscapedString, other.EscapedString) && CharactersOfCode == other.CharactersOfCode;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is UnEscapedStringResult && Equals((UnEscapedStringResult)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EscapedString.GetHashCode() * 397) ^ CharactersOfCode;
            }
        }

        public static bool operator ==(UnEscapedStringResult left, UnEscapedStringResult right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(UnEscapedStringResult left, UnEscapedStringResult right)
        {
            return !left.Equals(right);
        }

        #endregion
    }

    public static class StringEscaper
    {
        private sealed class EscapingStateMachine
        {
            private enum State
            {
                Beginnng,
                Normal,
                InEscape,
                InHex,
                End
            }

            private State _state = State.Beginnng;
            private readonly StringBuilder _buffer = new StringBuilder(25);
            private bool _hasMore = true;

            public UnEscapedStringResult Escape(string input)
            {
                _buffer.Clear();

                using (var enumerator = input.GetEnumerator())
                    ProcessStream(enumerator);

                return new UnEscapedStringResult(_buffer.ToString(), input.Length);
            }

            private void ProcessStream(CharEnumerator enumerator)
            {
                while (_hasMore)
                {
                    switch (_state)
                    {
                        case State.Beginnng:
                            ProcessBeginning(enumerator);
                            break;
                        case State.Normal:
                            ProcessNormal(enumerator);
                            break;
                        case State.InEscape:
                            ProcessEscape(enumerator);
                            break;
                        case State.InHex:
                            ProcessHex(enumerator);
                            break;
                        case State.End:
                            _hasMore = enumerator.MoveNext();
                            break;
                    }
                }
            }

            private void ProcessHex(CharEnumerator enumerator)
            {
                Debug.Assert(_state == State.InHex);
                Debug.Assert(enumerator.Current == 'x');

                _hasMore = enumerator.MoveNext();
                var c1 = enumerator.Current;
                _hasMore = enumerator.MoveNext();
                var c2 = enumerator.Current;

                var hex = $"{c1}{c2}";

                Debug.Assert(Regex.IsMatch(hex, "[a-fA-F0-9]{2}"), $"Not a hex string: {hex}");
                _buffer.Append((char)int.Parse(hex, NumberStyles.HexNumber));

                _state = State.Normal;
            }

            private void ProcessEscape(CharEnumerator enumerator)
            {
                Debug.Assert(_state == State.InEscape);
                _hasMore = enumerator.MoveNext();
                switch (enumerator.Current)
                {
                    case '\\':
                        _buffer.Append('\\');
                        _state = State.Normal;
                        break;
                    case '"':
                        _buffer.Append('"');
                        _state = State.Normal;
                        break;
                    case 'x':
                        _state = State.InHex;
                        break;
                }

            }

            private void ProcessNormal(CharEnumerator enumerator)
            {
                Debug.Assert(_state == State.Normal);
                _hasMore = enumerator.MoveNext();
                switch (enumerator.Current)
                {
                    case '"':
                        _state = State.End;
                        break;
                    case '\\':
                        _state = State.InEscape;
                        break;
                    default:
                        _buffer.Append(enumerator.Current);
                        break;
                }

            }

            private void ProcessBeginning(CharEnumerator enumerator)
            {
                Debug.Assert(_state == State.Beginnng, "not at beginning");
                _hasMore = enumerator.MoveNext();
                Debug.Assert(enumerator.Current == '"');

                _state = State.Normal;
            }
        }

        public static UnEscapedStringResult UnEscape(string input)
        {
            var machine = new EscapingStateMachine();
            return machine.Escape(input);
        }

    }
}
