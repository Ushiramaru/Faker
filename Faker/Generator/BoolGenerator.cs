﻿using System;

namespace Faker.Generator
{
    public class BoolGenerator : IGenerator
    {
        public object Generate()
        {
            return true;
        }

        public Type GetGenerationType()
        {
            return typeof(bool);
        }
    }
}