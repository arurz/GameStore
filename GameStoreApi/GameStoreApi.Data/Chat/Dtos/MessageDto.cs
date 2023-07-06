namespace GameStoreApi.Data.Chat.Dtos
{
	public class MessageDto
	{
		public int FromUserId { get; set; }
		public int? ToUserId { get; set; }
		public string Content { get; set; }
	}
}
