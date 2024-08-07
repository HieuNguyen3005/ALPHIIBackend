using System;
namespace ALPHII.Models.DTO
{
	public class ChangePasswordRequestDto
	{
		public string CurrentPassword { get; set; }
		public string NewPassword { get; set; }
	}
}

