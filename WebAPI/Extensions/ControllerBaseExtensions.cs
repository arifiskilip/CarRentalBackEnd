using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static async Task<IActionResult> HandleResultAsync(this ControllerBase controller, Task<IResult> resultTask)
        {
            var result = await resultTask;
            if (result.Succes)
            {
                return controller.Ok(result);
            }
            return controller.BadRequest(result);
        }

        public static async Task<IActionResult> HandleResultAsync<T>(this ControllerBase controller, Task<IDataResult<T>> resultTask)
        {
            var result = await resultTask;
            if (result.Succes)
            {
                return controller.Ok(result);
            }
            return controller.BadRequest(result);
        }
    }
}
