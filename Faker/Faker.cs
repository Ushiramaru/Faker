﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Faker.Generator;

namespace Faker
{
    public class Faker
    {
        private readonly Dictionary<Type, IGenerator> _generators;
        private readonly string _pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");

        public Faker()
        {
            _generators = new Dictionary<Type, IGenerator>();
//            InitDictionary();
            LoadGeneratorsFromDirectory(); 
        }

        private void InitDictionary()
        {
            IGenerator iGenerator = new BoolGenerator();
            _generators.Add(iGenerator.GetGenerationType(), iGenerator);
            iGenerator = new ByteGenerator();
            _generators.Add(iGenerator.GetGenerationType(), iGenerator);
            iGenerator = new CharGenerator();
            _generators.Add(iGenerator.GetGenerationType(), iGenerator);
            iGenerator = new DateTimeGenerator();
            _generators.Add(iGenerator.GetGenerationType(), iGenerator);
            iGenerator = new DoubleGenerator();
            _generators.Add(iGenerator.GetGenerationType(), iGenerator);
            iGenerator = new FloatGenerator();
            _generators.Add(iGenerator.GetGenerationType(), iGenerator);
            iGenerator = new IntGenerator();
            _generators.Add(iGenerator.GetGenerationType(), iGenerator);
            iGenerator = new LongGenerator();
            _generators.Add(iGenerator.GetGenerationType(), iGenerator);
            iGenerator = new SByteGenerator();
            _generators.Add(iGenerator.GetGenerationType(), iGenerator);
            iGenerator = new ShortGenerator();
            _generators.Add(iGenerator.GetGenerationType(), iGenerator);
            iGenerator = new StringGenerator();
            _generators.Add(iGenerator.GetGenerationType(), iGenerator);
            iGenerator = new UIntGenerator();
            _generators.Add(iGenerator.GetGenerationType(), iGenerator);
            iGenerator = new ULongGenerator();
            _generators.Add(iGenerator.GetGenerationType(), iGenerator);
            iGenerator = new UShortGenerator();
            _generators.Add(iGenerator.GetGenerationType(), iGenerator);
        }
        
        private void LoadGeneratorsFromDirectory()
        {
            if (_generators == null) return;
            
            var pluginDirectory = new DirectoryInfo(_pluginPath);
            if (!pluginDirectory.Exists)
            {
                pluginDirectory.Create();
                return;
            }

            var pluginFiles = Directory.GetFiles(pluginDirectory.FullName,"*.dll");

            foreach (var pluginFile in pluginFiles)
            {
                var assembly = Assembly.LoadFrom(pluginFile);
                LoadGeneratorsFromAssembly(assembly);
            }
        }

        private void LoadGeneratorsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(type => typeof(IGenerator).IsAssignableFrom(type));
            foreach (var type in types)
            {
                if (type.FullName == null) continue;
                if (!type.IsClass) continue;
                if (assembly.CreateInstance(type.FullName) is IGenerator generatorPlugin)
                {
                    _generators.Add(generatorPlugin.GetGenerationType(), generatorPlugin);
                }
            }
        }

        public object Create<T>()
        {
            var type = typeof(T);
            if (type.IsAbstract) return null;
            var instance = new object();
            if (_generators.TryGetValue(type, out var generator))
            {
                instance = generator.Generate();
            }
            else if (type.IsEnum)
            {
                instance = new EnumGenerator<T>().Generate();
            }
            else if (type.IsArray)
            {
                instance = new ArrayGenerator<T>().Generate();
            }
            else if (type == typeof(string))
            {
                return default(string);
            }
            else if (type.IsClass || type.IsValueType)
            {
                instance = CreateThroughConstructor(type);
                if (instance == null) return default(T);;
                StuffTheObject(instance);
            }
            else if (true)
            {
                return default(T);
            }

            return instance;
        }

        private object Create(Type type)
        {
            if (type.IsPointer) return IntPtr.Zero;
            var create = typeof(Faker).GetMethod("Create");
            return create == null ? null : create.MakeGenericMethod(type).Invoke(this, null);
        }

        private object CreateThroughConstructor(Type type)
        {
            var constructors = type.GetConstructors();
            var constructor = GetMaxParametersCountConstructor(constructors);
            if (constructor == null) return null;

            var parametersInfo = constructor.GetParameters();

            return constructor.Invoke(parametersInfo.Select(parameter => Create(parameter.ParameterType)).ToArray());
        }

        private static ConstructorInfo GetMaxParametersCountConstructor(IReadOnlyList<ConstructorInfo> constructors)
        {
            if (constructors == null || constructors.Count <= 0) return null;
            
            var constructorInfo = constructors[0];
            foreach (var constructor in constructors)
            {
                if (constructor.GetParameters().Length > constructorInfo.GetParameters().Length)
                {
                    constructorInfo = constructor;
                }
            }

            return constructorInfo;
        }
        
        private void StuffTheObject(object instance)
        {
            var type = instance.GetType();
            var fields = new List<FieldInfo>(type.GetFields());
            foreach (var field in fields)
            {
                if (field.IsLiteral) continue;
                var fieldType = field.FieldType;
                var value = Create(fieldType);
                field.SetValue(instance, value);
            }
            var properties = new List<PropertyInfo>(type.GetProperties());
            foreach (var property in properties)
            {
                if (!property.CanWrite) continue;
                var propertyType = property.PropertyType;
                var value = Create(propertyType);
                property.SetValue(instance, value);
            }
        }
    }
}