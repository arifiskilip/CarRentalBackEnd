using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Extensions;

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
            return await this.HandleResultAsync(_carImageService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm]CarImage carImage, [FromForm] IFormFile file)
        {

            return await this.HandleResultAsync(_carImageService.AddAsync(carImage, file));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CarImage carImage, [FromForm] IFormFile file)
        {
            return await this.HandleResultAsync(_carImageService.UpdateAsync(carImage, file));
        }

    }
}
