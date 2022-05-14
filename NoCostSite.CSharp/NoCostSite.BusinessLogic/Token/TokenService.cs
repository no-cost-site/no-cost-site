using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NoCostSite.BusinessLogic.Config;

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

        private string Issuer => ConfigContainer.Current.TokenIssuer;
        
        private string Audience => ConfigContainer.Current.TokenAudience;

        private string Key => ConfigContainer.Current.TokenSecureKey;

        private int ExpirationDays => ConfigContainer.Current.TokenExpirationDays;

        private SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}