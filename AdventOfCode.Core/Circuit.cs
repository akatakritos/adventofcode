using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

/*
--- Day 7: Some Assembly Required ---

This year, Santa brought little Bobby Tables a set of wires and bitwise logic gates! Unfortunately, little Bobby is a little under the recommended age range, and he needs help assembling the circuit.

Each wire has an identifier (some lowercase letters) and can carry a 16-bit signal (a number from 0 to 65535). A signal is provided to each wire by a gate, another wire, or some specific value. Each wire can only get a signal from one source, but can provide its signal to multiple destinations. A gate provides no signal until all of its inputs have a signal.

The included instructions booklet describes how to connect the parts together: x AND y -> z means to connect wires x and y to an AND gate, and then connect its output to wire z.

For example:

123 -> x means that the signal 123 is provided to wire x.
x AND y -> z means that the bitwise AND of wire x and wire y is provided to wire z.
p LSHIFT 2 -> q means that the value from wire p is left-shifted by 2 and then provided to wire q.
NOT e -> f means that the bitwise complement of the value from wire e is provided to wire f.
Other possible gates include OR (bitwise OR) and RSHIFT (right-shift). If, for some reason, you'd like to emulate the circuit instead, almost all programming languages (for example, C, JavaScript, or Python) provide operators for these gates.

For example, here is a simple circuit:

123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i
After it is run, these are the signals on the wires:

d: 72
e: 507
f: 492
g: 114
h: 65412
i: 65079
x: 123
y: 456
In little Bobby's kit's instructions booklet (provided as your puzzle input), what signal is ultimately provided to wire a?


--- Part Two ---

Now, take the signal you got on wire a, override wire b to that signal, and reset the other wires (including wire a). What new signal is ultimately provided to wire a?

Your puzzle answer was 2797.
*/
namespace AdventOfCode.Core
{
    public class Circuit
    {
        private readonly Dictionary<string, INode> _wires = new Dictionary<string, INode>();

        public void Evaluate(string wire)
        {
            if (ProcessAssignment(wire))
                return;
            if (ProcessUnaryOperation(wire))
                return;
            if (ProcessBinaryOperation(wire))
                return;

            throw new InvalidOperationException("Could not process command " + wire);
        }

        public void EvaluateAll(string[] wires)
        {
            foreach(var wire in wires)
                Evaluate(wire);
        }

        private bool ProcessBinaryOperation(string wire)
        {
            var match = Regex.Match(wire, @"^(?<lhs>[a-z]+|\d+) (?<op>[A-Z]+) (?<rhs>[a-z]+|\d+) -> (?<out>[a-z]+)$");
            if (match.Success)
            {
                var op = match.Groups["op"].Value;
                var lhs = ValueProviderFactory.Create(match.Groups["lhs"].Value);
                var rhs = ValueProviderFactory.Create(match.Groups["rhs"].Value);
                var output = match.Groups["out"].Value;

                switch (op)
                {
                    case "AND":
                        _wires[output] = new AndNode(output, lhs, rhs);
                        break;
                    case "OR":
                        _wires[output] = new OrNode(output, lhs, rhs);
                        break;
                    case "LSHIFT":
                        _wires[output] = new LeftShiftNode(output, lhs, rhs);
                        break;
                    case "RSHIFT":
                        _wires[output] = new RightShiftNode(output, lhs, rhs);
                        break;
                    default:
                        throw new InvalidOperationException("Did not recognize binary operator " + op);
                }

                return true;
            }

            return false;
        }

        private bool ProcessUnaryOperation(string wire)
        {
            var match = Regex.Match(wire, @"^(?<op>NOT) (?<lhs>[a-z]+) -> (?<out>[a-z]+)$");
            if (match.Success)
            {
                var op = match.Groups["op"].Value;
                var input = ValueProviderFactory.Create(match.Groups["lhs"].Value);
                var output = match.Groups["out"].Value;

                switch (op)
                {
                    case "NOT":
                        _wires[output] = new NotNode(output, input);
                        break;
                    default:
                        throw new InvalidOperationException("Unreconized unary operator " + op);
                }

                return true;
            }

            return false;
        }

        private bool ProcessAssignment(string wire)
        {
            var match = Regex.Match(wire, @"^(?<val>[a-z]+|\d+) -> (?<out>[a-z]+)$");
            if (match.Success)
            {
                var value = ValueProviderFactory.Create(match.Groups["val"].Value);
                var output = match.Groups["out"].Value;
                _wires[output] = new ValueNode(output, value);
                return true;
            }

            return false;
        }

        public IReadOnlyCollection<string> GetWireNames()
        {
            return _wires.Keys;
        }

        public ushort? GetWireValue(string name)
        {
            return _wires[name].CalculateValue(this);
        }

        public void Reset()
        {
            foreach(var node in _wires.Values)
                node.Reset();
        }

        public void OverrideInputNode(string node, ushort value)
        {
            _wires[node] = new ValueNode(node, new ConstantProvider(value));
        }
    }
}
