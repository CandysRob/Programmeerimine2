using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IYlesanneRepository
    {
        Task<Ylesanne> GetByIdAsync(int id);
        Task SaveAsync(Ylesanne ylesanne);
        Task DeleteAsync(Ylesanne ylesanne);
    }
}
