using GameStoreApi.Application.DomainValidation.Models;
using System;

namespace GameStoreApi.Application.DomainValidation.Services
{
	public static class DomainValidationService
	{
		public static void ThrowErrorMessage(Enum errorCode)
		{
			throw new DomainErrorException(new DomainErrorMessage(errorCode));
		}
	}
}
