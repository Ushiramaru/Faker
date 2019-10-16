using System;

namespace TestFaker.DTO
{
    public class Class1
    {
        private bool _bool;
        private byte _byte;
        private char _char;
        private double _double;
        private float _float;
        private int _int;
        private long _long;
        private sbyte _sbyte;
        private short _short;
        private uint _uint;
        private ulong _ulong;
        private ushort _ushort;

        public Class1()
        {
        }

        public Class1(bool _b, byte b, char c, double d, float f, int i, long l, sbyte @sbyte, short s, uint u, ulong @ulong, ushort @ushort)
        {
            _bool = _b;
            _byte = b;
            _char = c;
            _double = d;
            _float = f;
            _int = i;
            _long = l;
            _sbyte = @sbyte;
            _short = s;
            _uint = u;
            _ulong = @ulong;
            _ushort = @ushort;
        }

        protected bool Equals(object other)
        {
            if (this == other) return true;
            if (other == null) return false;
            if (GetType() != other.GetType()) return false;
            var class1 = (Class1) other;
            return _bool == class1._bool && _byte == class1._byte && _char == class1._char && _double.Equals(class1._double)
                   && _float.Equals(class1._float) && _int == class1._int && _long == class1._long && _sbyte == class1._sbyte
                   && _short == class1._short && _uint == class1._uint && _ulong == class1._ulong && _ushort == class1._ushort;
        }
    }
}