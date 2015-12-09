using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
