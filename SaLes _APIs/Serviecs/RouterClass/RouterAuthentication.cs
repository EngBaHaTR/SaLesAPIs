using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SaLes__APIs.Serviecs.BaseClass;
using SaLes__APIs.Serviecs.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using SaLes__APIs.Services.Security;
namespace SaLes__APIs.Serviecs.RouterClass
{
    public class RouterAuthentication : BaseRouter
    {
        private readonly IConfiguration _configuration;
        public RouterAuthentication(IConfiguration configuration)
        {
            _configuration = configuration;
            UrlFragment = "/api/Login";
            TagName = "Authentication";
        }
        public override void AddRoutes(WebApplication app)
        {
            app.MapPost($"{UrlFragment}", (UserLogin user, RUser serviecs) => Login(user, serviecs)).WithTags(TagName);
        }
        protected virtual async Task<IResult> Login(UserLogin user , RUser serviecs ) 
        {


            if (!string.IsNullOrEmpty(user.UserName) )
            {
                var userLogedin = await serviecs.Get(user);
                if (userLogedin == null) { return Results.NotFound("User Not Found"); }


                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, userLogedin.UserName),
                    new Claim("IsActive", userLogedin.IsActive.HasValue && userLogedin.IsActive.Value ? "true" : "false")
                };
                var token = new JwtSecurityToken(
                 issuer: _configuration["Jwt:Issuer"],
                 audience: _configuration["Jwt:Audience"],
                 claims: claims,
                 expires: DateTime.UtcNow.AddDays(60),
                 signingCredentials: new SigningCredentials(
                     new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                     SecurityAlgorithms.HmacSha256)
             );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Results.Ok(tokenString);
            }
            else { return Results.NotFound(" no user"); }
            
        }
    }
}
