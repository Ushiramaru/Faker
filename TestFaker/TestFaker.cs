using System;
using NUnit.Framework;
using TestFaker.DTO;
using Fake = Faker.Faker;

namespace TestFaker
{
    public class TestFaker
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

        private DateTime _dateTime;
        private int[] _ints;
        private SimpleEnum _simpleEnum;
        private string _string;
        
        private readonly Fake _fake = new Fake();

        [Test]
        public void RecursionControl()
        {
            var value = _fake.Create<Class5>();
            Assert.NotNull(value);
            Assert.NotNull(value._Class6);
            Assert.NotNull(value._Class6._Class5);
            Assert.NotNull(value._Class6._Class5._Class6);
            Assert.Null(value._Class6._Class5._Class6._Class5);
        }

        [Test]
        public void BoolNotStandardGeneration()
        {
            var value = _fake.Create<bool>();
            Assert.AreNotEqual(_bool, value);
        }
        
        [Test]
        public void ByteNotStandardGeneration()
        {
            var value = _fake.Create<byte>();
            Assert.AreNotEqual(_byte, value);
        }
        
        [Test]
        public void CharNotStandardGeneration()
        {
            var value = _fake.Create<char>();
            Assert.AreNotEqual(_char, value);
        }
        
        [Test]
        public void DoubleNotStandardGeneration()
        {
            var value = _fake.Create<double>();
            Assert.AreNotEqual(_double, value);
        }
        
        [Test]
        public void FloatNotStandardGeneration()
        {
            var value = (float) _fake.Create<float>();
            Assert.AreNotEqual(_float, value);
        }
        
        [Test]
        public void IntNotStandardGeneration()
        {
            var value = (int) _fake.Create<int>();
            Assert.AreNotEqual(_int, value);
        }
        
        [Test]
        public void LongNotStandardGeneration()
        {
            var value = (long) _fake.Create<long>();
            Assert.AreNotEqual(_long, value);
        }
        
        [Test]
        public void SByteNotStandardGeneration()
        {
            var value = (sbyte) _fake.Create<sbyte>();
            Assert.AreNotEqual(_sbyte, value);
        }
        
        [Test]
        public void ShortNotStandardGeneration()
        {
            var value = (short) _fake.Create<short>();
            Assert.AreNotEqual(_short, value);
        }
        
        [Test]
        public void UIntNotStandardGeneration()
        {
            var value = (uint) _fake.Create<uint>();
            Assert.AreNotEqual(_uint, value);
        }
        
        [Test]
        public void ULongNotStandardGeneration()
        {
            var value = (ulong) _fake.Create<ulong>();
            Assert.AreNotEqual(_ulong, value);
        }
        
        [Test]
        public void UShortNotStandardGeneration()
        {
            var value = (ushort) _fake.Create<ushort>();
            Assert.AreNotEqual(_ushort, value);
        }
        
        [Test]
        public void DateTimeNotStandardGeneration()
        {
            var value = (DateTime) _fake.Create<DateTime>();
            Assert.AreNotEqual(_dateTime, value);
        }
        
        [Test]
        public void ArrayNotStandardGeneration()
        {
            var value = (int[]) _fake.Create<int[]>();
            Assert.AreNotEqual(_ints, value);
            foreach (var variable in value)
            {
                Console.WriteLine(variable);
            }
        }
        
        [Test]
        public void EnumNotStandardGeneration()
        {
            var value = (SimpleEnum) _fake.Create<SimpleEnum>();
            Assert.AreNotEqual(_simpleEnum, value);
        }
        
        [Test]
        public void StringNotStandardGeneration()
        {
            var value = (string) _fake.Create<string>();
            Assert.AreNotEqual(_string, value);
        }

        [Test]
        public void GenerationThroughConstructor()
        {
            var o1 = new Class1();
            var value = _fake.Create<Class1>();
            Assert.AreNotEqual(01, value);
        }

        [Test]
        public void GenerationThroughProperties()
        {
            var o1 = new Class2();
            var value = _fake.Create<Class2>();
            Assert.AreNotEqual(01, value);
        }

        [Test]
        public void GenerationThroughFields()
        {
            var o1 = new Class3();
            var value = _fake.Create<Class3>();
            Assert.AreNotEqual(01, value);
        }
        
        [Test]
        public void GenerationDtoWithDto()
        {
            var o1 = new Class1();
            var value = (Class4) _fake.Create<Class4>();
            Assert.AreNotEqual(null, value._Class1);
            Assert.AreNotEqual(01, value._Class1);
        }
    }
}