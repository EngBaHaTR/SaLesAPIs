using Microsoft.EntityFrameworkCore;
using SaLes__APIs.Entity;
using SaLes__APIs.Serviecs.BaseClass;
using SaLes__APIs.Serviecs.RepositoryServices;
using SaLes__APIs.Serviecs.RouterClass;
using BahaDev.EX.UoW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SaLes__APIs.Serviecs.Security;
using SaLes__APIs.Services.Security;
namespace SaLes__APIs
{
    public static class SupProgram
    {
        public static IServiceCollection ConfigerDB(this IServiceCollection x , string con) 
        {
           
         return  x.AddDbContext<SelasContext>(u=>u.UseSqlServer(con));
        }
        public static void AddScopedClass (this IServiceCollection X ) 
        {
            X.AddScoped<BaseRouter, RouterCustomer>();
            X.AddScoped<BaseRouter, RouterInvoice>();
            X.AddScoped<BaseRouter, RouterProducts>();
            X.AddScoped<BaseRouter, RouterAuthentication>();
        }
        public static void AddScopedIRepositoryServices(this IServiceCollection x) 
        {
            x.AddScoped<RServieces<Customer,SelasContext>>();
            x.AddScoped<InvoiceRepository>();
            x.AddScoped<RUser>();
            x.AddScoped<RServieces<Product,SelasContext>>();
        }
        public static void AddAuthenticationcon(this IServiceCollection x , IConfiguration builder) 
        {
            x.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            option.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateActor = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder["Jwt:Issuer"],
                ValidAudience = builder["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder["Jwt:Key"]))
            }

            );
        }
        public static void AddAuthorizationcon(this IServiceCollection x) 
        {
            x.AddAuthorization();
        }

    }
}
