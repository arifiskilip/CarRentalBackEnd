using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CreditCardController : ControllerBase
	{
		private readonly ICreditCardService _creditCardService;

		public CreditCardController(ICreditCardService creditCardService)
		{
			_creditCardService = creditCardService;
		}

		[HttpGet]
		public async Task<IActionResult> CheckCreditCard([FromQuery]CreditCard creditCard)
		{
			var result = await _creditCardService.CheckCreditCardAsync(creditCard);
			return Ok(result);
		}
	}
}
