using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IToologiRepository
    {
        Task<toologi> GetByIdAsync(int id);
        Task SaveAsync(toologi entity);
        Task DeleteAsync(toologi entity);
    }
}
