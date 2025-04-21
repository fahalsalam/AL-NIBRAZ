using AL_Nibras_Ecom_API.Models.General;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace AL_Nibras_Ecom_API.Classes
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJWT(this IServiceCollection services, IConfiguration _configuration)
        {
            var authkey = DBOperation.JWtKey();
            services.Configure<JwtKey>(options =>
            {
                options._jwtKey = authkey;
            });

            services.AddAuthentication(item =>
            {
                item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(item =>
            {
                item.RequireHttpsMetadata = true;
                item.SaveToken = true;
                item.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DBOperation.DecryptString(authkey, DBOperation.APIString))),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                };
            });

            return services;
        }

        public static IServiceCollection ConnectionStrings(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DB"); 
            services.AddTransient<IDbConnection>(sp =>
            {
                return new SqlConnection(connectionString);
            });
           
            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
            return services;
        }

        public static IServiceCollection AddLocalServiceDependencies(this IServiceCollection services)
        {
            services.AddSingleton<JwtKey>();
            services.AddSingleton<APIResponse>();
            services.AddControllers();

            return services;
        }
    }
}
