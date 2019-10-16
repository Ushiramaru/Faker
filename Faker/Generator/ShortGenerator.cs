using System;

namespace Faker.Generator
{
    public class ShortGenerator : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate()
        {
            short result;
            do
            {
                result = (short) Random.Next(short.MinValue, short.MaxValue + 1);
            } while (result == 0);

            return result;
        }

        public Type GetGenerationType()
        {
            return typeof(short);
        }
    }
}