using CellphoneInventory.Core.Interfaces;
using CellphoneInventory.Core.Models;
using CellphoneInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CellphoneInventory.Infrastructure.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly AppDbContext _context;

        public DeviceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Device>> GetAllDevicesAsync()
        {
            return await _context.Devices.ToListAsync();
        }

        public async Task<Device> GetDeviceByIdAsync(int id)
        {
            return await _context.Devices.FindAsync(id);
        }

        public async Task<IEnumerable<Device>> SearchDevicesAsync(string searchTerm)
        {
            return await _context.Devices
                .Where(d => d.IMEI.Contains(searchTerm) ||
                           d.Brand.Contains(searchTerm) ||
                           d.Model.Contains(searchTerm) ||
                           d.Status.Contains(searchTerm) ||
                           d.Location.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<Device> AddDeviceAsync(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return device;
        }

        public async Task<Device> UpdateDeviceAsync(Device device)
        {
            _context.Entry(device).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return device;
        }

        public async Task<bool> DeleteDeviceAsync(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
                return false;

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}