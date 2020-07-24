using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Ford.Security.Bearer
{
    public class JwtSecurityKey
    {
        public static SecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}