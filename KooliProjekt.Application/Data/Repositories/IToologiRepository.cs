using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IToologiRepository
    {
        Task<Data.toologi> GetByIdAsync(int id);
        Task SaveAsync(Data.toologi entity);
        Task DeleteAsync(Data.toologi entity);
    }
}
