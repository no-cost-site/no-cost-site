using System.Threading.Tasks;
using NoCostSite.BusinessLogic.Token;
using NoCostSite.BusinessLogic.Users;
using NoCostSite.Utils;

namespace NoCostSite.BusinessLogic.Auth
{
    public class AuthService
    {
        private readonly TokenService _tokenService = new TokenService();
        private readonly UsersService _usersService = new UsersService();

        public bool IsAuth(string token)
        {
            return _tokenService.IsValid(token);
        }

        public async Task<string> Login(string? password)
        {
            await Validate();

            return _tokenService.Generate();

            async Task Validate()
            {
                Assert.Validate(() => !string.IsNullOrEmpty(password), "Password should be not empty");

                var isValidPassword = await _usersService.IsValidPassword(password!);
                Assert.Validate(() => isValidPassword, "Password is incorrect");
            }
        }
    }
}