using CellphoneInventory.Core.Models;

namespace CellphoneInventory.Core.Interfaces
{
    public interface ICellphoneRepository
    {
        Task<IEnumerable<Cellphone>> GetAllCellphonesAsync();
        Task<Cellphone> GetCellphoneByIdAsync(int id);
        Task<Cellphone> AddCellphoneAsync(Cellphone cellphone);
        Task<Cellphone> UpdateCellphoneAsync(Cellphone cellphone);
        Task<bool> DeleteCellphoneAsync(int id);
    }
}