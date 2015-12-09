using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core
{
    public interface IValueProvider
    {
        ushort? GetValue(Circuit circuit);
        void Reset();
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

        public void Reset()
        {
            // no op
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

        private ushort? _value;

        public ushort? GetValue(Circuit circuit)
        {
            if (_value.HasValue)
                return _value;

            return _value = circuit.GetWireValue(Name);
        }

        public void Reset()
        {
            _value = null;
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
}
