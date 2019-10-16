using System;

namespace Faker.Generator
{
    public class LongGenerator : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate()
        {
            var buffer = new byte[8];
            long result;
            do
            {
                Random.NextBytes(buffer);
                result = BitConverter.ToInt64(buffer, 0);
            } while (result == 0);

            return result;
        }

        public Type GetGenerationType()
        {
            return typeof(long);
        }
    }
}