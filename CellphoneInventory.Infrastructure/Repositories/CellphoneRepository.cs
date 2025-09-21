using CellphoneInventory.Core.Interfaces;
using CellphoneInventory.Core.Models;
using CellphoneInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CellphoneInventory.Infrastructure.Repositories
{
    public class CellphoneRepository : ICellphoneRepository
    {
        private readonly CellphoneDbContext _context;

        public CellphoneRepository(CellphoneDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cellphone>> GetAllCellphonesAsync()
        {
            return await _context.Cellphones.ToListAsync();
        }

        public async Task<Cellphone> GetCellphoneByIdAsync(int id)
        {
            return await _context.Cellphones.FindAsync(id) ?? throw new KeyNotFoundException($"Cellphone with ID {id} not found");
        }

        public async Task<Cellphone> AddCellphoneAsync(Cellphone cellphone)
        {
            _context.Cellphones.Add(cellphone);
            await _context.SaveChangesAsync();
            return cellphone;
        }

        public async Task<Cellphone> UpdateCellphoneAsync(Cellphone cellphone)
        {
            var existingCellphone = await _context.Cellphones.FindAsync(cellphone.Id);
            
            if (existingCellphone == null)
            {
                throw new KeyNotFoundException($"Cellphone with ID {cellphone.Id} not found");
            }

            // Actualizar propiedades
            _context.Entry(existingCellphone).CurrentValues.SetValues(cellphone);
            await _context.SaveChangesAsync();
            
            return existingCellphone;
        }

        public async Task<bool> DeleteCellphoneAsync(int id)
        {
            var cellphone = await _context.Cellphones.FindAsync(id);
            
            if (cellphone == null)
            {
                return false;
            }

            _context.Cellphones.Remove(cellphone);
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}