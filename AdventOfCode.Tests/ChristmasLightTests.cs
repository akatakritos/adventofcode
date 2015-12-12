using System;
using System.Collections.Generic;
using System.Linq;

using AdventOfCode.Core;

using NFluent;

using Xunit;

namespace AdventOfCode.Tests
{
    public class LightCommandTests
    {
        [Fact]
        public void ParseCommandOne()
        {
            var command = LightCommand.Parse("turn on 0,0 through 999,999");

            Check.That(command)
                .IsEqualTo(new LightCommand(LightCommandType.TurnOn, 0, 0, 999, 999));
        }

        [Fact]
        public void ParseAnotherCommand()
        {
            var command = LightCommand.Parse("toggle 0,0 through 999,0");
            Check.That(command)
                .IsEqualTo(new LightCommand(LightCommandType.Toggle, 0, 0, 999, 0));
        }

        [Fact]
        public void ParseYetAnotherCommand()
        {
            var command = LightCommand.Parse("turn off 499,499 through 500,500");
            Check.That(command)
                .IsEqualTo(new LightCommand(LightCommandType.TurnOff, 499, 499, 500, 500));

        }
    }
}
