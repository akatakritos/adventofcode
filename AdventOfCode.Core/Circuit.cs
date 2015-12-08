using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Core
{
    public interface INode
    {
        string Name { get; }
        ushort? CalculateValue(Circuit circuit);
    }

    public interface IValueProvider
    {
        ushort? GetValue(Circuit circuit);
    }
    public class ConstantProvider : IValueProvider
    {
        public ushort Value { get; }
        public ConstantProvider(ushort value)
        {
            Value = value;
        }

        public ushort? GetValue(Circuit circuit)
        {
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class NamedNodeProvider : IValueProvider
    {
        public string Name { get; }
        public NamedNodeProvider(string name)
        {
            Name = name;
        }

        public ushort? GetValue(Circuit circuit)
        {
            return circuit.GetWireValue(Name);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public static class ValueProviderFactory
    {
        public static IValueProvider Create(string input)
        {
            ushort value;
            if (ushort.TryParse(input, out value))
                return new ConstantProvider(value);

            return new NamedNodeProvider(input);
        }
    }

    public class LeftShiftNode : BinaryNode
    {
        public LeftShiftNode(string name, IValueProvider left, IValueProvider right)
            : base(name, left, right)
        {
            CommandCode = "LSHIFT";
        }

        protected override string CommandCode { get; }

        public override ushort? CalculateValue(Circuit circuit)
        {
            return (ushort?)(Left.GetValue(circuit) << Right.GetValue(circuit));
        }
    }

    public class RightShiftNode : BinaryNode
    {
        public RightShiftNode(string name, IValueProvider left, IValueProvider right)
            : base(name, left, right)
        {
            CommandCode = "RSHIFT";
        }

        protected override string CommandCode { get; }

        public override ushort? CalculateValue(Circuit circuit)
        {
            return (ushort?)(Left.GetValue(circuit) >> Right.GetValue(circuit));
        }
    }

    public abstract class BinaryNode : INode
    {
        public string Name { get; }
        public IValueProvider Left { get; }
        public IValueProvider Right { get; }

        protected BinaryNode(string name, IValueProvider left, IValueProvider right)
        {
            Name = name;
            Left = left;
            Right = right;
        }

        public abstract ushort? CalculateValue(Circuit circuit);
        protected abstract string CommandCode { get; }
        public override string ToString()
        {
            return $"{Left} {CommandCode} {Right} -> {Name}";
        }
    }

    public class AndNode : BinaryNode
    {
        public AndNode(string name, IValueProvider left, IValueProvider right)
            : base(name, left, right)
        {
            CommandCode = "AND";
        }

        public override ushort? CalculateValue(Circuit circuit)
        {
            var left = Left.GetValue(circuit);
            var right = Right.GetValue(circuit);
            return (ushort?)(left & right);
        }

        protected override string CommandCode { get; }
    }

    public class OrNode : BinaryNode
    {
        public OrNode(string name, IValueProvider left, IValueProvider right)
            : base(name, left, right)
        {
            CommandCode = "OR";
        }

        protected override string CommandCode { get; }

        public override ushort? CalculateValue(Circuit circuit)
        {
            var left = Left.GetValue(circuit);
            var right = Right.GetValue(circuit);
            return (ushort?)(left | right);
        }
    }

    public class NotNode : INode
    {
        public NotNode(string name, IValueProvider input)
        {
            Name = name;
            Input = input;
        }

        public string Name { get; }
        public IValueProvider Input { get; }

        ushort? INode.CalculateValue(Circuit circuit)
        {
            var input = Input.GetValue(circuit);
            return (ushort?)(~ input);
        }

        public override string ToString()
        {
            return $"NOT {Input} -> {Name}";
        }
    }

    public class ValueNode : INode
    {
        public string Name { get; }
        public IValueProvider Value { get; }

        public ValueNode(string name, IValueProvider value)
        {
            Name = name;
            Value = value;
        }

        public ushort? CalculateValue(Circuit circuit)
        {
            return Value.GetValue(circuit);
        }

        public override string ToString()
        {
            return $"{Value} -> {Name}";
        }
    }

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
            Console.WriteLine("GetWireValue: " + name);
            return _wires[name].CalculateValue(this);
        }
    }
}
