using GameStoreApi.Data.Users;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GameStoreApi.Application.Token
{
	public class JwtService
	{
		private readonly IConfiguration config;
		public JwtService(IConfiguration config)
		{
			this.config = config;
		}

		public async Task<string> GenerateJWT(User user)
		{
			var securityKey = await Task.Run(() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])));
			var credentials = await Task.Run(() => new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

			var claims = new[]
			{
				new Claim("userId", user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Sub, user.Username),
				new Claim("role", user.Role.Name)
			};

			var token = new JwtSecurityToken(config["Jwt:Issuer"],
				config["Jwt:Issuer"],
				claims,
				expires: DateTime.Now.AddHours(24.0),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
