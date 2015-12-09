using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core
{
    public interface INode
    {
        string Name { get; }
        ushort? CalculateValue(Circuit circuit);
        void Reset();
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

        public void Reset()
        {
            Left.Reset();
            Right.Reset();
        }

        protected abstract string CommandCode { get; }
        public override string ToString()
        {
            return $"{Left} {CommandCode} {Right} -> {Name}";
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

    public abstract class UnaryNode : INode
    {
        protected UnaryNode(string name, IValueProvider input)
        {
            Name = name;
            Input = input;
        }

        public string Name { get; }
        public IValueProvider Input { get; }

        public abstract ushort? CalculateValue(Circuit circuit);

        public void Reset()
        {
            Input.Reset();
        }
    }

    public class NotNode : UnaryNode
    {
        public NotNode(string name, IValueProvider input)
            :base (name, input)
        {
        }
        public override ushort? CalculateValue(Circuit circuit)
        {
            var input = Input.GetValue(circuit);
            return (ushort?)(~input);
        }

        public override string ToString()
        {
            return $"NOT {Input} -> {Name}";
        }
    }

    public class ValueNode : UnaryNode
    {
        public ValueNode(string name, IValueProvider input)
            : base(name, input)
        {
        }

        public override ushort? CalculateValue(Circuit circuit)
        {
            return Input.GetValue(circuit);
        }

        public override string ToString()
        {
            return $"{Input} -> {Name}";
        }
    }
}
