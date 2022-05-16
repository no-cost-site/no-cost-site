using System;
using System.Threading.Tasks;
using NoCostSite.Api.Dto.Auth;
using NoCostSite.BusinessLogic.Auth;
using NoCostSite.BusinessLogic.Pages;
using NoCostSite.BusinessLogic.Settings;
using NoCostSite.BusinessLogic.Templates;
using NoCostSite.BusinessLogic.Upload;
using NoCostSite.BusinessLogic.Users;
using NoCostSite.Function;

namespace NoCostSite.Api.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService = new AuthService();
        private readonly UsersService _usersService = new UsersService();
        private readonly TemplatesService _templatesService = new TemplatesService();
        private readonly PagesService _pagesService = new PagesService();
        private readonly UploadService _uploadService = new UploadService();
        private readonly SettingsService _settingsService = new SettingsService();
        
        public Task<ResultResponse> Check()
        {
            var isAuth = _authService.IsAuth(Context.Token!);
            var result = isAuth ? ResultResponse.Ok() : ResultResponse.Fail("Token invalid");
            return Task.FromResult(result);
        }

        public async Task<AuthLoginResponse> Login(AuthLoginRequest request)
        {
            var token = await _authService.Login(request.Password);
            return AuthLoginResponse.Ok(token);
        }

        public async Task<ResultResponse> Register(AuthRegisterRequest request)
        {
            var settings = new Settings
            {
                Language = request.Settings!.Language,
            };
            var template = new Template
            {
                Id = Guid.NewGuid(),
                Name = "Default template",
                Content = "<!-- Content -->",
            };
            var page = new Page
            {
                Id = Guid.NewGuid(),
                TemplateId = template.Id,
                Name = "index.html",
                Url = "",
                Title = "",
                Description = "",
                Keywords = "",
                Content = "",
            };

            await _settingsService.Upsert(settings);
            await _templatesService.Upsert(template);
            await _pagesService.Upsert(page);
            await _uploadService.UpsertPage(page.Id);
            await _usersService.Create(request.Password, request.PasswordConfirm);
            return ResultResponse.Ok();
        }

        public async Task<AuthIsInitResponse> IsInit()
        {
            var userExists = await _usersService.Exists();
            return AuthIsInitResponse.Ok(userExists);
        }
    }
}