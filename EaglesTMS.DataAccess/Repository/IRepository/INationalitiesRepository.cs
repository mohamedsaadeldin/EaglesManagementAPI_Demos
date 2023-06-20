namespace EaglesTMS.DataAccess.Repository.IRepository
{
    public interface INationalitiesRepository : IRepository<Nationalities>
    {
        Task UpdateNationalityAsync(Nationalities nationalities);
    }
}
