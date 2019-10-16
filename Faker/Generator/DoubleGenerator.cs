using System;

namespace Faker.Generator
{
    public class DoubleGenerator : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate()
        {
            var buffer = new byte[8];
            double result;
            do
            {
                Random.NextBytes(buffer);
                result = BitConverter.ToDouble(buffer, 0);
            } while (result == 0);

            return result;
        }

        public Type GetGenerationType()
        {
            return typeof(double);
        }
    }
}