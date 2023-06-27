namespace GameStoreApi.Infrastructure.Middlewares.Models
{
	public class ApiResponse<T>
	{
		public T Data { get; set; }
		public bool Succeeded { get; set; }
		public string Message { get; set; }
		public int StatusCode { get; set; }

		public static ApiResponse<T> Fail(string errorMessage)
		{
			return new ApiResponse<T> { Succeeded = false, Message = errorMessage };
		}
	}
}
