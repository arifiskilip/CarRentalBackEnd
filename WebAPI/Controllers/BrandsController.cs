using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await this.HandleResultAsync(_brandService.GetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return await this.HandleResultAsync(_brandService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Brand brand)
        {
            return await this.HandleResultAsync(_brandService.AddAsync(brand));
        }
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			return await this.HandleResultAsync(_brandService.DeleteAsync(id));
		}
		[HttpPost]
		public async Task<IActionResult> Update(Brand brand)
		{
			return await this.HandleResultAsync(_brandService.UpdateAsync(brand));
		}
	}
}
