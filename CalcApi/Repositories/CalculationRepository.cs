using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CalcApi.Models;

namespace CalcApi.Repositories
{
    public class CalculationRepository : ICalculationRepository
    {
        private readonly CalculationDb _context;

        public CalculationRepository(CalculationDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Calculation>> GetAllAsync()
        {
            return await _context.Calculations.ToListAsync();
        }

        public async Task<Calculation> GetByIdAsync(int id)
        {
            return await _context.Calculations.FindAsync(id);
        }

        public async Task AddAsync(Calculation calculation)
        {
            _context.Calculations.Add(calculation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Calculation calculation)
        {
            _context.Entry(calculation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var calculation = await _context.Calculations.FindAsync(id);
            if (calculation != null)
            {
                _context.Calculations.Remove(calculation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Calculations.AnyAsync(e => e.Id == id);
        }
    }
}
