namespace KooliProjekt.Application.Data.Repositories
{
    public class TootajaRepository : BaseRepository<Tootaja>, ITootajaRepository
    {
        public TootajaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
