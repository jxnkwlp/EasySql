namespace EasySql.Query
{
    public interface IQueryContextFactory
    {
        QueryContext Create(DbContextOptions options);
    }
}
