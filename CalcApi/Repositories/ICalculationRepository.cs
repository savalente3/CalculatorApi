using System.Collections.Generic;
using System.Threading.Tasks;
using CalcApi.Models;

namespace CalcApi.Repositories
{
    public interface ICalculationRepository
    {
        Task<IEnumerable<Calculation>> GetAllAsync();
        Task<Calculation> GetByIdAsync(int id);
        Task AddAsync(Calculation calculation);
        Task UpdateAsync(Calculation calculation);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
