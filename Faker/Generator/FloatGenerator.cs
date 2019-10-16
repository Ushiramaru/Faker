using System;

namespace Faker.Generator
{
    public class FloatGenerator : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate()
        {
            var buffer = new byte[4];
            float result;
            do
            {
                Random.NextBytes(buffer);
                result = BitConverter.ToSingle(buffer, 0);
            } while (result == 0);

            return result;
        }

        public Type GetGenerationType()
        {
            return typeof(float);
        }
    }
}