using GameStoreApi.Data.DomainValidation.Models;
using System;

namespace GameStoreApi.Data.DomainValidation.Services
{
	public static class DomainValidationService
	{
		public static void ThrowErrorMessage(Enum errorCode)
		{
			throw new DomainErrorException(new DomainErrorMessage(errorCode));
		}
	}
}
