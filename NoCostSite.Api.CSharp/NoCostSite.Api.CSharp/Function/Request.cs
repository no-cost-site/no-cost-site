// ReSharper disable InconsistentNaming

using System.Collections.Generic;

namespace NoCostSite.Api.CSharp.Function
{
    public class Request
    {
        public string httpMethod { get; set; } = null!;

        public string body { get; set; } = null!;

        public Dictionary<string, string> headers { get; set; } = null!;
    }
}