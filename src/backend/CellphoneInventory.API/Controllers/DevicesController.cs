using CellphoneInventory.Core.Interfaces;
using CellphoneInventory.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CellphoneInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;

        public DevicesController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            var devices = await _deviceRepository.GetAllDevicesAsync();
            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
            var device = await _deviceRepository.GetDeviceByIdAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        [HttpPost]
        public async Task<ActionResult<Device>> CreateDevice(Device device)
        {
            var createdDevice = await _deviceRepository.AddDeviceAsync(device);
            return CreatedAtAction(nameof(GetDevice), new { id = createdDevice.Id }, createdDevice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(int id, Device device)
        {
            if (id != device.Id)
            {
                return BadRequest();
            }

            var updatedDevice = await _deviceRepository.UpdateDeviceAsync(device);
            return Ok(updatedDevice);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var result = await _deviceRepository.DeleteDeviceAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}