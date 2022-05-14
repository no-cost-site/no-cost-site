using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NoCostSite.BusinessLogic.Token
{
    public class TokenService
    {
        public string Generate()
        {
            var availableFrom = DateTime.UtcNow;
            var availableTo = availableFrom.AddDays(ExpirationDays);
          
            var jwt = new JwtSecurityToken(
                Issuer,
                Audience,
                notBefore: availableFrom,            
                expires: availableTo,
                signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public bool IsValid(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = GetSymmetricSecurityKey(),
                    ValidIssuer = Issuer,
                    ValidAudience = Audience,
                };
                new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);
                return true;
            }
            catch (SecurityTokenValidationException)
            {
                return false;
            }
        }

        private string Issuer => TokenConfigContainer.Config.Issuer;
        
        private string Audience => TokenConfigContainer.Config.Audience;

        private string Key => TokenConfigContainer.Config.Key;

        private int ExpirationDays => TokenConfigContainer.Config.ExpirationDays;

        private SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}