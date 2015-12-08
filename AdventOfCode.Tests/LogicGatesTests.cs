using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AdventOfCode.Core;

using NFluent;

using Xunit;

namespace AdventOfCode.Tests
{
    public class LogicGatesTests
    {
        [Fact]
        public void Example()
        {
            var circuit = new Circuit();
            circuit.Evaluate("123 -> x");
            circuit.Evaluate("456 -> y");
            circuit.Evaluate("x AND y -> d");
            circuit.Evaluate("x OR y -> e");
            circuit.Evaluate("x LSHIFT 2 -> f");
            circuit.Evaluate("y RSHIFT 2 -> g");
            circuit.Evaluate("NOT x -> h");
            circuit.Evaluate("NOT y -> i");

            Check.That(circuit.GetWireValue("d")).IsEqualTo((ushort)72);
            Check.That(circuit.GetWireValue("e")).IsEqualTo((ushort)507);
            Check.That(circuit.GetWireValue("f")).IsEqualTo((ushort)492);
            Check.That(circuit.GetWireValue("g")).IsEqualTo((ushort)114);
            Check.That(circuit.GetWireValue("h")).IsEqualTo((ushort)65412);
            Check.That(circuit.GetWireValue("i")).IsEqualTo((ushort)65079);
            Check.That(circuit.GetWireValue("x")).IsEqualTo((ushort)123);
            Check.That(circuit.GetWireValue("y")).IsEqualTo((ushort)456);
        }

        [Fact]
        public void Assignment()
        {
            var circuit = new Circuit();
            circuit.Evaluate("123 -> abc");
            Check.That(circuit.GetWireValue("abc")).IsEqualTo((ushort)123);
        }

        [Fact]
        public void UnaryNot()
        {
            var circuit = new Circuit();
            circuit.Evaluate("123 -> x");
            circuit.Evaluate("NOT x -> h");

            Check.That(circuit.GetWireValue("h")).IsEqualTo((ushort)65412);
        }

        [Fact]
        public void BinaryAND()
        {
            var circuit = new Circuit();
            circuit.Evaluate("123 -> x");
            circuit.Evaluate("456 -> y");
            circuit.Evaluate("x AND y -> d");

            Check.That(circuit.GetWireValue("d")).IsEqualTo((ushort)72);
        }
    }
}
