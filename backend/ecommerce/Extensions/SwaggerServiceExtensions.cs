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
                var securotySchema = new OpenApiSecurityScheme
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
                opt.AddSecurityDefinition("Bearer", securotySchema);
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {securotySchema, new[] {"Bearer"} }
                };
          
            });

            return services;
        }
    }
}
