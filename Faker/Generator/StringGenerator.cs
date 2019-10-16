using System;
using System.Text;

namespace Faker.Generator
{
    public class StringGenerator : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate()
        {
            var builder = new StringBuilder();
            var size = Random.Next(byte.MaxValue + 1);
            for (var i = 0; i < size; i++)
            {
                var symbol = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() + 65)));
                builder.Append(symbol);
            }

            return builder.ToString();
        }

        public Type GetGenerationType()
        {
            return typeof(string);
        }
    }
}