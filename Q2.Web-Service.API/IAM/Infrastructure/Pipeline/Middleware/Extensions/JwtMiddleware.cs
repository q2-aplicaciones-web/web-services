using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Q2.Web_Service.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q2.Web_Service.API.IAM.Application.Internal.OutboundServices;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly TokenSettings _tokenSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<TokenSettings> tokenSettings)
    {
        _next = next;
        _tokenSettings = tokenSettings.Value;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
            AttachUserToContext(context, token);
        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = System.TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            context.Items["User"] = jwtToken;
        }
        catch
        {
            // No se adjunta usuario si el token no es válido
        }
    }
}
