using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NoCostSite.BusinessLogic.Settings;
using NoCostSite.Utils;

namespace NoCostSite.BusinessLogic.Users
{
    public class UsersService
    {
        private readonly UsersRepository _repository = new UsersRepository();
        
        public async Task Create(string? password, string? passwordConfirm)
        {
            await Validate();

            var user = new User
            {
                Password = HashPassword(password!)
            };

            await _repository.Upsert(user);
            
            async Task Validate()
            {
                Assert.Validate(() => !string.IsNullOrWhiteSpace(password), "Password should be not empty");
                Assert.Validate(() => !string.IsNullOrWhiteSpace(passwordConfirm),
                    "Confirm password should be not empty");
                Assert.Validate(() => password == passwordConfirm, "Passwords don't match");

                var existsUser = await _repository.TryRead();
                Assert.Validate(() => existsUser != null, "User already exists");
            }
        }

        public async Task<bool> IsValidPassword(string password)
        {
            var user = await _repository.TryRead();
            return user?.Password == HashPassword(password);
        }

        private string Key => SettingsContainer.Current.DataBaseSecureKey;

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.Default.GetBytes($"{password}{Key}")));
        }
    }
}