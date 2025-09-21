using CellphoneInventory.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CellphoneInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMovementRepository _movementRepository;

        public ReportsController(IDeviceRepository deviceRepository, IMovementRepository movementRepository)
        {
            _deviceRepository = deviceRepository;
            _movementRepository = movementRepository;
        }

        [HttpGet("stock")]
        public async Task<IActionResult> GetStockReport()
        {
            var devices = await _deviceRepository.GetAllDevicesAsync();
            
            var stockReport = devices
                .GroupBy(d => d.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToList();

            return Ok(stockReport);
        }
    }
}