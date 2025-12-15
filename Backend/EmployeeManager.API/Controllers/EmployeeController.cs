using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [Route("create")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        public IActionResult CreateEmployee()
        {
            try
            {
                return StatusCode(201);
            }
            catch(UnauthorizedAccessException e)
            {
                return StatusCode(403, e.Message);
            }
            catch(ArgumentException e)
            {
                return StatusCode(400, e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
