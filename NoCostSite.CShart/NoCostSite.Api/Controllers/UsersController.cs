using System.Threading.Tasks;
using NoCostSite.Api.Dto;
using NoCostSite.BusinessLogic.Users;
using NoCostSite.Function;

namespace NoCostSite.Api.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService = new UsersService();

        public async Task<object> ChangePassword(UsersChangePasswordRequest request)
        {
            await _usersService.ChangePassword(request.OldPassword, request.NewPassword, request.PasswordConfirm);
            return ResultResponse.Ok();
        }
    }
}