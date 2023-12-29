using System.Reflection;
using System.Text;
using BookStore.DbOperations;
using BookStore.Middlewares;
using BookStore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookStore;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration["Token:Issuer"],
                ValidAudience = Configuration["Token:Audience"],
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),
                ClockSkew = TimeSpan.Zero
            };
        });
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
        services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDbContext>());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddSingleton<ILoggerService, ConsoleLogger>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseCustomExceptionMiddleware();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}