using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NoCostSite.Utils;

namespace NoCostSite.TypeScript
{
    internal class ControllersBuilder
    {
        private const string ControllerEnd = "Controller";

        internal Controller[] Build(Type type)
        {
            var types = type.GetAllTypes().Where(IsController);
            return types.Select(Parse).ToArray();
        }

        private Controller Parse(Type type)
        {
            var actions = type
                .GetMethods()
                .Where(IsAction)
                .Select(Parse)
                .ToArray();

            return new Controller
            {
                Name = $"{type.Name.Replace("Controller", "")}Api",
                Actions = actions
            };
        }

        private ControllerAction Parse(MethodInfo methodInfo)
        {
            var requestType = methodInfo.GetParameters().FirstOrDefault()?.ParameterType;
            var responseType = methodInfo.ReturnType.ResolveType();
            var allTypes = GetAllTypes(new[] {requestType, responseType}
                .Where(x => x != null)
                .Select(x => x!)
                .ToArray())
                .Distinct();

            return new ControllerAction
            {
                Name = methodInfo.Name,
                Request = requestType != null ? ParseType(requestType) : null,
                Response = ParseType(responseType),
                AllTypes = allTypes.Select(ParseType).ToArray(),
            };
        }

        private ControllerType ParseType(Type type)
        {
            return new ControllerType
            {
                Name = type.Name,
                Type = type,
                Properties = type
                    .GetProperties()
                    .Select(Parse)
                    .ToArray(),
            };
        }

        private ControllerProperty Parse(PropertyInfo propertyInfo)
        {
            return new ControllerProperty
            {
                Name = propertyInfo.Name,
                Type = propertyInfo.PropertyType
            };
        }

        private bool IsController(Type type)
        {
            return type.Name.EndsWith(ControllerEnd);
        }

        private bool IsAction(MethodInfo methodInfo)
        {
            return methodInfo.IsPublic 
                   && methodInfo.ReturnType.Name.StartsWith("Task")
                   && methodInfo.GetParameters().Length < 2;
        }

        private IEnumerable<Type> GetAllTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                yield return type;

                var childs = type
                    .GetProperties()
                    .Select(x => x.PropertyType.ResolveType())
                    .Where(x => x.FullName!.StartsWith("Olrix."));

                foreach (var child in GetAllTypes(childs))
                {
                    yield return child;
                }
            }
        }
    }
}