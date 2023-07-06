namespace GameStoreApi.Data.Users.Dtos
{
	public class NewPasswordDto
	{
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
		public string RepeatNewPassword { get; set; }
	}
}
