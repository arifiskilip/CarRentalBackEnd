using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int pageIndex, int pageSize)
        {
            return await this.HandleResultAsync(_colorService.GetAllByPaginationAsync(pageIndex, pageSize));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Color color)
        {
            return await this.HandleResultAsync(_colorService.AddAsync(color));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return await this.HandleResultAsync(_colorService.GetAllAsync());
        }
		[HttpPost("Delete")]
		public async Task<IActionResult> Delete(int id)
		{
			return await this.HandleResultAsync(_colorService.DeleteAsync(id));
		}
		[HttpPost("Update")]
		public async Task<IActionResult> Update(Color color)
		{
			return await this.HandleResultAsync(_colorService.UpdateAsync(color));
		}

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            return await this.HandleResultAsync(_colorService.GetByIdAsync(id));
        }

	}
}
