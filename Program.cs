using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using user_management_api_dotnet8.Authentication;
using user_management_api_dotnet8.Data;
using user_management_api_dotnet8.DTOs;
using user_management_api_dotnet8.Models;
using user_management_api_dotnet8.OptionsSetUp;
using user_management_api_dotnet8.Seeding;
using user_management_api_dotnet8.Services;
using user_management_api_dotnet8.Validators;
using user_management_api_dotnet8.Validators.user_management_api_dotnet8.Validators;

namespace user_management_api_dotnet8
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();

            builder.Services.ConfigureOptions<JwtOptionsSetUp>();
            builder.Services.ConfigureOptions<JwtBearerOptionsSetUp>();
            //to handle 404 response(cant find the route)
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

            ////add validations
            //builder.Services.AddScoped<IValidator<LoginDto>, LoginRequestValidation>();
            //builder.Services.AddScoped<IValidator<SignUpDto>, SignupRequestValidation>();
            //builder.Services.AddScoped<IValidator<UserUpdateDto>, UpdateRequestValidation>();

            builder.Services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();

                // Make all routes require authorization by default
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<SignupRequestValidation>();



            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Seed roles and admin user
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var config = services.GetRequiredService<IConfiguration>();

                await IdentityDataSeeder.SeedRolesAsync(roleManager);
                await IdentityDataSeeder.SeedAdminUserAsync(userManager, config);
            }

            // Configure middleware pipeline
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
