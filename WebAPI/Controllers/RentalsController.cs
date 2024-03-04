using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Rental rental)
        {
            return await this.HandleResultAsync(_rentalService.AddAsync(rental));
        }

		[HttpGet]
		public async Task<IActionResult> CheckCarRental(int carId)
		{
            return await this.HandleResultAsync(_rentalService.CheckRentalCarAsync(carId));

		}
	}
}
