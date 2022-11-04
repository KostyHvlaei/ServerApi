using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Entities;
using Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Runtime.CompilerServices;
using Microsoft.OpenApi.Models;
using Entities.Models;

namespace ServerApi.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureCors(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", builder =>
					builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader());
			});
		}

		public static void ConfigureIISIntegration(this IServiceCollection services)
		{
			services.Configure<IISOptions>(options =>
			{
			});
		}
		
		public static void ConfigureLoggerService(this IServiceCollection services)
		{
			services.AddScoped<ILoggerManager, LoggerManager>();
		}

		public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<RepositoryContext>(opts =>
			{
				opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ServerApi"));
			});
		}

		public static void ConfigureRepositoryManager(this IServiceCollection services)
		{
			services.AddScoped<IRepositoryManager, RepositoryManager>();
		}

		public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtSettings = configuration.GetSection("JwtSettings");

			services.AddAuthentication(opt => {
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
					ValidAudience = jwtSettings.GetSection("validAudience").Value,
					IssuerSigningKey = new
					SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("SecretKey").ToString()))
				};
			});
		}

		public static void ConfigureSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(s =>
			{
				s.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Test Task Api",
					Version = "v1"
				});
			});
		}

		public static void ConfigureIdentity(this IServiceCollection services)
		{
			var builder = services.AddIdentityCore<User>(o =>
			{
				o.Password.RequireDigit = true;
				o.Password.RequireLowercase = false;
				o.Password.RequireUppercase = false;
				o.Password.RequireNonAlphanumeric = false;
				o.Password.RequiredLength = 10;
				o.User.RequireUniqueEmail = true;
			});
			builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
			builder.AddEntityFrameworkStores<RepositoryContext>()
			.AddDefaultTokenProviders();
		}
	}
}
