using Microsoft.IdentityModel.Tokens;

namespace StatementService.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var authority = config["Jwt:Authority"];
            var audience = config["Jwt:Audience"];

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = authority;
                    options.Audience = audience;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        RequireSignedTokens = true,
                        RequireExpirationTime = true
                    };

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                });

            services.AddAuthorization();

            return services;
        }
    }
}
