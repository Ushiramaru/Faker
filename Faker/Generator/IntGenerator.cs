using System;

namespace Faker.Generator
{
    public class IntGenerator : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate()
        {
            if (Random.Next() % 2 == 0)
            {
                return Random.Next(int.MaxValue) + 1;
            }
            
            return Random.Next(int.MinValue, 0);
        }

        public Type GetGenerationType()
        {
            return typeof(int);
        }
    }
}