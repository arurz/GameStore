using System;

namespace GameStoreApi.Infrastructure.Middlewares.Models
{
	public class SomeException : Exception
	{
		public SomeException() : base()
		{

		}

		public SomeException(string message) : base(message)
		{

		}
	}
}
