namespace EasySql.Databases
{
    public interface IDatabaseCommandContextFactory
    {
        DatabaseCommandContext Create(DbContextOptions options);
    }
}
