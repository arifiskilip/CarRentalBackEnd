using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            var result = await _colorService.GetAllByPaginationAsync(pageIndex, pageSize);
            if (result.Succes)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Color color)
        {
            var result = await _colorService.AddAsync(color);
            if (result.Succes)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _colorService.GetAllAsync();
            if (result.Succes)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
