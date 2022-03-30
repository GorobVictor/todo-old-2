using System.Security.Claims;
using core.Enums;
using core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace infrastructure.Services;

public class MyAuthorizationServiceSingelton : IMyAuthorizationServiceSingelton
{
    IHttpContextAccessor accessor;

    #region Members

    private int userId;
    private UserRole role;

    public MyAuthorizationServiceSingelton(IHttpContextAccessor accessor)
    {
        this.accessor = accessor;
    }

    #endregion Members

    public int UserIdAuthenticated
    {
        get
        {
            this.AssertAuthenticated();
            return this.userId;
        }
    }

    public int? UserIdAuthenticatedOrNull
    {
        get
        {
            if (this.userId <= 0 || (int)this.role < 0)
            {
                return null;
            }
            
            return this.userId;
        }
    }

    #region ---Private---
    protected void AssertAuthenticated()
    {
        this.ExtractClaims(true);
        if (this.userId <= 0 || (int)this.role < 0)
        {
            throw new Exception("Unauthorized");
        }
    }

    private void ExtractClaims(bool throwError)
    {
        ClaimsIdentity? claimsIdentity = this.accessor.HttpContext.User.Identity as ClaimsIdentity;

        if (claimsIdentity is null)
        {
            if (throwError)
            {
                throw new Exception("model = null");
            }
            else
            {
                return;
            }
        }

        if (claimsIdentity.Claims.Any())
        {
            Claim? claimUserId = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "userId");

            if (claimUserId != null)
            {
                this.userId = Convert.ToInt32(claimUserId.Value);
            }

            Claim? claimRoleId = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType);

            if (claimRoleId != null)
            {
                this.role = Enum.Parse<UserRole>(claimRoleId.Value);
            }
        }
    }

    #endregion
}