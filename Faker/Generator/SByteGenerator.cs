using System;

namespace Faker.Generator
{
    public class SByteGenerator : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate()
        {
            sbyte result;
            do
            {
                result = (sbyte) Random.Next(sbyte.MinValue, sbyte.MaxValue + 1);
            } while (result == 0);

            return result;
        }

        public Type GetGenerationType()
        {
            return typeof(sbyte);
        }
    }
}