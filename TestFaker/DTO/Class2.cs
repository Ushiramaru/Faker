namespace TestFaker.DTO
{
    public class Class2
    {
        public bool Bool { get; }
        public byte Byte { get; }
        public char Char { get; }
        public double Double { get; }
        public float Float { get; }
        public int Int { get; }
        public long Long { get; }
        public sbyte Sbyte { get; }
        public short Short { get; }
        public uint UInt { get; }
        public ulong Ulong { get; }
        public ushort Ushort { get; }

        protected bool Equals(object other)
        {
            if (this == other) return true;
            if (other == null) return false;
            if (GetType() != other.GetType()) return false;
            var class2 = (Class2) other;
            return Bool == class2.Bool && Byte == class2.Byte && Char == class2.Char && Double.Equals(class2.Double) && 
                   Float.Equals(class2.Float) && Int == class2.Int && Long == class2.Long && Sbyte == class2.Sbyte && 
                   Short == class2.Short && UInt == class2.UInt && Ulong == class2.Ulong && Ushort == class2.Ushort;
        }
    }
}