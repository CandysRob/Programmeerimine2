namespace KooliProjekt.Application.Data.Repositories
{
    public class ToologiRepository : BaseRepository<toologi>, IToologiRepository
    {
        public ToologiRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
