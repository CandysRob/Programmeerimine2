namespace KooliProjekt.Application.Data.Repositories
{
    public class YlesanneRepository : BaseRepository<Ylesanne>, IYlesanneRepository
    {
        public YlesanneRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
