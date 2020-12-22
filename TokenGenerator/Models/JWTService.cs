using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TokenGenerator.Managers;

namespace TokenGenerator.Models
{
    public class JWTService : IAuthService
    {
        public JWTService(string secretKey)
        {
            SecretKey = secretKey;
        }

        public string SecretKey { get; set; }

        public string GenerateToken(IAuthContainerModel model)
        {
            if (model == null || model.Claims == null || model.Claims.Length == 0)  
                throw new ArgumentException("Cannot Create Token, argument is invalid");

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor() {
                Subject = new ClaimsIdentity(model.Claims);
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(model.ExpireMinutes));
            };
        }

        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            throw new NotImplementedException();
        }

        public bool IsTokenValid(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException("token is invalid");

            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            try {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, 
                tokenValidationParameters, 
                out SecurityToken ValidatedToken);

                return true
            } catch {

            }
        }

        private SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Convert.FromBase64String(SecretKey);
            return new SymmetricSecurityKey(symmetricKey);
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }
    }
}
