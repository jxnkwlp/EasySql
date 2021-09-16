using System.Threading.Tasks;

namespace EasySql.Infrastructure
{
    public interface IDbContextOptionsValidator
    {
        Task<bool> ValidateAsync(DbContextOptions options);
    }
}
