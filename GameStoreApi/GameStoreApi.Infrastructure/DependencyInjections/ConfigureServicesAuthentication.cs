﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GameStoreApi.Infrastructure.DependencyInjections
{
	public static class ConfigereServicesAuthentication
	{
		public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			   .AddJwtBearer(options =>
			   {
				   options.TokenValidationParameters = new TokenValidationParameters
				   {
					   ValidateIssuer = true,
					   ValidateAudience = true,
					   ValidateLifetime = true,
					   ValidateIssuerSigningKey = true,
					   ValidIssuer = configuration["Jwt:Issuer"],
					   ValidAudience = configuration["Jwt:Issuer"],
					   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
				   };
			   });

			return services;
		}
	}
}
