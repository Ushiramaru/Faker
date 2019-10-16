using System;

namespace Faker.Generator
{
    public class ByteGenerator : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate()
        {
            var result = new byte[1];
            Random.NextBytes(result);
            return result[0];
        }

        public Type GetGenerationType()
        {
            return typeof(byte);
        }
    }
}