using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarImagesController : Controller
    {
        private readonly ICarImageService _carImageService;
        private readonly IUserDal _userDal;

        public CarImagesController(ICarImageService carImageService, IUserDal userDal)
        {
            _carImageService = carImageService;
            _userDal = userDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _carImageService.GetAllAsync();
            if (result.Succes)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm]CarImage carImage, [FromForm] IFormFile file)
        {
            var result = await _carImageService.AddAsync(carImage, file);
            if (result.Succes)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CarImage carImage, [FromForm] IFormFile file)
        {
            var result = await _carImageService.UpdateAsync(carImage, file);
            if (result.Succes)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet]

        public ActionResult Deneme()
        {
            return Ok(_carImageService.Deneme());
        }

    }
}
