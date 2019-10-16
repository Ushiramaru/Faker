using System;

namespace Faker.Generator
{
    public class EnumGenerator<T> : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate()
        {
            var type = typeof(T);
            var values = Enum.GetValues(type);
            return values.GetValue(Random.Next(1, values.Length));
        }

        public Type GetGenerationType()
        {
            return typeof(T);
        }
    }
}