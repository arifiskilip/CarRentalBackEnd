using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarsByBrandId(int brandId)
        {
            return await this.HandleResultAsync(_carService.GetCarsByBrandIdAsync(brandId));
        }
        [HttpGet]
        public async Task<IActionResult> GetCarsByColorId(int colorId)
        {
           return await this.HandleResultAsync(_carService.GetCarsByColorIdAsync(colorId));
        }
        [HttpPost]
        public async Task<IActionResult> Add(Car car)
        {
            return await this.HandleResultAsync(_carService.AddCarAsync(car));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWithColorAndBrandAsync()
        {
            return await this.HandleResultAsync(_carService.GetAllWithColorAndBrandAsync());
        }
        [HttpGet]
        public async Task<IActionResult> GetAllByBrandIdAndColorId(int brandId, int colorId)
        {
            return await this.HandleResultAsync(_carService.GetAllByBrandIdAndColorIdAsync(brandId, colorId));
        }

        [HttpGet]
        public async Task<IActionResult> GetCarById(int carId)
        {
            return await this.HandleResultAsync(_carService.GetCarByIdAsync(carId));
        }

    }
}
