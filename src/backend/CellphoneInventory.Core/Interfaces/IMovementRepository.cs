using CellphoneInventory.Core.Models;

namespace CellphoneInventory.Core.Interfaces
{
    public interface IMovementRepository
    {
        Task<IEnumerable<Movement>> GetAllMovementsAsync();
        Task<IEnumerable<Movement>> GetMovementsByDeviceIdAsync(int deviceId);
        Task<Movement> AddMovementAsync(Movement movement);
    }
}