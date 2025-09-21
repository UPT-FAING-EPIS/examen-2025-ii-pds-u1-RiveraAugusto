using CellphoneInventory.Core.Interfaces;
using CellphoneInventory.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CellphoneInventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CellphonesController : ControllerBase
    {
        private readonly ICellphoneRepository _repository;
        private readonly ILogger<CellphonesController> _logger;

        public CellphonesController(ICellphoneRepository repository, ILogger<CellphonesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cellphone>>> GetAllCellphones()
        {
            try
            {
                var cellphones = await _repository.GetAllCellphonesAsync();
                return Ok(cellphones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los celulares");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cellphone>> GetCellphoneById(int id)
        {
            try
            {
                var cellphone = await _repository.GetCellphoneByIdAsync(id);
                return Ok(cellphone);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el celular con ID {id}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Cellphone>> CreateCellphone(Cellphone cellphone)
        {
            try
            {
                var createdCellphone = await _repository.AddCellphoneAsync(cellphone);
                return CreatedAtAction(nameof(GetCellphoneById), new { id = createdCellphone.Id }, createdCellphone);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un nuevo celular");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCellphone(int id, Cellphone cellphone)
        {
            if (id != cellphone.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del celular");
            }

            try
            {
                await _repository.UpdateCellphoneAsync(cellphone);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el celular con ID {id}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCellphone(int id)
        {
            try
            {
                var result = await _repository.DeleteCellphoneAsync(id);
                if (!result)
                {
                    return NotFound($"Celular con ID {id} no encontrado");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el celular con ID {id}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}