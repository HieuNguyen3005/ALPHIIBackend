using System;
using ALPHII.Models.Domain;
using ALPHII.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ALPHII.Repositories
{
	public interface IAuthRepository
	{
        Task<ApplicationUser> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    }
}

