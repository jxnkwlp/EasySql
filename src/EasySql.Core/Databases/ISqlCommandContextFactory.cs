namespace EasySql.Databases
{
    public interface ISqlCommandContextFactory
    {
        DatabaseCommandContext Create(DbContextOptions options);
    }
}
