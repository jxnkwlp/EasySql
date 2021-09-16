namespace EasySql.Databases
{
    public class DatabaseCommandContext
    {
        public IDatabaseConnection Connection { get; }

        public DatabaseCommandContext(IDatabaseConnection connection)
        {
            Connection = connection;
        }
    }
}
