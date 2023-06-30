namespace GameStoreApi.Data.DomainValidation.Enums
{
	public enum ErrorCode
	{
		#region UserErrors

		User_EmailTaken = 201,
		User_InvalidCredentials = 202,
		User_UserAlreadyUnlocked = 203,
		User_ActivationTokenAlreadyUsed = 204,
		User_TokenExpired = 205,
		User_ChangePasswordNewPasswordMismatch = 206,
		User_ChangePasswordOldPasswordMismatch = 207,
		User_CannotRestoreUserPassword = 208,
		User_NotFound = 209,
		User_CartAlreadyExists = 210,
		User_UsernameTaken = 211,

		#endregion


		#region GameErrors

		Game_AlreadyExists = 301

		#endregion
	}
}
