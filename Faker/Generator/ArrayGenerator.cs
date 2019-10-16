using System;

namespace Faker.Generator
{
    public class ArrayGenerator<T> : IGenerator
    {
        private const byte MaxElement = 11;
        private static readonly Random Random = new Random();
        public object Generate()
        {
            var elementType = typeof(T).GetElementType();
            if (elementType == null) return null;
            var length = Random.Next(0, MaxElement);
            var result = Array.CreateInstance(elementType, length);
            var faker = new Faker();
            var type = typeof(Faker);
            var methodInfo = type.GetMethod("Create");
            if (methodInfo == null) return null;
            methodInfo = methodInfo.MakeGenericMethod(elementType);
            for (var i = 0; i < length; i++)
            {
                var item = methodInfo.Invoke(faker, null);
                result.SetValue(item, i);
            }

            return result;
        }

        public Type GetGenerationType()
        {
            return typeof(Array);
        }
    }
}