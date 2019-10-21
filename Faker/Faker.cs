using System;
using System.Collections.Concurrent;
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
        private int recurtion;
        private List<Type> _type;

        public Faker()
        {
            _generators = new Dictionary<Type, IGenerator>();
            _type = new List<Type>();
            LoadGeneratorsFromDirectory();
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
        
        // Create(Type type)
        // {
        
        // }
        
        // T Create<T>
        // return (T) Create(typeof(T))

        public T Create<T>()
        {
            var type = typeof(T);
            if (type.IsAbstract) return default(T);
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
                instance = null;
            }
            else if (type.IsClass || type.IsValueType)
            {
                if (_type.Contains(type)) 
                {
                    if (_type.IndexOf(type) == 0)
                    {
                        if (recurtion == 0)
                        {
                            recurtion++;
                        }
                        else
                        {
                            recurtion--;
                            return default;
                        }
                    }
                }
                _type.Add(type);
                instance = CreateThroughConstructor(type);
                if (instance == null) return default;
                StuffTheObject(instance);
                _type.Remove(type);
            }
            else
            {
                return default;
            }

            return (T) instance;
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
            // LINQ Max

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