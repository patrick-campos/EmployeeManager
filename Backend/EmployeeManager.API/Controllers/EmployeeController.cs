using EmployeeManager.Application.Abstractions.UseCases;
using EmployeeManager.Domain.DTO.Requests;
using EmployeeManager.Domain.DTO.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace EmployeeManager.API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private ICreateEmployeeUseCase _createEmployeeUseCase;
        private IDeleteEmployeeUseCase _deleteEmployeeUseCase;
        private IUpdateEmployeeUseCase _updateEmployeeUseCase;
        private IGetEmployeeUseCase _getEmployeeUseCase;

        public EmployeeController(ICreateEmployeeUseCase createEmployeeUseCase, 
                                  IDeleteEmployeeUseCase deleteEmployeeUseCase,
                                  IUpdateEmployeeUseCase updateEmployeeUseCase,
                                  IGetEmployeeUseCase getEmployeeUseCase)
        {
            _createEmployeeUseCase = createEmployeeUseCase;
            _deleteEmployeeUseCase = deleteEmployeeUseCase;
            _updateEmployeeUseCase = updateEmployeeUseCase;
            _getEmployeeUseCase = getEmployeeUseCase;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(EmployeeResponseDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateEmployee(EmployeeRequestDTO requestBody)
        {
            try
            {
                return StatusCode(201, await _createEmployeeUseCase.Execute(requestBody));
            }
            catch(UnauthorizedAccessException e)
            {
                return StatusCode(403, e.Message);
            }
            catch(ArgumentException e)
            {
                return StatusCode(400, e.Message);
            }
            catch(PostgresException e)
            {
                return StatusCode(400, e.MessageText);
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            try
            {
                await _deleteEmployeeUseCase.Execute(id, "Director");

                return StatusCode(204);
            }
            catch (UnauthorizedAccessException e)
            {
                return StatusCode(403, e.Message);
            }
            catch (ArgumentException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (PostgresException e)
            {
                return StatusCode(400, e.MessageText);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateEmployee(string id, [FromBody] EmployeeRequestDTO content)
        {
            try
            {
                await _updateEmployeeUseCase.Execute(content, id);

                return StatusCode(204);
            }
            catch (UnauthorizedAccessException e)
            {
                return StatusCode(403, e.Message);
            }
            catch (ArgumentException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (PostgresException e)
            {
                return StatusCode(400, e.MessageText);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(EmployeeResponseDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployee(string id)
        {
            try
            {
                return StatusCode(200, await _getEmployeeUseCase.Execute(id));
            }
            catch (UnauthorizedAccessException e)
            {
                return StatusCode(403, e.Message);
            }
            catch (ArgumentException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (PostgresException e)
            {
                return StatusCode(400, e.MessageText);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(EmployeeResponseDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                return StatusCode(200, await _getEmployeeUseCase.Execute());
            }
            catch (UnauthorizedAccessException e)
            {
                return StatusCode(403, e.Message);
            }
            catch (ArgumentException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (PostgresException e)
            {
                return StatusCode(400, e.MessageText);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
