namespace EasySql.Databases
{
    public interface IDatabase
    {
        TResult Execute<TResult>(IDatabaseCommand command, DatabaseCommandContext context);

        void SaveChanges();
    }
}
