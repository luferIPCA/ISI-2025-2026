/*
 * lufer
 * ISI
 * OAuth
 * Adapted from : https://www.telerik.com/blogs/asp-net-core-basics-authentication-authorization-jwt
 * Swashbuckle or NSwag
 * */
using AuthCore.Helpers;
using AuthCore.Models;
using AuthCore.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1º - Registering services controllers 
        builder.Services.AddControllers();

        //opção minimal API
        builder.Services.AddScoped<ICalculatorService, AuthCore.Services.CalculatorService>();

        //Tipo de autenticação: JwTBearer
        builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthSettings.PrivateKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        //mais simples
        //builder.Services.AddAuthorization(); // Add default authorization services
        //Costumizar Roles/Policies
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
            options.AddPolicy("Guest", policy => policy.RequireRole("guest"));
            options.AddPolicy("GuestPolicy", policy =>
                                            policy.RequireAssertion(context =>
            context.User.IsInRole("Admin") || context.User.IsInRole("Guest")));
        });

        builder.Services.AddTransient<AuthService>();

        //Documentação API/OpenAPI Specification
        builder.Services.AddEndpointsApiExplorer(); //for minimal API only
        builder.Services.AddSwaggerGen(options =>
        {
            //options.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthCore API", Version = "v1" });
            //Costumizar mais a documentação
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ISI RESTful API",
                Description = "An ASP.NET Core Web API for managing ISI RESTful Services",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Example Contact",
                    Url = new Uri("https://example.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            });

            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter your JWT token in this field",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            };

            options.AddSecurityDefinition("Bearer", securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
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
                    new string[] {}
                }
            };

            options.AddSecurityRequirement(securityRequirement);
        });

        var app = builder.Build();

        // Middleware to use authentication and authorization
        app.UseAuthentication();        //respeitar esta ordem!
        app.UseAuthorization();

        //Swagger
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            //app.UseSwaggerUI();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthCore API V1");
            });
        }

        app.MapControllers();

        //Minimal Endpoits
        #region Maping Minimal Endpoints

        app.MapPost("/authenticate", (User user, AuthService authService)
            => authService.GenerateToken(user));

        app.MapGet("/signin", () => "User Authenticated Successfully!").RequireAuthorization("Admin");

        //opção Map minimal API
        //Exige Autenticação
        //call https://localhost:7212/sub?x=2&y=3
        app.MapGet("/sub", (ICalculatorService c, int x, int y) =>
        {
            return Results.Ok(new { Result = c.Subtract(x, y) });
        }).RequireAuthorization("Admin");

        //https://localhost:7212/subOpen?x=2&y=3
        app.MapGet("/subOpen", (ICalculatorService c, int x, int y) =>
        {
            return Results.Ok(new { Result = c.Subtract(x, y) });
        });

        app.MapPost("/add/admin", (Dados data, ICalculatorService c) =>
        {
            if (data == null)
                return Results.BadRequest("Invalid input data");

            return Results.Ok(new { Result = c.Add(data.x, data.y) });
        })
            .RequireAuthorization(policyBuilder => policyBuilder.RequireRole("Admin"))
            .WithName("AddAdmin")
            .WithTags("Calculator");

        #endregion

        app.Run();


    }
}