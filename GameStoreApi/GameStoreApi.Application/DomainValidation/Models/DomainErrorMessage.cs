using Newtonsoft.Json.Converters;
using System;
using System.Text.Json.Serialization;

namespace GameStoreApi.Application.DomainValidation.Models
{
	public class DomainErrorMessage
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public Enum DomainErrorCode { get; set; }

		public DomainErrorMessage(Enum domainErrorCode)
		{
			DomainErrorCode = domainErrorCode;
		}
	}
}
