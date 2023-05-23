using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Identity.Web;
using System.IdentityModel.Tokens.Jwt;


namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.json");
            var config        = configuration.Build();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(options => { options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; })
                   .AddMicrosoftIdentityWebApi(config);

/*            builder.Services.AddAuthorization(config =>
            {
                config.AddPolicy("AuthZPolicy", policyBuilder =>
                                     policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement()
                                     {
                                         RequiredScopesConfigurationKey = $"AzureAd:Scopes"
                                     }));
            });
*/

            // Autho 
            /*
            .AddJwtBearer(options =>
     {
         options.Authority = "https://dev-kthygyk40xqm1u7o.us.auth0.com/";
         options.Audience  = "https://localhost:7147/";
     });
            */

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}