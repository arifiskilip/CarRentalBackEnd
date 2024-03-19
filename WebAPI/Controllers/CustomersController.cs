using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomersController : ControllerBase
	{
		private readonly ICustomerService _customerService;

		public CustomersController(ICustomerService customerService)
		{
			_customerService = customerService;
		}
		[HttpGet]
		public async Task<IActionResult> GetUserIdByCustomer(int userId)
		{
			return await this.HandleResultAsync(_customerService.GetUserIdByCustomerAsync(userId));
		}
	}
}
