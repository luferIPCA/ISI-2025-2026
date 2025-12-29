/*
 * lufer
 * ISI
 * */


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WS_Rest1.Models;

// Install with NuGet:
// Microsoft.Extensions.DependencyInjection;         
// Microsoft.AspNetCore.Authentication.JwtBearer;    

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1º - Registering services controllers 
        builder.Services.AddControllers();

        
        builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();

        #region Swagger Security
        // ver  Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        // Apresentação da API
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ISI - RESTful OAuth API", Version = "v1" });
            
            //-------------
            //Segurança no swagger
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter your JWT token in this field",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            };
            c.AddSecurityDefinition("Bearer", securityScheme);

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
            
            c.AddSecurityRequirement(securityRequirement);

            //----------------
        });
        #endregion

        #region Security simples

        //2º Segurança simples: Basta usar [Authorize]
        //builder.Services.AddAuthorization();
        //builder.Services.AddAuthentication("Bearer").AddJwtBearer();

        #endregion

        #region Security wiith Authentication and Authorization

        //Add support for authentication and authorization middleware
        builder.Services
            .AddAuthentication(x =>
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
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(AuthSettings.PrivateKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
        });

        #endregion


        //Serializar em XML
        builder.Services.AddControllers().AddXmlSerializerFormatters();
        builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ISI - RESTful OAuth API");
            //});
        }

        //app.UseHttpsRedirection();        //No use of HTTPs

        app.UseAuthentication();
        app.UseAuthorization();

        //All Controllers
        app.MapControllers();

        //or

        //Manually: Minimal API (.NET 8.0)

        //opended endpoint...not obriga autenticação
        app.MapGet("/signin0", () => "User Authenticated Successfully!");

        //endpoint obriga autenticação
        app.MapGet("/signin", () => "User Authenticated Successfully!").RequireAuthorization();
        
        
        app.Run();
    }
}