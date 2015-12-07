using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Core
{
    public static class InputData
    {
        public static string Load(string filename)
        {
            return File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "InputData", filename));
        }

        public static string[] LoadLines(string filename)
        {
            return File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "InputData", filename));
        }
    }
}
