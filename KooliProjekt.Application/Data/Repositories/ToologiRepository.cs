namespace KooliProjekt.Application.Data.Repositories
{
    public class ToologiRepository : BaseRepository<Data.toologi>, IToologiRepository
    {
        public ToologiRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
