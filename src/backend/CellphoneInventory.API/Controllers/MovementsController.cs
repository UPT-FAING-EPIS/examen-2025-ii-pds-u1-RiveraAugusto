using CellphoneInventory.Core.Interfaces;
using CellphoneInventory.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CellphoneInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementsController : ControllerBase
    {
        private readonly IMovementRepository _movementRepository;

        public MovementsController(IMovementRepository movementRepository)
        {
            _movementRepository = movementRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movement>>> GetMovements([FromQuery] int? deviceId)
        {
            if (deviceId.HasValue)
            {
                var deviceMovements = await _movementRepository.GetMovementsByDeviceIdAsync(deviceId.Value);
                return Ok(deviceMovements);
            }

            var movements = await _movementRepository.GetAllMovementsAsync();
            return Ok(movements);
        }

        [HttpPost]
        public async Task<ActionResult<Movement>> CreateMovement(Movement movement)
        {
            var createdMovement = await _movementRepository.AddMovementAsync(movement);
            return CreatedAtAction(nameof(GetMovements), new { deviceId = createdMovement.DeviceId }, createdMovement);
        }
    }
}