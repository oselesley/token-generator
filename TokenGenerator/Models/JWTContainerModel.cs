using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TokenGenerator.Managers;

namespace TokenGenerator.Models
{
    public class JWTContainerModel: IAuthContainerModel
    {

        #region Public Methods
        public string SecretKey { get; set; } = "05AAmicheAL4xddNwosugozie*%c#==";
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public int ExpireMinutes { get; set; }
        public Claim[] claims { get; set; }
        #endregion
    }
}
