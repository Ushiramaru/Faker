using System;

namespace Faker.Generator
{
    public class CharGenerator : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate()
        {
            return Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() + 65)));
        }

        public Type GetGenerationType()
        {
            return typeof(char);
        }
    }
}