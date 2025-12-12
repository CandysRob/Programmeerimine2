using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface ITootajaRepository
    {
        Task<Tootaja> GetByIdAsync(int id);
        Task SaveAsync(Tootaja tootaja);
        Task DeleteAsync(Tootaja tootaja);
    }
}
