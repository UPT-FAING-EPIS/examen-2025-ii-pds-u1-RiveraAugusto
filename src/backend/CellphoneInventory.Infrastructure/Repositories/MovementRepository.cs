using CellphoneInventory.Core.Interfaces;
using CellphoneInventory.Core.Models;
using CellphoneInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CellphoneInventory.Infrastructure.Repositories
{
    public class MovementRepository : IMovementRepository
    {
        private readonly AppDbContext _context;

        public MovementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movement>> GetAllMovementsAsync()
        {
            return await _context.Movements
                .Include(m => m.Device)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movement>> GetMovementsByDeviceIdAsync(int deviceId)
        {
            return await _context.Movements
                .Include(m => m.Device)
                .Where(m => m.DeviceId == deviceId)
                .ToListAsync();
        }

        public async Task<Movement> AddMovementAsync(Movement movement)
        {
            _context.Movements.Add(movement);
            await _context.SaveChangesAsync();
            return movement;
        }
    }
}