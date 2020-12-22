using System;
using System.Security.Claims;

namespace TokenGenerator.Managers
{
    public interface IAuthContainerModel
    {
        #region Members
        string SecretKey { get; set; }

        string SecurityAlgorithm { get; set; }

        int ExpireMinutes { get; set; }

        Claim[] claims { get; set; }

        #endregion
    }
}
