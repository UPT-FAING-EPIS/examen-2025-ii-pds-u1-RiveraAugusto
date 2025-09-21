using CellphoneInventory.Core.Models;

namespace CellphoneInventory.Core.Interfaces
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<Device>> GetAllDevicesAsync();
        Task<Device> GetDeviceByIdAsync(int id);
        Task<IEnumerable<Device>> SearchDevicesAsync(string searchTerm);
        Task<Device> AddDeviceAsync(Device device);
        Task<Device> UpdateDeviceAsync(Device device);
        Task<bool> DeleteDeviceAsync(int id);
    }
}