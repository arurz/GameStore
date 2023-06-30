using System.Security.Cryptography;
using System.Text;
using System;

namespace GameStoreApi.Application.Hashing
{
	public class HashService
	{
		public static string GenerateHashString(string password, ref string salt)
		{
			salt = GenerateSalt();

			var saltedPassword = password + salt;
			var bytePassword = Encoding.UTF8.GetBytes(saltedPassword);

			byte[] hashPassword = SHA512.HashData(bytePassword);
			var base64Hash = Convert.ToBase64String(hashPassword);

			return base64Hash;
		}

		public static string GenerateHashString(string password, string salt)
		{
			var saltedPass = password + salt;
			var saltedHash = Encoding.UTF8.GetBytes(saltedPass);

			byte[] hashPassword = SHA512.HashData(saltedHash);
			var base64Hash = Convert.ToBase64String(hashPassword);

			return base64Hash;
		}

		public static string GenerateSalt()
		{
			Random random = new();
			int MinSaltSize = 4;
			int MaxSaltSize = 8;

			int SaltSize = random.Next(MinSaltSize, MaxSaltSize);

			byte[] salt;
			new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

			return Convert.ToBase64String(salt);
		}
	}
}
