namespace EasySql.Databases
{
    public interface IDatabaseConnectionFactory
    {
        IDatabaseConnection Create(DbContextOptions options);
    }
}
