using System;
using System.Collections.Generic;
using System.Linq;
using NoCostSite.Utils;

namespace NoCostSite.TypeScript
{
    internal class TypesResolver
    {
        private const string DefaultType = "string";

        private readonly Dictionary<Type, string> _types;
        private readonly HashSet<Type> _dtoTypes;

        private TypesResolver(Dictionary<Type, string> types, ControllerType[] controllerTypes)
        {
            _types = types;
            _dtoTypes = controllerTypes.Select(x => x.Type).ToHashSet();
        }

        internal string Get(Type type)
        {
            var tsType = _types.TryGetValue(type.ResolveType(), out var typeString) ? typeString : DefaultType;
            return type.IsArray ? $"{tsType}[]" : tsType;
        }

        internal bool IsDto(Type type)
        {
            return _dtoTypes.Contains(type.ResolveType());
        }

        internal static TypesResolver Create(ControllerType[] dto)
        {
            var types = new[]
                {
                    (typeof(int), "number"),
                    (typeof(float), "number"),
                    (typeof(double), "number"),
                    (typeof(bool), "boolean"),
                }
                .Concat(dto.Select(x => (x!.Type, x.Name)))
                .ToDictionary(x => x.Item1, x => x.Item2);

            return new TypesResolver(types, dto);
        }
    }
}