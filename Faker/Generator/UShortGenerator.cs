using System;

namespace Faker.Generator
{
    public class UShortGenerator : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate()
        {
            return Random.Next(1, ushort.MaxValue + 1);
        }

        public Type GetGenerationType()
        {
            return typeof(ushort);
        }
    }
}