using Business.Abstract;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> GetUser(int userId)
		{
			return await this.HandleResultAsync(_userService.GetByUserIdAsync(userId));
		}

		[HttpPost]
		public async Task<IActionResult> Update([FromForm]User user, [FromForm] IFormFile? file)
		{
			return await this.HandleResultAsync(_userService.UpdateAsync(user,file));
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await this.HandleResultAsync(_userService.GetAllAsync());
		}
		[HttpPost]
		public async Task<IActionResult> Delete(User user)
		{
			return await this.HandleResultAsync(_userService.DeleteAsync(user));
		}
	}
}
