using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Domain.Entities;
using HS12_BlogProject.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace HS12_BlogProject.Application.Services.AppUserService
{
	public class AppUserService : IAppUserService
	{
		private readonly IAppUserRepository _appUserRepository;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

		public AppUserService(IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_appUserRepository = appUserRepository;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public async Task<UpdateProfileDTO> GetByUserName(string userName)
		{
			UpdateProfileDTO result = await _appUserRepository.GetFilteredFirstOrDefault(
				x => new UpdateProfileDTO
				{
					Id = x.Id,
					UserName = x.UserName,
					Email = x.Email,
					ImagePath = x.ImagePath,
					Password = x.PasswordHash
				},
				   x => x.UserName == userName);

			return result;
		}

		public async Task Logout()
		{
			await _signInManager.SignOutAsync();
		}

		public async Task<IdentityResult> Register(RegisterDTO model)
		{
			AppUser user = new AppUser
			{
				UserName = model.UserName,
				Email = model.Email,
				CreatedDate = DateTime.Now
			};

			IdentityResult result = await _userManager.CreateAsync(user, model.Password);
			if (result.Succeeded)
				await _signInManager.SignInAsync(user, isPersistent: false);
			return result;
		}

		public async Task<SignInResult> Login(LoginDTO model)
		{
			return await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
		}

		public async Task UpdateProfile(UpdateProfileDTO model)
		{
			AppUser user = await _appUserRepository.GetDefault(x => x.Id == model.Id);

			if (model.Password != null)
			{
				user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

				await _userManager.UpdateAsync(user);
			}

			if (model.Email != null)
			{
				AppUser isUserEmailExist = await _userManager.FindByEmailAsync(model.Email);

				if (isUserEmailExist == null)
					await _userManager.SetEmailAsync(user, model.Email);
			}
		}
	}
}
