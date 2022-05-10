namespace NoCostSite.Api.CSharp.Function
{
    public class ResultResponse
    {
        public bool IsSuccess { get; set; }
        
        public Error? Error { get; set; }

        public static ResultResponse Ok()
        {
            return new ResultResponse
            {
                IsSuccess = true,
            };
        }

        public static ResultResponse Fail(string message)
        {
            return new ResultResponse
            {
                IsSuccess = false,
                Error = new Error(message),
            };
        }
    }
}