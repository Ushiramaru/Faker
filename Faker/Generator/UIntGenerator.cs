using System;

namespace Faker.Generator
{
    public class UIntGenerator : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate()
        {
            var buffer = new byte[4];
            uint result;
            do
            {
                Random.NextBytes(buffer);
                result = BitConverter.ToUInt32(buffer, 0);
            } while (result == 0);

            return result;
        }

        public Type GetGenerationType()
        {
            return typeof(uint);
        }
    }
}