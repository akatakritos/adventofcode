using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace AdventOfCode.Core
{
    public class Circuit
    {
        private readonly Dictionary<string, ushort> _wires = new Dictionary<string, ushort>();

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
            var match = Regex.Match(wire, @"^(?<lhs>[a-z]+) (?<op>[A-Z]+) (?<rhs>[a-z]+|\d+) -> (?<out>[a-z]+)$");
            if (match.Success)
            {
                var op = match.Groups["op"].Value;
                var lhs = match.Groups["lhs"].Value;
                var rhs = match.Groups["rhs"].Value;
                var output = match.Groups["out"].Value;

                var input = _wires[lhs];
                switch (op)
                {
                    case "AND":
                        _wires[output] = (ushort)(input & _wires[rhs]);
                        break;
                    case "OR":
                        _wires[output] = (ushort)(input | _wires[rhs]);
                        break;
                    case "LSHIFT":
                        _wires[output] = (ushort)(input << ushort.Parse(rhs));
                        break;
                    case "RSHIFT":
                        _wires[output] = (ushort)(input >> ushort.Parse(rhs));
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
            var match = Regex.Match(wire, @"^(NOT) ([a-z]+) -> ([a-z]+)$");
            if (match.Success)
            {
                var op = match.Groups[1].Value;
                var input = match.Groups[2].Value;
                var output = match.Groups[3].Value;

                switch (op)
                {
                    case "NOT":
                        _wires[output] =(ushort)(~_wires[input]);
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
            var match = Regex.Match(wire, @"^(\d+) -> ([a-z]+)$");
            if (match.Success)
            {
                _wires[match.Groups[2].Value] = ushort.Parse(match.Groups[1].Value);
                return true;
            }

            return false;
        }

        public IReadOnlyCollection<string> GetWireNames()
        {
            return _wires.Keys;
        }

        public ushort GetWireValue(string name)
        {
            return _wires[name];
        }
    }
}
