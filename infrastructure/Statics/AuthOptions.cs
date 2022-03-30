using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace infrastructure.Statics;

public static class AuthOptions
{
    public static string ISSUER = "MoneyWallet";
    public static string AUDIENCE = "MoneyWalletClient";
    static string KEY = "secretkey.123456789123456789";
    public static int LIFETIME = 1;
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}