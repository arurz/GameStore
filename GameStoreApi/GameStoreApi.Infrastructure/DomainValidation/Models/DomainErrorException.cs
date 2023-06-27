using System;
using System.Collections.Generic;

namespace GameStoreApi.Infrastructure.DomainValidation.Models
{
	public class DomainErrorException : Exception
	{
		private List<DomainErrorMessage> errorMessages = new List<DomainErrorMessage>();
		public List<DomainErrorMessage> ErrorMessages => errorMessages;

		public DomainErrorException(DomainErrorMessage errorCode)
		{
			errorMessages.Add(errorCode);
		}

		public DomainErrorException(List<DomainErrorMessage> errorMessages)
		{
			this.errorMessages = errorMessages;
		}
	}
}
