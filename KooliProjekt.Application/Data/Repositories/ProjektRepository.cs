using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public class ProjektRepository : BaseRepository<Projekt>, IProjektRepository
    {
        public ProjektRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
