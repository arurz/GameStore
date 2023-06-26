using GameStoreApi.Data.Common.Interfaces;
using System;

namespace GameStoreApi.Data.Errors
{
	public class Error : IEntity
	{
		public int Id { get; set; }
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public string Verb { get; set; }
		public string Url { get; set; }
		public DateTime ErrorAppearanceDateTime { get; set; }
	}
}
