// ReSharper disable InconsistentNaming

using System.Collections.Generic;

namespace NoCostSite.Function
{
    public class Request
    {
        public string httpMethod { get; set; } = null!;

        public string body { get; set; } = null!;
        
        public bool isBase64Encoded { get; set; }

        public Dictionary<string, string> headers { get; set; } = null!;
        
        public Dictionary<string, string> queryStringParameters { get; set; } = null!;
    }
}