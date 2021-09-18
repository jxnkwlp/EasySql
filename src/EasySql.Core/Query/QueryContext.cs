namespace EasySql.Query
{
    public class QueryContext
    {
        public DbContextOptions Options { get; }

        public QueryContext(DbContextOptions options)
        {
            Options = options;
        }
    }
}
