using System;

namespace Faker.Generator
{
    public interface IGenerator
    {
        object Generate();

        Type GetGenerationType();
    }
}