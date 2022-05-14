using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NoCostSite.Function
{
    public class Executor
    {
        private readonly RequestContext _requestContext;
        private readonly Assembly _assembly;

        public Executor(RequestContext requestContext, Assembly assembly)
        {
            _requestContext = requestContext;
            _assembly = assembly;
        }

        public async Task<object> ExecuteAsync()
        {
            try
            {
                if (!TryGetClassTypeAndMethod(out var classType, out var method))
                {
                    throw new NotFoundException();
                }
                
                var @class = Activator.CreateInstance(classType);
                (@class as ControllerBase)!.Context = _requestContext;

                var inputType = method.GetParameters().SingleOrDefault()?.ParameterType;
                var inputs = inputType != null ? new[] {_requestContext.GetBody(inputType)} : null;

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
            var className = $"{_requestContext.Controller}Controller";
            
            classType = _assembly.GetTypes().SingleOrDefault(x => x.Name == className)!;
            method = classType?.GetMethod(_requestContext.Action)!;

            return classType != null && method != null;
        }
    }
}