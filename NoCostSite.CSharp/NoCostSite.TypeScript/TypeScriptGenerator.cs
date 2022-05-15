using System;
using System.IO;
using NoCostSite.Api.Controllers;
using NUnit.Framework;

namespace NoCostSite.TypeScript
{
    public class TypeScriptGenerator
    {
        private readonly ControllersBuilder _controllersBuilder = new ControllersBuilder();
        private readonly ControllersWriter _controllersWriter = new ControllersWriter();

        [Test]
        [Explicit]
        public void Generate()
        {
            Generate(
                typeof(AuthController),
                Path.GetFullPath("../../../../../NoCostSite.Front/no-cost-site/src/Api")
            );
        }

        public void Generate(Type type, string output)
        {
            var controllers = _controllersBuilder.Build(type);
            _controllersWriter.Write(controllers, output);
        }
    }
}