using Microsoft.EntityFrameworkCore;

namespace CalcApi.Models
{
    public class CalculationDb : DbContext
    {
        public CalculationDb(DbContextOptions<CalculationDb> options)
            : base(options)
        {
        }

        public DbSet<Calculation> Calculations { get; set; } = null!;
    }
}
