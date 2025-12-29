
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AuthRest.Repositories;
using AuthRest.Services;
using System.Text;
//swagger
using Microsoft.OpenApi.Models;
using System.Security.Claims;

namespace AuthRest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //Add All Controllers
            builder.Services.AddControllers();
            //Add Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            //Add AuthServices
            builder.Services.AddScoped<IAuthService, AuthService>();
            //Add TokenServices
            builder.Services.AddScoped<ITokenService, TokenService>();

            // Configurar Swagger/OpenAPI...ver amis em https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //configuração swagger por defeito
            //builder.Services.AddSwaggerGen();

            //configuração swagger mais trabalhada
            builder.Services.AddSwaggerGen(options =>
            {
                //Configração Página inicial do swagger
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OAuth REST Service - ISI - 2025",
                    Version = "v1",
                    Description = "JWT Authentication with User/Admin roles and Age policy - lufer (2025)"
                });

                // Configuração do JWT Bearer usado no Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",                   
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    //Type = SecuritySchemeType.ApiKey,
                    //Description = "Enter JWT token like: Bearer {your token}"
                    //Não requer escrever "Bearer"..automaticamente inserido
                    Type = SecuritySchemeType.Http,
                    Description = "Enter JWT token like: {your token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
            });

            //Required for Policies
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),

                        //Necessário em NET8.0 : claim-to-role mapping in JWT Bearer configuration
                        RoleClaimType = ClaimTypes.Role,// importante para [Authorize(Roles="...")]
                        NameClaimType = "Email"         // para User.Identity.Name
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdultUser", policy =>
                    policy.RequireRole("User")
                          .RequireAssertion(context =>
                          {
                              var ageClaim = context.User.FindFirst("Age")?.Value;
                              if (ageClaim == null) return false;
                              return int.TryParse(ageClaim, out var age) && age > 18;
                          }));
                    options.AddPolicy("AdultAdmin", policy =>
                        policy.RequireRole("Admin")
                              .RequireAssertion(context =>
                              {
                                  var ageClaim = context.User.FindFirst("Age")?.Value;
                                  if (ageClaim == null) return false;
                                  return int.TryParse(ageClaim, out var age) && age > 18;
                              }));

                        options.AddPolicy("AdminOrAdultUser", policy =>
                            policy.RequireAssertion(context =>
                            {
                                var role = context.User.FindFirst(ClaimTypes.Role)?.Value;

                                if (role == "Admin")
                                    return true; // Admin allowed

                                if (role == "User")
                                {
                                    var ageClaim = context.User.FindFirst("Age")?.Value;
                                    return int.TryParse(ageClaim, out var age) && age > 18;
                                }

                                return false; // Others denied
                            }));
            });
                    

            //ATENÇÃO:
            //All service registrations (AddDataProtection, AddAuthentication, AddAuthorization,
            //AddControllers, etc.) must happen before calling builder.Build():
            
            //Só agora se pode criar o build
            var app = builder.Build();
           

            // Configure the HTTP request pipeline.
            // Add Swagger

            // swagger by default
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            // swagger mais trabalhado
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "JWT Roles & Age Policy API v1");
                    options.RoutePrefix = "swagger"; // Swagger em exemplo URL: https://localhost:7158/swagger/index.html
                    //options.RoutePrefix = string.Empty; // Swagger na raiz! exemplo URL: https://localhost:7158/index.html
                });
            }

            app.UseHttpsRedirection();
            
            app.MapControllers();

            app.Run();
        }
    }
}
