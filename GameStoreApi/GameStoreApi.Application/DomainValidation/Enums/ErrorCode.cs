﻿namespace GameStoreApi.Application.DomainValidation.Enums
{
	public enum ErrorCode
	{
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

		Game_AlreadyExists = 211
	}
}