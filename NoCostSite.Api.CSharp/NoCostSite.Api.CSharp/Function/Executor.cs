using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NoCostSite.Utils;

namespace NoCostSite.Api.CSharp.Function
{
    public class Executor
    {
        private readonly string _controllerName;
        private readonly string _methodName;

        public Executor(string controllerName, string methodName)
        {
            _controllerName = controllerName;
            _methodName = methodName;
        }

        public async Task<object> ExecuteAsync(HttpContext httpContext, string body)
        {
            try
            {
                if (!TryGetClassTypeAndMethod(out var classType, out var method))
                {
                    throw new NotFoundException();
                }
                
                var @class = Activator.CreateInstance(classType);
                (@class as ControllerBase)!.Context = httpContext;

                var inputType = method.GetParameters().SingleOrDefault()?.ParameterType;
                var inputs = inputType != null ? new[] {body.ToObject(inputType)} : null;

                var task = method.Invoke(@class, inputs);
                return await (task as Task<object>)!;
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException!;
            }
        }

        // ReSharper disable once ConstantConditionalAccessQualifier
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        private bool TryGetClassTypeAndMethod(out Type classType, out MethodInfo method)
        {
            var className = $"{_controllerName}Controller";
            
            classType = GetType().Assembly.GetTypes().SingleOrDefault(x => x.Name == className)!;
            method = classType?.GetMethod(_methodName)!;

            return classType != null && method != null;
        }
    }
}