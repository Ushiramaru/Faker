using System;

namespace Faker.Generator
{
    public class DateTimeGenerator : IGenerator
    {
        private static readonly Random Random = new Random();
        public object Generate()
        {
            var buffer = new byte[8];
            long ticks;
            do
            { 
                Random.NextBytes(buffer); 
                ticks = BitConverter.ToInt64(buffer, 0);
            } while (ticks <= DateTime.MinValue.Ticks || ticks >= DateTime.MaxValue.Ticks);

            return new DateTime(ticks);
        }

        public Type GetGenerationType()
        {
            return typeof(DateTime);
        }
    }
}