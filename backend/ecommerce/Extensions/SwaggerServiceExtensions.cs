using Microsoft.OpenApi.Models;

namespace ecommerce.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "ecommerce", Version = "v1" });
                var securtySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                opt.AddSecurityDefinition("Bearer", securtySchema);
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {securtySchema, new[] {"Bearer"} }
                };
                opt.AddSecurityRequirement(securityRequirement);
            });

            return services;
        }
    }
}
