using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IProjektRepository
    {
        Task<Projekt> GetByIdAsync(int id);
        Task SaveAsync(Projekt projekt);
        Task DeleteAsync(Projekt projekt);
    }
}
